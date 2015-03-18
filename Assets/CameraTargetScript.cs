using UnityEngine;
using System.Collections;

public class CameraTargetScript : MonoBehaviour {
	
	public float speed = 5;
	public float speedMax = 10;
	public float speedAcceleration = 0.1f;
	public float speedAdditional = 0;
	public float dashSpeed = 20;
	public float dashTime = 3;
	public float dashDampen = 10;
	public float dashAcceleration = 20;
	public float dashTimer = 0;

	public enum CameraTargetStates{
		normal,
		dashStart,
		dash
	};

	public CameraTargetStates state = CameraTargetStates.normal;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Speed up progressively over time:
		speed += speedAcceleration * Time.deltaTime;

		// Deal with dashing mechanic:
		if (state == CameraTargetStates.dashStart)
		{
			if (speedAdditional <= dashSpeed)
			{
				speedAdditional += dashAcceleration * Time.deltaTime;
				if (speedAdditional > dashSpeed)
					speedAdditional = dashSpeed;
			}
		}
		else if (dashTimer > 0)
		{
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
			{
				dashTimer = 0;
			}
		}
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

	public void StartDashing()
	{
		state = CameraTargetStates.dashStart;
	}

	public void Dash()
	{
		state = CameraTargetStates.dash;
		//speedAdditional = dashSpeed;
		dashTimer = dashTime;
	}
}
