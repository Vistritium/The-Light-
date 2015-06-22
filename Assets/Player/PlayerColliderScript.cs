using UnityEngine;
using System.Collections;
//using PlayerControlScript;

public class PlayerColliderScript : MonoBehaviour {

	private PlayerControlScript script;

	void Start ()
	{
		script = transform.parent.gameObject.GetComponent<PlayerControlScript> ();
	}

	void OnTriggerEnter (Collider other)
	{
		//script.ReceiveHitInfo ("painis");

		// If the player is in AudiMode, blast hit boxes into oblivion:
		if (script.state == PlayerControlScript.StateTypes.powered && other.gameObject.GetComponent<Rigidbody>())
		{
			GameObject temp = GameObject.Find("Player");
			other.gameObject.GetComponent<Rigidbody>().constraints = ~(RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation);

			Vector3 newSpeed = other.gameObject.transform.position - transform.parent.position - (transform.parent.forward * script.carLength);
			newSpeed.y = 0;
			newSpeed.Normalize();
			newSpeed *= temp.GetComponent<CameraTargetScript>().speed * 4;
			newSpeed += Vector3.up * Random.Range(5f, 10f);

			other.gameObject.GetComponent<Rigidbody>().velocity = newSpeed;

			//other.gameObject.GetComponent<Rigidbody>().velocity = (other.gameObject.transform.position - transform.parent.position).normalized * temp.GetComponent<CameraTargetScript>().speed * 4 + Vector3.up * Random.Range(5f, 10f);
		}

		// Also, react accordingly to other hit objects:
		if (other.gameObject.tag == "DashPad")
		{
			//script.enteredTriggerTag = other.gameObject.tag;
			script.ReceiveHitInfo (other.gameObject.tag);

		}
		else if (other.gameObject.tag == "Boundary")
		{
			// Do whole lot of nothing
		}
		else if (other.gameObject.tag == "Ring")
		{
			//script.enteredTriggerTag = other.gameObject.tag;
			if (other.gameObject)
			{
				script.ReceiveHitInfo (other.gameObject.tag);
				Destroy(other.gameObject);
			}
		}
		// If you hit regular box, inform the player, from what direction did the hit come from:
		else
		{
			//if (script.enteredTriggerTag != "MiddlePlayerCollider")
			//	script.enteredTriggerTag = gameObject.tag;

			script.ReceiveHitInfo (gameObject.tag);
			//Debug.Log(other.gameObject.name);
		}
	}
}
