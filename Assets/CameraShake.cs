using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

	public static void ShakeCamera(){
		GameObject.Find ("Main Camera").GetComponent<CameraShake>().Shake ();
	}

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 1f;

	private float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;

	private float currentShakeAmount;

	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	public void Shake(){
		Debug.Log ("Shaking");
		shake = shakeDuration;
		originalPos = camTransform.localPosition;
		currentShakeAmount = shakeAmount;
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}
	
	void Update()
	{
		if (shake > 0)
		{
			Debug.Log("shakin");
			camTransform.localPosition = originalPos + Random.insideUnitSphere * currentShakeAmount;
			currentShakeAmount -= Time.deltaTime * decreaseFactor;
			shake -= Time.deltaTime;

			if(currentShakeAmount <= 0){
				shake = 0;
			}
		}
		else
		{
			shake = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}