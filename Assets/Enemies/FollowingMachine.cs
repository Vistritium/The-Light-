using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FollowingMachine : MonoBehaviour {

	private GameObject player;

	private Vector3 displacementFromPlayer;

	private Vector3 backDisplacement;

	public float backFactor = 3f;

	private float backLerpFactor = 0.05f;

	private bool oscilate = false;

	private Oscilator oscilator;

	private Vector3 initialPosition;
	
	// Use this for initialization
	void Start () {
		this.player = GameObject.Find ("Player");

		displacementFromPlayer = Vector3.right * 10f + Vector3.up * 3f + Vector3.forward * 3f;

		initialPosition = displacementFromPlayer;
		this.transform.localPosition = initialPosition;

		backDisplacement = Vector3.back * 8f;

		this.transform.parent = player.transform;

	}

	private void InitOscilator(){
		this.oscilate = true;
		oscilator =  new Oscilator (-3, 4, 1f);
	}
	
	// Update is called once per frame
	void Update () {

		

		var additionalPositionVector = Vector3.zero;

		if (backLerpFactor < 1.0f) {
			additionalPositionVector = Vector3.Lerp (backDisplacement, Vector3.zero, backLerpFactor);
			this.backLerpFactor += Time.deltaTime * backLerpFactor;
		} else if (!this.oscilate) {
			InitOscilator ();
		} else {
			additionalPositionVector = Vector3.forward * oscilator.GetCurrentValue ();
		}

		this.transform.localPosition = initialPosition + additionalPositionVector;
		

		//var gonnaBePosition = displacementFromPlayer + additionalPositionVector;

		//var gonnaBeDistance = Vector3.Distance (gonnaBePosition, this.transform.position);
	//	if (gonnaBeDistance > 1.8) {
		//	Debug.Log("distance: " + gonnaBeDistance);
	//	}

		//this.transform.position = Vector3.Lerp (this.transform.position, gonnaBePosition, 1f);

	}
}
