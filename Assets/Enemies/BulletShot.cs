using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BulletShot : MonoBehaviour {

	public TargetProvider targetProvider;
	public SpeedProvider speedProvider;
	public GameObject boundary;
	

	// Use this for initialization
	void Start () {

		this.speedProvider = GetComponent<SpeedProvider> ();
		if (speedProvider == null) {
			this.gameObject.AddComponent<SpeedProvider>();
			this.speedProvider = GetComponent<SpeedProvider> ();
		}

		this.targetProvider = GetComponent<TargetProvider> ();
		if (this.targetProvider == null) {
			this.gameObject.AddComponent<TargetProvider>();
			this.targetProvider = GetComponent<TargetProvider>();
		}


	}

	
	// Update is called once per frame
	void Update () {
		var toVector = (targetProvider.GetTarget() - this.transform.position);
		if (toVector.magnitude < 0.1) {
			Destroy(this.gameObject);
		} else {
			toVector.Normalize ();
			this.transform.Translate (toVector * Time.deltaTime * speedProvider.GetSpeed());
		}

	}

	


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != boundary) {
			Destroy (this.gameObject);
		}
	}



}
