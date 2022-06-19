using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;
using UnityEngine;
using static Config;
using UnityEngine.UI;
using UnityEngine.Events;
using ai;

namespace PowerBox
{
    partial class PowerBoxLawsWindow : SelectableObjects
    {
        private static List<DisasterAsset> customDisasters = new List<DisasterAsset>();
        private static List<DisasterAsset> customDisastersPool;

        public PowerBoxLawsWindow(){
            var powerBoxLawsWindow = NCMS.Utils.Windows.CreateNewWindow("powerBoxLawsWindow", "PowerBox World Laws");
            //powerBoxLawsWindow.titleText.text = "PowerBox World Laws";
            powerBoxLawsWindow.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);


            Helper.GodPowerTab.CustomWorldLaws.Add("spawnMadCreaturesLaw", false);

            Helper.GodPowerTab.CustomWorldLaws.Add("upgradeBuildingsLaw", true);
            
            Helper.GodPowerTab.CustomWorldLaws.Add("moreDisastersLaw", false);

            Helper.GodPowerTab.CustomWorldLaws.Add("spawnInsectsLaw", true);

            Helper.GodPowerTab.CustomWorldLaws.Add("shootingTroughMountainsLaw", true);

            Helper.GodPowerTab.CustomWorldLaws.Add("imperialThinkingLaw", false);

            Helper.GodPowerTab.CustomWorldLaws.Add("civsReproductionLaw", true);

            Helper.GodPowerTab.CustomWorldLaws.Add("animalsReproductionLaw", true);

            Helper.GodPowerTab.CustomWorldLaws.Add("regenerationLaw", false);

            Helper.GodPowerTab.CustomWorldLaws.Add("virusApocalypseLaw", false);


            initDisasters();
        }

        #region disasters
        private void initDisasters()
        {

            DisasterAsset plagueInfectionDisaster = new DisasterAsset();
            plagueInfectionDisaster.id = "plague_infection";
            plagueInfectionDisaster.rate = 2;
            plagueInfectionDisaster.chance = 0.5f;
            plagueInfectionDisaster.world_log = "worldlog_disaster_plague_infection";
            plagueInfectionDisaster.world_log_icon = "iconPlague";
            plagueInfectionDisaster.min_world_population = 150;
            plagueInfectionDisaster.type = DisasterType.Other;
            plagueInfectionDisaster.action = new DisasterAction((asset) => infectionAction(asset, "plague"));
            customDisasters.Add(plagueInfectionDisaster);

            DisasterAsset zombieInfectionDisaster = new DisasterAsset();
            zombieInfectionDisaster.id = "zombie_infection";
            zombieInfectionDisaster.rate = 2;
            zombieInfectionDisaster.chance = 0.5f;
            zombieInfectionDisaster.world_log = "worldlog_disaster_zombie_infection";
            zombieInfectionDisaster.world_log_icon = "iconInfected";
            zombieInfectionDisaster.min_world_population = 150;
            zombieInfectionDisaster.type = DisasterType.Other;
            zombieInfectionDisaster.action = new DisasterAction((asset) => infectionAction(asset, "infected"));
            customDisasters.Add(zombieInfectionDisaster);

            DisasterAsset tumorInfectionDisaster = new DisasterAsset();
            tumorInfectionDisaster.id = "tumor_infection";
            tumorInfectionDisaster.rate = 2;
            tumorInfectionDisaster.chance = 0.5f;
            tumorInfectionDisaster.world_log = "worldlog_disaster_tumor_infection";
            tumorInfectionDisaster.world_log_icon = "iconTumorInfection";
            tumorInfectionDisaster.min_world_population = 150;
            tumorInfectionDisaster.type = DisasterType.Other;
            tumorInfectionDisaster.action = new DisasterAction((asset) => infectionAction(asset, "tumorInfection"));
            customDisasters.Add(tumorInfectionDisaster);

            DisasterAsset mushInfectionDisaster = new DisasterAsset();
            mushInfectionDisaster.id = "mush_infection";
            mushInfectionDisaster.rate = 2;
            mushInfectionDisaster.chance = 0.5f;
            mushInfectionDisaster.world_log = "worldlog_disaster_mush_infection";
            mushInfectionDisaster.world_log_icon = "iconMushSpores";
            mushInfectionDisaster.min_world_population = 150;
            mushInfectionDisaster.type = DisasterType.Other;
            mushInfectionDisaster.action = new DisasterAction((asset) => infectionAction(asset, "mushSpores"));
            customDisasters.Add(mushInfectionDisaster);



            DisasterAsset spawnBloodRainCloud = new DisasterAsset();
            spawnBloodRainCloud.id = "blood_rain_cloud";
            spawnBloodRainCloud.rate = 10;
            spawnBloodRainCloud.chance = 0.5f;
            spawnBloodRainCloud.world_log = "worldlog_disaster_blood_rain_cloud";
            spawnBloodRainCloud.world_log_icon = "blood_rain_cloud";
            spawnBloodRainCloud.type = DisasterType.Other;
            spawnBloodRainCloud.action = new DisasterAction((el) => spawnCloudAction(el, "cloudBloodRain"));
            customDisasters.Add(spawnBloodRainCloud);

            DisasterAsset spawnBurgerSpiderCloud = new DisasterAsset();
            spawnBurgerSpiderCloud.id = "burger_spider_cloud";
            spawnBurgerSpiderCloud.rate = 5;
            spawnBurgerSpiderCloud.chance = 0.5f;
            spawnBurgerSpiderCloud.world_log = "worldlog_disaster_burger_spider_cloud";
            spawnBurgerSpiderCloud.world_log_icon = "burger_spider_cloud";
            spawnBurgerSpiderCloud.type = DisasterType.Other;
            spawnBurgerSpiderCloud.action = new DisasterAction((el) => spawnCloudAction(el, "cloudBurgerSpider"));
            customDisasters.Add(spawnBurgerSpiderCloud);
        }
        #endregion

        internal void initPowerBoxLaws(ScrollWindow window)
        {
            var rt = spriteHighlighter.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(60f, 60f);


            var Content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                GameObject.Destroy(Content.transform.GetChild(i).gameObject);
            }

            var civBg = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/world_laws/Background/Scroll View/Viewport/Content/Civ");

            var tabCivs = GameObject.Instantiate(civBg, Content.transform);
            tabCivs.name = "Civilizations";
            for (int i = 0; i < tabCivs.transform.childCount; i++)
            {
                if(tabCivs.transform.GetChild(i).gameObject.name != "Title")
                {
                    GameObject.Destroy(tabCivs.transform.GetChild(i).gameObject);
                }
            }
            var title = tabCivs.transform.Find("Title");
            GameObject.Destroy(title.GetComponent<LocalizedText>());
            title.GetComponent<Text>().text = "Civilizations";
            tabCivs.transform.localPosition = new Vector2(130, -65);
            tabCivs.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 80);

            var tabNature = GameObject.Instantiate(tabCivs, Content.transform);
            tabNature.name = "Nature";
            for (int i = 0; i < tabNature.transform.childCount; i++)
            {
                if (tabNature.transform.GetChild(i).gameObject.name != "Title")
                {
                    GameObject.Destroy(tabNature.transform.GetChild(i).gameObject);
                }
            }
            var title2 = tabNature.transform.Find("Title");
            GameObject.Destroy(title2.GetComponent<LocalizedText>());
            title2.GetComponent<Text>().text = "Nature";
            tabNature.transform.localPosition = new Vector2(130, -170);
            tabNature.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 80);


            var lawButton = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/world_laws/Background/Scroll View/Viewport/Content/Civ/world_law_diplomacy");//.GetComponent<WorldLawElement>();

            Debug.LogWarning($"lawButton.name: {lawButton?.name}");
            //startYPos = -100f;
            //startXPos = 55f;//55
            //XStep = 37.7f;//37
            //YStep = -37f;
            //countInRow = 5;

            #region civs laws


            var btnScale = new Vector2(1.2f, 1.2f);
            startYPos = 0f;
            startXPos = -70f;
            XStep = 46.6f;
            YStep = -37f;
            countInRow = 4;

            //startYPos = 0f;
            //startXPos = -75f;
            //XStep = 37.5f;
            //YStep = -37f;
            //countInRow = 5;

            var upgradeBuildingsLaw = Helper.GodPowerTab.createWorldLaw(
                "upgradeBuildingsLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.upgrade_buildings.png", 0, 0),
                tabCivs.transform,
                upgradeBuildingsLawClick,
                "Upgrade Buildings",
                "If disabled, buildings can't be upgraded by units or by powers",
                lawButton/*.gameObject*/);
            upgradeBuildingsLaw.transform.localPosition = GetPosByIndex(0);
            upgradeBuildingsLaw.transform.localScale = btnScale;


            var shootingTroughMountainsLaw = Helper.GodPowerTab.createWorldLaw(
                "shootingTroughMountainsLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.shoot_through_mountains.png", 0, 0),
                tabCivs.transform,
                ToggleLaw,
                "Shooting Trough Mountains",
                "If enabled, units can attack each other trough mountains",
                lawButton.gameObject);
            shootingTroughMountainsLaw.transform.localPosition = GetPosByIndex(1);
            shootingTroughMountainsLaw.transform.localScale = btnScale;


            //var imperialThinkingLaw = Helper.GodPowerTab.createWorldLaw(
            //    "imperialThinkingLaw",
            //    Resources.Load<Sprite>("ui/icons/iconkings"),
            //    tabCivs.transform,
            //    ToggleLaw,
            //    "Imperial Thinking",
            //    "If enabled, different races will capture other cities instead of destroying them",
            //    lawButton.gameObject);
            //imperialThinkingLaw.transform.localPosition = GetPosByIndex(2);
            //imperialThinkingLaw.transform.localScale = btnScale;


            var civsReproducitonLaw = Helper.GodPowerTab.createWorldLaw(
                "civsReproductionLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.civs_reproduction.png", 0, 0),
                tabCivs.transform,
                ToggleLaw,
                "Villagers Reproduction",
                "If disabled, villagers will not be able to reproduce themselves",
                lawButton.gameObject);
            civsReproducitonLaw.transform.localPosition = GetPosByIndex(2);
            civsReproducitonLaw.transform.localScale = btnScale;

            var virusApocalypseLaw = Helper.GodPowerTab.createWorldLaw(
                "virusApocalypseLaw",
                Resources.Load<Sprite>("ui/icons/iconplague"),
                tabCivs.transform,
                ToggleLaw,
                "Virus apocalypse",
                "If enabled, died creatures have chance to turn into zombie/tumor/mush",
                lawButton.gameObject);
            virusApocalypseLaw.transform.localPosition = GetPosByIndex(3);
            virusApocalypseLaw.transform.localScale = btnScale;

            #endregion


            ResetWrapVals();

            #region nature laws

            // btnScale = new Vector2(1.00f, 1.00f);

            // startYPos = 0f;
            // startXPos = -75f;
            // XStep = 37.5f;
            // YStep = -37f;
            // countInRow = 5;

            btnScale = new Vector2(1.2f, 1.2f);
            startYPos = 0f;
            startXPos = -70f;
            XStep = 46.6f;
            YStep = -37f;
            countInRow = 4;

            var spawnMadCreaturesLaw = Helper.GodPowerTab.createWorldLaw(
                "spawnMadCreaturesLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.mad_animals.png", 0, 0),
                tabNature.transform,
                spawnMadCreaturesLawClick,
                "Mad Animals",
                "All existing and new spawned animals will be with madness",
                lawButton.gameObject);
            spawnMadCreaturesLaw.transform.localPosition = GetPosByIndex(0);
            spawnMadCreaturesLaw.transform.localScale = btnScale;


            var moreDisastersLaw = Helper.GodPowerTab.createWorldLaw(
                "moreDisastersLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.disasters.png", 0, 0),
                tabNature.transform,
                ToggleLaw,
                "More Disasters",
                "Additional disasters: plague, tumor, zombie and mush infections, burger-spider rain and blood rain",
                lawButton.gameObject);
            moreDisastersLaw.transform.localPosition = GetPosByIndex(1);
            moreDisastersLaw.transform.localScale = btnScale;


            // var spawnInsectsLaw = Helper.GodPowerTab.createWorldLaw(
            //     "spawnInsectsLaw",
            //     Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.insects_spawn.png", 0, 0),
            //     tabNature.transform,
            //     ToggleLaw,
            //     "Insect Spawn",
            //     "Insects can randomly appear in your world",
            //     lawButton.gameObject);
            // spawnInsectsLaw.transform.localPosition = GetPosByIndex(2);
            // spawnInsectsLaw.transform.localScale = btnScale;


            var animalsReproducitonLaw = Helper.GodPowerTab.createWorldLaw(
                "animalsReproductionLaw",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".laws.animals_reproduction.png", 0, 0),
                tabNature.transform,
                ToggleLaw,
                "Animal Reproduction",
                "If disabled, animals will not be able to reproduce themselves",
                lawButton.gameObject);
            animalsReproducitonLaw.transform.localPosition = GetPosByIndex(2);
            animalsReproducitonLaw.transform.localScale = btnScale;


            var regenerationLaw = Helper.GodPowerTab.createWorldLaw(
                "regenerationLaw",
                Resources.Load<Sprite>("ui/icons/iconregeneration"),
                tabNature.transform,
                regenerationLawClick,
                "Regeneration",
                "All creatures will have regeneration",
                lawButton.gameObject);
            regenerationLaw.transform.localPosition = GetPosByIndex(3);
            regenerationLaw.transform.localScale = btnScale;

            #endregion

            ResetWrapVals();

            void ToggleLaw(WorldLawElement elem)
            {
                var id = getLawId(elem);

                Helper.GodPowerTab.CustomWorldLaws[id] = !Helper.GodPowerTab.CustomWorldLaws[id];

                Helper.GodPowerTab.updateIcon(elem);
            }
        }

        private string getLawId(WorldLawElement worldLawEl)
        {
            return Reflection.GetField(worldLawEl.GetType(), worldLawEl, "lawID") as string;
        }


        #region spawnMadCreaturesLaw actions
        private void spawnMadCreaturesLawClick(WorldLawElement worldLawEl)
        {
            var lawID = getLawId(worldLawEl);

            Helper.GodPowerTab.CustomWorldLaws[lawID] = !Helper.GodPowerTab.CustomWorldLaws[lawID];

            var actors = MapBox.instance.units.getSimpleList();

            foreach(var actor in actors)
            {
                if(madUnitsId.Contains(actor.stats.id))
                {
                    if (Helper.GodPowerTab.CustomWorldLaws[lawID])
                    {
                        actor.addTrait("madness");
                    }
                    else
                    {
                        actor.removeTrait("madness");
                    }
                }
            }
            //AssetManager.unitStats.init();

            Helper.GodPowerTab.updateIcon(worldLawEl);
        }

        static string[] madUnitsId = { "sheep", "cow", "penguin", "turtle", "crab", "grasshopper", "beetle", "chicken", "chick", "rat", "ratKing", "cat", "rabbit", "bear", "wolf", "piranha" /*"bee", "fly", "butterfly",*/ };
        public static void createNewUnitBySpawning_Postfix(Actor __result)
        {
            if (madUnitsId.Contains(__result.stats.id))
            {
                if (Helper.GodPowerTab.CustomWorldLaws["spawnMadCreaturesLaw"])
                    __result.addTrait("madness");
            }


            if (Helper.GodPowerTab.CustomWorldLaws["regenerationLaw"])
                __result.addTrait("regeneration");
        }

        public static void createNewUnit_Postfix(Actor __result)
        {
            if (madUnitsId.Contains(__result.stats.id))
            {
                if (Helper.GodPowerTab.CustomWorldLaws["spawnMadCreaturesLaw"])
                    __result.addTrait("madness");
            }


            if (Helper.GodPowerTab.CustomWorldLaws["regenerationLaw"])
                __result.addTrait("regeneration");

        }

        #endregion


        #region upgradeBuildingsLaw actions

        private string[] buildingUpgradeToggle = { "tent_", "house_", "1house_", "2house_", "3house_", "4house_", "hall_", "1hall_"};
        private string[] racePostfix = { "human", "elf", "dwarf", "orc" };

        private void upgradeBuildingsLawClick(WorldLawElement worldLawEl)
        {
            var lawID = getLawId(worldLawEl);

            Helper.GodPowerTab.CustomWorldLaws[lawID] = !Helper.GodPowerTab.CustomWorldLaws[lawID];

            for (int i = 0; i < buildingUpgradeToggle.Length; i++)
            {
                for (int j = 0; j < racePostfix.Length; j++)
                {
                    var building = AssetManager.buildings.get(buildingUpgradeToggle[i] + racePostfix[j]);

                    building.canBeUpgraded = Helper.GodPowerTab.CustomWorldLaws[lawID];
                }
            }

            Helper.GodPowerTab.updateIcon(worldLawEl);
        }

        #endregion


        #region moreDisastersLaw actions

        public static DisasterAsset getRandomAssetFromPool_Postfix(DisasterAsset __result)
        {

            if (customDisastersPool == null || customDisastersPool.Count == 0)
            {
                customDisastersPool = new List<DisasterAsset>();
                foreach (DisasterAsset disasterAsset in customDisasters)
                {
                    for (int index = 0; index < disasterAsset.rate; ++index)
                        customDisastersPool.Add(disasterAsset);
                }
            }

            var defaultPool = Reflection.GetField(AssetManager.disasters.GetType(), AssetManager.disasters, "_random_pool") as List<DisasterAsset>;

            if (defaultPool != null && defaultPool.Count != 0 && (!Toolbox.randomChance((float)1 / (float)((float)defaultPool.Count + (float)customDisastersPool.Count) * (float)customDisastersPool.Count)))
                return __result;

            DisasterAsset random = customDisastersPool.GetRandom<DisasterAsset>();

            if (random.min_world_population > MapBox.instance.getCivWorldPopulation())
                return __result;

            if (!Helper.GodPowerTab.CustomWorldLaws["moreDisastersLaw"])
                return __result;

            //var asset = Toolbox.getRandom<DisasterAsset>(customDisasters);

            return random;
        }

        #region infectionAction
        public static void infectionAction(DisasterAsset pAsset, string infectionTraitId)
        {
            City random = MapBox.instance.citiesList.GetRandom<City>();
            if (random == null || random.getPopulationUnits() < 50 || random.getTile() == null)
                return;

            List<Actor> actorList = new List<Actor>();
            List<Actor> simpleList = random.units.getSimpleList();
            simpleList.Shuffle<Actor>();
            foreach (Actor actor in simpleList)
            {
                if (Toolbox.randomChance(0.05f))
                    actorList.Add(actor);
            }

            foreach (ActorBase actorBase in actorList)
                actorBase.addTrait(infectionTraitId);

            var text = "Oh no! Some villagers are sick with the plague infection! No one knows how they got it... ";
            var icon = Resources.Load<Sprite>("ui/icons/iconplague");

            switch (infectionTraitId)
            {
                case "plague":
                    text = "Oh no! Some villagers are sick with the plague infection! No one knows how they got it... ";
                    icon = Resources.Load<Sprite>("ui/icons/iconplague");
                    break;
                case "infected":
                    text = "Oh no! Some villagers are sick with the zombie virus! No one knows how they got it... ";
                    icon = Resources.Load<Sprite>("ui/icons/iconinfected");
                    break;
                case "tumorInfection":
                    text = "Oh no! Some villagers are sick with the tumor infection! No one knows how they got it... ";
                    icon = Resources.Load<Sprite>("ui/icons/icontumorinfection");
                    break;
                case "mushSpores":
                    text = "Oh no! Some villagers are sick with the mush spores! No one knows how they got it... ";
                    icon = Resources.Load<Sprite>("ui/icons/iconmushspores");
                    break;
            }

            Helper.HisHud.newText(text, Toolbox.color_infected, icon);


        }

        #endregion

        #region spawnBurgerSpiderCloudAction

        public static void spawnCloudAction(DisasterAsset pAsset, string dropID) 
        {
            GodPower godPower = AssetManager.powers.get("cloudAcid");
            WorldTile random = MapBox.instance.tilesList.GetRandom<WorldTile>();
            var cloud = MapBox.instance.cloudController.getNext();
            var method = cloud.GetType().GetMethod("prepare", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new[] { typeof(Vector3), typeof(string) }, null);
            method.Invoke(cloud, new object[] { random.posV3, godPower.id });
            //cloud.CallMethod("prepare", random.posV3, godPower.id);
            Reflection.SetField<string>(cloud, "dropID", dropID);

            //MapBox.instance.cloudController.CallMethod("spawnCloud", random.posV3, godPower.id);

            switch (dropID)
            {
                case "cloudBurgerSpider":
                    cloud.sprRenderer.color = new Color(0.82f, 0.45f, 0.10f, 0.77f);
                    Helper.HisHud.newText("Oh no! Burger-spider rain!?.", Toolbox.color_log_warning, Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".units.burgerSpider.icon.png", 0, 0));
                    break;
                case "cloudBloodRain":
                    cloud.sprRenderer.color = new Color(0.76f, 0.09f, 0.09f, 0.77f);
                    Helper.HisHud.newText("Oh yes, blessed healing blood rain!", Toolbox.color_log_warning, Resources.Load<Sprite>("ui/icons/iconbloodrain"));
                    break;
            }


        }

        #endregion

        #endregion


        #region spawnInsectsLaw actions

        static string[] insects = { "bee", "fly", "butterfly", "beetle", "grasshopper" };
        public static bool updateAnimalSpawn_Prefix()
        {
            var mapChunkManager = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "mapChunkManager") as MapChunkManager;

            if (!MapBox.instance.worldLaws.world_law_animals_spawn.boolVal || mapChunkManager.list.Count == 0)
                return false;

            MapChunk random1 = mapChunkManager.list.GetRandom<MapChunk>();
            if (random1.regions.Count == 0)
                return false;
            MapRegion random2 = random1.regions.GetRandom<MapRegion>();
            if (random2.island.regions.Count < 5)
                return false; ;
            WorldTile random3 = random2.tiles.GetRandom<WorldTile>();
            if (random3.Type.spawn_units_list.Count == 0)
                return false;

            if (random3.building == null)
                return false;

            var buidingStats = Reflection.GetField(random3.building.GetType(), random3.building, "stats") as BuildingAsset;
            ActorStats actorStats = !(random3.building != null) || !random3.building.isNonUsable() || !buidingStats.spawnRats ? AssetManager.unitStats.get(random3.Type.spawn_units_list.GetRandom<string>()) : AssetManager.unitStats.get(AssetManager.unitStats.spawnInRuins.GetRandom<string>());
            if (actorStats == null || actorStats.currentAmount > actorStats.maxRandomAmount)
                return false;

            if (insects.Contains(actorStats.id) && !Helper.GodPowerTab.CustomWorldLaws["spawnInsectsLaw"])
                return false;

            MapBox.instance.CallMethod("getObjectsInChunks", random3, 0, MapObjectType.Actor);
            var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
            if (temp_map_objects.Count > 3)
                return false;

            MapBox.instance.createNewUnit(actorStats.id, random3, null, 0.0f, null);

            return false;
        }

        public static bool spawnUnit_Prefix(UnitSpawner __instance)
        {

            if (__instance.units_current >= __instance.units_limit)
                return false;

            var buidling = Reflection.GetField(__instance.GetType(), __instance, "building") as Building;
            var buidlingStats = Reflection.GetField(buidling.GetType(), buidling, "stats") as BuildingAsset;

            if (insects.Contains(buidlingStats.spawnUnits_asset) && !Helper.GodPowerTab.CustomWorldLaws["spawnInsectsLaw"])
                return false;

            __instance.setUnitFromHere(MapBox.instance.createNewUnit(buidlingStats.spawnUnits_asset, buidling.currentTile, null, 0.0f, null));

            return false;
        }

        private void spawnInsectsLawClick(WorldLawElement worldLawEl)
        {
            var lawID = getLawId(worldLawEl);

            Helper.GodPowerTab.CustomWorldLaws[lawID] = !Helper.GodPowerTab.CustomWorldLaws[lawID];

            for (int i = 0; i < buildingUpgradeToggle.Length; i++)
            {
                for (int j = 0; j < racePostfix.Length; j++)
                {
                    var building = AssetManager.buildings.get(buildingUpgradeToggle[i] + racePostfix[j]);

                    building.canBeUpgraded = Helper.GodPowerTab.CustomWorldLaws[lawID];
                }
            }

            worldLawEl.CallMethod("updateStatus");
        }

        #endregion


        #region shootingTroughMountainsLaw actions
        public static bool canAttackTarget_Postfix(bool __result, BaseSimObject __instance, BaseSimObject pTarget, MapBox ___world, Actor ___a)
        {
            bool sResult = __result;

            //var a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if(___a == null)
            {
                return sResult;
            }

            var s_attackType = (WeaponType)Reflection.GetField(___a.GetType(), ___a, "s_attackType");


            if (Helper.GodPowerTab.CustomWorldLaws["shootingTroughMountainsLaw"] || s_attackType != WeaponType.Range)
            {
                return __result;
            }

            bool isMountainBetween = false;

            var nomalizeVector = (pTarget.currentPosition - __instance.currentPosition).normalized;
            var currentPos = __instance.currentPosition;

            for (int i = 0; i < Mathf.RoundToInt(Toolbox.DistVec3(__instance.currentPosition, pTarget.currentPosition)); i++)
            {
                var tileName = ___world.GetTile(Mathf.RoundToInt(currentPos.x), Mathf.RoundToInt(currentPos.y)).main_type.id;


                if (tileName == "mountains")
                {
                    isMountainBetween = true;
                    break;
                }

                currentPos += nomalizeVector;
            }

            sResult = __result && !isMountainBetween;

            //if (Helper.GodPowerTab.CustomWorldLaws["imperialThinkingLaw"])
            //{
            //    if (pTarget.objectType == MapObjectType.Actor && !___world.worldLaws.world_law_angry_civilians.boolVal)
            //    {
            //        var a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

            //        if(a != null)
            //        {
            //            var aProfessionAsset = Reflection.GetField(a.GetType(), a, "professionAsset") as ProfessionAsset;
            //            var ___aProfessionAsset = Reflection.GetField(___a.GetType(), ___a, "professionAsset") as ProfessionAsset;

            //            if (sResult && a.kingdom.isCiv() && ___a.kingdom.isCiv())
            //            {
            //                return !___world.worldLaws.world_law_angry_civilians.boolVal && (aProfessionAsset.is_civilian || ___aProfessionAsset.is_civilian);
            //            }
            //        }
            //    }
            //    else if (pTarget.objectType == MapObjectType.Building)
            //    {
            //        if (sResult && ___a.kingdom.isCiv() && pTarget.kingdom.isCiv())
            //        {
            //            return false;
            //        }
            //        //Debug.Log("Building");
            //    }
            //}

            return sResult;
        }
        #endregion


        // #region imperialThinkingLaw action
        // public static bool updateConquest_Prefix(Actor pActor, Kingdom ___kingdom, Dictionary<Kingdom, int> ___capturingUnits)
        // {
        //     //var ___kingdom = Reflection.GetField(__instance.GetType(), __instance, "kingdom") as Kingdom;
        //     //var ___capturingUnits = Reflection.GetField(__instance.GetType(), __instance, "capturingUnits") as Dictionary<Kingdom, int>;

        //     var thisKingdomRace = Reflection.GetField(___kingdom.GetType(), ___kingdom, "race") as Race;
        //     var pActorKingdomRace = Reflection.GetField(pActor.kingdom.GetType(), pActor.kingdom, "race") as Race;

        //     var isEnemy = (bool)pActor.kingdom.CallMethod("isEnemy", ___kingdom);

        //     bool res = !pActor.kingdom.isCiv() || pActorKingdomRace != thisKingdomRace || pActor.kingdom != ___kingdom && !isEnemy;

        //     if (Helper.GodPowerTab.CustomWorldLaws["imperialThinkingLaw"])
        //         res = !pActor.kingdom.isCiv() || pActor.kingdom != ___kingdom && !isEnemy;

        //     if (res)
        //         return false;
        //     if (!___capturingUnits.ContainsKey(pActor.kingdom))
        //         ___capturingUnits[pActor.kingdom] = 1;
        //     else
        //         ___capturingUnits[pActor.kingdom]++;


        //     return false;
        // }
        // #endregion


        #region animalsReproducitonLaw action

        public static bool tryToProduceUnit_Prefix()
        {
            return Helper.GodPowerTab.CustomWorldLaws["civsReproductionLaw"];
        }

        public static bool tryStartBabymaking_Prefix(Actor pActor)
        {
            return !(!pActor.kingdom.isCiv() && !Helper.GodPowerTab.CustomWorldLaws["animalsReproductionLaw"]);
        }

        #endregion


        #region regenerationLaw action
        private void regenerationLawClick(WorldLawElement worldLawEl)
        {
            var lawID = getLawId(worldLawEl);

            Helper.GodPowerTab.CustomWorldLaws[lawID] = !Helper.GodPowerTab.CustomWorldLaws[lawID];

            var actors = MapBox.instance.units.getSimpleList();

            foreach (var actor in actors)
            {
                if (new string[] { "dragon", "unit_orc", "snowman", "snowman", "demon", "zombie", "necromancer", "druid", "plagueDoctor", "whiteMage", "evilMage", "mush_unit", "snowman", "walker", "livingPlants" }.Contains(actor.stats.id))
                {
                    if (Helper.GodPowerTab.CustomWorldLaws[lawID])
                    {
                        Debug.Log("Added to " + actor.stats.id);
                        actor.addTrait("regeneration");
                    }
                    else
                    {
                        actor.removeTrait("regeneration");
                    }
                }
            }
            //AssetManager.unitStats.init();

            Helper.GodPowerTab.updateIcon(worldLawEl);
        }
        #endregion


        #region virusApocalypseLaw actions
        public static void killHimself_Postfix(Actor __instance)
        {
            if (!Helper.GodPowerTab.CustomWorldLaws["virusApocalypseLaw"])
                return;


            var data = Reflection.GetField(__instance.GetType(), __instance, "data") as ActorStatus;

            var rnd = UnityEngine.Random.Range(0, 100);
            if (!data.alive && !__instance.haveTrait("infected") && !__instance.haveTrait("mushSpores") && !__instance.haveTrait("tumorInfection"))
            {

                if (rnd < 33 && __instance.stats.canTurnIntoZombie)
                {
                    turnIntoVirus(__instance, "zombie");
                    //ActionLibrary.turnIntoZombie(__instance);
                }
                else if (rnd < 66 && __instance.stats.canTurnIntoTumorMonster)
                {
                    turnIntoVirus(__instance, "tumor");
                    //ActionLibrary.turnIntoTumorMonster(__instance);
                }
                else if (rnd < 100 && __instance.stats.canTurnIntoMush)
                {
                    turnIntoVirus(__instance, "mush");
                    //ActionLibrary.turnIntoMush(__instance);
                }
            }

        }

        private static bool turnIntoVirus(BaseSimObject pTarget, string pType)
        {
            var a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a.gameObject == null || a == null || !a.inMapBorder())
                return false;
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("tumorInfection");
            string pStatsID = "";

            switch (pType)
            {
                case "zombie":
                    pStatsID = !string.IsNullOrEmpty(a.stats.zombieID) ? a.stats.zombieID : "zombie";

                    if (a.stats.id == "dragon")
                    {
                        a.removeTrait("fire_blood");
                        a.removeTrait("fire_proof");
                    }

                    break;
                case "tumor":
                    pStatsID = a.stats.tumorMonsterID;
                    break;
                case "mush":
                    pStatsID = a.stats.mushID;
                    break;
                default:
                    pStatsID = !string.IsNullOrEmpty(a.stats.zombieID) ? a.stats.zombieID : "zombie";
                    break;
            }

            Actor newUnit = MapBox.instance.createNewUnit(pStatsID, a.currentTile, (string)null, 0.0f, (ActorData)null);
            ActorTool.copyUnitToOtherUnit(a, newUnit);

            var aData = Reflection.GetField(a.GetType(), a, "data") as ActorStatus;
            var newData = Reflection.GetField(newUnit.GetType(), newUnit, "data") as ActorStatus;
            newData.firstName = "Un" + Toolbox.LowerCaseFirst(aData.firstName);
            var qualityChanger = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "qualityChanger") as QualityChanger;
            var lowRes = (bool)Reflection.GetField(qualityChanger.GetType(), qualityChanger, "lowRes");

            if (!lowRes)
                MapBox.instance.stackEffects.CallMethod("startSpawnEffect", newUnit.currentTile, "spawn");

            MapBox.instance.destroyActor((Actor)pTarget);

            return true;
        }
        #endregion
    }
}
