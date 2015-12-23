using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;

	public float moveH;
	public float moveV;

	private Vector3 pos;
	private GameObject floor;
	private BoxCollider box;

	// Use this for initialization
	void Start () {
		float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
		float horzExtent = vertExtent * Screen.width / Screen.height;
		pos.y = 45;

		floor = GameObject.Find("Plane");
		box = floor.GetComponent<BoxCollider>();
		leftBound = (float)((horzExtent - (box.size.x * floor.transform.localScale.x) / 2f));
		rightBound = (float)((box.size.x * floor.transform.localScale.x) / 2f - horzExtent);
		topBound = (float)(vertExtent - (box.size.z * floor.transform.localScale.z) / 2f);
		bottomBound = (float)((box.size.z * floor.transform.localScale.z) / 2f - vertExtent);	
		Debug.Log(vertExtent+" : "+horzExtent);
	}
	// Update is called once per frame
	void Update () {
		if(moveV != 0){
			pos.z += moveV;
			moveV = 0;
		}
		if(moveH != 0){
			pos.x += moveH;
			moveH = 0;
		}
		if(Input.GetKey(KeyCode.A)){
			moveH = -1;
		}
		if(Input.GetKey(KeyCode.D)){
			moveH = 1;
		}
		if(Input.GetKey(KeyCode.W)){
			moveV = 1;
		}
		if(Input.GetKey(KeyCode.S)){
			moveV = -1;
		}
		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.z = Mathf.Clamp(pos.z, topBound, bottomBound);
		transform.position = pos;
	}
}
