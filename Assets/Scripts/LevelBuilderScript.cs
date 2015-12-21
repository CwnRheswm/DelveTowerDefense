using UnityEditor;
using UnityEngine;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelBuilderScript : MonoBehaviour {

	private int width = 1;
	private int height = 1;

	private float hexW = 1f;
	private float hexH = 0.87f;
	private float rowShift = 0.5f;

	private bool newMap;
	private string levelName = "";
	private bool loadButton;
	private Vector2 loadPosition;
	private Rect loadRect = new Rect(240, 100, 130, 20);
	private string[] levels = new string[0];
	private bool levelBtn;

	public GameObject hex;
	private HexTypeScript hexes;
	private HexData[,] hexGraph = new HexData[0, 0];
	public HexGroup hexGroup;

	public GUIStyle labelStyle;
	public GUIStyle numStyle;
	//public List<Material> hexes = new List<Material>();
	//public Material[] mats;// = new Material[1];
	//public Texture[] buttons = new Texture[7];
	private bool isPainting;
	public List<Collider> c = new List<Collider>();

	private HexTypes painter;
	public Vector2 scrollPosition;
	public bool painterBtn = false;
	private bool paintTool = false;
	private bool largeCursor = false;
	private float paintRadius;
	private int painterIndex = 0;

	public Texture arrowUp;
	public Texture arrowUpPressed;
	private bool raiseGround = false;

	public Texture arrowDown;
	public Texture arrowDownPressed;
	private bool lowerGround = false;

	public GUIStyle emptyButtons;
	public Texture2D[] oreTextures = new Texture2D[5];
	private Texture2D orePainter;
	private bool orePaintMenu;
	private int orePaint = 0;
	private bool[] oreBools = new bool[0];
	private bool orePaintTool;

	public Texture2D podImage;

	private LineRenderer border;

	public GridGraph graph;

	private GameObject pStart;
	private GameObject[] eStart;

	private Rect menuWindowRect;
	public GUIStyle menuWindowStyle;
	public GUISkin menuSkin;

	void Awake(){
		border = GetComponent<LineRenderer> ();
		painter = hexGroup.hexes [0];
		scrollPosition = new Vector2 (Screen.width - 100, 100);
		var path = new DirectoryInfo (Application.persistentDataPath);
		var levelFiles = path.GetFiles();
		foreach (FileInfo file in levelFiles) {
			if(file.Name.Contains(".hex")){//".hex")){
				Array.Resize (ref levels, levels.Length + 1);
				levels[levels.Length - 1] = file.Name.Remove(file.Name.Length - 4);
				//Debug.Log (levels[levels.Length - 1]);
			}
		}
		menuWindowRect = new Rect(0, 0, Screen.width, 50);
		orePainter = oreTextures [orePaint];
	}

	void OnGUI(){
		//GUI.color = Color.grey;
		GUILayout.Window (0, menuWindowRect, MenuWindow, "", menuWindowStyle);
		hexes = GameObject.Find ("Hex Types").GetComponent<HexTypeScript> ();
		
		if(loadButton == true){

			GUILayout.Window (1,new Rect(240,100,130,20), LevelLoad, "");
		}
		//GameObject hex = hexes.hex;
		//var mats = hexes.mats;
		//mats = hexes.mats;

		GUI.Label (new Rect (10, 10, 100, 40), "HEIGHT:", labelStyle);
		GUI.Label (new Rect (10, 60, 100, 40), "WIDTH:", labelStyle);
		GUI.TextArea (new Rect (115, 10, 50, 40), height.ToString (), numStyle);
		GUI.TextArea (new Rect (115, 60, 50, 40), width.ToString (), numStyle);
		if (GUI.Button (new Rect (170, 12, 30, 15), "+")) {
			height++;
		}
		if (GUI.Button (new Rect (170, 33, 30, 15), "-")) {
			height--;
		}
		if (GUI.Button (new Rect (210, 12, 30, 15), "+X")) {
			height += 10;
		}
		if (GUI.Button (new Rect (210, 33, 30, 15), "-X")) {
			height -= 10;		
		}
		if (GUI.Button (new Rect (170, 62, 30, 15), "+")) {
			width++;
		}
		if (GUI.Button (new Rect (170, 83, 30, 15), "-")) {
			width--;
		}
		if (GUI.Button (new Rect (210, 62, 30, 15), "+X")) {
			width += 10;
		}
		if (GUI.Button (new Rect (210, 83, 30, 15), "-X")) {
			width -= 10;
		}

		if (!newMap){
			if(GUI.Button (new Rect (250, 20, 60, 70), "Create\n Map")){
				newMap = true;
				GraphMap ();
			}
		}
		else{
			levelName = GUI.TextArea (new Rect(10, 120, 200, 20), levelName);
			if(GUI.Button(new Rect(220, 120, 20, 20), "...")){
				levelBtn = !levelBtn;
			}
			if(levelBtn){
				/*Rect loadRect = new Rect(250, 120, 120, 10);
				Rect loadViewRect = new Rect(0, 0, 0, 20 * levels.Length);
				loadPosition = GUI.BeginScrollView(loadRect, loadPosition, loadViewRect);
				foreach(string level in levels){
					if(GUILayout.Button (level)){
						levelName = level;
						levelBtn = !levelBtn;
					}
				}
				GUI.EndScrollView ();*/

			}
			if(GUI.Button (new Rect(20, 150, 80, 30), "Save Grid")){
				Save ();
			}
			if(GUI.Button (new Rect(120, 150, 80, 30), "Load Grid")){
				Load ();
			}
			if(GUI.Button (new Rect (250, 20, 60, 70), "Update\n Map")){
				GraphMap ();
			}
			if (!raiseGround){
				if (GUI.Button (new Rect( Screen.width - 300, 10, 80, 80), arrowUp))
				{
					raiseGround = !raiseGround;
					lowerGround = false;
					paintTool = false;
				}
			}
			if (raiseGround){
				isPainting = true;
				if (GUI.Button (new Rect( Screen.width - 300, 10, 80, 80), arrowUpPressed)){
					raiseGround = !raiseGround;
				}
			}
			if (lowerGround){
				isPainting = true;
				if (GUI.Button (new Rect( Screen.width - 215, 10, 80, 80), arrowDownPressed)){
					lowerGround = !lowerGround;
				}
			}
			if (!lowerGround){
				if (GUI.Button (new Rect( Screen.width - 215, 10, 80, 80), arrowDown)){
					lowerGround = !lowerGround;
					raiseGround = false;
					paintTool = false;
				}
			}
			paintTool = GUI.Toggle (new Rect( Screen.width - 125, 10, 20, 20), paintTool, "");
			if (paintTool){
				isPainting = true;
				raiseGround = false;
				lowerGround = false;
				orePaintTool = false;
			}
			largeCursor = GUI.Toggle (new Rect(Screen.width - 127, 40, 30, 30), largeCursor, "");
			//Material Painter Button, flip on Y
			if (GUI.Button (new Rect(Screen.width - 100, 10, 80, 80), painter.btn)){
				painterBtn = !painterBtn;
			}

			if (painterBtn){
				Rect scrollRect = new Rect(Screen.width - 100, 100, 100, 300);
				Rect scrollViewRect = new Rect(0, 0, 0, 75 * hexGroup.hexes.Length);
				scrollPosition = GUI.BeginScrollView (scrollRect, scrollPosition, scrollViewRect);//(scrollPosition, GUILayout.Width(100), GUILayout.Height(350));
				for (int i = 0; i < hexGroup.hexes.Length; i++){
					//if (GUILayout.Button (hexGroup.hexes[i].btn, GUILayout.Width (70), GUILayout.Height (70))){

					if (GUILayout.Button (hexGroup.hexes[i].btn, emptyButtons, GUILayout.Width (70), GUILayout.Height (70))){
						painter = hexGroup.hexes[i];
						painterBtn = false;
						painterIndex = i;
					}
				}
				GUI.EndScrollView();
			
			}
			
			orePaintTool = GUI.Toggle (new Rect (Screen.width - 270, 100, 15, 15), orePaintTool, "");
			if (orePaintTool){
				isPainting = true;
				raiseGround = false;
				lowerGround = false;
				paintTool = false;
			}

			if(GUI.Button (new Rect(Screen.width - 252.5f, 100, 70, 70), orePainter, emptyButtons)){
				orePaintMenu = !orePaintMenu;
			}

			if (orePaintMenu){
				Array.Resize (ref oreBools, oreTextures.Length);
				float originX = (float)(Screen.width - 252.5f + 35.0f);
				float originY = 100.0f + 35.0f;
				//circleMenu (90, 180, oreTextures.Length, new Vector3(originX, originY, 0f), 70f, oreTextures);

				float minRange = 0f;
				float maxRange = 180f;
				float size = 55f;
				int num = oreBools.Length;

				float spread = (maxRange - minRange) / (num - 1);

				float r = size + (size / 2);
				float R1;
				float R2;

				for( int i = 0; i < num; i++ ){
					R1 = (float)((r * Math.Cos((Math.PI / 180)*(minRange + (spread * i))) + originX) - (size / 2));
					R2 = (float)((r * Math.Sin((Math.PI / 180)*(minRange + (spread * i))) + originY) - (size / 2));
					oreBools[i] = GUI.Button (new Rect( R1, R2, size, size), oreTextures[i], emptyButtons);
					if ( oreBools[i] ){
						orePaintMenu = false;
						orePaint = i;
						orePainter = oreTextures [orePaint];
					}
				}
			}
			if(GUI.Button (new Rect (Screen.width / 2 - 75, 10, 70, 70), "Player\nStart")){
				pStart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				pStart.transform.localScale = new Vector3(2f, 0.1f, 2f);
			}
			if(GUI.Button (new Rect (Screen.width / 2 + 5, 10, 70, 70), "Enemy\nStart")){
			}
			/*
			border.SetPosition (0, new Vector3(0.1f, 1f, 0f));
			border.SetPosition (1, new Vector3(width * hexW - 0.6f, 1f, 0f));
			border.SetPosition (2, new Vector3(width * hexW - 0.6f, 1f, height * hexH - hexH));
			border.SetPosition (3, new Vector3(0.1f, 1f, height * hexH - hexH));
			border.SetPosition (4, new Vector3(0.1f, 1f, 0f));
			*/
		}
	}
	/*void circleMenu( int minRange, int maxRange, int buttons, Vector3 origin, float size, Texture2D[] images)
	{

		float spread = (maxRange - minRange) / buttons;
		Debug.Log ("spread: " + spread);
		float r = size + (size / 4); 
		for( int i = 0; i < images.Length; i++ ){
			float R1 = (float)((r * Math.Cos(spread * i) + origin.x) - (size / 2));
			float R2 = (float)((r * Math.Sin(spread * i) + origin.y) - (size / 2));
			//oreBools[i] = 
			if (GUI.Button (new Rect( R1, R2, size, size), images[i]){

			}
		}	
	}*/
	void MenuWindow(int windowID){
		GUILayout.BeginHorizontal ();
		GUI.contentColor = Color.black;
		GUILayout.BeginVertical (GUILayout.Width (50));
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Height: " + height.ToString ());

		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("+1")) {
				height += 1;
		}
		if (GUILayout.Button ("+10")) {
				height += 10;
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-1")) {
				height -= 1;
		}
		if (GUILayout.Button ("-10")) {
				height -= 10;
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();

		GUILayout.Space (15);

		GUILayout.BeginVertical (GUILayout.Width (50));
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Width: " + width.ToString ());

		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("+1")) {
				width += 1;
		}
		if (GUILayout.Button ("+10")) {
				width += 10;
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-1")) {
				width -= 1;
		}
		if (GUILayout.Button ("-10")) {
				width -= 10;
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();

		GUILayout.Space (15);

		if (GUILayout.Button ("Update\nMap", GUILayout.Width (60), GUILayout.Height (70))) {
				GraphMap (); //should pass in height and width		
		}
		GUILayout.Space (15);
		GUILayout.BeginVertical (GUILayout.Width (110));
		GUILayout.Space (5);
		if (GUILayout.Button ("Load Map", GUILayout.Width (110))) {
			loadButton = true;
		}
		if (GUILayout.Button ("Save Map", GUILayout.Width (110))) {
				Save ();		
		}
		GUILayout.TextArea (levelName, GUILayout.Width (110));
		if(levelBtn){

		}
		GUILayout.EndVertical ();

		GUILayout.Space (15);
		//TOOLS section
		//Toolbar(Ground (Raise/Lower), hex selector)
		//Toggle(large paintbrush)
		GUILayout.BeginVertical (GUILayout.Width (110));
		GUI.contentColor = new Color (1, 1, 1, 1);
		GUILayout.Space (5);
		GUILayout.BeginHorizontal (GUILayout.Width (110));
		Texture[] tools = new Texture[3];
		tools [0] = arrowUp;
		tools [1] = arrowDown;
		tools [2] = painter.btn;
		GUILayout.Toolbar (0, tools, GUILayout.Height(40),GUILayout.Width (110));
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Space (15);
		GUILayout.Toggle (largeCursor, "Large Cursor");
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();

		GUILayout.Space (5);
		GUILayout.BeginVertical ();
		GUILayout.Space (15);
		GUILayout.Button (podImage, GUILayout.Width (50), GUILayout.Height(50));
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}
	void LevelLoad(int windowID){

		Rect loadViewRect = new Rect (0, 15, 0, 20 * levels.Length);
		Rect loadWindowRect = new Rect(0,15,120,120);

		loadPosition = GUI.BeginScrollView(loadWindowRect, loadPosition, loadViewRect);

		foreach(string level in levels){
			if(GUILayout.Button (level, GUILayout.Width(90))){
				levelName = level;
				//levelBtn = !levelBtn;
				loadButton = !loadButton;
				Load (); //should pass in what to load
			}
		}

		GUI.EndScrollView ();

	}
	void Update(){
		if(c.Count > 0){
			foreach (HexData hex in hexGraph) {
				if(c.Contains (hex.tile.GetComponent<MeshCollider>())){
					hex.mat = hex.tile.gameObject.GetComponent<MeshRenderer>().material;
					var name = hex.mat.name.Remove(hex.mat.name.Length - (" (Instance)".Length));
					//Debug.Log (hex.mat.name.Remove(hex.mat.name.Length - (" (Instance)".Length))+"   "+hexGroup.hexes[hex.matIndex].mat.name);
					if(name != hexGroup.hexes[hex.matIndex].mat.name){
						//Debug.Log ("mismatrch");
						for(int i = 0; i < hexGroup.hexes.GetLength (0); i++){
							if(name == hexGroup.hexes[i].mat.name){
								//Debug.Log (hex.mat.name+" "+hexGroup.hexes[i].mat.name+""+hex.tile.gameObject.GetComponent<MeshRenderer>().material+"  "+i);
								hex.matIndex = i;
								break;
							}
						}
					}
					hex.position = new Vector3( hex.tile.transform.position.x, hex.tile.transform.position.y, hex.tile.transform.position.z);
					hex.oreFlag = hex.tile.GetComponent<Animator>().GetInteger ("OreState");
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.I)) {
			raiseGround = !raiseGround;
			lowerGround = false;
			paintTool = false;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			lowerGround = !lowerGround;
			raiseGround = false;
			paintTool = false;
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			paintTool = !paintTool;
			raiseGround = false;
			lowerGround = false;
		}
		if (Input.GetMouseButton (0)) {
			if (orePaintTool || raiseGround || lowerGround || paintTool){
				RaycastHit ray;
				if(largeCursor){
					paintRadius = 1f;
				}
				else{
					paintRadius = 0.5f;
				}
				if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out ray)){
					if (ray.collider.tag == "Floor"){
						foreach(Collider collision in Physics.OverlapSphere(ray.collider.transform.position, paintRadius)){
							if (collision.tag == "Floor"){
								if (!(c.Contains (collision))){
									if (paintTool){
										if (!collision.GetComponent<MeshRenderer>().material.name.Contains (painter.mat.name)){
											//Debug.Log (painter.mat);
											collision.GetComponent<MeshRenderer>().material = painter.mat;
											float yRot = UnityEngine.Random.Range (0,7) * 60f;
											float xRot = UnityEngine.Random.Range (1,3) * 180f;
											collision.transform.RotateAround (collision.transform.position, Vector3.right, xRot);
											collision.transform.RotateAround (collision.transform.position, Vector3.up, yRot);
										}
									}
									if(lowerGround){
										Collider[] hexes = Physics.OverlapSphere (collision.transform.position, 1f);
										bool move = true;
										foreach(Collider hex in hexes){
											if (hex.transform.position.y > collision.transform.position.y + 0.75f){
												move = false;
											}
										}
										if (move == true){
											collision.transform.position -= new Vector3(0f, 0.25f, 0f);
										}
									}
									if(raiseGround){
										Collider[] hexes = Physics.OverlapSphere (collision.transform.position, 1f);
										bool move = true;
										foreach(Collider hex in hexes){
											if (hex.transform.position.y < collision.transform.position.y - 0.75f){
												move = false;
											}
										}
										if (move == true){
											collision.transform.position += new Vector3(0f, 0.25f, 0f);
										}
									}
									if(orePaintTool){
										collision.GetComponent<Animator>().SetInteger ("OreState", orePaint);
										Debug.Log (collision.GetComponentInParent<Animator>().GetInteger("OreState"));
									}
									c.Add (collision);
								}
							}
						}
					}
				}
			}
			else{
				
			}
		}
		if (Input.GetMouseButtonUp (0)){
			//painterBtn = false;
			//levelBtn = false;
			c.Clear ();
		}
	}

	public bool IsOdd(int num){
		//Debug.Log ("IsOdd: " + num);
		if (num == 0) {
			return false;
		}
		else{
			if (num % 2 == 0){
				return false;
			}
			else{
				return true;
			}
		}
	}

	public HexData[,] ResizeMD(HexData[,] hD, int h, int w){
		HexData[,] newHexGraph = new HexData[h, w];
		//Debug.Log ("New Size: " + newHexGraph.GetLength (0) + ", " + newHexGraph.GetLength (1)+"\nOld Size: "+hexGraph.GetLength(0)+", "+hexGraph.GetLength(1));

		int hG0 = hD.GetLength (0);
		int hG1 = hD.GetLength (1);
		int nHG0 = newHexGraph.GetLength (0);
		int nHG1 = newHexGraph.GetLength (1);
		int iLength = Math.Max (hG0, nHG0);
		int kLength = Math.Max (hG1, nHG1);

		for (int i = 0; i < iLength; i++) {
			for (int k = 0; k < kLength; k++){
				//Debug.Log (i+"  :  "+k);
				if(i < hG0 && i < nHG0 && k < hG1 && k < nHG1){
					newHexGraph[i, k] = hD[i, k];
					//Debug.Log ("Keep: "+i+":"+k+" : ");//+newHexGraph[i, k].tile.name);//.mat);
				}
				else{
					if(i < hG0 && k < hG1){
						//Debug.Log ("Destroy: "+i+":"+k+" : "+hD[i,k].tile.name);
						Destroy (hD[i, k].tile);
					}
				}
			}
		}
		//Debug.Log (newHexGraph.GetLength (0) + "  " + newHexGraph.GetLength (1));
		return hD = newHexGraph;
	}
	public void CreateHex(int h, int w, int matIndex, Vector3 pos = new Vector3()){
		float shift;
		if (IsOdd (h)){
			shift = rowShift;
		}
		else{
			shift = 0f;
		}
		var mat = hexGroup.hexes [matIndex].mat;

		hexGraph [h, w] = new HexData ();
		hexGraph [h, w].tile = Instantiate (hex) as GameObject;
		var tile = hexGraph[h, w].tile;
		tile.transform.parent = this.transform;
		//Debug.Log (pos);
		if (pos != new Vector3()) {
			tile.transform.position = pos;		
		}
		else{
			tile.transform.position = new Vector3((w * 1.0f) + shift, 0f, (h * 0.87f));
		}
		tile.gameObject.GetComponent<MeshRenderer>().material = mat;
		//hexGraph[h, w].mat = mat;
		hexGraph[h, w].matIndex = matIndex;
		hexGraph[h, w].position = tile.transform.position;
		hexGraph[h, w].rotation = tile.transform.rotation;
	}
	void LoadMap(HexSave[,] hS){
		height = hS.GetLength (0);
		width = hS.GetLength (1);
		hexGraph = ResizeMD (hexGraph, height, width);
		Vector3 pos;

		for (int h = 0; h < height; h++) {
			for(int w = 0; w < width; w++){
				//Debug.Log (h+", "+w+":"+hS[h,w].matIndex+", "+hexGroup.hexes[hS[h,w].matIndex].mat);
				if(hexGraph[h,w] == null){
					pos = new Vector3(hS[h,w].posX, hS[h,w].posY, hS[h,w].posZ);
					CreateHex (h, w, hS[h,w].matIndex, pos);
				}
				else{
					hexGraph[h,w].tile.transform.position = new Vector3(hS[h,w].posX, hS[h,w].posY, hS[h,w].posZ);
					hexGraph[h,w].matIndex = hS[h,w].matIndex;
					hexGraph[h,w].tile.gameObject.GetComponent<MeshRenderer>().material = hexGroup.hexes[hS[h,w].matIndex].mat;
				}
				hexGraph[h,w].oreFlag = hS[h,w].oreFlag;
				hexGraph[h,w].tile.GetComponent<Animator>().SetInteger ("OreState", hexGraph[h,w].oreFlag);
				//Debug.Log (h+", "+w+": "+hexGraph[h,w].oreFlag);
			}		
		}
		GraphMap ();
	}
	public void GraphMap(){
		hexGraph = ResizeMD (hexGraph, height, width);
		//Debug.Log ("Lengths: " + hexGraph.GetLength (0) + ", " + hexGraph.GetLength (1));

		if (graph == null) {
			graph = GameControlScript.control.graph;
		}
		graph.width = width * 2;
		graph.depth = Convert.ToInt32 ((height * 2) * 0.87);
		graph.center = new Vector3((graph.width / 2) * graph.nodeSize, 0, (graph.depth / 2) * graph.nodeSize);
		
		graph.UpdateSizeFromWidthDepth();

		int columns = width;
		int col = 0;
		int c = 0;
		int rows = height;
		int row = 0;
		int r = 0;

		for (int a = 0; a < ((columns * rows)); a++) {
			//int c = col;
			//int r = row;

			if(hexGraph[r, c] == null){
				CreateHex (r, c, painterIndex);
			}

			if (r == 0 || c == columns - 1){
				if (row < rows - 1){
					//Debug.Log ("row++");
					row++;
					r = row;
					c = col;
				}
				else if (col < columns - 1){
					//Debug.Log ("col++");
					col++;
					r = row;
					c = col;
				}
			}
			else if (r > 0 && c < columns){
				//Debug.Log ("r- c+");
				r--;
				c++;
			}
		}

		if (width > 9 || height > 9) {
			//GameObject.Find ("Main Camera").GetComponent<Camera>().
		}
		AstarPath.active.Scan ();
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		//string fName = Path.GetFileNameWithoutExtension (EditorApplication.currentScene);
		if (File.Exists (Application.persistentDataPath + "/" + levelName + ".hex")) {
			file = File.Open (Application.persistentDataPath + "/" + levelName + ".hex", FileMode.Open);
		} 
		else {
			file = File.Create (Application.persistentDataPath + "/" + levelName + ".hex");
		}
		//Debug.Log (hexGraph.GetLength (0) + "  Lengths  " + hexGraph.GetLength (1));
		HexSave[,] hexSave = new HexSave[hexGraph.GetLength (0), hexGraph.GetLength (1)];
		for (int i = 0; i < hexGraph.GetLength (0); i++) {
			for (int k = 0; k < hexGraph.GetLength (1); k++){
				HexSave hS = new HexSave();
				hS.matIndex = hexGraph[i,k].matIndex;
				//Debug.Log (hS.matIndex+"  :  "+hexGraph[i,k].matIndex);
				hS.posX = hexGraph[i,k].position.x;
				hS.posY = hexGraph[i,k].position.y;
				//Debug.Log (hS.posY);
				hS.posZ = hexGraph[i,k].position.z;
				hS.oreFlag = hexGraph[i, k].oreFlag;
				hexSave[i,k] = hS;
			}
		}
		bf.Serialize (file, hexSave);
		file.Close ();
		Debug.Log (levelName + " Saved!");
		/*
		SpawnList data = new SpawnList ();
		foreach (UnitCallScript unit in unitList) {
			SpawnData spawn = new SpawnData();
			spawn.callTime = unit.callTime;
			spawn.groupNum = unit.groupNum;
			spawn.sinceLast = unit.sinceLast;
			spawn.unitName = unit.unit.name;
			
			data.spawnData.Add(spawn);
		}
		
		bf.Serialize (file, data);
		file.Close ();
		Debug.Log ("Saved " + file.Name);
		*/
	}
	
	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		if (File.Exists (Application.persistentDataPath + "/" + levelName + ".hex")) {
			FileStream file = File.Open (Application.persistentDataPath + "/" + levelName + ".hex", FileMode.Open);
			HexSave[,] data = (HexSave[,])bf.Deserialize (file);
			file.Close();

			//Debug.Log ("Array I: "+data.GetLength (0)+" Array K: "+data.GetLength (1)+"; "+data[0,0].matIndex);
			//HexData[,] hD = new HexData[data.GetLength (0), data.GetLength (1)];
			//for(int r = 0; r < hD.GetLength (0); r++){
			//	for (int c = 0; c < hD.GetLength (1); c++){
			//		hD[r, c].matIndex = data[r,c].matIndex;
			//		hD[r,c].mat = hexGroup.hexes[data[r,c].matIndex].mat;
			//	}
			//}
			LoadMap (data);
		}
	}
}
