using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {

	public float forwardSpeed = 3;
	public float maxForwardSpeed = 10;
	public float forwardAccel = 0.1f;

	public float turnSpeed = 1;
	public float turnAccel = 1;
	public float turnDampen = 3;

	public float hitForce = 10;
	public float hitDamage = 6;
	public float hitStunDuration = 1;

	public float stunTimer = 0;

	public float hp = 10;
	public float hpMax = 10;
	public float hpRegen = 5;

	public string enteredTriggerTag = "";


	private int turnPressed = 0;

	public enum StateTypes{
		normal,
		stunned
	};

	public StateTypes state = StateTypes.normal;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, forwardSpeed);
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

		#endregion

		#region Speeds Management

		turnPressed = 0;

		// Since you can't change speedx, speedy or speedz directly, make a temp variable to operate on:
		Vector3 tempSpeed = GetComponent<Rigidbody>().velocity;

		// React accordingly if you hit something.
		if (enteredTriggerTag != "" && state == StateTypes.normal)
		{
			if (enteredTriggerTag == "MiddlePlayerCollider")
			{
				hp = 0;
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

		enteredTriggerTag = "";

		// Calculate input:
		if (Input.GetKey (KeyCode.A) && state == StateTypes.normal)
		{
			tempSpeed [0] -= turnAccel * Time.deltaTime;
			turnPressed = 1;
		}
		else if (Input.GetKey (KeyCode.D) && state == StateTypes.normal)
		{
			tempSpeed [0] += turnAccel * Time.deltaTime;
			turnPressed = 1;
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

		if (tempSpeed [2] < maxForwardSpeed)
		{
			tempSpeed [2] += forwardAccel * Time.deltaTime;
		}
		else
		{
			tempSpeed [2] = maxForwardSpeed;
		}

		// If you are dead, do something:
		if (hp <= 0)
		{
			tempSpeed[1] = 4;
		}

		// After speed calculations have been made, apply values from temp variable to real speed vector:
		GetComponent<Rigidbody>().velocity = tempSpeed;

		#endregion
	}
}
