using UnityEngine;
using System.Collections;
//using PlayerControlScript;

public class PlayerColliderScript : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			PlayerControlScript temp = transform.parent.gameObject.GetComponent<PlayerControlScript> ();
			temp.enteredTriggerTag = gameObject.tag;
			//Debug.Log(other.gameObject.name);
		}
	}
}
