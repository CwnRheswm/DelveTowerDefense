using UnityEngine;
using Pathfinding;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	public static GameHandler handler;

	private bool paused;
	private float gameTime;
	private float deltaTime;

	public GameObject mainBase; //to be set for a playthrough
	public GameObject mainBaseGame;
	public BasesArray[] casteBases;

	public Vector3 playerStart; //to be dynamically set by a child GO, will require function

	//used for running the A* pathfinding
	public AstarData path;
	public GridGraph graph;

	// Use this for initialization
	void Awake () {
		//make sure to check for and inheret children of any pre-existing handler
		if (handler == null) {
			Debug.Log("Handler == null");
			DontDestroyOnLoad(gameObject);
			handler = this;
		} else {
			Debug.Log("Handler exists");
			InheritChildren();
			Destroy(gameObject);
		}
		
		if (AstarPath.active){
			path = AstarPath.active.astarData;
			graph = path.gridGraph;
		}
	}
	void GameUpdate(){
		deltaTime = Time.deltaTime;
		gameTime += Time.deltaTime;

	}
	// Update is called once per frame
	void Update () {
		if(!IsPaused()){
			GameUpdate();
		}
	}

	public bool IsPaused() {
		return paused;
	}
	public void SetPaused(bool pause){
		paused = pause;
	}
	public void TogglePause(){
		paused = !paused;
	}
	public float GetDelta(){
		return deltaTime;
	}

	public void InheritChildren() {
		Debug.Log("Inherting: "+transform.GetChild(0));
		//Inherit children of a handler you seek to destroy
		foreach(Transform child in transform){
			Debug.Log(child.name);
			child.parent = handler.transform;
			EndConditions end = child.GetComponent<EndConditions>();
			end.enabled = true;
		}
	}
	public void SetStart(){
		//called by child to set the Start position
	}
	public void SetMainBase(int baseIndex){
		mainBase = casteBases[baseIndex].baseTower;
	}
}
 [Serializable]
 public class BasesArray {
 	public GameObject baseTower;
 }