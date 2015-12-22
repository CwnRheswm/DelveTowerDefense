using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*
[CustomEditor(typeof(LevelBuilderScript))]

public class levelBuilderEditor : Editor {

	public bool loaded = false;
	public bool matsList = true;
	public bool hexList = false;
	private Material newHex;

	public override void OnInspectorGUI ()
	{
		LevelBuilderScript lbs = target as LevelBuilderScript;

		if (!loaded) {
			Debug.Log ("Loading level builder attributes");
			lbs.LoadList();
			loaded = true;
		}

		EditorGUI.indentLevel = 1;

		matsList = EditorGUILayout.Foldout (matsList, "Mats List");
		hexList = EditorGUILayout.Foldout (hexList, "Hex List");
		
		if (hexList){
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if (GUILayout.Button ("Add Hex")){
				//lbs.mats.
				//Array.
				lbs.hexes.Add (newHex);
			}
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();

			EditorGUI.indentLevel = 2;
			//foreach (GameObject hex in lbs.hexes){
			for (int i = 0; i < lbs.hexes.Count; i++){
				EditorGUILayout.BeginHorizontal ();
				if (lbs.hexes[i] != null){
					EditorGUILayout.LabelField(lbs.hexes[i].name);
				}
				else{
					lbs.hexes[i] = EditorGUILayout.ObjectField (lbs.hexes[i], typeof(Material), true) as Material;
				}
				EditorGUILayout.Space ();
				if (GUILayout.Button ("X", GUILayout.Width (25), GUILayout.Height (15))){
					lbs.hexes.RemoveAt (i);
				}
				EditorGUILayout.EndHorizontal ();
			}

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if(GUILayout.Button ("Update List")){
				lbs.UpdateList ();
			}
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();
		}
	}
}
*/