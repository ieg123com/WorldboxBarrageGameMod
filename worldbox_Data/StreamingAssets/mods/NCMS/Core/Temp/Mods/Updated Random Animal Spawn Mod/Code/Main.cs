using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;

namespace UpdatedRandomAnimalSpawnMod{
    [ModEntry]//This is unique attribute should be somewhere in your code, above class : MonoBehaviour
    class Main : MonoBehaviour{
        void Awake(){
            Debug.Log($"{Mod.Info.Name} loaded!");

            Init();
            //Debug.Log(Mod.description);
        }

        void Init(){
            var sand = AssetManager.tiles.get("sand");
            sand.addUnitsToSpawn("turtle", "crab");
            var grass = AssetManager.topTiles.get("grass_low");
            grass.addUnitsToSpawn("cow");
            // var grass_flowers = TileType.getGen("grass_flowers");
            // grass_flowers.spawnCreatures.Add("bear");
            var forest = AssetManager.topTiles.get("grass_high");
            forest.addUnitsToSpawn("cat");
            // var forest_flowers = TileType.getGen("forest_flowers");
            // forest_flowers.spawnCreatures.Add("bear");
            var forest_soil_frozen = AssetManager.topTiles.get("snow_high");
            forest_soil_frozen.addUnitsToSpawn("penguin");
        }
    }

}
//Debug.Log(TestObject.testString);