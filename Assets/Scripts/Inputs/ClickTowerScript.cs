using UnityEngine;
using System.Collections;

public class ClickTowerScript : MonoBehaviour {

	private GameObject menu;
	
	public bool isOpen = false;

	private RaycastHit[] hits;
	private RaycastHit hit;
	
	private Vector3 openPos = new Vector3 (0, 0.3f, 0);
	private Vector3 closePos = new Vector3(-100,-100,-100);

	// Use this for initialization
	void Start () {
		menu = transform.FindChild ("BuildMenu").gameObject;
		if (menu != null) {
			//openPos = transform.localPosition;
			menu.gameObject.transform.position = closePos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (menu != null) {
			if(isOpen){
				menu.transform.localPosition = openPos;
			}		
			else{
				menu.transform.localPosition = closePos;
			}
		}
	}

	public void OnClick(){
		isOpen = true;
		InputHandlerScript.input.selected = menu;
	}

	public void OffClick(){
		isOpen = false;
	}
}
