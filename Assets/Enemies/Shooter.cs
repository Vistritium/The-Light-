using UnityEngine;
using System.Collections;
using AssemblyCSharp;

//[RequireComponent(typeof(TargetProviderProvider), typeof(SpeedProviderProvider), typeof(FiringTimeProvider))]
public class Shooter : MonoBehaviour {

	public GameObject bullet;
	private TargetProviderProvider targetProviderProvider;
	private SpeedProviderProvider speedProviderProvider;
	private FiringTimeProvider firingTimeProvider;
	
//	public void Initialize(GameObject bullet, TargetProvider targetProviderProvider, SpeedProvider speedProviderProvider, FiringTimeProvider firingTimeProvider){
//		this.bullet = bullet;
//		this.targetProviderProvider = targetProviderProvider;
//		this.speedProviderProvider = speedProviderProvider;
//		this.firingTimeProvider = firingTimeProvider;
//	}

	void Awake(){
		UpdateReferences ();
	}


	void UpdateReferences(){
		this.targetProviderProvider = GetComponent<TargetProviderProvider>();
		this.speedProviderProvider = GetComponent<SpeedProviderProvider>();
		this.firingTimeProvider = GetComponent<FiringTimeProvider>();
	}

	void Update(){
		if (bullet != null && targetProviderProvider != null && speedProviderProvider != null && firingTimeProvider != null) {
			if(firingTimeProvider.ShouldFire()){
				var objectToFire = Instantiate(bullet);
				objectToFire.transform.position = this.transform.position;
				targetProviderProvider.ProvideTargetProvider(objectToFire);
				speedProviderProvider.ProvideSpeedProvider(objectToFire);
				objectToFire.SetActive(true);
			}
		} 


	}



}
