using UnityEngine;
using System.Collections;

public class StageConditions : MonoBehaviour {

	/*
	Controls for what Upgrades/Towers/Etc. are available on each level
	*/

	public bool avaTowerHammer;
	public bool avaTowerBarricade;
	public bool avaTowerTrench;
	public bool avaTowerBorer;
	public bool avaTowerAlgal;
	public bool avaTowerHarvester;

	public bool avaOreCopper;
	public bool avaOreLodestone;
	public bool avaOreBrinmistr;
	public bool avaOreMithril;

	public bool avaBitMattock;
	public bool avaBitOscillation;
	public bool avaBitCompression;
	public bool avaBitEngine;
	public bool avaBitAlembic;
	public bool avaBitSilvered;

	//Tower upgrades bool array
	//3 Common1, 3 Common2, 3 CommonFinal, 3 Rare1, 3 Rare1Final, 3 Rare2, 3 Rare2Final 
	public bool[] avaUpgradesHammer = new bool[21];
	public bool[] avaUpgradesBarricade = new bool[21];
	public bool[] avaUpgradesTrench = new bool[21];
	public bool[] avaUpgradesBorer = new bool[21];
	public bool[] avaUpgradesAlgal = new bool[21];
	public bool[] avaUpgradesHarvester = new bool[21];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
