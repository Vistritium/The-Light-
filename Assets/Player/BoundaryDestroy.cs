using UnityEngine;
using System.Collections;

public class BoundaryDestroy : MonoBehaviour {
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
