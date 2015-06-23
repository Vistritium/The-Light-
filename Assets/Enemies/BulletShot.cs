using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BulletShot : MonoBehaviour {

	public TargetProvider targetProvider;
	public SpeedProvider speedProvider;
	public GameObject boundary;

	bool over = false;
	

	// Use this for initialization
	void Start () {
        this.speedProvider = GetComponent<SpeedProvider>();
        this.targetProvider = GetComponent<TargetProvider>();

/*
		this.speedProvider = GetComponent<SpeedProvider> ();
		if (speedProvider == null) {
			this.gameObject.AddComponent<SpeedProvider>();
			this.speedProvider = GetComponent<SpeedProvider> ();
		}

		this.targetProvider = GetComponent<TargetProvider> ();
		if (this.targetProvider == null) {
			this.gameObject.AddComponent<TargetProvider>();
			this.targetProvider = GetComponent<TargetProvider>();
		}d
*/


	}

	void collision(){
		var bulletCollisionHandler = GetComponent<BulletCollisionHandler> ();

		if (bulletCollisionHandler == null) {
			Debug.Log("Destroying whole bulllet");
			Destroy (this.gameObject);
		} else {
			Debug.Log("Destroying only component");
			over = true;
			Destroy(this);
		}


	}

	
	// Update is called once per frame
	void Update () {

		if (!over) {
			var target = targetProvider.GetTarget();
			
			var toVector = (target - this.transform.position);
			if (toVector.magnitude < 1) {
				collision();
			} else {
				
				var normalized = toVector.normalized;
				
				var speed = speedProvider.GetSpeed();
				
				this.transform.position = this.transform.position + normalized * Time.deltaTime * speed;
			}
		}



	}

	


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != boundary) {
			collision();

		}
	}



}
