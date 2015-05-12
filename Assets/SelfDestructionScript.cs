using UnityEngine;
using System.Collections;

public class SelfDestructionScript : MonoBehaviour {

	public float countdown = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;

		if (countdown <= 0)
			Destroy (this);
	}
}
