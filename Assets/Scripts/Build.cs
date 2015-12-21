using UnityEngine;

public class Build : MonoBehaviour {

	public GameObject target;
	public GameObject blueprint;
	public bitTypes bit;
	public Parser parser;
	private Walk walk;
	private Transform towerParent;
	// Use this for initialization
	void Start () {
		towerParent = LayerBuildScript.buildLayer.transform;
		walk = GetComponent<Walk>();
		if(target == null | blueprint == null & bit == bitTypes.NONE){
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void GameUpdate () {

		var collisions = Physics.OverlapSphere(transform.position, 3);
		for (var i = 0; i < collisions.Length; i++){
			if (collisions[i].gameObject == target){
				walk.enterTarget = true;
			}
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject == target){
			if(blueprint != null){
				blueprint = Instantiate(blueprint) as GameObject;
				blueprint.transform.parent = towerParent;
				blueprint.transform.position = target.transform.position;
				AstarPath.active.UpdateGraphs(blueprint.GetComponent<Collider>().bounds);
				Destroy(gameObject);
				CommonClasses.common.MassRepath();
				target.GetComponent<ClickBlueprintScript>().Built();
			}
			else if (bit != bitTypes.NONE){
				Debug.Log("UPGRADE!!!!");
			}
		}
	}
	void Update(){
		if(!GameHandler.handler.IsPaused()){
			GameUpdate();
		}
	}
}
