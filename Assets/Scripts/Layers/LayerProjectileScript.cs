using UnityEngine;
using System.Collections;

public class LayerProjectileScript : MonoBehaviour {

	public static LayerProjectileScript projLayer;
	
	void Awake(){
		projLayer = this;
	}
}
