using AssemblyCSharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SingleLightingCone : MonoBehaviour {

    public int iterations = 8;
    public int numberOfGeneratedFlavors = 10;
	public float timeOffset = 0;
	public float timeOfBolt = 1f;
	public int flavor = 0;
	public float power = 0.05f;

	private float timeToChange;

	public Vector3 from = new Vector3(0, -10, 0);
	public Vector3 to = new Vector3(0, 10, 0);

    public GameObject lightingObject;

    private List<List<List<Vector3>>> points;

    private int currentIndex = 0;

    private List<GameObject> lightingObjects; 
	private List<GameObject> usedGameObjects;

	private float currentLerp;

	public float GetCurrentLerp(){

		return currentLerp;
	}


	void Awake(){
	//	timeToChange = Time.time + timeOfBolt;
	//	currentLerp = (Time.time - (timeToChange - timeOfBolt)) / timeOfBolt;
	//	Debug.Log ("Current lerp: " + currentLerp);
		
	}


	// Update is called once per frame
	void Update () {

		if (points != null && points.Count > 0) {
			if (Time.time >= timeToChange) {
				timeToChange += timeOfBolt;
				IteratePoints();
			}
			

		}
		currentLerp = (Time.time - (timeToChange - timeOfBolt)) / timeOfBolt;

		if (currentLerp > 1) {
			if(points == null){
				Debug.Log("Points are null");
			}
			Debug.Log(string.Format("Current lerp > 1.lerp: {0}, time:{1}, timeToChange:{2}, timeOfBolt:{3}",currentLerp, Time.time, timeToChange, timeOfBolt)); 
		}

	}


    public void GenerateNew()
    {
        points = new List<List<List<Vector3>>>();
        for (int i = 0; i < numberOfGeneratedFlavors; i++)
        {
			var generatedPoints = BoltPointsGenerator.GeneratePoints(from, to, power, iterations, (i + 1) * ((flavor + 1) * 100));
            generatedPoints.RemoveAt(0);
            points.Add(generatedPoints);
        }

        lightingObjects = new List<GameObject>();

        foreach (var segmentPoints in points[0])
        {
            Vector3? lastPoint = null;

            foreach (Vector3 point in segmentPoints)
            {
                if (lastPoint.HasValue)
                {
                    var generateGameObject = GenerateGameObject(lastPoint.Value, point);
                    lightingObjects.Add(generateGameObject);
                }
                lastPoint = point;
            }
        }

    }

	// Use this for initialization
	void Start ()
	{
		timeToChange = Time.time + timeOffset;
	   // GenerateNew();
       // InvokeRepeating("IteratePoints", 0.3f, 0.5f);

	}

    private GameObject GenerateGameObject(Vector3 from, Vector3 to)
    {
        var generated = Instantiate(lightingObject);
        generated.transform.parent = this.transform;

        reuseGameObject(from, to, generated);

        return generated;
    }


    private void IteratePoints()
    {
        currentIndex = currentIndex + 1;
        if (currentIndex >= points.Count)
        {
            currentIndex = 0;
        }

        int gameObjectIndex = 0;

        foreach (var segmentPoints in points[currentIndex])
        {
            Vector3? lastPoint = null;

            foreach (Vector3 point in segmentPoints)
            {
                if (lastPoint.HasValue)
                {
                    reuseGameObject(lastPoint.Value, point, lightingObjects[gameObjectIndex]);
                    gameObjectIndex = gameObjectIndex + 1;
                }
                lastPoint = point;
            }
        }

    }

    private void reuseGameObject(Vector3 from, Vector3 to, GameObject reuse)
    {
        var middle = Vector3.Lerp(from, to, 0.5f);

        var distance = Vector3.Distance(@from, to);

        reuse.transform.localScale = new Vector3(reuse.transform.localScale.x, reuse.transform.localScale.y, distance);

        reuse.transform.localPosition = middle;

		var fromGlobal = this.transform.TransformPoint (from);
		reuse.transform.LookAt(fromGlobal);
		
		
    }

    


}
