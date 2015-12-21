using UnityEngine;
using System.Collections;

public class ClickMenuScript : MonoBehaviour {

	private GameObject tower;
	private ClickTowerScript click;

	// Use this for initialization
	void Start () {
		tower = transform.parent.gameObject;
		click = tower.GetComponent<ClickTowerScript> ();
	}

	public void OnClick(){
		click.isOpen = true;
	}

	public void OffClick(){
		click.isOpen = false;
	}
}
