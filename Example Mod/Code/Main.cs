using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;

namespace ExampleMod{
    [ModEntry]
    class Main : MonoBehaviour{
        void Awake(){
            // Initializing Coffee Cloud power
            CoffeeCloud.init();

            // Initializing Tiles Window
            TilesWindow.init();

            // Initializing Tiles Button
            TilesPowerButton.init();
        }
    }
}