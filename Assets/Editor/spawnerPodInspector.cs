using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
/*
[CustomEditor(typeof(SpawningPodScript))]

public class spawnerPodInspector : Editor {

	public bool loaded = false;
	private bool nmeList = true;
	private bool nmeGroup = true;

	public override void OnInspectorGUI()
	{
		SpawningPodScript spawn = target as SpawningPodScript;
		//Debug.Log (spawn.unitList[0]);
		if(!loaded){
			if(GameControlScript.control == null){
				GameControlScript.control = GameObject.Find ("GameControl").GetComponent<GameControlScript>();
			}
			Debug.Log (spawn.nmeInScene.Length);
			if(spawn.nmeInScene.Length > 0){
				Debug.Log ("Loading Spawn File");
				spawn.Load ();
			}
			loaded = true;
		}

		if( nmeList = EditorGUILayout.Foldout (nmeList, "Enemies In Scene:"))
		{
			EditorGUI.indentLevel = 2;
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Num: " + spawn.nmeInScene.Length);
			if (GUILayout.Button ("+")) {
				if(spawn.nmeInScene != null){
					Array.Resize (ref spawn.nmeInScene, spawn.nmeInScene.Length + 1);
				}
				else{
					spawn.nmeInScene = new GameObject[1];
				}
			}
			if (GUILayout.Button ("-")) {
				Array.Resize (ref spawn.nmeInScene, spawn.nmeInScene.Length - 1);	
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUI.indentLevel = 3;
			for(int i = 0; i < spawn.nmeInScene.Length; i++){
				spawn.nmeInScene[i] = EditorGUILayout.ObjectField ("PreFab: ", spawn.nmeInScene[i], typeof(GameObject), true) as GameObject;
			}
			EditorGUI.indentLevel = 0;
		}

		EditorGUILayout.Space ();

		if (nmeGroup = EditorGUILayout.Foldout (nmeGroup, "Enemy Groups:")){
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if(GUILayout.Button ("Add New Spawn Group"))
			{
				//spawn.unitList.Add (new UnitCallScript());//(ScriptableObject.CreateInstance<UnitCallScript>());
				//spawn.unitList.Add (new SpawnGroup());
				Array.Resize (ref spawn.unitArray, spawn.unitArray.Length + 1);
				spawn.unitArray[spawn.unitArray.Length - 1] = new SpawnGroup();
			}
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();

			//if(spawn != null){
			for (int i = 0; i < spawn.unitArray.Length; i++) 
			{


				if(spawn.unitArray[i].locked)
				{
//					spawn.unitList[i].locked = false;
//					EditorGUILayout.Space ();
//					EditorGUILayout.BeginHorizontal ();
//					EditorGUILayout.LabelField(spawn.unitList[i].unit.name.ToString ());
//					EditorGUILayout.Space ();
//					if(GUILayout.Button ("><", GUILayout.Width (30)))
//					{
//						spawn.unitList[i].locked = false;
//					}
//					EditorGUILayout.Space ();
//					EditorGUILayout.EndHorizontal ();
//
//					EditorGUILayout.BeginHorizontal ();
//					EditorGUILayout.Space ();
//					EditorGUILayout.LabelField ("Num: ", GUILayout.Width (40));
//					EditorGUILayout.LabelField(spawn.unitList[i].groupNum.ToString (), GUILayout.Width (40));
//					EditorGUILayout.LabelField ("Time: ", GUILayout.Width (40));
//					EditorGUILayout.LabelField(spawn.unitList[i].callTime.ToString (), GUILayout.Width (40));
//					spawn.unitList[i].sinceLast = spawn.unitList[i].callTime;
//					if (i != 0)
//					{
//						spawn.unitList[i].sinceLast = spawn.unitList[i].callTime - spawn.unitList[i-1].callTime;
//					}
//					EditorGUILayout.LabelField("dT: " + spawn.unitList[i].sinceLast, GUILayout.Width (40));
//					EditorGUILayout.EndHorizontal ();
//					EditorGUILayout.Space ();
				}

				else
				{
					EditorGUI.indentLevel = 1;
					EditorGUILayout.Space ();
					EditorGUIUtility.labelWidth = 120f;
					EditorGUIUtility.fieldWidth = 20f;
					Array.Resize (ref spawn.unitArray[i].spawnUnit, EditorGUILayout.IntField ("Enemy Types: ",spawn.unitArray[i].spawnUnit.Length,GUILayout.Width(200)));
					for(int k = 0; k < spawn.unitArray[i].spawnUnit.Length; k++){
						if(spawn.unitArray[i].spawnUnit[k] == null){
							spawn.unitArray[i].spawnUnit[k] = new SpawnUnit();
						}
					}
					int nmeIndex = 0;
					string[] nmeStrings = new string[spawn.nmeInScene.Length];
					for(int k = 0; k < spawn.nmeInScene.Length; k++){
						if(spawn.nmeInScene[k] != null){
							nmeStrings[k] = spawn.nmeInScene[k].name;
						}
					}
					EditorGUI.indentLevel = 3;
					EditorGUILayout.BeginHorizontal ();
					//EditorGUILayout.LabelField ("Time: ", GUILayout.Width (60));

					spawn.unitArray[i].callTime = EditorGUILayout.FloatField("Time: ",spawn.unitArray[i].callTime);//, GUILayout.Width (90));
					spawn.unitArray[i].sinceLast = spawn.unitArray[i].callTime;
					if (i != 0)
					{
						spawn.unitArray[i].sinceLast = spawn.unitArray[i].callTime - spawn.unitArray[i-1].callTime;
					}
					EditorGUILayout.LabelField("dT: " + spawn.unitArray[i].sinceLast, GUILayout.Width (240));
					EditorGUILayout.Space ();
					EditorGUILayout.EndHorizontal ();
					EditorGUI.indentLevel = 2;
					foreach(SpawnUnit unit in spawn.unitArray[i].spawnUnit){
						EditorGUILayout.BeginHorizontal ();
						unit.nmeIndex = EditorGUILayout.Popup(unit.nmeIndex.ToString (),unit.nmeIndex, nmeStrings, GUILayout.Width (300));

						//spawn.unitList[i].unit = EditorGUILayout.ObjectField (spawn.unitList[i].unit, typeof(GameObject), true) as GameObject;
//						EditorGUILayout.Space ();
//						if(GUILayout.Button ("<>", GUILayout.Width (30)))
//						{
//							if(spawn.unitList[i].unit != null)
//							{
//								spawn.unitList[i].locked = true;
//								Debug.Log ("Saving "+spawn.unitList[i].unit.name);
//								spawn.Save ();
//							}
//						}
//						if(GUILayout.Button ("-", GUILayout.Width (15)))
//						{
//							spawn.unitList.RemoveAt (i);
//							break;
//						}
						//EditorGUILayout.Space ();
						//EditorGUILayout.EndHorizontal ();
						
						//EditorGUILayout.BeginHorizontal ();
						//EditorGUILayout.LabelField ("Num: ", GUILayout.Width (40));
						
						unit.groupSize = EditorGUILayout.IntField(unit.groupSize, GUILayout.Width (80));
						EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Space ();
					}
				}
			}
		}
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Save")) {
			spawn.Save ();
		}
		EditorGUILayout.Space ();
		if (GUILayout.Button ("Load")) {
			spawn.Load ();
		}
		EditorGUILayout.Space ();
		if (GUILayout.Button ("- Groups")) {
			Array.Resize (ref spawn.unitArray, spawn.unitArray.Length - 1);
		}
		EditorGUILayout.Space ();
		if (GUILayout.Button ("- Pod")) 
		{
			spawn.unitArray = new SpawnGroup[0];
		}

		EditorGUILayout.EndHorizontal ();
	}
	
}
*/