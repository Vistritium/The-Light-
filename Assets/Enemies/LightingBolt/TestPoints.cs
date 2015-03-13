using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class TestPoints : MonoBehaviour {

	public GameObject target;

	private List<List<Vector3>> points;

	// Use this for initialization
	void Start () {
		GenerateNew ();
		

	}


	private void GenerateNew(){

		points = BoltPointsGenerator.GeneratePoints (this.transform.position + Vector3.left * 10, this.transform.position + Vector3.right * 10, 10, 2);
	}
	
	// Update is called once per frame
	void Update () {
	

		if(Input.GetKeyDown(KeyCode.P)){
			GenerateNew();


		}




		foreach (var segmentPoints in points) {
			Vector3? lastPoint = null;

			foreach(Vector3 point in segmentPoints){
				if(lastPoint.HasValue){
					Debug.DrawLine(lastPoint.Value, point);
				}
				lastPoint = point;
			}
		}



	}
}
