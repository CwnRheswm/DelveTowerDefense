using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Missile))]

public class MissileEditor : Editor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void OnInspectorGUI() {
    	Missile missile = target as Missile;
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.Separator();
        EditorGUILayout.Space();

        if (missile.persistentEffect != null){
        	var miss2 = missile.persistentEffect.gameObject;

        	EditorGUILayout.BeginHorizontal();
        	
        	miss2 = EditorGUILayout.ObjectField("Missile:   ", miss2, typeof(GameObject), true) as GameObject;
        	EditorGUILayout.LabelField(miss2.name);
        	EditorGUILayout.EndHorizontal();
        }
    }
}
