using UnityEngine;

namespace Assets.Player
{
    public class SimpleMover : MonoBehaviour
    {

        public float speed = 5f;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
            this.transform.position = this.transform.position + Time.deltaTime*Vector3.forward*speed;
        }
    }
}
