using UnityEngine;
using System.Collections;

public class LayerRendererScript : MonoBehaviour {

	public static LayerRendererScript rendererLayer;

	void Awake () {
		rendererLayer = this;
	}
}
