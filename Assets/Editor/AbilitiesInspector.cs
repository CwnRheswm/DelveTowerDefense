using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Abilities))]

public class AbilitiesInspector : Editor {

	public int arraySize;
	private RectOffset rctOff;
	
public override void OnInspectorGUI()
    {	
    	Abilities abilities = target as Abilities;
    	arraySize = abilities.abilityBook.Length;
    	arraySize = EditorGUILayout.IntField("Ability Book Size: ",arraySize);
    	EditorGUILayout.LabelField("Global Cooldown: "+abilities.globalCooldownTimer);
    	if (arraySize != abilities.abilityBook.Length){
    		Array.Resize(ref abilities.abilityBook, arraySize);
    	}
    	if(abilities.abilityBook.Length > 0){
	    	foreach (AbilityBook ability in abilities.abilityBook){
	    		EditorGUI.indentLevel = 1;
	    		ability.projectile = EditorGUILayout.ObjectField("Projectile: ", ability.projectile, typeof(GameObject), true) as GameObject;
	    		EditorGUI.indentLevel = 3;
	    		if (ability.projectile != null){
	    			var missile = ability.projectile.GetComponent<Missile>();
	    			if (ability.foldout = EditorGUILayout.Foldout(ability.foldout, "See Details")){
	    				EditorGUI.indentLevel = 4;
	    				EditorGUILayout.LabelField("Tower GameObject: "+missile.tower);
		    			//bool isActive
		    			ability.isActive = EditorGUILayout.Toggle("Is Active: ", ability.isActive);

		    			//damage
		    			var r = EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Damage: "+missile.damage);
		    			ability.damagePlus = EditorGUILayout.FloatField("Add: ",ability.damagePlus);
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//range
		    			EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Range: "+missile.range);
		    			ability.rangePlus = EditorGUILayout.FloatField("Add: ", ability.rangePlus);
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//targets
		    			EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Targets: "+missile.maxTargets);
		    			ability.maxTargetsPlus = EditorGUILayout.IntField("Add: ", ability.maxTargetsPlus);
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//cooldown
		    			EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Cooldown: "+missile.cooldown+" / "+ability.cooldown);
		    			ability.cooldownPlus = EditorGUILayout.FloatField("Add: ", ability.cooldownPlus);
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//castTime
		    			EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Cast Time: "+missile.castTime);
		    			ability.castTimePlus = EditorGUILayout.FloatField("Add: ", ability.castTimePlus);
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//has Splash
		    			EditorGUILayout.BeginHorizontal();
		    			EditorGUIUtility.labelWidth = 100;
		    			EditorGUILayout.LabelField("Has Splash: "+missile.hasSplash);
		    			if (GUILayout.Button("Toggle Splash")){
		    				missile.hasSplash = !missile.hasSplash;
		    			}
		    			EditorGUIUtility.labelWidth = 0;
		    			EditorGUILayout.EndHorizontal();

		    			//sDamage
		    			if (missile.hasSplash){
			    			EditorGUILayout.BeginHorizontal();
			    			EditorGUIUtility.labelWidth = 100;
			    			EditorGUILayout.LabelField("Splash Damage: "+missile.sDamage);
			    			ability.sDamagePlus = EditorGUILayout.FloatField("Add: ",ability.sDamagePlus);
			    			EditorGUIUtility.labelWidth = 0;
			    			EditorGUILayout.EndHorizontal();

			    			//sRange
			    			EditorGUILayout.BeginHorizontal();
			    			EditorGUIUtility.labelWidth = 100;
			    			EditorGUILayout.LabelField("Splash Range: "+missile.sRange);
			    			ability.sRangePlus = EditorGUILayout.FloatField("Add: ",ability.sRangePlus);
			    			EditorGUIUtility.labelWidth = 0;
			    			EditorGUILayout.EndHorizontal();
			    		}
		    		}
	    		}
	    	}
	    }
    }
}
