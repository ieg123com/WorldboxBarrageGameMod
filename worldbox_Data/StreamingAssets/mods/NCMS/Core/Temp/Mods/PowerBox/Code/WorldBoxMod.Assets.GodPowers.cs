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
using System.Collections;

namespace PowerBox
{
    partial class WorldBoxMod
    {
        private static void initGodPowersAssets()
        {
            #region unitsSpawns

            GodPower burgerSpider = new GodPower();
            burgerSpider.id = "spawnBurgerSpider";
            burgerSpider.rank = PowerRank.Rank0_free;
            burgerSpider.unselectWhenWindow = true;
            burgerSpider.showSpawnEffect = "spawn";
            burgerSpider.actorSpawnHeight = 3f;
            burgerSpider.name = "spawnBurgerSpider";
            burgerSpider.spawnSound = "spawnAnt";
            burgerSpider.actorStatsId = "burgerSpider";
            burgerSpider.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(burgerSpider);


            GodPower MaximCreature = new GodPower();
            MaximCreature.id = "spawnMaximCreature";
            MaximCreature.rank = PowerRank.Rank0_free;
            MaximCreature.unselectWhenWindow = true;
            MaximCreature.showSpawnEffect = "spawn";
            MaximCreature.actorSpawnHeight = 3f;
            MaximCreature.name = "spawnMaximCreature";
            MaximCreature.spawnSound = "spawnHuman";
            MaximCreature.actorStatsId = "MaximCreature";
            MaximCreature.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(MaximCreature);



            GodPower MastefCreature = new GodPower();
            MastefCreature.id = "spawnMastefCreature";
            MastefCreature.rank = PowerRank.Rank0_free;
            MastefCreature.unselectWhenWindow = true;
            MastefCreature.showSpawnEffect = "spawn";
            MastefCreature.actorSpawnHeight = 3f;
            MastefCreature.name = "spawnMastefCreature";
            MastefCreature.spawnSound = "spawnHuman";
            MastefCreature.actorStatsId = "MastefCreature";
            MastefCreature.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(MastefCreature);


            GodPower GregCreature = new GodPower();
            GregCreature.id = "spawnGregCreature";
            GregCreature.rank = PowerRank.Rank0_free;
            GregCreature.unselectWhenWindow = true;
            GregCreature.showSpawnEffect = "spawn";
            GregCreature.actorSpawnHeight = 3f;
            GregCreature.name = "spawnGregCreature";
            GregCreature.spawnSound = "spawnHuman";
            GregCreature.actorStatsId = "greg";
            GregCreature.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(GregCreature);


            GodPower TumorCreatureUnit = new GodPower();
            TumorCreatureUnit.id = "spawnTumorCreatureUnit";
            TumorCreatureUnit.name = "spawnTumorCreatureUnit";
            TumorCreatureUnit.rank = PowerRank.Rank0_free;
            TumorCreatureUnit.unselectWhenWindow = true;
            TumorCreatureUnit.showSpawnEffect = "spawn";
            TumorCreatureUnit.actorSpawnHeight = 3f;
            TumorCreatureUnit.spawnSound = "Tumor_Spawn01";
            TumorCreatureUnit.actorStatsId = "tumor_monster_unit";
            TumorCreatureUnit.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(TumorCreatureUnit);


            GodPower TumorCreatureAnimal = new GodPower();
            TumorCreatureAnimal.id = "spawnTumorCreatureAnimal";
            TumorCreatureAnimal.name = "spawnTumorCreatureAnimal";
            TumorCreatureAnimal.rank = PowerRank.Rank0_free;
            TumorCreatureAnimal.unselectWhenWindow = true;
            TumorCreatureAnimal.showSpawnEffect = "spawn";
            TumorCreatureAnimal.actorSpawnHeight = 3f;
            TumorCreatureAnimal.spawnSound = "Tumor_Spawn01";
            TumorCreatureAnimal.actorStatsId = "tumor_monster_animal";
            TumorCreatureAnimal.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(TumorCreatureAnimal);


            GodPower MushCreatureUnit = new GodPower();
            MushCreatureUnit.id = "spawnMushCreatureUnit";
            MushCreatureUnit.name = "spawnMushCreatureUnit";
            MushCreatureUnit.rank = PowerRank.Rank0_free;
            MushCreatureUnit.unselectWhenWindow = true;
            MushCreatureUnit.showSpawnEffect = "spawn";
            MushCreatureUnit.actorSpawnHeight = 3f;
            MushCreatureUnit.spawnSound = "spawnZombie";
            MushCreatureUnit.actorStatsId = "mush_unit";
            MushCreatureUnit.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(MushCreatureUnit);


            GodPower MushCreatureAnimal = new GodPower();
            MushCreatureAnimal.id = "spawnMushCreatureAnimal";
            MushCreatureAnimal.name = "spawnMushCreatureAnimal";
            MushCreatureAnimal.rank = PowerRank.Rank0_free;
            MushCreatureAnimal.unselectWhenWindow = true;
            MushCreatureAnimal.showSpawnEffect = "spawn";
            MushCreatureAnimal.actorSpawnHeight = 3f;
            MushCreatureAnimal.spawnSound = "spawnZombie";
            MushCreatureAnimal.actorStatsId = "mush_animal";
            MushCreatureAnimal.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("spawnUnit", pTile, pPower); });
            AssetManager.powers.add(MushCreatureAnimal);


            #endregion


            #region traits

            // GodPower addTraits = new GodPower();
            // addTraits.id = "addTraits";
            // addTraits.holdAction = true;
            // addTraits.showToolSizes = true;
            // addTraits.unselectWhenWindow = true;
            // addTraits.name = "addTraits";
            // addTraits.dropID = "addTraits";
            // addTraits.fallingChance = 0.01f;
            // addTraits.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            // addTraits.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            // AssetManager.powers.add(addTraits);


            // GodPower removeTraits = new GodPower();
            // removeTraits.id = "removeTraits";
            // removeTraits.holdAction = true;
            // removeTraits.showToolSizes = true;
            // removeTraits.unselectWhenWindow = true;
            // removeTraits.name = "removeTraits";
            // removeTraits.dropID = "addTraits";
            // removeTraits.fallingChance = 0.01f;
            // removeTraits.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            // removeTraits.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            // AssetManager.powers.add(removeTraits);


            // DropAsset addTraitsDrop = new DropAsset();
            // addTraitsDrop.id = "addTraits";
            // addTraitsDrop.path_texture = "drops/drop_fireworks";
            // addTraitsDrop.animated = true;
            // addTraitsDrop.animation_speed = 0.03f;
            // addTraitsDrop.default_scale = 0.1f;
            // addTraitsDrop.action_landed = new DropsAction(action_addTraits);
            // AssetManager.drops.add(addTraitsDrop);

            #endregion


            #region items

            GodPower addItems = new GodPower();
            addItems.id = "addItems";
            addItems.holdAction = true;
            addItems.showToolSizes = true;
            addItems.unselectWhenWindow = true;
            addItems.name = "addItems";
            addItems.dropID = "addItems";
            addItems.fallingChance = 0.01f;
            addItems.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            addItems.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(addItems);

            GodPower removeItems = new GodPower();
            removeItems.id = "removeItems";
            removeItems.holdAction = true;
            removeItems.showToolSizes = true;
            removeItems.unselectWhenWindow = true;
            removeItems.name = "removeItems";
            removeItems.dropID = "addItems";
            removeItems.fallingChance = 0.01f;
            removeItems.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            removeItems.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(removeItems);


            DropAsset addItemsDrop = new DropAsset();
            addItemsDrop.id = "addItems";
            addItemsDrop.path_texture = "drops/drop_fireworks";
            addItemsDrop.animated = true;
            addItemsDrop.animation_speed = 0.03f;
            addItemsDrop.default_scale = 0.1f;
            addItemsDrop.action_landed = new DropsAction(action_addItems);
            AssetManager.drops.add(addItemsDrop);

            #endregion


            #region friendship and war

            var friendship = AssetManager.powers.get("friendship");
            //GodPower friendshipNotRandom = AssetManager.powers.clone("friendshipNR", "friendship");
            var friendshipNotRandom = new GodPower();
            Helper.Utils.CopyClass(friendship, friendshipNotRandom, true);

            friendshipNotRandom.id = "friendshipNR";
            friendshipNotRandom.name = "friendshipNR";
            friendshipNotRandom.showToolSizes = false;
            friendshipNotRandom.forceBrush = "circ_0";
            friendshipNotRandom.fallingChance = 0.03f;
            friendshipNotRandom.holdAction = true;
            friendshipNotRandom.showToolSizes = false;
            friendshipNotRandom.unselectWhenWindow = true;
            friendshipNotRandom.dropID = "friendshipNR";
            friendshipNotRandom.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            //friendshipNotRandom.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(friendshipNotRandom);


            var friendshipDrop = AssetManager.drops.get("friendship");
            //DropAsset friendshipNotRandomDrop = AssetManager.drops.clone("friendshipNR", "friendship");
            var friendshipNotRandomDrop = new DropAsset();
            Helper.Utils.CopyClass(friendshipDrop, friendshipNotRandomDrop, true);

            friendshipNotRandomDrop.id = "friendshipNR";
            friendshipNotRandomDrop.path_texture = "drops/drop_friendship";
            friendshipNotRandomDrop.random_frame = true;
            friendshipNotRandomDrop.default_scale = 0.5f;
            friendshipNotRandomDrop.action_landed = new DropsAction(action_freiendshipNotRandom);
            AssetManager.drops.add(friendshipNotRandomDrop);

            var spite = AssetManager.powers.get("spite");
            //GodPower spiteNotRandom = AssetManager.powers.clone("spiteNR", "spite");
            GodPower spiteNotRandom = new GodPower();
            Helper.Utils.CopyClass(spite, spiteNotRandom, true);

            spiteNotRandom.id = "spiteNR";
            spiteNotRandom.name = "spiteNR";
            spiteNotRandom.showToolSizes = false;
            spiteNotRandom.forceBrush = "circ_0";
            spiteNotRandom.fallingChance = 0.03f;
            spiteNotRandom.holdAction = true;
            spiteNotRandom.showToolSizes = false;
            spiteNotRandom.unselectWhenWindow = true;
            spiteNotRandom.dropID = "spiteNR";
            spiteNotRandom.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            //spiteNotRandom.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(spiteNotRandom);

            var spiteDrop = AssetManager.drops.get("spite");
            //DropAsset spiteNotRandomDrop = AssetManager.drops.clone("spiteNR", "spite");
            var spiteNotRandomDrop = new DropAsset();
            Helper.Utils.CopyClass(spiteDrop, spiteNotRandomDrop, true);

            spiteNotRandomDrop.id = "spiteNR";
            spiteNotRandomDrop.path_texture = "drops/drop_spite";
            spiteNotRandomDrop.random_frame = true;
            spiteNotRandomDrop.default_scale = 0.1f;
            spiteNotRandomDrop.action_landed = new DropsAction(action_spiteNotRandom);
            AssetManager.drops.add(spiteNotRandomDrop);

            #endregion


            #region upgrade and downgrade buildings

            GodPower upgradeBuildingAdd = new GodPower();
            upgradeBuildingAdd.id = "upgradeBuildingAdd";
            upgradeBuildingAdd.holdAction = true;
            upgradeBuildingAdd.showToolSizes = true;
            upgradeBuildingAdd.unselectWhenWindow = true;
            upgradeBuildingAdd.name = "upgradeBuildingAdd";
            upgradeBuildingAdd.dropID = "upgradeBuildingAdd";
            upgradeBuildingAdd.fallingChance = 0.01f;
            upgradeBuildingAdd.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            upgradeBuildingAdd.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(upgradeBuildingAdd);


            DropAsset upgradebuildingAddDrop = new DropAsset();
            upgradebuildingAddDrop.id = "upgradeBuildingAdd";
            upgradebuildingAddDrop.path_texture = "drops/drop_snow";
            upgradebuildingAddDrop.animated = true;
            upgradebuildingAddDrop.animation_speed = 0.03f;
            upgradebuildingAddDrop.default_scale = 0.1f;
            upgradebuildingAddDrop.action_landed = new DropsAction(action_upgradeBuildingAdd);
            AssetManager.drops.add(upgradebuildingAddDrop);


            GodPower downgradeBuildingAdd = new GodPower();
            downgradeBuildingAdd.id = "downgradeBuildingAdd";
            downgradeBuildingAdd.holdAction = true;
            downgradeBuildingAdd.showToolSizes = true;
            downgradeBuildingAdd.unselectWhenWindow = true;
            downgradeBuildingAdd.name = "downgradeBuildingAdd";
            downgradeBuildingAdd.dropID = "downgradeBuildingAdd";
            downgradeBuildingAdd.fallingChance = 0.01f;
            downgradeBuildingAdd.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            downgradeBuildingAdd.click_power_brush_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower); });
            AssetManager.powers.add(downgradeBuildingAdd);


            DropAsset downgradeBuildingAddDrop = new DropAsset();
            downgradeBuildingAddDrop.id = "downgradeBuildingAdd";
            downgradeBuildingAddDrop.path_texture = "drops/drop_snow";
            downgradeBuildingAddDrop.animated = true;
            downgradeBuildingAddDrop.animation_speed = 0.03f;
            downgradeBuildingAddDrop.default_scale = 0.1f;
            downgradeBuildingAddDrop.action_landed = new DropsAction(action_downgradeBuildingAdd);
            AssetManager.drops.add(downgradeBuildingAddDrop);

            #endregion


            #region makeColony
            var makeColony = new GodPower();
            makeColony.id = "makeColony";
            makeColony.name = "makeColony";
            makeColony.showToolSizes = false;
            makeColony.forceBrush = "circ_0";
            makeColony.fallingChance = 0.03f;
            makeColony.holdAction = true;
            makeColony.showToolSizes = false;
            makeColony.unselectWhenWindow = true;
            makeColony.dropID = "makeColony";
            makeColony.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(makeColony);


            var makeColonyDrop = new DropAsset();
            makeColonyDrop.id = "makeColony";
            makeColonyDrop.path_texture = "drops/drop_gold";
            makeColonyDrop.random_frame = true;
            makeColonyDrop.default_scale = 0.1f;
            makeColonyDrop.action_landed = new DropsAction(action_makeColony);
            AssetManager.drops.add(makeColonyDrop);

            #endregion


            #region expandCitysBorders and reduceCitysBorders
            var expandCitysBorders = new GodPower();
            expandCitysBorders.id = "expandCitysBorders";
            expandCitysBorders.name = "expandCitysBorders";
            expandCitysBorders.forceBrush = "circ_0";
            expandCitysBorders.fallingChance = 0.03f;
            expandCitysBorders.holdAction = true;
            expandCitysBorders.showToolSizes = false;
            expandCitysBorders.unselectWhenWindow = true;
            expandCitysBorders.click_action = new PowerActionWithID(action_expandCitysBorders);
            AssetManager.powers.add(expandCitysBorders);


            var reduceCitysBorders = new GodPower();
            reduceCitysBorders.id = "reduceCitysBorders";
            reduceCitysBorders.name = "reduceCitysBorders";
            reduceCitysBorders.forceBrush = "circ_0";
            reduceCitysBorders.fallingChance = 0.03f;
            reduceCitysBorders.holdAction = true;
            reduceCitysBorders.showToolSizes = false;
            reduceCitysBorders.unselectWhenWindow = true;
            reduceCitysBorders.click_action = new PowerActionWithID(action_reduceCitysBorders);
            AssetManager.powers.add(reduceCitysBorders);
            #endregion


            #region randomKingdomColor
            // var randomKingdomColor = new GodPower();
            // randomKingdomColor.id = "randomKingdomColor";
            // randomKingdomColor.name = "randomKingdomColor";
            // randomKingdomColor.showToolSizes = false;
            // randomKingdomColor.forceBrush = "circ_0";
            // randomKingdomColor.fallingChance = 0.03f;
            // randomKingdomColor.holdAction = true;
            // randomKingdomColor.unselectWhenWindow = true;
            // randomKingdomColor.dropID = "randomKingdomColor";
            // randomKingdomColor.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            // AssetManager.powers.add(randomKingdomColor);


            // var randomKingdomColorDrop = new DropAsset();
            // randomKingdomColorDrop.id = "randomKingdomColor";
            // randomKingdomColorDrop.path_texture = "drops/drop_gold";
            // randomKingdomColorDrop.random_frame = true;
            // randomKingdomColorDrop.default_scale = 0.1f;
            // randomKingdomColorDrop.action_landed = new DropsAction(action_randomKingdomColor);
            // AssetManager.drops.add(randomKingdomColorDrop);

            // allColors.AddRange(KingdomColors.dict["human"].list);
            // allColors.AddRange(KingdomColors.dict["elf"].list);
            // allColors.AddRange(KingdomColors.dict["dwarf"].list);
            // allColors.AddRange(KingdomColors.dict["orc"].list);

            #endregion


            #region spawnBlodRainCloud

            var spawnBloodRainCloud = new GodPower();
            spawnBloodRainCloud.id = "bloodRainCloudSpawn";
            spawnBloodRainCloud.name = "bloodRainCloudSpawn";
            spawnBloodRainCloud.forceBrush = "circ_0";
            spawnBloodRainCloud.fallingChance = 0.03f;
            spawnBloodRainCloud.holdAction = true;
            spawnBloodRainCloud.showToolSizes = false;
            spawnBloodRainCloud.unselectWhenWindow = true;
            spawnBloodRainCloud.dropID = "cloudBloodRainD";
            spawnBloodRainCloud.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(spawnBloodRainCloud);

            DropAsset cloudBloodRainD = new DropAsset();
            cloudBloodRainD.id = "cloudBloodRainD";
            cloudBloodRainD.path_texture = "drops/drop_blood";
            cloudBloodRainD.random_frame = true;
            cloudBloodRainD.default_scale = 0.2f;
            cloudBloodRainD.sound_drop = "fallingWater";
            cloudBloodRainD.fallingHeight = (Vector3)new Vector2(0f, 0f);
            cloudBloodRainD.action_landed = new DropsAction(action_spawnCloud);
            AssetManager.drops.add(cloudBloodRainD);

            DropAsset cloudBloodRain = new DropAsset();
            cloudBloodRain.id = "cloudBloodRain";
            cloudBloodRain.path_texture = "drops/drop_blood";
            cloudBloodRain.random_frame = true;
            cloudBloodRain.default_scale = 0.2f;
            cloudBloodRain.sound_drop = "fallingWater";
            cloudBloodRain.fallingHeight = (Vector3)new Vector2(30f, 45f);
            cloudBloodRain.action_landed = new DropsAction(DropsLibrary.action_bloodRain);
            AssetManager.drops.add(cloudBloodRain);

            #endregion


            #region cloudBurgerSpider
            var spawnBurgerSpiderCloud = new GodPower();
            spawnBurgerSpiderCloud.id = "burgerSpiderCloudSpawn";
            spawnBurgerSpiderCloud.name = "burgerSpiderCloudSpawn";
            spawnBurgerSpiderCloud.forceBrush = "circ_0";
            spawnBurgerSpiderCloud.fallingChance = 0.03f;
            spawnBurgerSpiderCloud.holdAction = true;
            spawnBurgerSpiderCloud.showToolSizes = false;
            spawnBurgerSpiderCloud.unselectWhenWindow = true;
            spawnBurgerSpiderCloud.dropID = "cloudBurgerSpiderD";
            spawnBurgerSpiderCloud.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(spawnBurgerSpiderCloud);

            DropAsset cloudBurgerSpiderD = new DropAsset();
            cloudBurgerSpiderD.id = "cloudBurgerSpiderD";
            cloudBurgerSpiderD.path_texture = "drops/drop_blood";
            cloudBurgerSpiderD.random_frame = true;
            cloudBurgerSpiderD.default_scale = 0.2f;
            cloudBurgerSpiderD.sound_drop = "fallingWater";
            cloudBurgerSpiderD.fallingHeight = (Vector3)new Vector2(0f, 0f);
            cloudBurgerSpiderD.action_landed = new DropsAction(action_spawnCloud);
            AssetManager.drops.add(cloudBurgerSpiderD);

            DropAsset cloudBurgerSpider = new DropAsset();
            cloudBurgerSpider.id = "cloudBurgerSpider";
            cloudBurgerSpider.path_texture = "drops/drop_blood";
            cloudBurgerSpider.random_frame = true;
            cloudBurgerSpider.default_scale = 0.2f;
            cloudBurgerSpider.sound_drop = "fallingWater";
            cloudBurgerSpider.fallingHeight = (Vector3)new Vector2(30f, 45f);
            cloudBurgerSpider.action_landed = new DropsAction(action_spawnBurgerSpider);
            AssetManager.drops.add(cloudBurgerSpider);

            #endregion


            #region spawnBoats
            var spawnTradingBoat = new GodPower();
            spawnTradingBoat.id = "spawnTradingBoat";
            spawnTradingBoat.name = "spawnTradingBoat";
            spawnTradingBoat.showSpawnEffect = "spawn";
            spawnTradingBoat.showToolSizes = false;
            spawnTradingBoat.forceBrush = "circ_0";
            spawnTradingBoat.fallingChance = 0.03f;
            spawnTradingBoat.holdAction = true;
            spawnTradingBoat.showToolSizes = false;
            spawnTradingBoat.unselectWhenWindow = true;
            spawnTradingBoat.actorStatsId = "boat_trading";
            spawnTradingBoat.dropID = "spawnTradingBoat";
            spawnTradingBoat.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(spawnTradingBoat);

            var spawnTradingBoatDrop = new DropAsset();
            spawnTradingBoatDrop.id = "spawnTradingBoat";
            spawnTradingBoatDrop.path_texture = "drops/drop_metal";
            spawnTradingBoatDrop.random_frame = true;
            spawnTradingBoatDrop.default_scale = 0.1f;
            spawnTradingBoatDrop.action_landed = new DropsAction((pTile, pDropID) => action_spawnBoat(pTile, pDropID, "boat_trading"));
            AssetManager.drops.add(spawnTradingBoatDrop);


            var spawnTransportBoat = new GodPower();
            spawnTransportBoat.id = "spawnTransportBoat";
            spawnTransportBoat.name = "spawnTransportBoat";
            spawnTransportBoat.showSpawnEffect = "spawn";
            spawnTransportBoat.showToolSizes = false;
            spawnTransportBoat.forceBrush = "circ_0";
            spawnTransportBoat.fallingChance = 0.03f;
            spawnTransportBoat.holdAction = true;
            spawnTransportBoat.showToolSizes = false;
            spawnTransportBoat.unselectWhenWindow = true;
            spawnTransportBoat.actorStatsId = "boat_transport";
            spawnTransportBoat.dropID = "spawnTransportBoat";
            spawnTransportBoat.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(spawnTransportBoat);

            var spawnTransportBoatDrop = new DropAsset();
            spawnTransportBoatDrop.id = "spawnTransportBoat";
            spawnTransportBoatDrop.path_texture = "drops/drop_metal";
            spawnTransportBoatDrop.random_frame = true;
            spawnTransportBoatDrop.default_scale = 0.1f;
            spawnTransportBoatDrop.action_landed = new DropsAction((pTile, pDropID) => action_spawnBoat(pTile, pDropID, "boat_transport"));
            AssetManager.drops.add(spawnTransportBoatDrop);


            var spawnFishingBoat = new GodPower();
            spawnFishingBoat.id = "spawnFishingBoat";
            spawnFishingBoat.name = "spawnFishingBoat";
            spawnFishingBoat.showSpawnEffect = "spawn";
            spawnFishingBoat.showToolSizes = false;
            spawnFishingBoat.forceBrush = "circ_0";
            spawnFishingBoat.fallingChance = 0.03f;
            spawnFishingBoat.holdAction = true;
            spawnFishingBoat.showToolSizes = false;
            spawnFishingBoat.unselectWhenWindow = true;
            spawnFishingBoat.actorStatsId = "boat_fishing";
            spawnFishingBoat.dropID = "spawnFishingBoat";
            spawnFishingBoat.click_power_action = new PowerAction((WorldTile pTile, GodPower pPower) => { return (bool)AssetManager.powers.CallMethod("spawnDrops", pTile, pPower); });
            AssetManager.powers.add(spawnFishingBoat);

            var spawnFishingBoatDrop = new DropAsset();
            spawnFishingBoatDrop.id = "spawnFishingBoat";
            spawnFishingBoatDrop.path_texture = "drops/drop_metal";
            spawnFishingBoatDrop.random_frame = true;
            spawnFishingBoatDrop.default_scale = 0.1f;
            spawnFishingBoatDrop.action_landed = new DropsAction((pTile, pDropID) => action_spawnBoat(pTile, pDropID, "boat_fishing"));
            AssetManager.drops.add(spawnFishingBoatDrop);
            #endregion
        }

        #region actions

        #region action_addTraits
        // public static void action_addTraits(WorldTile pTile = null, string pDropID = null)
        // {
        //     //var actor = ActionLibrary.getActorNearPos()
        //     MapBox.instance.CallMethod("getObjectsInChunks", pTile, 3, MapObjectType.Actor);
        //     var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
        //     foreach (Actor tempMapObject in temp_map_objects)
        //     {
        //         if (tempMapObject.base_data.alive)
        //         {
        //             if(SelectableObjects.TType == PowerType.add && AddRemoveTraitsWindow.SelectedToAdd.Count != 0)
        //             {
        //                 for (int i = 0; i < AddRemoveTraitsWindow.SelectedToAdd.Count; i++)
        //                 {
        //                     tempMapObject.addTrait(AddRemoveTraitsWindow.SelectedToAdd[i]);
        //                 }
        //             }
        //             else if(SelectableObjects.TType == PowerType.remove && AddRemoveTraitsWindow.SelectedToRemove.Count != 0)
        //             {
        //                 for (int i = 0; i < AddRemoveTraitsWindow.SelectedToRemove.Count; i++)
        //                 {
        //                     tempMapObject.removeTrait(AddRemoveTraitsWindow.SelectedToRemove[i]);
        //                 }
        //             }

        //             tempMapObject.startShake(0.3f, 0.1f, true, true);
        //             tempMapObject.startColorEffect("white");
        //         }
        //     }
        // }

        #endregion


        #region action_freiendshipNotRandom
        private static Kingdom PeaceInitiator;
        private static Kingdom WarInitiator;
        public static void action_freiendshipNotRandom(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile.zone.city == null)
                return;

            var kingdom = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;

            if (kingdom.civs_enemies.Count == 0)
                return;

            if (PeaceInitiator == null)
            {
                var wantMessage = "Kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(kingdom), true) + ">" + kingdom.name + "</color> want to make peace with someone...";
                Helper.KingdomThings.newText(wantMessage, Toolbox.color_log_good);
                PeaceInitiator = kingdom;
                return;
            }
            else if (PeaceInitiator != null && PeaceInitiator.id != kingdom.id)
            {
                if (PeaceInitiator.alive)
                {
                    MapBox.instance.kingdoms.diplomacyManager.CallMethod("startPeace", PeaceInitiator, kingdom, true);


                    WorldLog.logNewPeace(PeaceInitiator, kingdom);

                    PeaceInitiator = null;
                }
                else
                {
                    var fallenMessage = "Fallen kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(PeaceInitiator), true) + ">" + PeaceInitiator.name + "</color> cant make peace...";
                    Helper.KingdomThings.newText(fallenMessage, Toolbox.color_log_neutral);
                }
            }
        }
        #endregion


        #region action_spiteNotRandom
        public static void action_spiteNotRandom(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile.zone.city == null)
                return;

            var kingdom = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;

            if (kingdom.civs_allies.Count == 0)
                return;

            if (WarInitiator == null)
            {
                var wantMessage = "Kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(kingdom), true) + ">" + kingdom.name + "</color> want to declare war on someone...";
                Helper.KingdomThings.newText(wantMessage, Toolbox.color_log_warning);
                WarInitiator = kingdom;
                return;
            }
            else if (WarInitiator != null && WarInitiator.id != kingdom.id)
            {
                if (WarInitiator.alive)
                {
                    MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", WarInitiator, kingdom, true);


                    WorldLog.logNewWar(WarInitiator, kingdom);

                    WarInitiator = null;
                }
                else
                {
                    var fallenMessage = "Fallen kingdom <color=" + Toolbox.colorToHex(Helper.KingdomThings.GetKingdomColor(WarInitiator), true) + ">" + WarInitiator.name + "</color> cant declare war...";
                    Helper.KingdomThings.newText(fallenMessage, Toolbox.color_log_neutral);
                }
            }
        }
        #endregion


        #region action_upgradeBuildingAdd
        public static void action_upgradeBuildingAdd(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile.building != null)
            {
                var stats = Reflection.GetField(pTile.building.GetType(), pTile.building, "stats") as BuildingAsset;

                if(stats.id.Contains("goblin")){
                    return;
                }

                var data = Reflection.GetField(pTile.building.GetType(), pTile.building, "data") as BuildingData;
                if (stats.canBeUpgraded && !((data.state == BuildingState.Ruins) || data.underConstruction || data.state == BuildingState.CivAbandoned))
                {

                    BuildingAsset pTemplate = AssetManager.buildings.get(stats.upgradeTo);
                    if (pTile.building.city != null)
                    {
                        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, false);
                    }
                    pTile.building.CallMethod("setTemplate", pTemplate);

                    if (pTile.building.city != null)
                    {
                        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, true);
                    }
                    pTile.building.CallMethod("setSpriteMain", true);
                    pTile.building.CallMethod("updateStats");
                    var curStats = Reflection.GetField(pTile.building.GetType(), pTile.building, "curStats") as BaseStats;

                    data.health = curStats.health;
                    pTile.building.CallMethod("fillTiles");
                }

            }
        }
        #endregion


        #region action_downgradeBuildingAdd
        public static void action_downgradeBuildingAdd(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile.building != null)
            {
                var stats = Reflection.GetField(pTile.building.GetType(), pTile.building, "stats") as BuildingAsset;
                
                if(stats.id.Contains("goblin")){
                    return;
                }

                var data = Reflection.GetField(pTile.building.GetType(), pTile.building, "data") as BuildingData;


                var downgradeTo = AssetManager.buildings.list.Find(b => b.upgradeTo == stats.id);

                if(downgradeTo != null && !((data.state == BuildingState.Ruins) || data.underConstruction || data.state == BuildingState.CivAbandoned))
                {
                    BuildingAsset pTemplate = AssetManager.buildings.get(downgradeTo.id);

                    if (pTile.building.city != null)
                    {
                        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, false);
                    }
                    pTile.building.CallMethod("setTemplate", pTemplate);

                    if (pTile.building.city != null)
                    {
                        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, true);
                    }
                    pTile.building.CallMethod("setSpriteMain", true);
                    pTile.building.CallMethod("updateStats");

                    var curStats = Reflection.GetField(pTile.building.GetType(), pTile.building, "curStats") as BaseStats;

                    data.health = curStats.health;
                    pTile.building.CallMethod("fillTiles");
                }

                //if (stats.canBeUpgraded && !((data.state == BuildingState.Ruins) || data.underConstruction || data.state == BuildingState.CivAbandoned))
                //{

                //    BuildingAsset pTemplate = AssetManager.buildings.get(stats.upgradeTo);
                //    if (pTile.building.city != null)
                //    {
                //        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, false);
                //    }
                //    pTile.building.CallMethod("setTemplate", pTemplate);

                //    if (pTile.building.city != null)
                //    {
                //        pTile.building.city.CallMethod("setBuildingDictID", pTile.building, true);
                //    }
                //    pTile.building.CallMethod("setSpriteMain", true);
                //    pTile.building.CallMethod("updateStats");
                //    var curStats = Reflection.GetField(pTile.building.GetType(), pTile.building, "curStats") as BaseStats;

                //    data.health = curStats.health;
                //    pTile.building.CallMethod("fillTiles");
                //}

            }
        }
        #endregion


        #region action_makeColony
        public static void action_makeColony(WorldTile pTile = null, string pDropID = null)
        {
            MapBox.instance.CallMethod("getObjectsInChunks", pTile, 4, MapObjectType.Actor);
            var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;

            foreach (Actor tempMapObject in temp_map_objects)
            {
                if (tempMapObject.base_data.alive && tempMapObject.kingdom.isCiv())
                {
                    var ai = Reflection.GetField(tempMapObject.GetType(), tempMapObject, "ai") as AiSystemActor;
                    tempMapObject.startShake(0.3f, 0.1f, true, true);
                    tempMapObject.startColorEffect("white");

                    if(tempMapObject.currentTile.zone.city == null)
                    {
                        var kingdom = tempMapObject.kingdom;
                        var world = MapBox.instance;

                        TileZone zone = tempMapObject.currentTile.zone;
                        //if (!zone.goodForNewCity)
                        //    return;

                        if (kingdom != null && kingdom.isNomads())
                            kingdom = null;

                        var race = Reflection.GetField(tempMapObject.GetType(), tempMapObject, "race") as Race;

                        City city1 = world.buildNewCity(zone, null, race, false, kingdom);

                        if (city1 == null)
                            return;

                        city1.newCityEvent();

                        Reflection.SetField(city1, "race", race);


                        City city2 = tempMapObject.city;
                        if (city2 != null)
                        {

                            tempMapObject.kingdom.newCityBuiltEvent(city1);
                            city2.removeCitizen(tempMapObject, false, AttackType.Other);
                            tempMapObject.removeFromCity();
                        }
                        tempMapObject.CallMethod("becomeCitizen", city1);
                        WorldLog.logNewCity(city1);
                    }
                    else
                    {
                        if(tempMapObject.city != pTile.zone.city)
                        {
                            tempMapObject.city.removeCitizen(tempMapObject, false, AttackType.Other);
                            tempMapObject.removeFromCity();
                            tempMapObject.CallMethod("becomeCitizen", pTile.zone.city);
                            var kingdomN = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;
                            tempMapObject.kingdom = kingdomN;
                            //pTile.zone.city.findNewJob(tempMapObject);
                        }
                    }
                }
            }
        }

        #endregion


        #region action_addItems
        public static void action_addItems(WorldTile pTile = null, string pDropID = null)
        {
            MapBox.instance.CallMethod("getObjectsInChunks", pTile, 4, MapObjectType.Actor);
            var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
            foreach (Actor tempMapObject in temp_map_objects)
            {
                if (tempMapObject.base_data.alive && tempMapObject.stats.use_items)
                {
                    if (SelectableObjects.TType == PowerType.add)
                    {
                        foreach(var pr in EditItemsWindow.choosenForAddSlots)
                        {
                            var data = pr.Value;

                            if (data == null) continue;

                            var slotType = AssetManager.items.get(data.id).equipmentType;



                            var unitSlot = tempMapObject.equipment.getSlot(slotType);

                            if (unitSlot.data != null)
                            {
                                if (!EditItemsWindow.compareItems(data, unitSlot.data))
                                {
                                    unitSlot.CallMethod("setItem", data);
                                }
                            }
                            else
                            {
                                unitSlot.CallMethod("setItem", data);
                            }

                            Reflection.SetField(tempMapObject, "statsDirty", true);
                        }
                    }
                    else if (SelectableObjects.TType == PowerType.remove)
                    {
                        var chs = EditItemsWindow.choosenForRemoveSlots.Values.ToList();
                        foreach (var pr in EditItemsWindow.choosenForRemoveSlots)
                        {
                            var data = pr.Value;
                            if (data == null) continue;
                            var slotType = AssetManager.items.get(data.id).equipmentType;


                            var unitSlot = tempMapObject.equipment.getSlot(slotType);

                            if (unitSlot.data != null)
                            {
                                if (EditItemsWindow.compareItems(data, unitSlot.data))
                                {
                                    unitSlot.emptySlot();
                                }
                            }

                            Reflection.SetField(tempMapObject, "statsDirty", true);
                        }
                    }

                    tempMapObject.startShake(0.3f, 0.1f, true, true);
                    tempMapObject.startColorEffect("white");
                }
            }
        }

        #endregion


        #region action_randomKingdomColor

        // static List<KingdomColor> allColors = new List<KingdomColor>();
        // public static void action_randomKingdomColor(WorldTile pTile = null, string pDropID = null)
        // {
        //     if (pTile.zone.city == null)
        //         return;

        //     var kingdom = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;


        //     var kingdomColor = Reflection.GetField(kingdom.GetType(), kingdom, "kingdomColor") as KingdomColor;
        //     var val = UnityEngine.Random.Range(0, allColors.Count - 1);

        //     var colorR = allColors[val];

        //     kingdomColor.colorBorderBannerIcon = colorR.colorBorderBannerIcon;
        //     kingdomColor.colorBorderInside = colorR.colorBorderInside;
        //     kingdomColor.colorBorderInsideAlpha = colorR.colorBorderInsideAlpha;
        //     kingdomColor.colorBorderOut = colorR.colorBorderOut;
        //     kingdomColor.initColor();
            

        //     var zoneCalculator = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "zoneCalculator");
        //     zoneCalculator.CallMethod("setDrawnZonesDirty");

            
        //     var mode = (ZoneDisplayMode)Reflection.GetField(zoneCalculator.GetType(), zoneCalculator, "_mode");
        //     zoneCalculator.CallMethod("setMode", ZoneDisplayMode.None);
        //     //this.sprRnd.enabled = false;
        //     zoneCalculator.CallMethod("redrawZones");
        //     zoneCalculator.CallMethod("setMode", mode);
        //     zoneCalculator.CallMethod("redrawZones");

        //     //var currentMode = zoneCalculator.CallMethod("getCurrentMode") as MapMode;


        //     foreach (var city in kingdom.cities)
        //     {
        //         foreach (var building in city.buildings.getSimpleList())
        //         {
        //             Sprite spriteBuilding = UnitSpriteConstructor.getSpriteBuilding(building, kingdomColor); 
        //             var spriteRenderer = Reflection.GetField(building.GetType(), building, "spriteRenderer") as SpriteRenderer;
        //             spriteRenderer.sprite = spriteBuilding;
        //             // Debug.Log(spriteBuilding == null);

        //             // var spriteRenderer = Reflection.GetField(building.GetType(), building, "spriteRenderer") as SpriteRenderer;
        //             // spriteRenderer.sprite = UnitSpriteConstructor.createNewSpriteBuilding(spriteRenderer.sprite, kingdomColor); 
                    
        //             //building.CallMethod("setSpriteMain", true);
        //         }
        //     }
        // }

        #endregion


        #region action_spawnBurgerSpider

        public static void action_spawnBurgerSpider(WorldTile pTile = null, string pDropID = null)
        {
            GodPower godPower = AssetManager.powers.get("spawnBurgerSpider");
            if (godPower.showSpawnEffect != string.Empty)
            {
                MapBox.instance.stackEffects.CallMethod("startSpawnEffect", pTile, godPower.showSpawnEffect);
            }


            MapBox.instance.createNewUnit(godPower.actorStatsId, pTile, "", godPower.actorSpawnHeight, null);
        }

        #endregion


        #region action_spawnCloud
        public static void action_spawnCloud(WorldTile pTile = null, string pDropID = null)
        {
            GodPower godPower = AssetManager.powers.get("cloudAcid");
            WorldTile random;

            if (pDropID.EndsWith("D"))
            {
                random = pTile;
            }
            else
            {
                random = MapBox.instance.tilesList.GetRandom<WorldTile>();
            }

            var cloud = MapBox.instance.cloudController.getNext();
            var method = cloud.GetType().GetMethod("prepare", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new[] { typeof(Vector3), typeof(string) }, null);
            method.Invoke(cloud, new object[] { random.posV3, godPower.id });
            //cloud.CallMethod("prepare", random.posV3, godPower.id);

            if (pDropID.EndsWith("D"))
                pDropID = pDropID.Remove(pDropID.Length - 1, 1);

            Reflection.SetField<string>(cloud, "dropID", pDropID);

            //MapBox.instance.cloudController.CallMethod("spawnCloud", random.posV3, godPower.id);

            switch (pDropID)
            {
                case "cloudBurgerSpider":
                    cloud.sprRenderer.color = new Color(0.82f, 0.45f, 0.10f, 0.77f);
                    break;
                case "cloudBloodRain":
                    cloud.sprRenderer.color = new Color(0.76f, 0.09f, 0.09f, 0.77f);
                    break;
            }


        }

        #endregion


        #region action_expandCitysBorders and action_reduceCitysBorders
        private static City ToCityZone;
        public static bool action_expandCitysBorders(WorldTile pTile = null, string pPowerId = null)
        {
            if(pTile.zone.city != null)
            {
                ToCityZone = pTile.zone.city;
            }
            else if(pTile.zone.city == null && ToCityZone != null)
            {
                ToCityZone.CallMethod("addZone", pTile.zone);
                //ToCityZone.addZone
            }

            MapBox.instance.StartCoroutine(resetToCityZone());

            return true;
        }
        public static IEnumerator resetToCityZone()
        {
            yield return new WaitForFixedUpdate();

            if (!Input.GetMouseButton(0))
            {
                ToCityZone = null;
            }
        }

        public static bool action_reduceCitysBorders(WorldTile pTile = null, string pPowerId = null)
        {
            if (pTile.zone.city != null)
            {
                pTile.zone.city.CallMethod("removeZone", pTile.zone, true);
            }

            return true;
        }

        #endregion


        #region action_spawnBoat

        public static void action_spawnBoat(WorldTile pTile = null, string pDropID = null, string boatType = "boat_trading")
        {
            if (pTile.zone.city == null)
                return;

            if (!pTile.Type.liquid)
                return;

            MapBox.instance.stackEffects.CallMethod("startSpawnEffect", pTile, "spawn");

            var actor = MapBox.instance.createNewUnit(boatType, pTile, boatType, 1f, null);

            var kingdom = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;
            //data.age = 18;
            actor.CallMethod("setKingdom", kingdom);
            actor.CallMethod("setCity", pTile.zone.city);
        }

        #endregion


        #endregion
    }
}
