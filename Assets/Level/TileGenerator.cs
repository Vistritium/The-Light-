using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Level
{
    public class TileGenerator : MonoBehaviour
    {
        public float generateDistance = 10;

        public delegate void NewTilesAdded(List<GameObject> tiles);

        public event NewTilesAdded newTilesAdded;

        private TileChooser tileChooser;

        private GameObject lastTile;
        private Vector3 direction = Vector3.forward;
        private int tileCounter = 1;

        private GameObject player;
        private GameObject levelObject;

        private List<GameObject> allTilesWithoutLast = new List<GameObject>();

        // Use this for initialization
        private void Start()
        {
            this.tileChooser = GetComponent<TileChooser>();
            this.lastTile = GameObject.Find("Tile0");
            this.player = GameObject.Find("Player");
            this.levelObject = GameObject.Find("Level");
        }


        private string GetNameForNextTile()
        {
            return String.Format("Tile{0}", tileCounter);
        }

        private void SetPositionForNextTile(GameObject nextTile)
        {
            var lengthFactor = 10f;
            Vector3 lastTilePos = lastTile.transform.position;
            var lastTileLength = lastTile.transform.localScale.x;
            var nextTileLength = nextTile.transform.localScale.x;
            var borderPos = lastTilePos + direction*lastTileLength*0.5f*lengthFactor;
            var newPos = borderPos + direction*nextTileLength*0.5f*lengthFactor;

            nextTile.transform.position = newPos;
        }

        // Update is called once per frame
        private void Update()
        {
            var lastTileDistanceFromPlayer = Vector3.Distance(player.transform.position, lastTile.transform.position);
            if (lastTileDistanceFromPlayer < generateDistance)
            {
                var newlyGenerated = new List<GameObject>();

                newlyGenerated.Add(GenerateNext());
                newlyGenerated.Add(GenerateNext());
                newlyGenerated.Add(GenerateNext());

                if (newTilesAdded != null)
                {
                    newTilesAdded.Invoke(newlyGenerated);
                }
                
            }
        }

        private GameObject GenerateNext()
        {
            //create new object and name it
            var tileObject = tileChooser.GetTileObject();
            tileObject.name = GetNameForNextTile();
            tileCounter = tileCounter + 1;

            //add to proper parent object
            tileObject.transform.parent = levelObject.transform;

            //set position
            SetPositionForNextTile(tileObject);

            lastTile = tileObject;

            allTilesWithoutLast.Add(lastTile);

            return lastTile;
        }
    }
}