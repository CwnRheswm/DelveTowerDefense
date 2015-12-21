using UnityEngine;
using System.Collections;

public class ClickBitScript : MonoBehaviour {

	public static ClickBitScript bitScript;
	private bool selected = false;
	public bitTypes bit;
	private GameObject tower;

	private RaycastHit[] hits;
	private RaycastHit hit;

	private VaultScript coffer;
	private ClickTowerScript click;

	void Start () {
		coffer = VaultScript.vault; //GameObject.FindGameObjectWithTag ("MainBase").GetComponent<VaultScript> ();
		//menu = transform.parent.GetComponent<ClickMenuScript> ();
		tower = transform.parent.parent.gameObject;
		click = tower.GetComponent<ClickTowerScript> ();
	}

	void OrderBit()
	{
		coffer.CheckFunds (tower, bit);
	}

	public void OnClick(){
		//if(selected){
		//	Debug.Log (bit.ToString () + "Ordered");
		OrderBit();
		click.OffClick ();
		//	selected = false;
		//}
		//else{
		//	selected = true;
		//	click.OnClick ();
		//	Debug.Log (bit.ToString () + " Selected");
		//}
	}

	public void OffClick(){

	}

	public void Hide(){
		transform.localScale = new Vector3 (0, 0, 0);
	}
}
