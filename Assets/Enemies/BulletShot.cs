using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BulletShot : MonoBehaviour {

	public TargetProvider targetProvider;
	public SpeedProvider speedProvider;
	public GameObject boundary;

    private Vector3 initialPosition = default(Vector3);

	bool over = false;

    private float lastDistance = float.MaxValue;
	

	// Use this for initialization
	void Start () {
        this.speedProvider = GetComponent<SpeedProvider>();
        this.targetProvider = GetComponent<TargetProvider>();
	    
/*
		this.speedProvider = GetComponent<SpeedProvider> ();
		if (speedProvider == null) {
			this.gameObject.AddComponent<SpeedProvider>();
			this.speedProvider = GetComponent<SpeedProvider> ();
		}

		this.targetProvider = GetComponent<TargetProvider> ();
		if (this.targetProvider == null) {
			this.gameObject.AddComponent<TargetProvider>();
			this.targetProvider = GetComponent<TargetProvider>();
		}d
*/


	}

    void Awake()
    {
       
    }

	void collision(){
		var bulletCollisionHandler = GetComponent<BulletCollisionHandler> ();

		if (bulletCollisionHandler == null) {
			Destroy (this.gameObject);
		} else {
			over = true;
            bulletCollisionHandler.Collide();
			Destroy(this);
		}


	}

	
	// Update is called once per frame
	void Update () {

		if (!over) {
			var target = targetProvider.GetTarget();

			this.transform.LookAt(target);

		    if (initialPosition == default(Vector3))
		    {
                this.initialPosition = this.transform.position;
		    }
			
			var toVector = (target - this.transform.position);
				
				var normalized = toVector.normalized;
				
				var speed = speedProvider.GetSpeed();

			    var newPosition = this.transform.position + normalized * Time.deltaTime * speed;

			    var distance = Vector3.Distance(target, newPosition);

			    if (distance <= lastDistance)
			    {
			        this.lastDistance = distance;
                    this.transform.position = newPosition;
			    }
			    else
			    {
			        this.transform.position = target;
                    collision();
			    }

			   
			
		}



	}

	


	void OnTriggerEnter(Collider other)
	{

	    if (other.name.StartsWith("box"))
	    {
            other.GetComponent<Rigidbody>().constraints = ~(RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation);
	    }


	    other.gameObject.GetComponent<Rigidbody>().velocity = Random.onUnitSphere*50 + Vector3.up * 70;

/*		if (other.gameObject != boundary) {
			collision();

		}*/
	}



}
