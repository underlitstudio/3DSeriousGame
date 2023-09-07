using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Shape : MonoBehaviour {

	/// <summary>
	/// The partner of the shape.
	/// </summary>
	public Shape partner;

	/// <summary>
	/// The AudioClip that plays on connection.
	/// </summary>
	public AudioClip clip;

	/// <summary>
	/// Whether the shape and its partner are connected or not.
	/// </summary>
	[HideInInspector]
	public bool connected;

	/// <summary>
	/// Whether the user clicked on shape or not.
	/// </summary>
	[HideInInspector]
	public bool shapeClicked;

	/// <summary>
	/// The game manager reference.
	/// </summary>
	private static GameManager gameManager;

	void Start(){
		this.connected = false;

		if (gameManager == null) {
			gameManager = GameObject.FindObjectOfType<GameManager> ();
		}
	}

	/// <summary>
	/// Reset this instance.
	/// </summary>
	public void Reset(){
		connected = false;
		shapeClicked = false;
	}

	/// <summary>
	/// The shape click up event.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickUpEvent(Shape shape){
		gameManager.OnShapeClickUp (shape);
	}

	/// <summary>
	/// The shape click down event.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickDownEvent(Shape shape){
		gameManager.OnShapeClickDown (shape);
	}

	/// <summary>
	/// The shape enter event.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickEnterEvent(Shape shape){
		gameManager.OnShapeClickEnter (shape);
	}

	/// <summary>
	/// The shape exit event.
	/// </summary>
	/// <param name="shape">Shape.</param>
	public void OnShapeClickExitEvent(Shape shape){
		gameManager.OnShapeClickExit (shape);
	}

	/// <summary>
	/// Set the color of the shape.
	/// </summary>
	public void SetColor(Color color){
		transform.Find ("Border").GetComponent<Image> ().color = color;
		transform.Find ("Text").GetComponent<Text> ().color = color;
	}
}
