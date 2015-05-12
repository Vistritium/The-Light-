using UnityEngine;
using System.Collections;
using AssemblyCSharp;

[RequireComponent(typeof(FiringTimeProvider))]
public class LaserShootingComponent : MonoBehaviour {

	ElectricLaser electricLaser;

	Vector3 localPlaceFrom;
	Vector3 localPlaceTo;

	GameObject player;

	GameObject sparks;

	bool firing = false;

	float currentShootingTime = 0;

	FiringTimeProvider firingTimeProvider; 

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Audi");
		firingTimeProvider = GetComponent<FiringTimeProvider> ();
		electricLaser = ElectricLaser.GetElectricLaser ();

		electricLaser.gameObject.SetActive (false);

		sparks = Templates.GetTemplate ("Sparks");
		sparks = Instantiate (sparks);
		sparks.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {



		if (!firing && firingTimeProvider.ShouldFire ()) {
			Debug.Log("Firing");
			firing = true;

			var from = player.transform.position + Vector3.forward * 60f + Random.Range(-5, 5) * Vector3.right;
			var to = player.transform.position + Random.Range(-5, 5) * Vector3.right + Vector3.back * 10f;


			localPlaceFrom = this.transform.InverseTransformPoint(from);
			localPlaceTo = this.transform.InverseTransformPoint(to);

			electricLaser.gameObject.SetActive (true);
			electricLaser.UpdatePositions();

			currentShootingTime = 0;
			sparks.SetActive(true);

		}

		if (firing) {

			currentShootingTime += Time.deltaTime * 0.5f;



			var currentPoint = this.transform.TransformPoint(Vector3.Lerp(localPlaceFrom, localPlaceTo, currentShootingTime));

			electricLaser.from = this.transform.position;
			electricLaser.to = currentPoint;

			sparks.transform.position = currentPoint;


			if(currentShootingTime >= 1){
				firing = false;
				sparks.SetActive(false);
				electricLaser.from = Vector3.zero;
				electricLaser.to = Vector3.zero;

				electricLaser.gameObject.SetActive (false);


			}

		}


	}
}
