using UnityEngine;
using System.Collections;

public class TestLaserSpawner : MonoBehaviour {

	ElectricLaser electricLaser;

	// Use this for initialization
	void Start () {
		this.electricLaser = ElectricLaser.GetElectricLaser ();
	}
	
	// Update is called once per frame
	void Update () {
		electricLaser.from = this.transform.position;
		electricLaser.to = Vector3.zero;
	}
}
