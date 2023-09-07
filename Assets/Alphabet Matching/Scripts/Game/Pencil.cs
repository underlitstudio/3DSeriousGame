using UnityEngine;
using System.Collections;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class Pencil : MonoBehaviour {

	/// <summary>
	/// The color of the pencil (RGBA).
	/// </summary>
	public Color color = Color.white;

	/// <summary>
	/// The game manager reference.
	/// </summary>
	private static GameManager gameManager;

	/// <summary>
	/// Whether to run scaling to the target scale or not.
	/// </summary>
	private bool runScale;

	/// <summary>
	/// The target scale.
	/// </summary>
	private Vector3 targetScale = Vector3.zero;

	/// <summary>
	/// The temp scale(used temporary).
	/// </summary>
	private Vector3 tempScale = Vector3.zero;

	/// <summary>
	/// The speed of scale .
	/// </summary>
	private static float scaleSpeed = 25;

	void Start(){

		if(gameManager == null)
			gameManager = GameObject.FindObjectOfType<GameManager> ();
		
		if (gameManager.pencilDefaultScale == Vector3.zero) {
			gameManager.pencilDefaultScale = transform.localScale;
		}

		if (gameManager.pencilActiveScale == Vector3.zero) {
			gameManager.pencilActiveScale = gameManager.pencilDefaultScale * 1.2f;
		}
	}

	void Update(){
		if (runScale) {
			tempScale.x = Mathf.Lerp (tempScale.x, targetScale.x, Time.smoothDeltaTime * scaleSpeed);
			tempScale.y = Mathf.Lerp (tempScale.y, targetScale.y, Time.smoothDeltaTime * scaleSpeed);
			transform.localScale = tempScale;
			if (Mathf.Approximately(tempScale.x,targetScale.x) && Mathf.Approximately(tempScale.y,targetScale.y)) {
				runScale = false;
			}
		}
	}

	/// <summary>
	/// On pencil click event.
	/// </summary>
	/// <param name="pencil">Pencil.</param>
	public void OnPencilClick(Pencil pencil){
		if (pencil == null) {
			return;
		}

		pencil.SetScaleToActive ();

		gameManager.currentPencil = pencil;
		if (gameManager.previousPencil != null ) {
			if (gameManager.currentPencil.GetInstanceID () != gameManager.previousPencil.GetInstanceID ()) {
				gameManager.previousPencil.SetScaleToDefault ();
			}
		}

		gameManager.previousPencil = pencil;

	}

	/// <summary>
	/// Set the scale to default scale.
	/// </summary>
	public void SetScaleToDefault(){
		targetScale =  gameManager.pencilDefaultScale;
		tempScale = transform.localScale;
		runScale = true;
	}

	/// <summary>
	/// Set the scale to active scale.
	/// </summary>
	public void SetScaleToActive(){
		targetScale = gameManager.pencilActiveScale;
		tempScale =transform.localScale;
		runScale = true;
	}
}
