using HarmonyLib;
using ReflectionUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Config;
using System.IO;
using System.Xml.Serialization;

namespace PowerBox
{
    partial class WorldBoxMod
    {
        public static void createButtons()
        {
            initActorsAssets();
            initGodPowersAssets();

            var instantinateFrom = NCMS.Utils.GameObjects.FindEvenInactive("crab");

            Helper.GodPowerTab.createButton(
                "AboutMod", 
                Resources.Load<Sprite>("ui/icons/iconabout"), 
                Helper.GodPowerTab.additionalPowersTab.transform, 
                About_Mod_Button_Click, 
                "About mod", 
                "Read about this mod");

            var dbgBtn = Helper.GodPowerTab.createButton(
                "DebugButtonAdd",
                Resources.Load<Sprite>("ui/icons/icondebug"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                Debug_Button_Add_Click,
                "Debug button",
                "Shows default debug window",
                null//Helper.Utils.FindEvenInactive("DebugButton")
                );

            GameObject.Destroy(dbgBtn.transform.GetComponent<DebugButton>());

            // Helper.GodPowerTab.createButton(
            //     "powerBoxLaws",
            //     Resources.Load<Sprite>("ui/icons/iconworldlaws"),
            //     Helper.GodPowerTab.additionalPowersTab.transform,
            //     PowerBox_Laws_Click,
            //     "PowerBox World Laws",
            //     "Additional world laws");

            Helper.GodPowerTab.AddLine();

            #region spawns

            Helper.GodPowerTab.createButton(
                "spawnMaximCreature", 
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.MaximCreature.icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform, 
                null, 
                "Maxim", 
                "Cute little pumpkin with a nice smile", 
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnMastefCreature", 
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.MastefCreature.icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform, 
                null, 
                "Mastef", 
                "What makes a man, Mr. Lebowski?", 
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnBurgerSpider",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.burgerSpider.icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Burger-spider",
                "Comes at nights and steals the kids. Loves to eat",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnGregCreature",
                Resources.Load<Sprite>("ui/icons/icongreg"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Greg",
                "Greg?  Greg...",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnTumorCreatureUnit",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.tumor_unit_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Tumor monster unit",
                "",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnTumorCreatureAnimal",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.tumor_animal_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Tumor monster animal",
                "",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnMushCreatureUnit",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.mush_unit_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Mush unit",
                "Double click to edit",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnMushCreatureAnimal",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.mush_animal_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Mush animal",
                "",
                instantinateFrom);

            #endregion

            // Helper.GodPowerTab.AddLine();

            #region units

            // Helper.GodPowerTab.createButton(
            //     "addTraits",
            //     Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.traits_plus.png"),
            //     Helper.GodPowerTab.additionalPowersTab.transform,
            //     Add_Traits_Button_Click,
            //     "Add traits",
            //     "Select traits and then add it to creatures",
            //     instantinateFrom);

            // Helper.GodPowerTab.createButton(
            //     "removeTraits",
            //     Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.traits_minus.png"),
            //     Helper.GodPowerTab.additionalPowersTab.transform,
            //     Remove_Traits_Button_Click,
            //     "Remove traits",
            //     "Select traits and then remove it from creatures",
            //     instantinateFrom);


            Helper.GodPowerTab.createButton(
                "addItems",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.items_plus.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                Add_Items_Button_Click,
                "Add items",
                "Select items and then add it from creatures",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "removeItems",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.items_minus.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                Remove_Items_Button_Click,
                "Remove items",
                "Select items and then remove it from creatures",
                instantinateFrom);

            #endregion

            Helper.GodPowerTab.AddLine();

            #region cities

            Helper.GodPowerTab.createButton(
                "friendshipNR",
                Resources.Load<Sprite>("ui/icons/iconfriendship"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Friendship(not random)",
                "Force kingdom to make peace with someone",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "spiteNR",
                Resources.Load<Sprite>("ui/icons/iconspite"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Spite(not random)",
                "Force kingdom to start a war with someone",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "upgradeBuildingAdd",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.upgrade_building_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Upgrade Building",
                "Upgrade buildings level right now without any cost to villagers",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "downgradeBuildingAdd",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.downgrade_building_icon.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Downgrade Building",
                "Downgrade buildings level right now without any pay to villagers",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "expandCitysBorders",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.borders1.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Expand city's borders",
                "Drag city's border and move it to expand",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "reduceCitysBorders",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.borders2.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Reduce city's borders",
                "Removing city borders",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "makeColony",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.colonies.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Make colony",
                "Use it on civilized villagers, which outside of their city and on free land, to make new colony",
                instantinateFrom);

            // Helper.GodPowerTab.createButton(
            //     "randomKingdomColor",
            //     Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.random_colors.png"),
            //     Helper.GodPowerTab.additionalPowersTab.transform,
            //     null,
            //     "Change kingdom color",
            //     "Changing kingdom color to other random color",
            //     instantinateFrom);


            Helper.GodPowerTab.createButton(
                "spawnFishingBoat",
                Resources.Load<Sprite>("actors/boats/boat_fishing"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Fishing boat",
                "Spawn fishing boat. Spawns only on water, and only in kingdom's borders",
                instantinateFrom);


            Helper.GodPowerTab.createButton(
                "spawnTransportBoat",
                Resources.Load<Sprite>("actors/boats/boat_trading_dwarf"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Transport boat",
                "Spawn transport boat. Spawns only on water, and only in kingdom's borders",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "spawnTradingBoat",
                Resources.Load<Sprite>("actors/boats/boat_transport_dwarf"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Trading boat",
                "Spawn trading boat. Spawns only on water, and only in kingdom's borders",
                instantinateFrom);


            #endregion

            Helper.GodPowerTab.AddLine();

            #region other stuff

            Helper.GodPowerTab.createButton(
                "bloodRainCloudSpawn",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.blood_rain.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Blood cloud",
                "Raining blood. Healing creatures",
                instantinateFrom);

            Helper.GodPowerTab.createButton(
                "burgerSpiderCloudSpawn",
                Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.powers.burgerspider_rain.png"),
                Helper.GodPowerTab.additionalPowersTab.transform,
                null,
                "Burger spider cloud",
                "Cloudy with a chance of burger spiders",
                instantinateFrom);

            #endregion
        }

        public static void About_Mod_Button_Click()
        {
            NCMS.Utils.Windows.ShowWindow("aboutPowerBox");
        }



        public static void Debug_Button_Add_Click()
        {
            NCMS.Utils.GameObjects.FindEvenInactive("DebugButton").GetComponent<Button>().onClick.Invoke();
        }



        // public static void Add_Traits_Button_Click()
        // {
        //     traitsButtonClick("addTraits", PowerType.add);
        // }

        // public static void Remove_Traits_Button_Click()
        // {
        //     traitsButtonClick("removeTraits", PowerType.remove);
        // }
        // private static void traitsButtonClick(string name, PowerType type)
        // {
        //     var pbSelector = Reflection.GetField(typeof(PowerButtonSelector), null, "instance") as PowerButtonSelector;
        //     if (pbSelector.isPowerSelected(name))
        //     {
        //         var newWindow = NCMS.Utils.Windows.GetWindow("addRemoveTraitsWindow");
        //         newWindow.clickShow();


        //         //var addRemoveTraits = new AddRemoveTraitsWindow();
        //         WorldBoxMod.addRemoveTraitsWindow.initAddRemoveTraits(newWindow, type);
        //     }
        //     else
        //     {
        //         pbSelector.unselectAll();
        //     }
        // }

        public static void Add_Items_Button_Click()
        {
            //Debug.LogWarning("Add items button clicked");
            itemsButtonClick("addItems", PowerType.add);
        }

        public static void Remove_Items_Button_Click()
        {
            //Debug.LogWarning("Remove items button clicked");
            itemsButtonClick("removeItems", PowerType.remove);
        }

        private static void itemsButtonClick(string name, PowerType type)
        {
            var pbSelector = Reflection.GetField(typeof(PowerButtonSelector), null, "instance") as PowerButtonSelector;
            if (pbSelector.isPowerSelected(name))
            {
                var newWindow = NCMS.Utils.Windows.GetWindow("editItems");
                newWindow.clickShow();

                WorldBoxMod.editItemsWindow.initEditItems(newWindow, null, type);
            }
            else
            {
                pbSelector.unselectAll();
            }
        }

        private static bool customLawsInited = false;
        private static void PowerBox_Laws_Click()
        {
            var newWindow = NCMS.Utils.Windows.GetWindow("powerBoxLawsWindow");
            newWindow.clickShow();

            if (!customLawsInited)
            {
                customLawsInited = true;
                WorldBoxMod.powerBoxLawsWindow.initPowerBoxLaws(newWindow);
                //initPowerBoxLaws(newWindow);
            }
        }


    }
}
