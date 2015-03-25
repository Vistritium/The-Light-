using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {

	public float forwardSpeed = 3;
	public float maxForwardSpeed = 10;
	public float forwardAccel = 0.1f;

	public float turnSpeed = 1;
	public float turnAccel = 1;
	public float turnDampen = 3;

	public float dashLocalSpeed = 10;
	public float dashDampen = 10;

	public float dashDistance = 0;

	public float hitForce = 10;
	public float hitDamage = 6;
	public float hitStunDuration = 1;

	public float stunTimer = 0;

	public float hp = 10;
	public float hpMax = 10;
	public float hpRegen = 5;

	public float rotationForce = 5;
	public float rotationStart = 10;
	public float rotation = 0;
	public float rotationSpeed = 0;

	public string enteredTriggerTag = "";


	private int turnPressed = 0;

	public enum StateTypes{
		normal,
		stunned
	};

	public int dashing = 0;

	public StateTypes state = StateTypes.normal;

	private GameObject cameraTarget;

	// Use this for initialization
	void Start () {
		//cameraTarget = GameObject.Find ("Player");
		cameraTarget = transform.parent.gameObject;

		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, forwardSpeed);

		transform.position = cameraTarget.transform.position;
	}


	
	// Update is called once per frame
	void Update () {

		#region Timers and States

		if (hp > 0)
		{
			hp += hpRegen * Time.deltaTime;
			if (hp > hpMax)
				hp = hpMax;
		}

		if (stunTimer > 0)
		{
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0)
			{
				stunTimer = 0;
				state = StateTypes.normal;
			}
		}

		// Rotate the car forward if it hit something:
		rotation += rotationSpeed * Time.deltaTime;
		rotationSpeed -= rotationForce * Time.deltaTime;

		if (rotation < 0)
		{
			rotation = 0;
			rotationSpeed = 0;
		}

		var temp = transform.rotation;
		temp.z = rotation;
		temp.y = 180;
		transform.rotation = temp;

		var temp2 = transform.position;
		temp2.y = transform.rotation.z * 5;
		transform.position = temp2;


		#endregion

		#region Speeds Management

		turnPressed = 0;

		// Since you can't change speedx, speedy or speedz directly, make a temp variable to operate on:
		Vector3 tempSpeed = GetComponent<Rigidbody>().velocity;

		// React accordingly if you hit something.
		if (enteredTriggerTag != "" && state != StateTypes.stunned)
		{
			// If you touched DashPad:
			if (enteredTriggerTag == "DashPad")
			{
				if (state == StateTypes.normal)
				{
					dashing = 1;
					tempSpeed[2] = dashLocalSpeed;

					cameraTarget.GetComponent<CameraTargetScript> ().StartDashing();
				}
			}
			// If you hit a wall, check, which side and subtract hp:
			else
			{
				if (enteredTriggerTag == "MiddlePlayerCollider")
				{
					hp = 0;
					cameraTarget.GetComponent<CameraTargetScript> ().Stop();

					rotationSpeed = rotationStart;
				}
				else if (enteredTriggerTag == "LeftPlayerCollider")
				{
					tempSpeed[0] = hitForce;
				}
				else if (enteredTriggerTag == "RightPlayerCollider")
				{
					tempSpeed[0] = -hitForce;
				}

				state = StateTypes.stunned;
				stunTimer = hitStunDuration;

				hp -= hitDamage;
			}
		}

		enteredTriggerTag = "";

		// Calculate input:
		if (hp > 0)
		{
			if (Input.GetKey (KeyCode.A) && state != StateTypes.stunned)
			{
				tempSpeed [0] -= turnAccel * Time.deltaTime;
				turnPressed = 1;
			}
			else if (Input.GetKey (KeyCode.D) && state != StateTypes.stunned)
			{
				tempSpeed [0] += turnAccel * Time.deltaTime;
				turnPressed = 1;
			}
		}

		// Keep max turning speed:
		if (Mathf.Abs (tempSpeed [0]) > turnSpeed && state != StateTypes.stunned)
		{
			tempSpeed[0] = Mathf.Abs (tempSpeed [0]) / tempSpeed[0];
			tempSpeed[0] *= turnSpeed;
		}

		// If no turning key has been pressed, stop turning:
		if (turnPressed == 0)
		{
			// If you are barely moving sideways, set your speed to 0:
			if (Mathf.Abs(tempSpeed[0]) < turnDampen * Time.deltaTime)
				tempSpeed[0] = 0;
			// Else, keep slowing down your turning speed:
			else
				tempSpeed[0] -= (Mathf.Abs (tempSpeed [0]) / tempSpeed[0]) * turnDampen * Time.deltaTime;
		}

		// If you're dashing, change your speed accordingly:
		if (dashing == 1)
		{
			// Add or subtract acceleration from speed:
			if (dashDistance != 0 && transform.position.z < cameraTarget.transform.position.z + dashDistance)
			{
				tempSpeed[2] += dashDampen * Time.deltaTime;
				// Keep minimal speed, so you reach your original position eventually:
				if (tempSpeed[2] > -1f)
					tempSpeed[2] = -1f;
			}
			else
			{
				tempSpeed[2] -= dashDampen * Time.deltaTime;
			}

			// If you reached the top point of your movement, calculate your dostance from original point:
			if (dashDistance == 0 && Mathf.Abs(tempSpeed[2]) <= dashDampen * Time.deltaTime)
			{
				dashDistance = transform.position.z - cameraTarget.transform.position.z;
				dashDistance = dashDistance / 2;
			}

			// If you reached original point, stop dashing:
			if (transform.position.z < cameraTarget.transform.position.z && tempSpeed[2] < dashLocalSpeed / 2)
			{
				tempSpeed[2] = 0;
				transform.position = new Vector3(transform.position.x, cameraTarget.transform.position.y, transform.position.z);
				dashDistance = 0;
				dashing = 0;
				cameraTarget.GetComponent<CameraTargetScript> ().Dash();
			}
		}

		/*if (tempSpeed [2] < maxForwardSpeed)
		{
			tempSpeed [2] += forwardAccel * Time.deltaTime;
		}
		else
		{
			tempSpeed [2] = maxForwardSpeed;
		}*/

		// If you are dead, do something:
		if (hp <= 0)
		{
			//tempSpeed[1] = 4;
			cameraTarget.GetComponent<CameraTargetScript> ().EndMoving();

			// Stop dashing:
			dashing = 0;

			tempSpeed[2] = 0;
		}

		// After speed calculations have been made, apply values from temp variable to real speed vector:
		GetComponent<Rigidbody>().velocity = tempSpeed;

		#endregion
	}
}
