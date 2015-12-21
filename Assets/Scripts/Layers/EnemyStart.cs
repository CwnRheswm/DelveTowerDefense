using UnityEngine;
using System.Collections;

public class EnemyStart : MonoBehaviour {

	public static EnemyStart enemyStart;

	void Awake(){
		enemyStart = this;
	}
}
