using UnityEngine;
using System.Collections;

public class LayerBuildScript : MonoBehaviour {

	public static LayerBuildScript buildLayer;
	
	void Awake(){
		buildLayer = this;
	}
}
