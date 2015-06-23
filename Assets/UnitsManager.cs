using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;

public class UnitsManager : MonoBehaviour {

	public List<Action> actions;

	public enum MachineDuration {
		LONG_DURATION,
		MEDIUM_DURATION,
		SHORT_DURATION,

	}

	public float shortDurtation = 15;
	public float mediumDuration = 35f;
	public float longDuration = 50f;


	// Use this for initialization
	void Start () {
	

		/*
		Defer.DeferAction (() => {
			SpawnLaserBallShooterMachine(MachineDuration.SHORT_DURATION);
		}, 1f);
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public GameObject SpawnLaserMachine(MachineDuration machineDuration){

		float duration = 0;

		switch (machineDuration) {
		case MachineDuration.LONG_DURATION:
			duration = longDuration;
			break;
		case MachineDuration.MEDIUM_DURATION:
			duration = mediumDuration;
			break;
		case MachineDuration.SHORT_DURATION:
			duration = shortDurtation;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}



		var laserMachineTemplate = Templates.GetTemplate ("LaserShootingMachine");

		var newLaserMachine = Instantiate (laserMachineTemplate);

		newLaserMachine.SetActive (true);

		// ZIS GOT CHANGED:
		//Defer.DeferAction (() => {
			//newLaserMachine.SendMessage("Remove");
		//}, duration);



		return newLaserMachine;
	}


    public GameObject SpawnLaserBallShooterMachine(MachineDuration machineDuration)
    {

        float duration = 0;

        switch (machineDuration)
        {
            case MachineDuration.LONG_DURATION:
                duration = longDuration;
                break;
            case MachineDuration.MEDIUM_DURATION:
                duration = mediumDuration;
                break;
            case MachineDuration.SHORT_DURATION:
                duration = shortDurtation;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }



        var machineTemplate = Templates.GetTemplate("BallsShootingMachine");

        var newShootingMachine = Instantiate(machineTemplate);

        newShootingMachine.SetActive(true);

        // ZIS GOT CHANGED:
        Defer.DeferAction (() => {
            Debug.Log("sending remove");
            newShootingMachine.SendMessage("Remove");
        }, duration);



        return newShootingMachine;
    }

}
