using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * Using the Level Manager Script:
 * 	This script is used to hold variables generated during Editor/Level Building runtime
 *  to populate GameObjects with Prefabs and attributes that can be validated instead
 *  of just putting them into the Editor
 * 
 * Neccessary Objects to Start a Level:
 *  In order to have a basic working level a few GameObject HAVE to be in the level.
 *  Hierarchy GameObjects:
 * 		these GOs are the basic background structure
 * 		Renderer
 * 			MainCamera
 * 			DirectionalLight
 * 		0_Floor
 * 			A*
 * 			PlayerStart
 * 			Ores
 * 		1_Build
 * 		2_Unit
 * 			SpawningPod
 * 		3_Projectile
 * 		6_Graveyard
 * 		GameControl
 * 	These should be in the scene already
 * 	Objects tracked and recorded in the Level Manager Script
 * 		PlayerStart
 * 			when ever the PlayerStart is moved, the position should be tracked and recorded
 * 		SpawningPod
 * 			the SpawningPod is one of the main objects that needs to be tracked and recorded to be populated post-run-time
 * 			Three main things can be instantiated onto the screen that need to be tracked
 * 				New Spawning Locations
 * 					The Spawning Pod, instead of having a set position from which it spawns groups (as in Alpha)
 * 					must have at least 1 SpawningLocation attributed to it. These are recorded in an array from 
 * 					which Spawning Groups can reference their location.
 * 					&& TO DO:  add the ability to create/destroy new Spawning Locations
 * 				New Spawning Groups
 * 					Spawning Groups are intitially set as having a INT spawnTime, INT spawnLocation (referencing the
 * 					spawningLocation array). It also has a UNITDATABASE unitDb to which new Spawns are add.
 * 					&& TO DO:  add the ability to create/destroy Spawn Groups during Building Run Time, then add
 * 								ability to add/remove units
 * 				New Spawns	
 * 					spawns are add to spawning groups; the initial spawn group is Group 0, this group does not spawn
 * 					from a spawning location, instead the units in Group 0 are spawned on the map before the level begins.
 * 					Units that spawn from a Pod and proceded as normal enemies have a STR unitName, VECTOR3 position (which
 * 					is their position the the Spawning Group troop
 * 					On the first instance of adding the unit to the map, the unitName should be added to the top level
 * 					STR[] unitList.
 * 					&& TO DO:  record unitName and unitPosition when adding the unit to the map
 * 			After run time, LevelMangagerScript should:
 * 				make the length of SpawningPod.locations equal to spawnLocations.Length, then set each array item
 * 				 in SpawningPod.locations[i] equal to spawnLocations[i].
 * 				make the length of SpawningPod.groups equal to spawnGroups.Length, copy the unitDB information from
 * 				 LevelManagerScript.spawnGroups into SpawningPod.groups; AssetDatabse.LoadAtPath the prefabs 
 * 				 corresponding to the names
 * 		Ores
 * 			Array that tracks the position and types of Ores. Holds those in an Ores array[], populates an array
 * 			 in the Editor, which Instantiates all those ores when the level is being set up
 * 		Objects
 * 			NOT IMPLEMENTED
 * 			Tracks the position and type of non-standard objects; e.g. gates, waterflow, traps, etc.
 */

public class LevelManagerScript : MonoBehaviour {

	public string level;

	public Vector3 playerStartPosition;
	//When pulling in a new Unit, either into a SpawingPod or on the screen
	public string[] unitList; //add the unit name to the unitList if not present IS THIS NEEDED
	public SpawnGroup[] spawnGroups; //Spawning Pod has multiple groups, spawning at different times and in different positions
	public Vector3[] spawnLocations; //Spawning Pod can have multiple locations
	//ore
	public OreArray[] ores;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnValidate () {
		//populate the 
	}
}

public class OreArray
{
	public OreType type;
	public Vector3 position;
}
[Serializable]
public class SpawnGroup
{
	//if a unit is not spawning from a Pod during runtime, i.e. is on the map initially, Time & Location remain as -1;
	public int callTime = -1; //time since beginning that this group will spawn
	public int spawnLocationIndex = -1; //reference to the SpawningPod.spawnLocations record that this group spawns from
	public SpawnUnit[] spawnUnit; //add the unit details to the unitDB
}
[Serializable]
public class SpawnUnit
{
	public string name; //name of the unit
	//public int spawnGroup; //if the unit is spawned from a SpawningPod, add the spawnGroup number (>0); if not spawned, or spawned on a particular call, the spawnGroup is 0
	public Vector3 position; //if spawnGroup == 0: this is the position on the screen; else >0: this is the relative position in the troop
	//do they need anything else as a unit?
	public GameObject unit;
	public int groupSize;
}
/*public class SpawnLocation
{
	public Vector3 position; //location from which enemies spawn
}*/
