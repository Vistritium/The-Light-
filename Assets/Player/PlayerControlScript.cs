using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

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

	public float powerUpDuration = 10;
	public float powerWarningTime = 1.5f;

	public float stunTimer = 0;
	public float poweredTimer = 0;

	public float hp = 10;
	public float hpMax = 10;
	public float hpRegen = 5;
	public Texture2D healthTexture;
	private float barWidth;
	private float barHeight;

	public float rotationForce = 5;
	public float rotationStart = 10;
	public float rotation = 0;
	public float rotationSpeed = 0;

	private float trailTargetTime = 0;
	private float trailChangeSpeed = 0.2f;

	public float wheelRotationSpeed = 2;
	private float wheelTotalRotation = 0;
	private float lastTurningSpeed = 0;

	public GameObject[] wheelObjects;

	public float trackWidth = 8;

	public float carLength = 10;

	public float fireRate;

	public string enteredTriggerTag = "";

	private int turnPressed = 0;

	private float nextFire = 0;

	bool dead = false;

	private bool carStopped = false;

	public enum StateTypes{
		normal,
		powered,
		stunned
	};

	public int dashing = 0;

	public StateTypes state = StateTypes.normal;

	public int ringsCollected = 0;
	private bool ringFlag = false;

	private GameObject cameraTarget;
	private GameObject modelTarget;
	private TrailRenderer[] trails;
	public GameObject[] smokes;
	public GameObject dashEffect;
	public GameObject audiStartParticles;

	public GameObject shot;
	public Transform shotSpawn;

	public GameObject needle;
	public Text scoreText;
	private float score = 0f;

	void Awake()
	{
		barHeight = Screen.height * 0.04f;
		barWidth = barHeight * 10.0f;
		
	}

	void OnGUI()
	{
//		GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
//		                         Screen.height - barHeight - 300,
//		                         hp * barWidth / hpMax,
//		                         barHeight),
//		                healthTexture);
	}

	// Use this for initialization
	void Start () {
		AssetDatabase.Refresh();

		//cameraTarget = GameObject.Find ("Player");
		cameraTarget = transform.parent.gameObject;
		modelTarget = GameObject.Find ("Model");

		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, forwardSpeed);

		transform.position = cameraTarget.transform.position;

		trails = new TrailRenderer[2];

		trails [0] = GameObject.Find ("Trail1").GetComponent<TrailRenderer>();
		trails [1] = GameObject.Find ("Trail2").GetComponent<TrailRenderer>();

		//dashEffect = GameObject.Find ("DashEffect");

		//smokes = new GameObject[2];

		//smokes[0] = GameObject.Find ("Smoker1");
		//smokes[1] = GameObject.Find ("Smoker2");
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel("main");
			
		}
		int i;

		ringFlag = false;
		#region Timers and States

		if (hp > 0)
		{
			hp += hpRegen * Time.deltaTime;
			if (hp > hpMax)
				hp = hpMax;
		}

		if (poweredTimer > 0)
		{
			poweredTimer -= Time.deltaTime;

			if (poweredTimer <= 0)
			{
				poweredTimer = 0;
				state = StateTypes.normal;

				//EndSmokeEffects();
				//EndDashEffects();
				EndAudiEffects();
			}
			else if (poweredTimer <= powerWarningTime)
				EndDashEffects();
			else if (poweredTimer <= powerWarningTime * 2)
				EndSmokeEffects();
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


		for (i = 0; i < 2; i++)
		{
			if (Mathf.Abs(trails[i].time - trailTargetTime) < trailChangeSpeed * Time.deltaTime)
				trails[i].time = trailTargetTime;
			else
				trails[i].time += trailChangeSpeed * Time.deltaTime * Mathf.Sign(trailTargetTime - trails[i].time);
		}

		#endregion

		#region Rotations

		// Rotate the car forward if it hit something:
		rotation += rotationSpeed * Time.deltaTime;
		rotationSpeed -= rotationForce * Time.deltaTime;

		if (rotation < 0)
		{
			rotation = 0;
			rotationSpeed = 0;
		}

		var temp = transform.rotation;
		transform.rotation = Quaternion.AngleAxis(rotation, Vector3.right);
       

		var temp2 = transform.position;
		temp2.y = rotation * 0.05f;
		transform.position = temp2;


		// Rotate the model:
		if (hp > 0)
		{
			modelTarget.transform.LookAt(modelTarget.transform.position - GetComponent<Rigidbody>().velocity - cameraTarget.GetComponent<CameraTargetScript>().speed * Vector3.forward);
			modelTarget.transform.Rotate(new Vector3(0, 90, 0));
		}

		// Rotate wheels accortingly to speed:
		for (i = 0; i < 4; i++)
		{
			wheelTotalRotation += wheelRotationSpeed * (cameraTarget.GetComponent<CameraTargetScript>().speed + cameraTarget.GetComponent<CameraTargetScript>().speedAdditional) * Time.deltaTime;

			//wheelObjects[i].transform.rotation = Quaternion.AngleAxis(-wheelTotalRotation, modelTarget.transform.forward);
			if (i < 2)
				wheelObjects[i].transform.localRotation = Quaternion.Euler(0, (GetComponent<Rigidbody>().velocity[0] - lastTurningSpeed) * Time.deltaTime * 1500f, -wheelTotalRotation);
			else
				wheelObjects[i].transform.localRotation = Quaternion.Euler(0, 0, -wheelTotalRotation);
			//wheelObjects[i].transform.Rotate(new Vector3(0, -90, 0));
			//wheelObjects[i].transform.Rotate(modelTarget.transform.right, wheelRotationSpeed * cameraTarget.GetComponent<CameraTargetScript>().speed); 
		}

		lastTurningSpeed = GetComponent<Rigidbody>().velocity[0];

		#endregion

		#region Speeds Management

		turnPressed = 0;

		// Since you can't change speedx, speedy or speedz directly, make a temp variable to operate on:
		Vector3 tempSpeed = GetComponent<Rigidbody>().velocity;

		// React accordingly if you hit something.
		if (enteredTriggerTag != "" && state != StateTypes.stunned && carStopped == false)
		{
			// If you touched DashPad:
			if (enteredTriggerTag == "DashPad" && state != StateTypes.powered)
			{
				if (state == StateTypes.normal)
				{
					dashing = 1;
					tempSpeed[2] = dashLocalSpeed;

					cameraTarget.GetComponent<CameraTargetScript> ().StartDashing();
					ApplyDashEffects();
				}
			}
			// If you collected Audi ring:
			else if (enteredTriggerTag == "Ring")
			{
				ringsCollected++;
				cameraTarget.GetComponent<CameraTargetScript> ().PickRing(transform.position);

				if (ringsCollected >= 4)
				{
					ringsCollected = 0;

					cameraTarget.GetComponent<CameraTargetScript> ().EnterAudi();

					state = StateTypes.powered;
					poweredTimer = powerUpDuration;

					ApplyDashEffects();
					ApplySmokeEffects();
				}
			}
			// If you hit a wall, check, which side and subtract hp:
			else if (state != StateTypes.powered)
			{
				if (enteredTriggerTag == "MiddlePlayerCollider")
				{
					hp = 0;
					cameraTarget.GetComponent<CameraTargetScript> ().Stop();

					rotationSpeed = rotationStart;
					carStopped = true;
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
			else
			{

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


			if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				
			}

		}


		// Keep max turning speed:
		if (Mathf.Abs (tempSpeed [0]) > turnSpeed && state != StateTypes.stunned)
		{
			tempSpeed[0] = Mathf.Abs (tempSpeed [0]) / tempSpeed[0];
			tempSpeed[0] *= turnSpeed;
		}

		// When in Audi Mode, no boundaries keep you inside the track, so theese have to instead:
		if (state == StateTypes.powered)
		{
			if (tempSpeed[0] > 0 && transform.localPosition.x > trackWidth)
			{
				tempSpeed[0] = 0;
				turnPressed = 0;
			}

			if (tempSpeed[0] < 0 && transform.localPosition.x < -trackWidth)
			{
				tempSpeed[0] = 0;
				turnPressed = 0;
			}
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
				//cameraTarget.GetComponent<CameraTargetScript> ().Dash();
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

			if(!dead){
				DeadOnce();
			}
			dead = true;
			
		}

		// After speed calculations have been made, apply values from temp variable to real speed vector:
		GetComponent<Rigidbody>().velocity = tempSpeed;

		#endregion


		#region Update HUD

/*		needle.transform.rotation = Quaternion.identity;
		Debug.Log(cameraTarget.GetComponent<CameraTargetScript>().speed);
		Debug.Log(cameraTarget.GetComponent<CameraTargetScript>().speedAdditional);
		Debug.Log(cameraTarget.GetComponent<CameraTargetScript>().speedMax);

		needle.transform.RotateAround(needle.transform.position, needle.transform.forward,
		                              125 -
		                              ((cameraTarget.GetComponent<CameraTargetScript>().speed +
		 								cameraTarget.GetComponent<CameraTargetScript>().speedAdditional)
		                              / (cameraTarget.GetComponent<CameraTargetScript>().speedMax + 1))
		                              * 220);// * Time.deltaTime);
*/
		score += (cameraTarget.GetComponent<CameraTargetScript>().speed +
		          cameraTarget.GetComponent<CameraTargetScript>().speedAdditional) * Time.deltaTime;

		scoreText.text = Mathf.Floor(score).ToString();

		#endregion
	}

	public void ApplyDashEffects()
	{
		//for (int i = 0; i < 2; i++)
		//	trails [i].time = 0.1f;
		trailTargetTime = 0.1f;
	}

	public void EndDashEffects()
	{
		//for (int i = 0; i < 2; i++)
		//	trails [i].time = 0f;
		trailTargetTime = 0;
	}

	public void ApplySmokeEffects()
	{
		for (int i = 0; i < 2; i++)
			smokes [i].SetActive (true);
	}

	public void EndSmokeEffects()
	{
		for (int i = 0; i < 2; i++)
			smokes [i].SetActive (false);
	}

	public void ApplyAudiEffects()
	{
		GameObject temp = (GameObject)Instantiate (audiStartParticles, transform.position, transform.rotation);
		temp.transform.parent = transform;
		dashEffect.SetActive(true);
	}

	public void EndAudiEffects()
	{
		dashEffect.SetActive(false);
	}

	public void ReceiveHitInfo(string tag){
		ReceiveHitInfo (tag, default(Vector3));
	}

	public void ReceiveHitInfo(string tag, Vector3 pos){
		// React accordingly if you hit something.
		if (tag != "" && (state != StateTypes.stunned || tag == "MiddlePlayerCollider") && carStopped == false)
		{
			// Since you can't change speedx, speedy or speedz directly, make a temp variable to operate on:
			Vector3 tempSpeed = GetComponent<Rigidbody>().velocity;

			// If you touched DashPad:
			if (tag == "DashPad" && state != StateTypes.powered)
			{
				if (state == StateTypes.normal)
				{
					dashing = 1;
					tempSpeed[2] = dashLocalSpeed;
					
					cameraTarget.GetComponent<CameraTargetScript> ().StartDashing();
					ApplyDashEffects();
				}
			}
			// If you collected Audi ring:
			else if (tag == "Ring" && ringFlag == false)
			{
				ringFlag = true;
				ringsCollected++;
				cameraTarget.GetComponent<CameraTargetScript> ().PickRing(pos);
				
				if (ringsCollected >= 4)
				{
					ringsCollected = 0;
					
					cameraTarget.GetComponent<CameraTargetScript> ().EnterAudi();
					
					state = StateTypes.powered;
					poweredTimer = powerUpDuration;
					
					ApplyDashEffects();
					ApplySmokeEffects();
					ApplyAudiEffects();
				}
			}
			// If you hit a wall, check, which side and subtract hp:
			else if (state != StateTypes.powered)
			{
				if (tag == "MiddlePlayerCollider")
				{
					hp = 0;
					cameraTarget.GetComponent<CameraTargetScript> ().Stop();
					
					rotationSpeed = rotationStart;
					carStopped = true;
				}
				else if (tag == "LeftPlayerCollider")
				{
					tempSpeed[0] = hitForce;
				}
				else if (tag == "RightPlayerCollider")
				{
					tempSpeed[0] = -hitForce;
				}
				
				state = StateTypes.stunned;
				stunTimer = hitStunDuration;
				
				hp -= hitDamage;
			}
			else
			{
				
			}

			// After speed calculations have been made, apply values from temp variable to real speed vector:
			GetComponent<Rigidbody>().velocity = tempSpeed;
		}
	}

	//called ONCE when player is dead
	private void DeadOnce(){
		CameraShake.ShakeCamera ();
	}
}
