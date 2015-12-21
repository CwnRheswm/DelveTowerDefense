using UnityEngine;
using System.Collections;

public enum Parser
{
	tower,
	menu,
	unit,
	bit,
	blueprint
}

public class OnClickScript : MonoBehaviour {

	private Parser parser;


	void Start(){
		if (GetComponent<ClickBitScript> () != null) {
						parser = Parser.bit;		
				} else if (GetComponent<ClickBlueprintScript> () != null) {
						parser = Parser.blueprint;
				} else if (GetComponent<ClickMenuScript> () != null) {
						parser = Parser.menu;
				} else if (GetComponent<ClickTowerScript> () != null) {
						parser = Parser.tower;
				} //else if (GetComponent<ClickUnitScript> () != null) {}
		//Debug.Log(parser);
	}

	public void OnClick()
	{
		switch (parser) 
		{
		case(Parser.bit):
			GetComponent<ClickBitScript>().OnClick ();
			break;
		case(Parser.blueprint):
			GetComponent<ClickBlueprintScript>().OnClick ();
			break;
		case(Parser.menu):
			GetComponent<ClickMenuScript>().OnClick ();
			break;
		case(Parser.tower):
			transform.GetComponent<ClickTowerScript>().OnClick ();
			break;
		case(Parser.unit):
			//GetComponent<ClickUnitScript>().OnClick();
			break;
		}
	}

	public void OffClick()
	{
		switch (parser)
		{
		case (Parser.tower):
			GetComponent<ClickTowerScript>().OffClick ();		
			break;
		case (Parser.menu):
			GetComponent<ClickMenuScript>().OffClick ();		
			break;
		case (Parser.blueprint):
			GetComponent<ClickBlueprintScript>().OffClick ();		
			break;
		case (Parser.bit):
			GetComponent<ClickBitScript>().OffClick();		
			break;
		case (Parser.unit):
			//GetCompoenent<ClickUnitScript>().OffClick();
			break;
		}
	}
}
