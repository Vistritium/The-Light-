using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BulletCollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void Collide()
    {
        var obstacle = Instantiate(Templates.GetTemplate("Obstacle"));
        obstacle.SetActive(true);
        obstacle.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);

    }
}
