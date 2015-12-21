using UnityEngine;
using System.Collections;

public class SpellBook : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Cast (string gestureClass, PDollarGestureRecognizer.Gesture gesture, LineRenderer line) {
		Debug.Log ("Cast " + gestureClass + "!");
		switch (gestureClass) 
		{
		case("Circle"):
			float maxX = float.MinValue;
			float minX = float.MaxValue;
			float maxY = float.MinValue;
			float minY = float.MaxValue;
			foreach(PDollarGestureRecognizer.Point point in gesture.Points){
				if(point.X > maxX){
					maxX = point.X;
				}
				if(point.X < minX){
					minX = point.X;
				}
				if(point.Y > maxY){
					maxY = point.Y;
				}
				if(point.Y < minY){
					minY = point.Y;
				}
			}
			Vector3 center = new Vector3((maxX + minX)/2, 1, (maxY + minY)/2);
			line.SetVertexCount (33);
			line.SetPosition (32, center);
			Debug.Log (center);
			break;
		case("line"):
			break;
		case("X"):
			break;
		}
	}
}
