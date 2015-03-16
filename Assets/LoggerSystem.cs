using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class LoggerSystem : MonoBehaviour {

	bool logged = false;

	// Use this for initialization
	void Start () {
	
	}

		
	// Update is called once per frame
	void Update () {
		if (!logged) {
			logged = true;
			var deserialized = BoltPointsGenerator.deserialized;
			if(deserialized != 0 ){
				Debug.Log(string.Format("{0} deserialized {1} files", typeof(BoltPointsGenerator).Name, deserialized));
			}

			var serialized = BoltPointsGenerator.serialized;
			if(serialized != 0 ){
				Debug.Log(string.Format("{0} serialized {1} files", typeof(BoltPointsGenerator).Name, serialized));
			}

		}
	}
}