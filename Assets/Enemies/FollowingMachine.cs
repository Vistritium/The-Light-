﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class FollowingMachine : MonoBehaviour {

	private GameObject player;

	public Vector3 displacementFromPlayer =  Vector3.right * 10f + Vector3.up * 3f + Vector3.forward * 3f;

	public Vector3 backDisplacement = Vector3.back * 8f;

	private float backLerpFactor = 0.05f;

	private bool oscilate = false;

	private Oscilator oscilator;

	private Vector3 initialPosition;

	public event Action onPosition;
	
	// Use this for initialization
	void Start () {
		this.player = GameObject.Find ("Player");

		initialPosition = displacementFromPlayer;
		this.transform.localPosition = initialPosition;

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
			if(onPosition != null){
				onPosition.Invoke();
			}
		} else {
			additionalPositionVector = Vector3.forward * oscilator.GetCurrentValue ();
		}

		this.transform.localPosition = initialPosition + additionalPositionVector;
		
	//	var gonnaBePosition = initialPosition + additionalPositionVector;

	//	this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, gonnaBePosition, Time.deltaTime * 0.1f);

	}
}
