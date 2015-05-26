using UnityEngine;
using System.Collections;

public class Singleton<Instance> : MonoBehaviour where Instance : Singleton<Instance> {
	public static Instance instance;
	
	public virtual void Awake() {
		//DontDestroyOnLoad(gameObject);
		if (instance)
		{
			Destroy (gameObject);
		}
		else
		{
			instance = this as Instance;
			DontDestroyOnLoad (gameObject);
		}
		
	}
}