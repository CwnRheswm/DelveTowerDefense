using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Linq;

public class ClickBlueprintScript : MonoBehaviour {

	private bool isLoose = false;
	private bool isBuilt = false;
	private GameObject floor;

	public Vector3 homePos;
	private Vector3 newPos;
	private float levitate = 0.5f;

	public GameObject blueprint;

	//private Transform build;
	private Transform parent;
	private Transform ancestor;
	private ClickMenuScript menu;
	private VaultScript coffer;
	private AbilityBook abilityBook;

	//private Parser parser = Parser.blueprint;

	private Color RED = new Color(1, 0, 0);
	private Color GREEN = new Color(0, 1, 0);
	private Color YELLOW = new Color(1, 1, 0);
	private float mRng;
	private float aRng;
	private LineRenderer[] spheres;
	private bool oreCheck;
	private GraphNode nearest;

	// Use this for initialization
	void Start () {
		coffer = VaultScript.vault;
		parent = transform.parent;
		ancestor = parent.parent;
		homePos = transform.localPosition;

		menu = parent.GetComponent<ClickMenuScript> ();

		Abilities abilityScript = blueprint.GetComponent<Abilities>();
		foreach(AbilityBook ability in abilityScript.abilityBook){
			if (ability.projectile.name.Contains("Mining")){
				mRng = ability.range;
			}
		}
		//mRng = blueprint.GetComponent<MiningScript> ().miningRange;
		//aRng = blueprint.GetComponent<AttackScript> ().attackRange;
		spheres = GetComponentsInChildren<LineRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (isLoose) {
			spheres[0].SetColors (RED, RED);
			spheres[0].SetVertexCount (45);
			spheres[1].SetColors (YELLOW, YELLOW);
			spheres[1].SetVertexCount (45);

			RaycastHit[] hits;
			RaycastHit hit;
			hits = Physics.RaycastAll (Camera.main.ScreenPointToRay(Input.mousePosition) );
			oreCheck = false;
			for (int i = 0; i < hits.Length; i++){
				hit = hits[i];
				if (hit.transform.tag == "Floor"){
					newPos = new Vector3(hit.point.x, hit.point.y + levitate, hit.point.z);
				}

				if(InputHandlerScript.input.selected == this.gameObject){
					for(int k = 0; k < 45; k++){
						spheres[0].SetPosition(k,
									           new Vector3(0 + (mRng * Mathf.Cos (k * Mathf.PI / 20f)),
									           			   //0,
									           			   0 + (mRng * Mathf.Sin (k * Mathf.PI / 20f)),
						            					   0));
						spheres[1].SetPosition(k,
						                       new Vector3(0 + (aRng * Mathf.Cos (k * Mathf.PI / 20f)),
						            					   //0,
						            					   0 + (aRng * Mathf.Sin (k * Mathf.PI / 20f)),
						            					   0));
						//Debug.Log (mRng * Mathf.Cos (k * Mathf.PI / 20f));
					}
				}
				var mineRange = Physics.OverlapSphere(newPos, mRng);
				foreach(Collider collision in mineRange){
					if(collision.tag == "Iron" || GetComponent<Collider>().tag == "Brin" || collision.tag == "Lode" || collision.tag == "Mith"){
						oreCheck = true;
					}
				}
				nearest = (GraphNode)AstarPath.active.GetNearest(newPos);
				if(oreCheck && nearest.Walkable){
					spheres[0].SetColors (GREEN, GREEN);
				}

			}
			transform.position = newPos;

			if(Input.GetMouseButtonUp (0)){
				if(oreCheck && nearest.Walkable){
						OnRelease ();
				}
				else{
					ReturnHome ();
				}
			}
		}
	}

	public void OnRelease(){
		spheres [0].SetVertexCount (0);
		spheres [1].SetVertexCount (1);
		if (blueprint != null && coffer.CheckFunds (gameObject, blueprint) ){
			transform.parent = LayerBuildScript.buildLayer.transform;
			OffClick ();
		}
		else{
			ReturnHome ();
		}

	}

	public void ReturnHome(){
		transform.localPosition = homePos;
		OffClick ();
	}

	public void Built(){
		//transform.parent = parent;
		//transform.localPosition = homePos;
		isBuilt = true;
		gameObject.SetActive(false);
	}

	public void OnClick(){
		Debug.Log ("Clicked "+gameObject.name);
		if(!isBuilt){
			menu.OnClick ();
			isLoose = true;
			//Debug.Log (isLoose);
		}
	}

	public void OffClick(){
		isLoose = false;
		isBuilt = true;
		menu.OffClick ();
		//Debug.Log (isLoose);
	}
}
