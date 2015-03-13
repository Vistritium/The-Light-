using UnityEngine;
using System.Collections;

public class LightingEffect : MonoBehaviour {

	public Vector3 from = new Vector3(0, -10, 0);
	public Vector3 to = new Vector3(0, 10, 0);

	private Vector3 nextTarget;

	private GameObject pointObject;

	// Use this for initialization
	void Start () {
		pointObject = new GameObject ("pointObject");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
