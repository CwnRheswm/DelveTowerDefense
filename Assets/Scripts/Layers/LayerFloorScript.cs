using UnityEngine;
using System.Collections;

public class LayerFloorScript : MonoBehaviour {

	public static LayerFloorScript floorLayer;
	
	void Awake(){
		floorLayer = this;
	}
}
