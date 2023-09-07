using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
	
private int trueMatch = 0;
private int falseMatch = 0;
	/// <summary>
	/// Whether the GameManager is interactable or not.
	/// </summary>
	private bool interactable;

	/// <summary>
	/// The enable dragging.
	/// </summary>
	private bool enableDragging;

	/// <summary>
	/// The current shape.
	/// </summary>
	private Shape currentShape;

	/// <summary>
	/// The left,right groups transform.
	/// </summary>
	public Transform leftGroup,rightGroup;

	/// <summary>
	/// The previous shape.
	/// </summary>
	private Shape previousShape;

	/// <summary>
	/// The last click position.
	/// </summary>
	private Vector3 lastClickPosition;

	/// <summary>
	/// The dragging line.
	/// </summary>
	public Line draggingLine;

	/// <summary>
	/// The line prefab.
	/// </summary>
	public GameObject linePrefab;

	/// <summary>
	/// The line arrow.
	/// </summary>
	public Transform lineArrow;

	/// <summary>
	/// The current pencil.
	/// </summary>
	public Pencil currentPencil;

	/// <summary>
	/// The previous pencil.
	/// </summary>
	[HideInInspector]
	public Pencil previousPencil;

	/// <summary>
	/// The default scale of the pencil.
	/// </summary>
	[HideInInspector]
	public Vector3 pencilDefaultScale = Vector3.zero;

	/// <summary>
	/// The active scale of the pencil.
	/// </summary>
	[HideInInspector]
	public Vector3 pencilActiveScale = Vector3.zero;

	/// <summary>
	/// The done sound effect.
	/// </summary>
	public AudioClip doneSFX;

	/// <summary>
	/// The pencils references in the scene.
	/// </summary>
	private Pencil[] pencils;

	/// <summary>
	/// The current line sorting order.
	/// </summary>
	private int currentLineSortingOrder;

	/// <summary>
	/// The normal color of the shape.
	/// </summary>
	public Color shapeNormalColor = Color.red;

	/// <summary>
	/// Whether to make a random shuffle on start or not.
	/// </summary>
	public bool randomShuffleOnStart = true;

    /// <summary>
    /// Static instance of this class
    /// </summary>
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start ()
	{
		//Setting up references
		pencils = GameObject.FindObjectsOfType<Pencil> ();
		interactable = true;
		if (currentPencil != null) {
			previousPencil = currentPencil;
			currentPencil.SetScaleToActive ();
		}

		if (draggingLine != null) {
			currentLineSortingOrder = draggingLine.lineSortingOrder;
		}

		if (randomShuffleOnStart) {
			RandomShuffle (leftGroup);
			RandomShuffle (rightGroup);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!interactable) {
			return;
		}

		if (enableDragging) {
			lastClickPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			lastClickPosition.z = 0;
			lineArrow.GetComponent<SpriteRenderer> ().enabled = true;
			lineArrow.transform.position = lastClickPosition;
			draggingLine.SetSecondPoint (lastClickPosition);
		} 
	}
		

	/// <summary>
	/// Random shuffle Left group shapes , Right group shapes.
	/// </summary>
	private void RandomShuffle(Transform groupTransform){

		if (groupTransform == null) {
			Debug.LogWarning("Group transform reference is not defined in the GameManager component");
			return;
		}

		Shape[] groupShapes = groupTransform.GetComponentsInChildren<Shape> ();

		if (groupShapes == null) {
			return;
		}

		if (groupShapes.Length == 0) {
			return;
		}

		List<int> indexes = new List<int>();

		for (int i = 0; i < groupShapes.Length; i++) {
			indexes.Add(i);
		}

		CommonUtil.Shuffle<int> (indexes);

		Vector3 tempPos;
		for (int i = 0; i < groupShapes.Length; i++) {
			//Shuffle Positions
			tempPos = groupShapes [i].transform.position;
			groupShapes [i].transform.position = groupShapes [indexes [i]].transform.position;
			groupShapes [indexes [i]].transform.position = tempPos;
		}
	}

	/// <summary>
	/// On shape click down.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickDown (Shape shape)
	{
		if (shape == null || !interactable) {
			return;
		}
		currentShape = previousShape = shape;
		currentShape.shapeClicked = true;

		if (currentShape.connected) {
			currentShape = null;
			return;
		}

		draggingLine.SetFirstPoint (previousShape.transform.position);
		draggingLine.isLerping = false;
		enableDragging = true;

		if (currentPencil != null) {
			draggingLine.SetColor (currentPencil.color);
			if (draggingLine.lineArrow != null) {
				draggingLine.lineArrow.GetComponent<SpriteRenderer> ().color = currentPencil.color;
			}
		}

		currentShape.SetColor (currentPencil.color);
		lineArrow.GetComponent<SpriteRenderer> ().enabled = true;
	}

	/// <summary>
	/// On Shape click enter.
	/// </summary>
	public void OnShapeClickEnter (Shape shape)
	{
		if (shape == null || !interactable)
    {
        return;
    }

    currentShape = shape;

    if (currentShape.connected)
    {
        currentShape = null;
        return;
    }

    currentShape.SetColor(currentPencil.color);

    if (previousShape == null)
    {
        return;
    }

    // Check if the current shape is on the opposite side
    bool isOnOppositeSide = (currentShape.transform.parent != previousShape.transform.parent);

    if (isOnOppositeSide)
    {
        Debug.Log("New connection between " + previousShape.name + " and " + currentShape.name);
		if (currentShape.partner == previousShape && previousShape.partner == currentShape)
		{
            trueMatch++;
		}
		else{falseMatch++;}

        // Set connected flag to true for both shapes
        currentShape.connected = true;
        previousShape.connected = true;

        // Check if the matching level is completed or not
        bool matchingCompleted = MatchingCompleted();

        if (matchingCompleted)
        {
            Debug.Log("Matching level has been completed successfully");
			LogMatches();

            if (doneSFX != null)
                AudioSources.instance.audioSources[1].PlayOneShot(doneSFX);

            WinArea.Show();
        }
        

        // Create the connection line
        GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        line.name = "Line " + previousShape.name + " => " + currentShape.name;
        Line lineComponent = line.GetComponent<Line>();
        lineComponent.SetSortingOrder(currentLineSortingOrder);
        lineComponent.InitLine();
        lineComponent.SetFirstPoint(previousShape.transform.position);
        lineComponent.SetSecondPoint(currentShape.transform.position);
        lineComponent.SetColor(currentPencil.color);

        // Reset the dragging element
        draggingLine.SetSortingOrder(currentLineSortingOrder);
        draggingLine.SetFirstPoint(draggingLine.secondPoint);
        draggingLine.lineArrow.GetComponent<SpriteRenderer>().enabled = false;
        previousShape = currentShape = null;
        enableDragging = false;
        currentLineSortingOrder++;

        // Set a new random pencil
        currentPencil.SetScaleToDefault();
        currentPencil = previousPencil = pencils[Random.Range(0, pencils.Length)];
        currentPencil.SetScaleToActive();
    }
    
	}

	/// <summary>
	/// On Shape click exit.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickExit(Shape shape){
		if (shape == null || !interactable) {
			return;
		}

		if (currentShape != null) {
			if (!currentShape.connected && !currentShape.shapeClicked) {
				currentShape.SetColor (shapeNormalColor);
			}
		}

		if (previousShape != null) {
			if (!previousShape.connected && !previousShape.shapeClicked) {
				previousShape.SetColor (shapeNormalColor);
			}
		}
	}

	/// <summary>
	/// On shape click up.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickUp (Shape shape)
	{
		if (shape == null || !interactable) {
			return;
		}

		if (currentShape != null) {
			if (!currentShape.connected) {
				currentShape.SetColor (shapeNormalColor);
			}
			currentShape.shapeClicked = false;
		}

		if (previousShape != null) {
			if (!previousShape.connected) {
				previousShape.SetColor (shapeNormalColor);
			}
			previousShape.shapeClicked = false;
		}

		enableDragging = false;
		currentShape = previousShape = null;
		draggingLine.isLerping = true;
	}

	/// <summary>
	/// Check whether the matching level is completed or not.
	/// </summary>
	/// <returns><c>true</c>, if completed was matchinged, <c>false</c> otherwise.</returns>
	private bool MatchingCompleted ()
	{
		Shape[] shapes = GameObject.FindObjectsOfType<Shape> ();
		foreach (Shape shape in shapes) {
			if (!shape.connected) {
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Clean the screen.
	/// </summary>
	public void CleanScreen ()
	{
		//Clean connection lines
		GameObject[] connectionLines = GameObject.FindGameObjectsWithTag ("Line");
		foreach (GameObject ob in connectionLines) {
			Destroy (ob);
		}

		//Reset shapes
		Shape[] shapes = GameObject.FindObjectsOfType <Shape> ();
		foreach (Shape shape in shapes) {
			shape.Reset ();
			shape.SetColor (shapeNormalColor);
		}

		currentShape = previousShape = null;
	}

	/// <summary>
	/// Disable the game manager.
	/// </summary>
	public void DisableGameManager ()
	{
		interactable = false;
	}
		
	/// <summary>
	/// Enable the game manager.
	/// </summary>
	public void EnableGameManager ()
	{
		interactable = true;
	}


	public void LogMatches()
{
    Debug.Log("correct Matches: " + trueMatch);
    Debug.Log("incorrect Matches: " + falseMatch);
}
}