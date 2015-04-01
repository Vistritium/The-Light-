using System.Collections.Generic;
using System.Linq;
using Assets.Level;
using UnityEngine;
using System;

namespace Assets
{
    public class TerrainGenerator : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            var tileGenerator = GetComponent<TileGenerator>();
            tileGenerator.newTilesAdded += NewTilesAdded;
        }

        // Update is called once per frame
        private void Update()
        {
        }


        //called when new tiles are generated
        private void NewTilesAdded(List<GameObject> newTiles)
        {
            Debug.Log("New tiles added: " + newTiles.Select(x => x.ToString()).Aggregate((x1, x2) => x1 + " " + x2));
        }
    }
}