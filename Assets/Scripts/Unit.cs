using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	private Graveyard graveyard;
	private Walk walk;
	private Body body;
	private LayerUnitScript home;

	// Use this for initialization
	void Start () {
		graveyard = Graveyard.graveyard;
		walk = GetComponent<Walk>();
		body = GetComponent<Body>();
		home = LayerUnitScript.unitLayer;

	}
	
	void GameUpdate(){
		if(body.curHealth <= 0){
			Die();
		}
	}

	// Update is called once per frame
	void Update () {
		if(!GameHandler.handler.IsPaused()){
			GameUpdate();
		}
	}
	public void Die(){
		gameObject.SetActive(false);
		transform.parent = Graveyard.graveyard.transform;
		transform.localPosition = new Vector3(0,0,0);
		walk.target = null;
		walk.path = null;

		string tombName = gameObject.name;
		//tombName = tombName.Remove(tombName.Length - 7);

		if(!Graveyard.graveyard.tombstones.ContainsKey(tombName)){
			Graveyard.graveyard.tombstones.Add(tombName, new List<GameObject>());
		}
		Graveyard.graveyard.tombstones[tombName].Add (gameObject);
	}

	public void Ressurect(Vector3 spawnPoint){
		transform.parent = home.transform;
		transform.localScale = new Vector3(1,1,1);
		transform.position = spawnPoint;
		if(body != null){
			body.curHealth = body.maxHealth;
		}
		//activate Unit
		gameObject.SetActive(true);
		//remove Unit from Graveyard ledger
		Graveyard.graveyard.tombstones[gameObject.name].RemoveAt(0);
	}
}
