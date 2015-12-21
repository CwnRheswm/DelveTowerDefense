using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VaultScript : MonoBehaviour {

	public static VaultScript vault;
	public bool isDebug = false;

	public float[] coffer = new float[4];
	public GameObject dwarf;
	private GameHandler handler;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
		vault = this;
		handler = GameHandler.handler;
		for(int i = 0; i < coffer.Length; i++){
			//coffer[i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//var text = GameObject.Find ("GUI Text");
		//text.GetComponent<GUIText>().text = coffer["Iron"].ToString();
		if(isDebug){}
	}
	void OnGUI(){
		GUI.Label(new Rect(10,10,100,20), "Iron Ores: "+coffer[0]);
		var copperGear = GameObject.Find("Ore_Copper");
		if(GameHandler.handler.mainBaseGame){
			//Debug.Log(copperGear.transform.position);
			GUI.Label(new Rect(copperGear.transform.position.x, copperGear.transform.position.y, 20,20), ""+coffer[0].ToString());
		}
	}
	public void DepositFunds(float[] deposit){
		for(int i = 0; i < deposit.Length; i++){
			coffer[i] += deposit[i];
		}
	}
	public void WithdrawFunds(float[] withdraw){
		for (int i = 0; (i < withdraw.Length && i < coffer.Length); i++){
			coffer[i] -= withdraw[i];
		}
	}
	public bool CheckFunds(GameObject target, GameObject tower){
		Body body = tower.GetComponent<Body> ();
		if (body != null) {
			for (int c = 0; c < body.cost.Length; c++){
				Debug.Log(tower.name+" costs "+body.cost[c]+" ["+c+"] and you have "+coffer[c]+" ["+c+"]");
				if (coffer[c] < body.cost[c]){
					Debug.Log("Not enough resources to build a "+tower.name);
					return false;
				}
			}
			SendRunner (target, tower);
			WithdrawFunds(body.cost);
			Debug.Log ("Sending runner to "+target.name+" to build a "+tower.name);
			return true;
			
		}
		else
		{
			Debug.Log ("We can't send a runner if we don't have a place to send her to!");
			return false;
		}
	}

	public bool CheckFunds(GameObject tower, bitTypes bit){
		UpgradeScript upgrade = tower.GetComponent<UpgradeScript> ();
		if (upgrade != null) {
			foreach(UpgradeBit ugBit in upgrade.upgradeDict){
				if(ugBit.bit == bit){
					for (int c = 0; c < coffer.Length; c++){
						if(coffer[c] < ugBit.upgradeLevels[ugBit.currentLevel].cost[c]){
							Debug.Log("Hammer Hit You, We don't have the resources to install a "+bit.ToString()+" right now!");
							return false;
						}
					}
					WithdrawFunds(ugBit.upgradeLevels[ugBit.currentLevel].cost);
					/*if(coffer[1] >= ugBit.upgradeLevels[ugBit.currentLevel].costIron &&
					   coffer[2] >= ugBit.upgradeLevels[ugBit.currentLevel].costLode &&
					   coffer["Brin"] >= ugBit.upgradeLevels[ugBit.currentLevel].costBrin &&
					   coffer ["Mith"] >= ugBit.upgradeLevels[ugBit.currentLevel].costMith)
					{
						coffer["Iron"] -= ugBit.upgradeLevels[ugBit.currentLevel].costIron;
						coffer["Lode"] -= ugBit.upgradeLevels[ugBit.currentLevel].costLode;
						coffer["Brin"] -= ugBit.upgradeLevels[ugBit.currentLevel].costBrin;
						coffer["Mith"] -= ugBit.upgradeLevels[ugBit.currentLevel].costMith;
						*/
					SendRunner(tower, bit);
					
					Debug.Log ("Sending a runner with a "+bit.ToString ()+" to install into the "+tower.name+"!");
					return true;
					//}
					/*else
					{
						Debug.Log ("Not enough resources to install a "+bit.ToString ()+"!");
						return false;
					}*/
				}
			}
			Debug.Log ("I don't think a "+tower.name+" can use a "+bit.ToString ()+"!");
			return false;
		}
		else
		{
			Debug.Log (tower.name+" can't be upgraded!");
			return false;
		}
	} 

	void SendRunner(GameObject target, GameObject tower) //, GameObject item, Parser parser)
	{
		Vector3 doorway = GameHandler.handler.mainBaseGame.transform.position;// new Vector3 (handler.mainBaseGame.transform.position.x, handler.mainBaseGame.transform.position.y, handler.mainBaseGame.transform.position.z - 5);
		GameObject dwarfRunner = (GameObject)Instantiate (dwarf, doorway, dwarf.transform.rotation);
		dwarfRunner.transform.parent = LayerUnitScript.unitLayer.transform;
		dwarfRunner.GetComponent<Walk> ().target = target;
		dwarfRunner.GetComponent<Build>().target = target;
		dwarfRunner.GetComponent<Build> ().blueprint = tower;
	}
	void SendRunner(GameObject target, bitTypes bit){
		Vector3 doorway = new Vector3 (handler.mainBaseGame.transform.position.x, handler.mainBaseGame.transform.position.y, handler.mainBaseGame.transform.position.z - 7);
		GameObject dwarfRunner = (GameObject)Instantiate (dwarf, doorway, dwarf.transform.rotation);
		dwarfRunner.GetComponent<WalkScript> ().target = target;
		dwarfRunner.GetComponent<BuildScript> ().target = target;
		dwarfRunner.GetComponent<BuildScript> ().bit = bit;
	}
}
