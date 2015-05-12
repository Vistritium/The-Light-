using UnityEngine;
using AssemblyCSharp;

namespace Assets.Level
{
    public class TileChooser : MonoBehaviour
    {


        public GameObject basicTerrain;
		public GameObject basicWall;
		public GameObject wallType1;
		public GameObject wallType2;

		private float lengthFactorX = 2.85f;
		private float lengthFactorZ = 5f;

		public int wallType1ChancePercent = 30;
		public int wallType2ChancePercent = 18;



		private GameObject getWall(){

			GameObject result = null;

			float random = Random.Range (0f, 1f);

			float type1Range = wallType1ChancePercent * 0.01f;
			float type2Range = wallType2ChancePercent * 0.01f + type1Range;

			if (random < type1Range) {
				result = Instantiate(wallType1);
			} else if (random < type2Range) {
				result = Instantiate(wallType2);
			} else {
				result = Instantiate(basicWall);
			}


			return result;
		}


        public GameObject GetTileObject()
        {

			var newTile = Instantiate(basicTerrain);
			newTile.SetActive (true);

			
			var leftWall = getWall ();

			leftWall.transform.parent = newTile.transform;
			leftWall.transform.localPosition = 
				Vector3.left * newTile.transform.localScale.x * lengthFactorX + 
					Vector3.forward * newTile.transform.localScale.z * lengthFactorZ;

			var rightWall = Instantiate (leftWall);

			rightWall.transform.parent = newTile.transform;
			rightWall.transform.localPosition = Vector3.right * newTile.transform.localScale.x * lengthFactorX;

			rightWall.transform.localScale = leftWall.transform.localScale;

			rightWall.transform.localEulerAngles = rightWall.transform.localEulerAngles + new Vector3 (0, 180, 0);

			Debug.Log(newTile.transform.localPosition);

           /* newTile.tag = basicTerrain.tag;
            newTile.layer = basicTerrain.layer;*/
            return newTile;
        }

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
