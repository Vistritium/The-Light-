using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour {

	public GameObject target = null;

	public float distance = 1;
	public float height = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(0, height, target.transform.position[2] - distance);
	}
}
