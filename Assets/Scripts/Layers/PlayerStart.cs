using UnityEngine;
using System.Collections;

public class PlayerStart : MonoBehaviour {

	public static PlayerStart playerStart;
	private GameHandler handler;
	//public GameObject main;

	void Awake(){
		playerStart = this;
		handler = GameHandler.handler;
	}

	void Start(){
		handler.mainBaseGame = (GameObject) Instantiate (handler.mainBase, new Vector3(0,0,0), handler.mainBase.transform.rotation);
		//Debug.Log (main.name+"  "+	 handler.mainBase.name);
		handler.mainBaseGame.transform.parent = LayerBuildScript.buildLayer.transform;
		handler.mainBaseGame.transform.localPosition = new Vector3 (playerStart.transform.position.x, 0, this.transform.position.z);
		handler.playerStart = transform.position;
		this.gameObject.SetActive (false);
	}
}
