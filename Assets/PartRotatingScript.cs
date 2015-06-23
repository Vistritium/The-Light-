using UnityEngine;
using System.Collections;

public class PartRotatingScript : MonoBehaviour {

	public GameObject[] parts;
	public int[] axises;
	public float[] multipliers;
	public Vector3[] baseRotations;

	private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;

		for (int i = 0; i < parts.Length; i++)
		{
			Vector3 temp = baseRotations[i];
			temp[axises[i]] += timer * multipliers[i];

			parts[i].transform.localRotation = Quaternion.Euler(temp);
		}
	}
}
