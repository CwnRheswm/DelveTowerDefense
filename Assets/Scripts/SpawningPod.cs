using UnityEngine;
using System;
using System.Collections.Generic;

public class SpawningPod : MonoBehaviour {

	public static SpawningPod pod;

	public float timeElapsed = 0f;

	public Vector3[] troopLocations; //holds locations of any pod loadouts
	public Troop[] troopLoadout; //array that holds each Spawn Groups metrics, location, time, unit, size
	public Troop troopUnit;
	private GameObject spawn; //used to instantiate individual spawn
	private Vector3 troopLocation; //used to instantiate individual spawn with their troop

	private int troopIndex = 0; //used to iterate through troopLoadouts
	private bool isEmpty = false;

	//private Graveyard graveyard;
	//private GameHandler handler;

	// Use this for initialization
	void Start () {
		if (pod == null){
			DontDestroyOnLoad(gameObject);
			pod = this;
		} else {
			Destroy(gameObject);
		}
		troopUnit = SetTroopLoadout();
		//graveyard = Graveyard.graveyard;
		//handler = GameHandler.handler;
	}

	void GameUpdate ()  {
		timeElapsed += Time.deltaTime;

		//if (troopIndex < troopLoadout.Length){
		if(!isEmpty){
			if (timeElapsed > troopUnit.spawnTime){
				for (int i = 0; i < troopUnit.groupSize; i++){
					troopLocation = troopLocations[troopUnit.spawnLocationIndex];
					spawn = Graveyard.graveyard.CheckTombstone(troopUnit.unit.name);
					//Debug.Log(troopUnit.unit.name+" : "+spawn);
					if (spawn != null) {
						//ressurect the unit, reset stats, place at it's pod location
						spawn.GetComponent<Unit>().Ressurect(troopLocation);
						//spawn.gameObject.SetActive(true);
						//Graveyard.graveyard.tombstones[troopUnit.unit.name].RemoveAt(0);
					} else {
						spawn = Instantiate(troopUnit.unit);
						//add to Unit Layer
						spawn.transform.position = troopLocation;
						//choose unit's AI targeting and send
						//WalkScript().walk
					}
					spawn.transform.parent = LayerEnemyScript.enemyLayer.transform; //LayerUnitScript.unitLayer.transform;
					Walk walk = spawn.GetComponent<Walk>();
					walk.target = GameHandler.handler.mainBaseGame; //PlayerStart.playerStart.main;
					//walk.Repath();
					spawn = null;
				}
				troopIndex++;
				if (troopIndex < troopLoadout.Length){
					troopUnit = SetTroopLoadout();
				} else {
					isEmpty = true;
					Debug.Log("Pod Is Empty!");
				}
			}
		}
	}
	public bool GetIsEmpty(){
		return isEmpty;
	}
	// Update is called once per frame
	void Update () {
		if(!GameHandler.handler.IsPaused()){ 
			GameUpdate();
		}
	}
	Troop SetTroopLoadout(){
		return troopLoadout[troopIndex];
	}

}

[Serializable]
public class Troop {
	public GameObject unit;
	public int groupSize;
	public float spawnTime;
	public int spawnLocationIndex;
}
