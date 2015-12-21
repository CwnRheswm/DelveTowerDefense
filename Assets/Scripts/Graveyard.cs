using UnityEngine;
using System.Collections.Generic;

public class Graveyard : MonoBehaviour {

	public static Graveyard graveyard;
	
	public Dictionary<string, List<GameObject>> tombstones;

	void Awake() {
		if(graveyard == null) {
			graveyard = this;
		} else {
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		tombstones = new Dictionary<string, List<GameObject>>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public GameObject CheckTombstone(string unitName){
		if (tombstones.ContainsKey(unitName)) {
			if (tombstones[unitName].Count > 0){
				GameObject grave = tombstones[unitName][0];
				//tombstones[unitName].RemoveAt (0);
				return grave;
			} else {
				return null;
			}
		} else {
			return null;
		}
	}

	public void DigGrave(GameObject unit){
		//adds a tombstone to the tombstones Dictionary
	}
}
