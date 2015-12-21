using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiningScript : MonoBehaviour {

	public bool built = false; //DELETE

	public float gc;
	public int animI;
	public Animator animate;

	public float miningRange;
	public float miningCooldown;

	public float ironMiningRate;
	public float lodestoneMiningRate;
	public float brinmistrMiningRate;
	public float mithrilMiningRate;

	private int ironOres = 0;
	private int lodeOres = 0;
	private int brinOres = 0;
	private int mithOres = 0;

	private VaultScript vault;

	private BodyScript body;

	// Use this for initialization
	public void Start () {
		body = transform.GetComponent<BodyScript> ();
		GetOres ();
		vault = VaultScript.vault;
		//GameObject.FindWithTag ("MainBase").GetComponent<VaultScript>();
	}

	public void Mine(){
		var ironPlus = ironMiningRate * ironOres;
		var lodePlus = lodestoneMiningRate * lodeOres;
		var brinPlus = brinmistrMiningRate * brinOres;
		var mithPlus = mithrilMiningRate * mithOres;

		var coffer = vault.coffer;

		//coffer.
		/*coffer["Iron"] += ironPlus;
		coffer["Lode"] += lodePlus;
		coffer["Brin"] += brinPlus;
		coffer["Mith"] += mithPlus;
*/
		body.OnMining (miningCooldown);
		body.Animate ("mine");

	}

	void GetOres(){
		ironOres = 0;
		lodeOres = 0;
		brinOres = 0;
		mithOres = 0;

		var hits = Physics.OverlapSphere (transform.position, miningRange);
		for (int i = 0; i < hits.Length; i++) {
			if(hits[i].tag == "Iron"){
				ironOres += 1;
			}
			/*if(hits[i].tag == "Floor"){
				var hit = hits[i];
				if (hit.GetComponent<Animator>().GetInteger ("OreState") == 1){
					ironOres += 1;
				}
			}*/
		}
		/*
		if (hits.Length > 0){
			for (int i = 0; i < hits.Length; i++){
				//if (hits[i].gameObject.layer == 9){
				var hit = hits[i];
				var ore = hit.GetComponent<OreScript>();
				if (hit.gameObject.tag == "Iron" && ore.isMined == false){
					ironOres += 1;
					ore.isMined = true;
					//if (animate != null){
						//animate.mined = true;
					//}
				}
				if (hit.gameObject.tag == "Lode" && ore.isMined == false){
					lodeOres += 1;
					ore.isMined = true;
					if (animate != null){
						//animate.mined = true;
					}
				}
				if (hit.gameObject.tag == "Brin" && ore.isMined == false){
					brinOres += 1;
					ore.isMined = true;
					if (animate != null){
						//animate.mined = true;
					}
				}
				if (hit.gameObject.tag == "Mith" && ore.isMined == false){
					mithOres += 1;
					ore.isMined = true;
					if (animate != null){
						//animate.mined = true;
					}
				}
				//}
			}
		}*/
		//Debug.Log ("There are " + ironOres + " Iron Ores in range.");
		
	}
}
