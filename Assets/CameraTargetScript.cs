using UnityEngine;
using System.Collections;

public class CameraTargetScript : MonoBehaviour {
	public string enteredTriggerTag = "";
	public float speed = 5;
	public float speedMax = 10;
	public float speedAcceleration = 0.1f;
	public float speedAdditional = 0;
	public float deadDampen = 20;
	public float dashSpeed = 20;
	public float dashTime = 3;
	public float dashDampen = 10;
	public float dashAcceleration = 20;
	public float dashTimer = 0;
	public float audiTime = 10;

	public Vector3 ringsBaseOffset;
	public Vector3 ringsSmallOffset;
	public Vector3 ringsMoveOffset;
	public float ringsMoveTime = 2;
	public float ringsCounter = 0;
	private int ringsMoveDirection = 0;
	private int ringsAmount = 0;
	private Vector3 ringAdditionalBaseOffset;
	private GameObject ringAdditional;

	public GameObject mistGenerator;
	public GameObject audiRing;
	private GameObject[] rings;

	private PlayerControlScript player;
	
	private float savedTime = 0;
	
	public enum CameraTargetStates{
		normal,
		dashStart,
		dash,
		end
	};

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;

	
	public CameraTargetStates state = CameraTargetStates.normal;

	void Awake()
	{
		if (enteredTriggerTag == "MiddlePlayerCollider")
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Audi").GetComponent<PlayerControlScript>();

		rings = new GameObject[4];

		for (int i = 0; i < 4; i++)
		{
			rings[i] = Instantiate (audiRing);
			rings[i].transform.parent = this.transform;
			rings[i].transform.localPosition = ringsBaseOffset + ringsSmallOffset * i;

			rings[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		#region Collecting audi rings

		// If the audi ring collection is in progress, move the counter accordingly:
		if (ringsCounter > 0)
		{
			ringsCounter += ringsMoveDirection * Time.deltaTime;
		}

		if (ringsCounter > ringsMoveTime * 2)
		{
			ringsCounter = ringsMoveTime;
			ringsMoveDirection = -1;

			Destroy(ringAdditional);
			rings[ringsAmount].SetActive(true);
			ringsAmount++;

			if (ringsAmount >= 4)
			{
				ringsAmount = 0;

				for (int i = 0; i < 4; i++)
					rings[i].SetActive(false);

				GameObject temp = Instantiate (mistGenerator);
				temp.transform.parent = this.transform;
				temp.transform.localPosition = ringsBaseOffset + ringsSmallOffset * 2 + ringsMoveOffset;
			}
		}

		if (ringsCounter < ringsMoveTime)
		{
			for (int i = 0; i < 4; i++)
			{
				rings[i].transform.localPosition = ringsBaseOffset + ringsSmallOffset * i + ringsMoveOffset * (ringsCounter / ringsMoveTime);
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				rings[i].transform.localPosition = ringsBaseOffset + ringsSmallOffset * i + ringsMoveOffset;
			}
		}

		if (ringsCounter > 0 && ringsCounter < ringsMoveTime * 1.5f && ringsMoveDirection == 1)
		{
			ringAdditional.transform.localPosition = ringAdditionalBaseOffset + ((ringsBaseOffset + ringsSmallOffset * ringsAmount + ringsMoveOffset) - ringAdditionalBaseOffset) * (ringsCounter / (ringsMoveTime * 1.5f));
		}

		#endregion


		/*
		if (shake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			camTransform.localPosition = originalPos;
		}*/


		// Speed up progressively over time:
		if (speed < speedMax)
			speed += speedAcceleration * Time.deltaTime;
		
		// If player is ded, slow down:
		if (state == CameraTargetStates.end)
		{
			speed -= deadDampen * Time.deltaTime;
			if (speed < 0)
				speed = 0;
			
			speedAdditional -= deadDampen * Time.deltaTime;
			if (speedAdditional < 0)
				speedAdditional = 0;
		}
		
		// Deal with dashing mechanic:
		if (state == CameraTargetStates.dashStart)
		{
			if (speedAdditional <= dashSpeed)
			{
				speedAdditional += dashAcceleration * Time.deltaTime;
				if (speedAdditional > dashSpeed)
				{
					speedAdditional = dashSpeed;
					
					state = CameraTargetStates.dash;
					dashTimer = savedTime;
				}
			}
		}
		// Aka. if you're in the middle of dashing:
		else if (dashTimer > 0)
		{
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
			{
				dashTimer = 0;
				player.EndDashEffects();
			}
		}
		// Aka. if you're not dashing and have to lose additional speed:
		else if (speedAdditional > 0)
		{
			speedAdditional -= dashDampen * Time.deltaTime;
			if (speedAdditional <= 0)
			{
				speedAdditional = 0;
				state = CameraTargetStates.normal;
			}
		}
		
		this.transform.position = this.transform.position + Time.deltaTime*Vector3.forward*(speed + speedAdditional);
	}

	public void StartDashing(){
		StartDashing (-1f);
	}
	
	public void StartDashing(float arg0)
	{
		state = CameraTargetStates.dashStart;
		
		if (arg0 < 0)
			arg0 = dashTime;
		
		savedTime = arg0;
	}
	
	public void EnterAudi()
	{
		state = CameraTargetStates.dashStart;
		
		savedTime = audiTime;
	}
	
	public void Dash()
	{
		state = CameraTargetStates.dash;
		//speedAdditional = dashSpeed;
		dashTimer = dashTime;
	}
	
	public void EndMoving()
	{
		if (state != CameraTargetStates.end)
		{
			speedMax = 0;
			state = CameraTargetStates.end;
		}
	}

	public void PickRing(Vector3 pos)
	{
		ringsCounter = 0.1f;

		ringsMoveDirection = 1;

		ringAdditional = Instantiate (audiRing);
		ringAdditional.transform.parent = this.transform;
		ringAdditional.GetComponent<BoxCollider> ().enabled = false;

		ringAdditionalBaseOffset = transform.InverseTransformPoint(pos);
		//rings[i].transform.localPosition = ringsBaseOffset + ringsSmallOffset * i;
	}
	
	public void Stop()
	{
		speed = 0;
		speedMax = 0;
		speedAdditional = 0;
	}
}
