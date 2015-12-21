using UnityEngine;
using System.Collections;

public class EndConditions : MonoBehaviour {

	/*
	Holds conditions for Victory and Defeat
	Each condition is defined by two Arrays:
		victoryConditionSet = bool Array that sets whether a condition is set for that level

		victoryConditionCheck = bool Array that checks against vCS 

		defeatConditionSet
	*/
	public bool vcEnemies;
	public bool vcResources;
	public bool vcTime;
	public bool vcBoss;

	public bool dcMainBase;
	public bool dcTime;

	private GameHandler handler;
	private SpawningPod pod;
	private LayerEnemyScript enemyLayer;
	private Body homeBaseBody;

	void OnEnable(){
		Start();
	}
	// Use this for initialization
	void Start () {
		handler = GameHandler.handler;
		pod = SpawningPod.pod;
		enemyLayer = LayerEnemyScript.enemyLayer;
		homeBaseBody = handler.mainBaseGame.GetComponent<Body>();
	}
	
	// Update is called once per frame
	void GameUpdate () {
		if(vcEnemies){
			Debug.Log(pod.GetIsEmpty());
			if(pod.GetIsEmpty()){
				Debug.Log("Enemies has only "+enemyLayer.transform.childCount+" left!");
				if (enemyLayer.transform.childCount <= 0){
					Debug.Log("VICTORY!!!!");
					handler.SetPaused(true);
				}
			}
		}
		if(dcMainBase){
			if(homeBaseBody.curHealth <= 0){
				Debug.Log("DEFEAT!!!");
				handler.SetPaused(true);
			}
		}
	}

	void Update(){
		if(!handler.IsPaused()){
			GameUpdate();
		}
	}
}
