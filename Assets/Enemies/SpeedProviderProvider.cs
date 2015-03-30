
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class SpeedProviderProvider : MonoBehaviour
	{

		public float speed = 1f;

		public virtual void ProvideSpeedProvider(GameObject gameObject){
			gameObject.AddComponent<SpeedProvider> ();
			gameObject.GetComponent<SpeedProvider> ().speed = speed;
		}


	}
}

