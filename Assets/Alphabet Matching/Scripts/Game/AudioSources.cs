using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[DisallowMultipleComponent]
public class AudioSources : MonoBehaviour {

	/// <summary>
	/// This Gameobject defined as a Singleton.
	/// </summary>
	public static AudioSources instance;

	/// <summary>
	/// The audio sources references.
	/// First Audio Souce used for the music
	/// Second Audio Souce used for the sound effects
	/// </summary>
	[HideInInspector]
	public AudioSource [] audioSources;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			audioSources = GetComponents<AudioSource>();
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
