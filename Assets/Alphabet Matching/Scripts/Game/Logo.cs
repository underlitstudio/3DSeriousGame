using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[DisallowMultipleComponent]
public class Logo : MonoBehaviour
{
	/// <summary>
	/// The sleep time.
	/// </summary>
	public float sleepTime = 5;

	/// <summary>
	/// The name of the scene to load.
	/// </summary>
	public string nextSceneName;

	// Use this for initialization
	void Start ()
	{
		Invoke ("LoadScene", sleepTime);
	}

	private void LoadScene ()
	{
		if (string.IsNullOrEmpty (nextSceneName)) {
			return;
		}
        StartCoroutine(SceneLoader.instance.LoadScene(nextSceneName));
    }
}
