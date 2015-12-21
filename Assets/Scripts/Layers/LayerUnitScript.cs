using UnityEngine;
using System.Collections;

public class LayerUnitScript : MonoBehaviour {

	public static LayerUnitScript unitLayer;

	void Awake(){
		unitLayer = this;
	}
}
