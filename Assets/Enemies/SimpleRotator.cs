using UnityEngine;
using System.Collections;

public class SimpleRotator : MonoBehaviour {

	public float speed = 1f;
	public Vector3 axis = Vector3.up;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround (axis, Time.deltaTime * speed);
	}
}
