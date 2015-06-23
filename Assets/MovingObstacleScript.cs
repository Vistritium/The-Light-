using UnityEngine;
using System.Collections;

public class MovingObstacleScript : MonoBehaviour {

	public float timeToMove = 1;
	public float activationDistance = 80;
	private bool active = false;
	private float timer = 0;
	private GameObject player;

	public Vector3 startPos;
	public Vector3 endPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		//endPos = transform.position + new Vector3 (6, 0, 0);

		player = GameObject.Find ("Audi");
	}
	
	// Update is called once per frame
	void Update () {
		if (player && active == false)
		{
			if (transform.position.z - player.transform.position.z <= activationDistance)
				active = true;
		}

		if (active == true && timer <= timeToMove)
		{
			float part;

			timer += Time.deltaTime;
			if (timer > timeToMove)
			{
				part = 1;
			}
			else
			{
				part = timer / timeToMove;
			}


			transform.position = ((endPos - startPos) * part) + startPos;
		}
	}
}
