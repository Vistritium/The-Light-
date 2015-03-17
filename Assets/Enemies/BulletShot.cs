using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BulletShot : MonoBehaviour {

	public Vector3 target = Vector3.zero;
	public SpeedProvider speedProvider;


	private class SimpleSpeedProvider : SpeedProvider {
		public float GetSpeed(){
			return 5f;
		}
	}

	// Use this for initialization
	void Start () {
		if (speedProvider == null) {
			speedProvider = new SimpleSpeedProvider();
		}
		
	}

	void SetSpeedProvider(SpeedProvider provider){
		this.speedProvider = provider;
	}

	void SetTarget(Vector3 target){
		this.target = target;
	}
	
	// Update is called once per frame
	void Update () {
		var toVector = (target - this.transform.position);
		toVector.Normalize ();

		this.transform.Translate (toVector * Time.deltaTime * speedProvider.GetSpeed());
	}


	void OnTriggerEnter(Collider other)
	{
		Destroy (this.gameObject);
	}



}
