using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class TestPoints : MonoBehaviour {

	public GameObject target;

    public int iterations = 8;
    public int numberOfGeneratedFlavors = 10;

	private List<List<List<Vector3>>> points;
    private List<List<Vector3>> currentPoints;
    private int currentIndex = 0;

	// Use this for initialization
	void Start () {
		GenerateNew ();

        InvokeRepeating("IteratePoints", 0.3f, 0.05f);
	    IteratePoints();
	}


	private void GenerateNew(){
        points = new List<List<List<Vector3>>>();
	    for (int i = 0; i < numberOfGeneratedFlavors; i++)
	    {
	        var generatedPoints = BoltPointsGenerator.GeneratePoints(this.transform.position + Vector3.left * 10, this.transform.position + Vector3.right * 10, 0.05f, iterations, i);
	        generatedPoints.RemoveAt(0);
            points.Add(generatedPoints);
	    }
	}

    private void IteratePoints()
    {
        currentIndex = currentIndex + 1;
        if (currentIndex >= points.Count)
        {
            currentIndex = 0;
        }
        currentPoints = points[currentIndex];
    }
	
	// Update is called once per frame
	void Update () {



        foreach (var segmentPoints in currentPoints)
        {
			Vector3? lastPoint = null;

            foreach (Vector3 point in segmentPoints)
            {
				if(lastPoint.HasValue){
					Debug.DrawLine(lastPoint.Value, point);
				}
				lastPoint = point;
			}
		}



	}
}
