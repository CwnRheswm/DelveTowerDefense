using UnityEngine;
using Pathfinding;
public class Walk : MonoBehaviour {

	private GameHandler handler;
	public GameObject target;
	public Vector3 targetPosition;
	
	public Seeker seeker;
	private CharacterController controller;
	private Body body;
	
	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float baseSpeed;
	public float speed;
	public Movements movement = Movements.small;
	public bool enterTarget = false;

	//movement penalties
	private float[] groundPenalties = new float[5]; 
	/*
	private float basicGr;
	private float brokenGr;
	private float stalagmiteGr;
	private float stalagtiteGr;
	private float swampGr;
	*/

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	
	//The waypoint we are currently moving towards
	public int currentWaypoint = 0;
	
	// Use this for initialization
	void Start () {
		handler = GameHandler.handler;
		body = GetComponent<Body>();
		if (body != null){baseSpeed = body.movementSpeed;}
		targetPosition = target.transform.position;
		//Get a reference to the Seeker component we added earlier
		seeker = transform.GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		GetPenalties ();

		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		//Debug.Log (transform.position);
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
		
	}
	public void OnPathComplete (Path p) {
		//Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
	void GameUpdate(){
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) { //removed >= for >
			//Debug.Log ("End Of Path Reached");
			return;
		}
		GraphNode nearest = (GraphNode)AstarPath.active.GetNearest(transform.position);
		speed = baseSpeed * groundPenalties[nearest.Tag];

		if(enterTarget) {
			Vector3 dir = (targetPosition - transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime;
			if(controller == null){
				transform.position += dir;
			}
			else{
				controller.Move (dir);
			}
		}
		else{

			//Direction to the next waypoint
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime;
			if(controller == null){
				transform.position += dir;
			}
			else{
				controller.Move (dir);
			}
			//Check if we are close enough to the next waypoint
			//If we are, proceed to follow the next waypoint
			if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance && currentWaypoint < path.vectorPath.Count) {
				currentWaypoint++;
				return;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(!handler.IsPaused()){
			GameUpdate();
		}
	}
	public void Repath() {
		if(target != null)
		{
			//Debug.Log(transform.position+" : "+target.transform.position);
			targetPosition = target.transform.position;
			seeker = GetComponent<Seeker>();
			Path p = seeker.GetNewPath (transform.position, target.transform.position);
			seeker.StartPath (p, OnPathComplete);
		}
	}
	public void GetPenalties(){
		//if (body != null)
		//{
			if (movement == Movements.small) 
			{
				groundPenalties[0] = 1f; //0;  BASIC
				seeker.tagPenalties[0] = 1000;
				groundPenalties[1] = 0.6f; //2;  BROKEN
				seeker.tagPenalties[1] = 4000;
				groundPenalties[2] = 0.8f;  //1;  STALAGMITE
				seeker.tagPenalties[2] = 2000;
				groundPenalties[3] = 1f;  //0;  STALAGTITE
				seeker.tagPenalties[3] = 1000;
				groundPenalties[4] = 0.6f;  //2;  SWAMP
				seeker.tagPenalties[4] = 4000;
			}

			if (movement == Movements.large) 
			{
				groundPenalties[0] = 1f;  //0
				seeker.tagPenalties[0] = 1000;
				groundPenalties[1] = 1f;  //0
				seeker.tagPenalties[1] = 1000;
				groundPenalties[2] = 0.6f;  //2
				seeker.tagPenalties[2] = 4000;
				groundPenalties[3] = 0.8f;  //1
				seeker.tagPenalties[3] = 2000;
				groundPenalties[4] = 1f;  //0
				seeker.tagPenalties[4] = 1000;
			}

			if (movement == Movements.seige) 
			{
				groundPenalties[0] = 1f;  //0
				seeker.tagPenalties[0] = 1000;
				groundPenalties[1] = 0.8f;  //1
				seeker.tagPenalties[1] = 2000;
				groundPenalties[2] = 0.4f; //4
				seeker.tagPenalties[2] = 8000;
				groundPenalties[3] = 1f;  //0
				seeker.tagPenalties[3] = 1000;
				groundPenalties[4] = 0.4f;  //4
				seeker.tagPenalties[4] = 8000;
			}

			if (movement == Movements.flying) 
			{
				groundPenalties[0] = 1f;  //0
				seeker.tagPenalties[0] = 1000;
				groundPenalties[1] = 1f;  //0
				seeker.tagPenalties[1] = 1000;
				groundPenalties[2] = 1f;  //0
				seeker.tagPenalties[2] = 1000;
				groundPenalties[3] = 0.4f;  //4
				seeker.tagPenalties[3] = 8000;
				groundPenalties[4] = 1.2f;  //+1
				seeker.tagPenalties[4] = 0;
			}
		//}
	}

}
