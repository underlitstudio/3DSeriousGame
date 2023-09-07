using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[CustomEditor (typeof(AudioSources))]
public class AudioSourcesEditor: Editor
{
	public override void OnInspectorGUI ()
	{
        AudioSources attrib = (AudioSources)target;//get the target

        EditorGUILayout.Separator();
        #if !(UNITY_5 || UNITY_2017 || UNITY_2018_0 || UNITY_2018_1 || UNITY_2018_2)
		    //Unity 2018.3 or higher
		    EditorGUILayout.BeginHorizontal();
		    GUI.backgroundColor = Colors.cyanColor;
		    EditorGUILayout.Separator();
		    if(PrefabUtility.GetPrefabParent(attrib.gameObject)!=null)
		    if (GUILayout.Button("Apply", GUILayout.Width(70), GUILayout.Height(30), GUILayout.ExpandWidth(false)))
		    {
			    PrefabUtility.ApplyPrefabInstance(attrib.gameObject, InteractionMode.AutomatedAction);
		    }
		    GUI.backgroundColor = Colors.whiteColor;
		    EditorGUILayout.EndHorizontal();
        #endif
       EditorGUILayout.Separator();

        EditorGUILayout.Separator ();
		EditorGUILayout.HelpBox ("Use the first AudioSource component for the Music.", MessageType.Info);
		EditorGUILayout.HelpBox ("Click on Apply button that located on the top to save your changes", MessageType.Info);
		EditorGUILayout.Separator ();
	}
}

