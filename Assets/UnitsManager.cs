using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;

public class UnitsManager : MonoBehaviour {

	public List<Action> actions;

	public enum LaserMachineType {
		LONG_DURATION,
		MEDIUM_DURATION,
		SHORT_DURATION,

	}

	public float shortDurtation = 25f;
	public float mediumDuration = 35f;
	public float longDuration = 50f;


	// Use this for initialization
	void Start () {
	


		Defer.DeferAction (() => {
		//	SpawnLaserMachine (LaserMachineType.SHORT_DURATION);
		}, 1f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void SpawnLaserMachine(LaserMachineType machineType){

		float duration = 0;

		switch (machineType) {
		case LaserMachineType.LONG_DURATION:
			duration = longDuration;
			break;
		case LaserMachineType.MEDIUM_DURATION:
			duration = mediumDuration;
			break;
		case LaserMachineType.SHORT_DURATION:
			duration = shortDurtation;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}



		var laserMachineTemplate = Templates.GetTemplate ("LaserShootingMachine");

		var newLaserMachine = Instantiate (laserMachineTemplate);

		newLaserMachine.SetActive (true);

		Defer.DeferAction (() => {
			newLaserMachine.SendMessage("Remove");
		}, duration);




	}


}
