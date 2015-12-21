using UnityEngine;
using System.Collections;

public class LayerEnemyScript : MonoBehaviour {

	public static LayerEnemyScript enemyLayer;

	void Awake(){
		enemyLayer = this;
	}
}
