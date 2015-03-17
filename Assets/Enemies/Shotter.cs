using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ShotAtPlayer : MonoBehaviour {

	private GameObject bullet;
	private TargetProviderProvider targetProviderProvider;
	private SpeedProviderProvider speedProviderProvider;
	private FiringTimeProvider firingTimeProvider;
	
	public void Initialize(GameObject bullet, TargetProviderProvider targetProviderProvider, SpeedProviderProvider speedProviderProvider, FiringTimeProvider firingTimeProvider){
		this.bullet = bullet;
		this.targetProviderProvider = targetProviderProvider;
		this.speedProviderProvider = speedProviderProvider;
		this.firingTimeProvider = firingTimeProvider;
	}

	void Update(){
		if (bullet != null && targetProviderProvider != null && speedProviderProvider != null && firingTimeProvider != null) {
			if(firingTimeProvider.ShouldFire()){
				var objectToFire = Instantiate(bullet);
				


			}
		} else {
			Debug.Log("Something is null");
		}


	}



}
