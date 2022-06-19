using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Config;
using ReflectionUtility;
using System.Reflection;
using UnityEngine.Tilemaps;
using System.IO;
using NCMS;

namespace PowerBox
{
    //[BepInPlugin(id, "PowerBox", "0.1.1.0")]
    [ModEntry]
    public partial class WorldBoxMod : MonoBehaviour
    {
        public const string resources = "PowerBox.Resources";

        public const string id = "nikon.mods.worldbox.powerbox";
        public Harmony harmony;


        private bool initializedUpdate = false;
        private bool initializedGui = false;


        public void Awake()
        {
            harmony = new Harmony(id);
            Helper.GodPowerTab.patch(harmony);
            Patching(harmony);

            //Helper.GodPowerTab.init();
        }

        public void Update()
        {
            if (!gameLoaded) return;

            
            if (!initializedUpdate)
            {
                MODDED = true;
                //Helper.Windows.init();
                initializedUpdate = true;
            }



        }

        private void OnGUI() {
            if (!gameLoaded) return;

            if (!initializedGui)
            {
                Helper.GodPowerTab.init();
                createButtons();
                initWindows();
                initializedGui = true;
            }
        }

        

        private void Patching(Harmony harmony)
        {
            //Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(PowerButton), "Awake"), AccessTools.Method(typeof(Helper.GodPowerTab), "Awake_Prefix"));
            //Debug.Log("Pre patch: PowerButton.Awake");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(WindowCreatureInfo), "loadTraits"), AccessTools.Method(typeof(WorldBoxMod), "loadTraits_prefix"));
            Debug.Log("Prefix: WindowCreatureInfo.loadTraits");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(ActorAnimationLoader), "generateAnimation"), AccessTools.Method(typeof(WorldBoxMod), "generateAnimation_Postfix"));
            Debug.Log("Postfix: ActorAnimationLoader.generateAnimation");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(PowerButton), "unselectActivePower"), AccessTools.Method(typeof(WorldBoxMod), "unselectActivePower_Postfix"));
            Debug.Log("Postfix: PowerButton.unselectActivePower");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(HoveringBgIconManager), "OnEnable"), AccessTools.Method(typeof(WorldBoxMod), "OnEnable_Prefix"));
            Debug.Log("Prefix: HoveringBgIconManager.OnEnable");

            // Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(BannerLoader), "load"), AccessTools.Method(typeof(WorldBoxMod), "load_Postfix"));
            // Debug.Log("Postfix: BannerLoader.load");

            // Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(SaveManager), "loadData"), AccessTools.Method(typeof(WorldBoxMod), "loadData_Postfix"));
            // Debug.Log("Postfix: SaveManager.load");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(MapBox), "spawnNewUnit"), AccessTools.Method(typeof(PowerBoxLawsWindow), "createNewUnitBySpawning_Postfix"));
            Debug.Log("Postfix: MapBox.spawnNewUnit");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(MapBox), "createNewUnit"), AccessTools.Method(typeof(PowerBoxLawsWindow), "createNewUnit_Postfix"));
            Debug.Log("Postfix: MapBox.createNewUnit");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(DisasterLibrary), "getRandomAssetFromPool"), AccessTools.Method(typeof(PowerBoxLawsWindow), "getRandomAssetFromPool_Postfix"));
            Debug.Log("Postfix: DisasterLibrary.getRandomAssetFromPool");

            // Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(WorldBehaviour), "updateAnimalSpawn"), AccessTools.Method(typeof(WorldBoxMod), "updateAnimalSpawn_Prefix"));
            // Debug.Log("Prefix: WorldBehaviour.updateAnimalSpawn");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(UnitSpawner), "spawnUnit"), AccessTools.Method(typeof(PowerBoxLawsWindow), "spawnUnit_Prefix"));
            Debug.Log("Prefix: UnitSpawner.spawnUnit");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(BaseSimObject), "canAttackTarget"), AccessTools.Method(typeof(PowerBoxLawsWindow), "canAttackTarget_Postfix"));
            Debug.Log("Postfix: Actor.canAttackTarget");

            //Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(City), "updateConquest"), AccessTools.Method(typeof(WorldBoxMod), "updateConquest_Prefix"));
            //Debug.Log("Prefix: City.updateConquest");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(ai.behaviours.CityBehProduceUnit), "tryToProduceUnit"), AccessTools.Method(typeof(PowerBoxLawsWindow), "tryToProduceUnit_Prefix"));
            Debug.Log("Prefix: ai.behaviours.CityBehProduceUnit.tryToProduceUnit_Prefix");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(ai.behaviours.BehTryBabymaking), "tryStartBabymaking"), AccessTools.Method(typeof(PowerBoxLawsWindow), "tryStartBabymaking_Prefix"));
            Debug.Log("Prefix: ai.behaviours.BehTryBabymaking.tryStartBabymaking");

            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "killHimself"), AccessTools.Method(typeof(PowerBoxLawsWindow), "killHimself_Postfix"));
            Debug.Log("Postfix: Actor.killHimself");

            
        }

        internal static Actor UNIT;
        internal static ActorStatus data;
        public static void loadTraits_prefix(WindowCreatureInfo __instance)
        {
            UNIT = selectedUnit;
            data = (ActorStatus)Reflection.GetField(UNIT.GetType(), UNIT, "data");
        }

        public static List<string> texturePathes = new List<string>();
        public static AnimationDataUnit generateAnimation_Postfix(AnimationDataUnit __result, ActorAnimationLoader __instance, string pSheetPath, ActorStats pStats, Dictionary<string, AnimationDataUnit> ___dict_units)
        {
            if (!texturePathes.Any(x => x == pSheetPath.Remove(0, 7))) return __result;

            AnimationDataUnit pAnimData = new AnimationDataUnit();
            Reflection.SetField(pAnimData, "sprites", new Dictionary<string, Sprite>());
            var sprites = Reflection.GetField(pAnimData.GetType(), pAnimData, "sprites") as Dictionary<string, Sprite>;

            var names = new string[6] { "swim_0", "swim_1", "swim_2", "walk_0", "walk_1", "walk_2" };

            foreach (string name in names)
            {
                var sprite = Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.units.{pStats.id}.{name}.png", 0.5f, 0);
                sprite.name = name;
                sprites.Add(name, sprite);
            }

            Reflection.SetField(pAnimData, "frameData", new Dictionary<string, AnimationFrameData>());

            Reflection.SetField(pAnimData, "id", pSheetPath);

            Reflection.CallStaticMethod(typeof(ActorAnimationLoader), "generateFrameData", pAnimData, sprites, "walk_0,walk_1,walk_2,walk_3,swim_0,swim_1,swim_2,swim_3");

            ___dict_units.Remove(pSheetPath);
            ___dict_units.Add(pSheetPath, pAnimData);

            if (pStats.animation_swim != "")
            {

                ActorAnimation anim = (ActorAnimation)Reflection.CallStaticMethod(typeof(ActorAnimationLoader), "createAnim", 0, sprites, pStats.animation_swim, pStats.animation_swim_speed);
                Reflection.SetField(pAnimData, "swimming", anim);
            }
            if (pStats.animation_walk != "")
            {
                ActorAnimation anim = (ActorAnimation)Reflection.CallStaticMethod(typeof(ActorAnimationLoader), "createAnim", 1, sprites, pStats.animation_walk, pStats.animation_walk_speed);
                Reflection.SetField(pAnimData, "walking", anim);
            }
            if (pStats.animation_idle != "")
            {
                ActorAnimation anim = (ActorAnimation)Reflection.CallStaticMethod(typeof(ActorAnimationLoader), "createAnim", 2, sprites, pStats.animation_idle, pStats.animation_idle_speed);
                Reflection.SetField(pAnimData, "idle", anim);
            }

            return pAnimData;
        }

        public static void unselectActivePower_Postfix(PowerButton __instance)
        {
            string message = "";
            if (__instance.name == "friendshipNR" && PeaceInitiator != null)
            {
                message = "Kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(PeaceInitiator), true) + ">" + PeaceInitiator.name + "</color> changed its mind about the truce...";

                PeaceInitiator = null;
            }
            else if (__instance.name == "spiteNR" && WarInitiator != null)
            {
                message = "Kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(WarInitiator), true) + ">" + WarInitiator.name + "</color> changed its mind about declaring war...";

                WarInitiator = null;
            }

            if (message != "") Helper.KingdomThings.newText(message, Toolbox.color_log_neutral);
        }

        public static void OnEnable_Prefix(HoveringBgIconManager __instance)
        {
            var places = Reflection.GetField(__instance.GetType(), __instance, "places") as List<Transform>;

            var toRemove = new List<int>();
            for (int i = 0; i < places.Count; i++)
            {
                if (Mathf.RoundToInt(places[i].localPosition.x) == 124f || Mathf.RoundToInt(places[i].localPosition.x) == 138f)
                {
                    toRemove.Add(i);
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                places.RemoveAt(toRemove[i]);
            }
        }

        // public static void load_Postfix(BannerLoader __instance, Kingdom pKingdom)
        // {
        //     if (EditBannerWindow.customBanners.ContainsKey(pKingdom.id))
        //     {
        //         __instance.partBackround.sprite = EditBannerWindow.customBanners[pKingdom.id].CurrentBG.sprite;
        //         __instance.partIcon.sprite = EditBannerWindow.customBanners[pKingdom.id].CurrentIcon.sprite;
        //     }
        // }


        // public static void loadData_Postfix()
        // {
        //     EditBannerWindow.customBanners = new Dictionary<string, customBanner>();
        // }

        public static void load_Prefix(string pPath, BuildingSpritesContainer __instance, Dictionary<string, BuildingAnimationData> ___dict, Dictionary<string, Sprite> ___dict_sprites)
        {
            Debug.Log(pPath);
        }
    }
}
