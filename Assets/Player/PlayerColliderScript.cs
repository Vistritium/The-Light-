using UnityEngine;
using System.Collections;
//using PlayerControlScript;

public class PlayerColliderScript : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "DashPad")
		{
			PlayerControlScript temp = transform.parent.gameObject.GetComponent<PlayerControlScript> ();
			temp.enteredTriggerTag = other.gameObject.tag;

		}
		else if (other.gameObject.tag == "Boundary")
		{
			// Do whole lot of nothing
		}
		else
		{
			PlayerControlScript temp = transform.parent.gameObject.GetComponent<PlayerControlScript> ();
			if (temp.enteredTriggerTag != "MiddlePlayerCollider")
				temp.enteredTriggerTag = gameObject.tag;
			//Debug.Log(other.gameObject.name);
		}
	}
}
