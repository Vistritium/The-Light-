using AssemblyCSharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingEffect : MonoBehaviour {

    public int iterations = 8;
    public int numberOfGeneratedFlavors = 10;

	public Vector3 from = new Vector3(0, -10, 0);
	public Vector3 to = new Vector3(0, 10, 0);

    public GameObject lightingObject;

    private List<List<List<Vector3>>> points;

    private int currentIndex = 0;

    private List<GameObject> lightingObjects; 


    private void GenerateNew()
    {
        points = new List<List<List<Vector3>>>();
        for (int i = 0; i < numberOfGeneratedFlavors; i++)
        {
            var generatedPoints = BoltPointsGenerator.GeneratePoints(Vector3.left * 10, Vector3.right * 10, 0.05f, iterations, i);
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
	    GenerateNew();
        InvokeRepeating("IteratePoints", 0.3f, 0.02f);

        foreach (var segmentPoints in points[0])
        {

            foreach (Vector3 point in segmentPoints)
            {
               Debug.Log(point);
            }
        }
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

        reuse.transform.LookAt(Vector3.Scale(from, this.transform.localScale) + this.transform.position);
    }

    

    // Update is called once per frame
	void Update () {
	        
	}
}
