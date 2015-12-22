using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UpgradeScript))]

public class upgradeScriptInspector : Editor {

	/*private bool cantAdd = false;
	private bool changeWarning = false;

	private bool bit0Open = false;
	private bool bit1Open = false;
	private bool bit2Open = false;
	private bool bit3Open = false;
	private bool commOpen = false;
	private bool rareA0Open = false;
	private bool rareA1Open = false;
	private bool rareB0Open = false;
	private bool rareB1Open = false;*/

	//private List<bool> openList = new List<bool>();

	public override void OnInspectorGUI(){
		UpgradeScript ug = target as UpgradeScript;

		if(!ug.lockUpgrades){
			EditorGUILayout.Separator ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("   Common Bits:");
			EditorGUILayout.LabelField ("Rare Bits:");
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			ug.mattock = EditorGUILayout.Toggle ("Mattock Head", ug.mattock);
			ug.alembic = EditorGUILayout.Toggle ("Spark Alembic", ug.alembic);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			ug.compression = EditorGUILayout.Toggle ("Compression Unit", ug.compression);
			ug.engine = EditorGUILayout.Toggle ("Lode Engine", ug.engine);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			ug.oscillation = EditorGUILayout.Toggle ("Oscillation Element", ug.oscillation);
			ug.silvered = EditorGUILayout.Toggle ("Silvered Leaf", ug.silvered);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Separator ();
			
			CheckBitLists (ug);
			
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if (GUILayout.Button ("Lock Upgrades")) {
				ug.lockUpgrades = true;
				addBits(ug);
			}
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();
		}
		else{
			EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (300));
			EditorGUILayout.Space ();
			if(GUILayout.Button ("Unlock Upgrades", GUILayout.Width (120))){
				ug.lockUpgrades = false;
			}
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Separator ();
			EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (150));
			EditorGUI.indentLevel = 2;
			EditorGUIUtility.labelWidth = 80f;
			EditorGUILayout.BeginVertical ();
			if(ug.mattock){
				EditorGUILayout.LabelField ("Mattock Head");
			}
			if(ug.compression){
				EditorGUILayout.LabelField ("Compression Unit");
			}
			if(ug.oscillation){
				EditorGUILayout.LabelField ("Oscillation Element");
			}
			EditorGUILayout.EndVertical ();
			EditorGUILayout.BeginVertical ();
			if(ug.alembic){
				EditorGUILayout.LabelField ("Spark Alembic");
			}
			if(ug.engine){
				EditorGUILayout.LabelField ("Lode Engine");
			}
			if(ug.silvered){
				EditorGUILayout.LabelField ("Silvered Leaf");
			}
			EditorGUILayout.EndVertical ();
			EditorGUI.indentLevel = 1;
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (150));
			if(GUILayout.Button ("+*", GUILayout.Width (25))){
				foreach(UpgradeBit bit in ug.upgradeDict){
					bit.bitOpen = true;
				}
			}
			if(GUILayout.Button ("-*", GUILayout.Width (25))){
				foreach(UpgradeBit bit in ug.upgradeDict){
					bit.bitOpen = false;
				}
			}
			EditorGUILayout.Space ();
			if(GUILayout.Button ("Save Upgrades", GUILayout.Width (100))){
				ug.Save();
			}
			//EditorGUILayout.Space ();
			if(GUILayout.Button ("Load Upgrades", GUILayout.Width (100))){
				ug.Load ();
				foreach(UpgradeBit bit in ug.upgradeDict){
					bit.bitOpen = false;
				}
			}
			//EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Separator ();


			foreach(UpgradeBit bit in ug.upgradeDict){
				bit.bitOpen = EditorGUILayout.Foldout (bit.bitOpen, bit.bit.ToString ()+"      "+bit.upgradeLevels.Count.ToString ());
				if(bit.bitOpen){
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (bit.currentLevel.ToString (), GUILayout.Width (40));
					if(GUILayout.Button ("+", GUILayout.Width (30))){
						bit.upgradeLevels.Add (new UpgradeLevels());
					}
					if(GUILayout.Button ("-", GUILayout.Width (30))){
						bit.upgradeLevels.RemoveAt (bit.upgradeLevels.Count - 1);
					}
					//EditorGUILayout.LabelField (bit.upgradeLevels.Count.ToString ());
					EditorGUILayout.EndHorizontal ();
					
					for(int i = 0; i < bit.upgradeLevels.Count; i++){
						EditorGUI.indentLevel = 1;
						EditorGUILayout.BeginHorizontal ();
						var level = bit.upgradeLevels[i];
						EditorGUILayout.LabelField ( "Level "+i+": ", GUILayout.Width (80));
						EditorGUILayout.EndHorizontal ();


						EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth (150));
						EditorGUIUtility.labelWidth = 80f;
						EditorGUI.indentLevel = 3;
						EditorGUILayout.BeginVertical ();
						level.costIron = EditorGUILayout.FloatField ("Iron:", level.costIron, GUILayout.Width (110));
						level.costBrin = EditorGUILayout.FloatField ("Brin:", level.costBrin, GUILayout.Width (110));
						EditorGUILayout.EndVertical ();
						//EditorGUILayout.EndHorizontal ();

						//EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.BeginVertical ();
						level.costLode = EditorGUILayout.FloatField ("Lode:", level.costLode, GUILayout.Width (110));
						level.costMith = EditorGUILayout.FloatField ("Mith:", level.costMith, GUILayout.Width (110));
						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (180));
						EditorGUILayout.Space ();
						if(GUILayout.Button ("Add", GUILayout.Width (50))){
							level.details.Add (new UpgradeDetail());
						}
						if(GUILayout.Button ("Remove", GUILayout.Width (60))){
							level.details.RemoveAt (level.details.Count - 1);
						}
						EditorGUILayout.Space ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Separator ();	
						
						EditorGUI.indentLevel = 2;
						for(int j = 0; j < level.details.Count; j++){
							EditorGUILayout.BeginHorizontal ();
							var detail = level.details[j];
							detail.upgradeType = (UpgradeType)EditorGUILayout.EnumPopup (detail.upgradeType, GUILayout.Width(150));
							switch(detail.upgradeType)
							{
							case(UpgradeType.AttackType):
								detail.attack = (AttackTypes)EditorGUILayout.EnumPopup (detail.attack, GUILayout.Width (100));
								break;
							case(UpgradeType.Defenses):
								detail.defense = (Defenses)EditorGUILayout.EnumPopup (detail.defense, GUILayout.Width (100));
								break;
							default:
								detail.val = EditorGUILayout.FloatField (detail.val, GUILayout.Width (100));
								break;
							}
							EditorGUILayout.EndHorizontal ();
						}	
						EditorGUI.indentLevel = 0;
					}
				}
			}
		}
		EditorGUILayout.Separator ();
		EditorUtility.SetDirty (ug);
	}

	void CheckBitLists(UpgradeScript ug){
		if (ug.mattock && !ug.commons.Contains (bitTypes.mattock)) {
			ug.commons.Add (bitTypes.mattock);
		}
		else{
			if (!ug.mattock && ug.commons.Contains (bitTypes.mattock)) {
				ug.commons.Remove (bitTypes.mattock);
			}
		}
		if (ug.compression && !ug.commons.Contains (bitTypes.compress)) {
			ug.commons.Add (bitTypes.compress);
		}
		else{
			if (!ug.compression && ug.commons.Contains (bitTypes.compress)) {
				ug.commons.Remove (bitTypes.compress);
			}
		}
		if (ug.oscillation && !ug.commons.Contains (bitTypes.oscillate)) {
			ug.commons.Add (bitTypes.oscillate);
		}
		else{
			if (!ug.oscillation && ug.commons.Contains (bitTypes.oscillate)) {
				ug.commons.Remove (bitTypes.oscillate);
			}
		}
		
		if (ug.commons.Count > 2) {
			switch(ug.commons[0])
			{
			case(bitTypes.mattock):
				ug.mattock = false;
				break;
			case(bitTypes.compress):
				ug.compression = false;
				break;
			case(bitTypes.oscillate):
				ug.oscillation = false;
				break;
			}
			ug.commons.RemoveAt (0);
		}
		
		if (ug.engine && !ug.rares.Contains (bitTypes.engine)) {
			ug.rares.Add (bitTypes.engine);
		}
		else{
			if (!ug.engine && ug.rares.Contains (bitTypes.engine)) {
				ug.rares.Remove (bitTypes.engine);
			}
		}
		if (ug.alembic && !ug.rares.Contains (bitTypes.alembic)) {
			ug.rares.Add (bitTypes.alembic);
		}
		else{
			if (!ug.alembic && ug.rares.Contains (bitTypes.alembic)) {
				ug.rares.Remove (bitTypes.alembic);
			}
		}
		if (ug.silvered && !ug.rares.Contains (bitTypes.silvered)) {
			ug.rares.Add (bitTypes.silvered);
		}
		else{
			if (!ug.silvered && ug.rares.Contains (bitTypes.silvered)) {
				ug.rares.Remove (bitTypes.silvered);
			}
		}
		
		if (ug.rares.Count > 2) {
			switch(ug.rares[0])
			{
			case(bitTypes.alembic):
				ug.alembic = false;
				break;
			case(bitTypes.engine):
				ug.engine = false;
				break;
			case(bitTypes.silvered):
				ug.silvered = false;
				break;
			}
			ug.rares.RemoveAt (0);
		}
	}


	void addBits(UpgradeScript ug){
		while (ug.upgradeDict.Count < 7){
			Debug.Log ("Bits Counts: "+ug.upgradeDict.Count);
			ug.upgradeDict.Add (new UpgradeBit());
		}
		
		List<bitTypes> trueBits = new List<bitTypes> ();
		if (ug.mattock) {
			trueBits.Add (bitTypes.mattock);
		}
		if(ug.oscillation){
			trueBits.Add (bitTypes.oscillate);
		}
		if(ug.compression){
			trueBits.Add (bitTypes.compress);
		}
		if(ug.alembic){
			trueBits.Add (bitTypes.alembic);
		}
		if (ug.engine) {
			trueBits.Add (bitTypes.engine);
		}
		if(ug.silvered){
			trueBits.Add (bitTypes.silvered);
		}
		trueBits.Add (bitTypes.commonAdd);
		trueBits.Add (bitTypes.rare1Add);
		trueBits.Add (bitTypes.rare2Add);
		
		for (int i = 0; i < trueBits.Count; i++) {
			ug.upgradeDict[i].bit = trueBits[i];
			
		}
	}
}