using UnityEngine;
using System.Collections;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[DisallowMultipleComponent]
public class WinArea : MonoBehaviour
{
		/// <summary>
		/// Win area animator.
		/// </summary>
		private static Animator WinAreaAnimator;

		// Use this for initialization
		void Awake ()
		{
				///Setting up the references
				if (WinAreaAnimator == null) {
						WinAreaAnimator = GetComponent<Animator> ();
				}
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				///Hide the Win Area
				Hide ();
		}

		///Show the Win Area
		public static void Show ()
		{
				if (WinAreaAnimator == null) {
						return;
				}
				WinAreaAnimator.SetTrigger ("Running");
		}
		///Hide the Win Area
		public static void Hide ()
		{
			if(WinAreaAnimator!=null)
				WinAreaAnimator.SetBool ("Running", false);
		}
}