using System;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;

namespace ExampleMod{
    class TilesPowerButton{

         // Defining power and button as internal static, because we will use it in TilesWindow class
        internal static GodPower power;
        internal static PowerButton button;

        // Initializing Tiles Button
        public static void init(){

            // Creating new drawing god's power by cloning prepared _terraformTiles power.
            power = AssetManager.powers.clone("anyTile", "_terraformTiles");
            power.name = "anyTile";

            // Setting deep_ocean as default tile type
            power.tileType = "deep_ocean"; 

            // creating new PowerButton
            button = PowerButtons.CreateButton(  
                // should be the same as power.id
                "anyTile",

                // using ingame sprite for our new button
                Resources.Load<Sprite>("ui/icons/icontilesoil"),

                "Select Tile To Draw",
                "Select any tile which represents in game and draw with it",
                Vector2.zero,

                // Type of our power: GodPower.
                ButtonType.GodPower, 
                /*
                This button shows window, so basicaly it should be ButtonType.Click. 
                But it will be also power, so we need to make it with ButtonType.GodPower
                Even if it will be ButtonType.GodPower, we still be able to set on click action
                */

                null,
                
                // On click action
                OpenWindow 
            );

            // Adding our button to the tab
            PowerButtons.AddButtonToTab(
                button,

                // Drawings tab
                PowerTab.Drawing,

                // Position of button in this tab. 345.6f by x is the position of snow cloud and + 36 offset. 18 by y means this button will be on first row.
                new Vector2(345.6f + 36, 18)); 
        }

        private static void OpenWindow(){
            if (TilesWindow.pbsInstance.isPowerSelected("anyTile"))
            {
                // Showing tilesWindow window
                Windows.ShowWindow("tilesWindow"); 
            }
            else
            {
                TilesWindow.pbsInstance.unselectAll();
            }
        }
    }
}