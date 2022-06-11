using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;

namespace ExampleMod
{
    class CoffeeCloud
    {

        // Initializing Coffee Cloud power
        public static void init()
        {

            // Creating new God Power
            var spawnCoffeeCloud = new GodPower();

            spawnCoffeeCloud.id = "coffeeCloudSpawn";
            spawnCoffeeCloud.name = "coffeeCloudSpawn";
            spawnCoffeeCloud.forceBrush = "sqr_0";
            spawnCoffeeCloud.type = PowerActionType.Special;

            // Action, which represents cloud spawning
            spawnCoffeeCloud.click_action = new PowerActionWithID(action_spawnCloud);

            // We dont need to spawn many clouds just by holding
            spawnCoffeeCloud.holdAction = false;

            // We dont need to select tool size
            spawnCoffeeCloud.showToolSizes = false;

            spawnCoffeeCloud.unselectWhenWindow = true;

            //// We dont need falling pixel
            //spawnCoffeeCloud.fallingPixel = false; 

            // Adding new god power to the AssetManager
            AssetManager.powers.add(spawnCoffeeCloud);

            // Creating new PowerButton
            var button = NCMS.Utils.PowerButtons.CreateButton(

                // Must be the same as spawnCoffeeCloud.id
                "coffeeCloudSpawn",

                // Using ingame sprite for our new button
                Resources.Load<Sprite>("ui/icons/iconcoffee"),
                "Coffee Cloud",
                "Raining coffee",
                Vector2.zero,

                // Type of our power: GodPower
                NCMS.Utils.ButtonType.GodPower
            );

            // Adding our button to the tab
            NCMS.Utils.PowerButtons.AddButtonToTab(

                // Our button
                button,

                // Nature tab
                NCMS.Utils.PowerTab.Nature,

                // Position of button in this tab. 489.6f by x is the position of snow cloud and + 36 offset. -18 by y means this button will be on second row
                new Vector2(680.6f + 36, -18));
        }

        public static bool action_spawnCloud(WorldTile pTile = null, string pPower = "")
        {
            // We need some default cloud to use for create out own
            GodPower godPower = AssetManager.powers.get("cloudAcid");

            // Check tile for null just in case
            if (pTile == null)
            {
                // Exit from method if null
                return false;
            }

            var cloud = MapBox.instance.cloudController.getNext();

            // Calling cloud.prepare(pTile.posV3, godPower.id) using reflections
            var method = cloud.GetType().GetMethod("prepare", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new[] { typeof(Vector3), typeof(string) }, null);
            method.Invoke(cloud, new object[] { pTile.posV3, godPower.id });


            // Setting coffee as drop id
            Reflection.SetField<string>(cloud, "dropID", "coffee");

            // Setting brown color for this cloud
            cloud.sprRenderer.color = new Color(0.30f, 0.21f, 0.15f, 0.77f);


            return true;
        }
    }
}