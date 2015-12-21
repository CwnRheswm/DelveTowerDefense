using UnityEngine;
using System.Collections;

public class TopMenuScript : MonoBehaviour {

	public GUIStyle splashName;
	private float newGY;
	private float loadY;
	private float settY;
	private float credY;
	private float stg1Y;

	private bool lowerButtons = false;
	private bool newGameOpen = false;

	private int heightMove = 0;
	private float end = 70;

	void Awake(){
		newGY = 0f;
		loadY = 0f;
		settY = 0f;
		credY = 0f;
		stg1Y = 0f;
	}

	void OnGUI(){
		GUI.Label (new Rect ((Screen.width / 2) - 100, 10, 400, 60), "Delve Into Nidvallir", splashName);

		bool newGame  = GUI.Button (new Rect((Screen.width / 2) - 50, ((Screen.height / 2) - 15) + newGY, 100, 30), "New Game");
		bool loadGame = GUI.Button (new Rect((Screen.width / 2) - 50, ((Screen.height / 2) + 25) + loadY, 100, 30), "Load Game");
		bool settings = GUI.Button (new Rect((Screen.width / 2) - 50, ((Screen.height / 2) + 65) + settY, 100, 30), "Settings");
		bool credits  = GUI.Button (new Rect((Screen.width / 2) - 50, ((Screen.height / 2) + 105)+ credY, 100, 30), "Credits");
		bool loadStg1 = GUI.Button (new Rect((Screen.width / 2) - 50, ((Screen.height / 2) + 145)+ stg1Y, 100, 30), "Load Stage 1");

		if (newGame)
		{
			lowerButtons = !lowerButtons;

		}
		if (newGameOpen)
		{
			//bool anvil = 
			if(GUI.Button(new Rect((Screen.width / 2) - 160, ((Screen.height / 2) - 20), 100, 50), "Anvil Caste"))
			{
				GameHandler.handler.SetMainBase(0);
				//var thing = (GameObject)Instantiate(GameControlScript.control.mainBase, new Vector3(0,0,0), GameControlScript.control.mainBase.transform.rotation);
				Application.LoadLevel(1);
			}
			//bool gear = 
			if(GUI.Button(new Rect((Screen.width / 2) - 50, ((Screen.height / 2) - 20), 100, 50), "Gear Caste"))
			{
				GameControlScript.control.mainBase = GameControlScript.control.gear;
			}
			//bool hammer = 
			if(GUI.Button (new Rect((Screen.width / 2) + 60, ((Screen.height / 2) - 20), 100, 50), "Hammer Caste"))
			{
				GameHandler.handler.SetMainBase(0);
			}
		}
		if (loadGame)
		{
			
		}
		if (settings) 
		{

		}
		if (credits)
		{

		}
		if (loadStg1)
		{
			Application.LoadLevel (1);
		}

	}

	void Update(){
		if(lowerButtons){
			if(heightMove < end){
				loadY+= 0.5f;
				settY+= 0.5f;
				credY+= 0.5f;
				stg1Y+= 0.5f;
				newGY--;
				heightMove++;
			}
			else
			{
				newGameOpen = true;
			}
		}
		if (!lowerButtons) {
			newGameOpen = false;
			if(heightMove > 0){
				loadY-= 0.5f;
				settY-= 0.5f;
				credY-= 0.5f;
				stg1Y-= 0.5f;
				newGY++;
				heightMove--;
			}
		}
	}
}
