using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	private Body body;

	public float[] miningRate = new float[4];
/*	private float ironMiningRate;
	private float lodestoneMiningRate;
	private float brinmistrMiningRate;
	private float mithrilMiningRate;
*/
	// Use this for initialization
	void Start () {
		body = GetComponent<Body>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void DepositOres(float[] ores){
		for(int d = 0; d < miningRate.Length; d++){
			ores[d] *= miningRate[d];
		}
		VaultScript.vault.DepositFunds(ores);
		Debug.Log("Deposited "+ores[0]);
	}
	public void Die(){
		body.enabled = false;
	}
}
