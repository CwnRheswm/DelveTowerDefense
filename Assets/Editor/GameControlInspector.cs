using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CustomEditor(typeof(GameControlScript))]
public class GameControlInspector : Editor {

	public bool openUnitDB;
	public GameControlScript gcs;

	void Awake(){
		gcs = target as GameControlScript;

		if (GameControlScript.control == null) {
			GameControlScript.control = gcs;
		}
	}
	public override void OnInspectorGUI ()
	{
		//test script for loading Assets in Editor by Script
		//gcs.anvil = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Feral_Kobold.prefab", typeof(GameObject)) as GameObject;
		//Debug.Log(gcs.anvil);

		//base.OnInspectorGUI ();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Main Base: ");
		if(gcs.mainBase != null){
			EditorGUILayout.LabelField (gcs.mainBase.name);
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUI.indentLevel = 2;
		gcs.anvil = EditorGUILayout.ObjectField ("Anvil: ", gcs.anvil, typeof(GameObject), true) as GameObject;
		gcs.gear = EditorGUILayout.ObjectField ("Gear Base: ", gcs.gear, typeof(GameObject), true) as GameObject;
		gcs.hammer = EditorGUILayout.ObjectField ("Hammer Base: ", gcs.hammer, typeof(GameObject), true) as GameObject;
		EditorGUI.indentLevel = 1;
		//Debug.Log (gcs.unitDB.units.);
		EditorGUILayout.Space ();
		if (gcs.unitDB.units.Count == 0) {
			gcs.Load ();
			gcs.LoadGO ();
			Debug.Log (gcs.unitDB.units.Count);
			//if(gcs.unitDB == null){
			//	gcs.unitDB = new UnitDB ();
			//}
		}
		openUnitDB = EditorGUILayout.Foldout (openUnitDB, "Unit DB Details");
		if(openUnitDB){
			EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (200f));
			EditorGUILayout.LabelField ("alpha", GUILayout.Width (50));
			EditorGUILayout.LabelField ("object", GUILayout.Width (100));
			EditorGUILayout.LabelField ("name", GUILayout.Width (80));
			EditorGUILayout.LabelField ("X", GUILayout.Width (60));
			if (GUILayout.Button ("+")) {
				gcs.unitDB.units.Add (new UnitRecord());		
			}
			EditorGUILayout.EndHorizontal ();
			if(gcs.unitDB.units.Count > 0){
				//foreach (UnitRecord unit in gcs.unitDB.units) {
				for(int i = 0; i < gcs.unitDB.units.Count; i++) {
					UnitRecord unit = gcs.unitDB.units[i];
					EditorGUILayout.BeginHorizontal ();
					unit.alphaCode = EditorGUILayout.TextField(unit.alphaCode, GUILayout.Width (50));
					unit.unit = EditorGUILayout.ObjectField (unit.unit, typeof(GameObject), true, GUILayout.Width (100)) as GameObject;
					unit.name = EditorGUILayout.TextField (unit.name, GUILayout.Width (80));
					unit.isInScene = EditorGUILayout.Toggle(unit.isInScene, GUILayout.Width (30)); 
						//If unit.isInScene is set, you populate (Resources.Load) the object field, otherwise you 
						//leave it blank. This lets you have a persistant link to the alpha code
					if(unit.alphaCode != null){
						unit.alphaCode = unit.alphaCode.ToLower ();
						if(unit.alphaCode.Length > 4){
							unit.alphaCode = unit.alphaCode.Remove(4);
						}
					}
					if(unit.unit != null){
						unit.name = unit.unit.name;
					}
					if(unit.alphaCode != null && unit.unit != null){
						if(GUILayout.Button ("><", GUILayout.Width (30))){
							gcs.Save ();
						}
					}
					if(GUILayout.Button("-", GUILayout.Width (20))){
						gcs.unitDB.units.Remove (unit);
					}
					EditorGUILayout.EndHorizontal ();
				}
			}
		}
	}
}
