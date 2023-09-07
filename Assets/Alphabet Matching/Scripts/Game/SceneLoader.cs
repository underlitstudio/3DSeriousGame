using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[DisallowMultipleComponent]
public class SceneLoader : MonoBehaviour {

    /// <summary>
    /// The canvas group.
    /// </summary>
    public CanvasGroup canvasGroup;

	/// <summary>
	/// This Gameobject defined as a Singleton.
	/// </summary>
	public static SceneLoader instance;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
			return;
		}

		if (canvasGroup == null) {
			canvasGroup = GetComponent<CanvasGroup> ();
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDestroy(){
		if (this.GetInstanceID () == instance.GetInstanceID ()) {
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}

	/// <summary>
	/// On Load the scene.
	/// </summary>
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (canvasGroup != null) {
			canvasGroup.alpha = 1;
			StartCoroutine (CanvasFade (FadeType.FADE_OUT));
		}
	}

	/// <summary>
	/// Loads scene coroutine.
	/// </summary>
	public IEnumerator LoadScene (string sceneName)
	{
		gameObject.SetActive (true);

		yield return 0;

		if (!string.IsNullOrEmpty (sceneName)) {

			if (canvasGroup != null) {
				canvasGroup.alpha = 0;
				yield return StartCoroutine (CanvasFade (FadeType.FADE_IN));
			}
			SceneManager.LoadScene (sceneName);
		}
	}

	/// <summary>
	/// Fade in/out the canvas.
	/// </summary>
	public IEnumerator CanvasFade(FadeType fadeType){

		canvasGroup.blocksRaycasts = true;

		float delay = 0.03f;
		float alphaOffset = 0.1f;

		alphaOffset = (fadeType == FadeType.FADE_IN ? alphaOffset : -alphaOffset);

		while (fadeType == FadeType.FADE_IN ? canvasGroup.alpha < 1 : canvasGroup.alpha > 0) {
			yield return new WaitForSecondsRealtime (delay);
			canvasGroup.alpha += alphaOffset;
		}

		canvasGroup.blocksRaycasts = false;

		if(fadeType == FadeType.FADE_OUT)
			gameObject.SetActive (false);
	}

	public enum FadeType{
		FADE_IN,
		FADE_OUT
	}
}
