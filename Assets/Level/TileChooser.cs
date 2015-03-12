using UnityEngine;

namespace Assets.Level
{
    public class TileChooser : MonoBehaviour
    {

        public GameObject basicTerrain;

        public GameObject GetTileObject()
        {
            var newTile = Instantiate(basicTerrain);
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
