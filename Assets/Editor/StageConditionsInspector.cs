using UnityEngine;
using System.Collections;

public class StageConditionsInspector : MonoBehaviour {

	//Pattern should be:
	//3 Common1, 3 Common2, 3 Common Final, 3 Rare1, 3 Rare1 Final, 3 Rare2, 3 Rare2 Final
	private string[] upgradesBarricadeText = new string[21] {"Mattock Level 1","Mattock Level 2","Mattock Level 3","Oscillation Level 1","Oscillation Level 2","Oscillation Level 3","Bolt Thrower Level 1","Bolt Thrower Level 2","Bolt Thrower Level 3","Alembic Level 1","Alembic Level 2","Alembic Level 3","Spark Auger Level 1","Spark Auger Level 2","Spark Auger Level 3","Silvered Leaf Level 1","Silvered Leaf Level 2","Silvered Leaf Level 3","Concussive Bombs Level 1","Concussive Bombs Level 2","Concussive Bombs Level 3"};
	private string[] upgradesHammerText = new string[21];
	private string[] upgradesTrenchText = new string[21];
	private string[] upgradesAlgalText = new string[21];
	private string[] upgradesHarvesterText = new string[21];
	private string[] upgradesBorerText = new string[21];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
