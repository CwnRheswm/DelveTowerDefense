using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PDollarGestureRecognizer;

public class InputHandlerScript : MonoBehaviour {

	public static InputHandlerScript input;

	private RaycastHit[] hits;
	private RaycastHit hit;

	public GameObject selected;
	private OnClickScript click;
	private bool dblClick = false;
	private bool isClicked = false;
	private float clickTime = 0f;
	private float dblClickDelta = 0.39f;

	private Gesture[] trainingSet;

	private List<Point> points = new List<Point>();
	private LineRenderer line;
	private int ptInt = 0;

	void Awake(){
		if(input == null){
			DontDestroyOnLoad (gameObject);
			input = this;
		}
		else
		{
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		line.SetColors (Color.blue, Color.blue);
		line.SetWidth (2f, 2f);
		line.SetVertexCount (0);
		trainingSet = LoadTrainingSet ();
	}

	private Gesture[] LoadTrainingSet()
	{
		string path = "Assets/Resources/Gestures";
		DirectoryInfo dir = new DirectoryInfo(path);
		FileInfo[] info = dir.GetFiles ("*.xml");
		List<Gesture> gestures = new List<Gesture>();

		foreach(FileInfo file in info)
		{
			string fileName = Path.GetFileNameWithoutExtension (file.Name);
			gestures.Add (GestureIO.ReadGesture(fileName));
		}
		return gestures.ToArray();
	}

	// Update is called once per frame
	void Update () {
		/*
		if (dblClick) {
			hits = Physics.RaycastAll (Camera.main.ScreenPointToRay(Input.mousePosition) );
			for (int i = 0; i < hits.Length; i++){
				hit = hits[i];
				if (hit.transform.tag == "Floor"){
					ptInt += 1;
					line.SetVertexCount (ptInt);
					line.SetPosition (ptInt - 1, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z) );
					points.Add (new Point (hit.point.x, hit.point.z, 0));
				}
			}
		}*/
		if (isClicked) {
			clickTime += Time.deltaTime;
		}
		if (clickTime > dblClickDelta) {
			Click();

		}
		if (Input.GetMouseButtonUp (0)) {
			if(dblClick){
				selected = null;
				dblClick = false;
				if(points.Count > 1){
					RecognizeGesture ();
				}
				ptInt = 0;
				line.SetVertexCount (ptInt);
			}
		}
		if (Input.GetMouseButtonDown (0)) 
		{
			/*
			if(isClicked && clickTime < dblClickDelta){
				dblClick = true;
				isClicked = false;
				clickTime = 0f;
			}*/
			if(!isClicked){
				isClicked = true;
			}
			if(isClicked){
				if(clickTime >= dblClickDelta){
					DoubleClick();
				}
			}
		}
		if(Input.GetMouseButton (1)){
			Debug.Log("Holding Right Click");
			Camera.main.GetComponent<CameraControl>().moveV = -Input.GetAxis("Mouse Y");
			Camera.main.GetComponent<CameraControl>().moveH = -Input.GetAxis("Mouse X");
		}
	}

	private void LongPress(){

	}
	private void DoubleClick(){
		clickTime = 0f;
		isClicked = false;
	}
	private void Click(){
		clickTime = 0f;
		isClicked = false;
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
		
		if(selected != null)
		{
			if(hit.transform != selected.transform)
			{
				Debug.Log("A "+selected.transform+" was selected, but "+hit.transform+" was clicked");
				//selected.GetComponent<OnClickScript>().OffClick();
				click.OffClick ();
				selected = null;
			}
		}

		if(hit.transform != null){
			click = hit.transform.GetComponent<OnClickScript>();
		}

		if(click != null)
		{
			Debug.Log(hit.transform+" was clicked");
			click.OnClick();
			selected = hit.transform.gameObject;
		}
	
	}

	private void RecognizeGesture()
	{
		var pointsToArray = points.ToArray ();
		Gesture candidate = new Gesture(pointsToArray);
		line.SetVertexCount(0);
		for (int p = 0; p < candidate.Points.Length; p++) {
			line.SetVertexCount(p+1);
			line.SetPosition (p, new Vector3(candidate.Points[p].X * 50, 10, candidate.Points[p].Y * 50));
		}
		string gestureClass = PointCloudRecognizer.Classify(candidate, trainingSet);
		GameControlScript.control.mainBase.GetComponent<AbilityScript> ().Cast (gestureClass, candidate, line);
		points.Clear ();
	}
}
