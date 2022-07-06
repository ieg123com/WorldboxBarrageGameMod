using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;

namespace WarBox
{
    class Button
    {
        public static void init()
        {
          DropAsset spawnBoat = new DropAsset();
          spawnBoat.id = "spawnBoat";
          spawnBoat.path_texture = "drops/drop_metal";
          spawnBoat.random_frame = true;
          spawnBoat.default_scale = 0.08f;
          spawnBoat.action_landed = new DropsAction(action_spawnBoatDrop);
          AssetManager.drops.add(spawnBoat);

          GodPower BoatPower = new GodPower();
          BoatPower.id = "BoatPowerbutton";
          BoatPower.name = "BoatPowerbutton";
          BoatPower.showSpawnEffect = "spawn";
          BoatPower.showToolSizes = false;
          BoatPower.forceBrush = "circ_0";
          BoatPower.holdAction = false;
          BoatPower.unselectWhenWindow = true;
          BoatPower.fallingChance = 0.03f;
          BoatPower.actorStatsId = "boat_trading";
          BoatPower.dropID = "spawnBoat";
          BoatPower.click_power_action = new PowerAction(action_Drop);
          AssetManager.powers.add(BoatPower);

          var BoatButton = NCMS.Utils.PowerButtons.CreateButton(
          "BoatPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.iconBoat.png"),
          "Marine Boat",
          "Spawn Military boat near a dock",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          BoatButton,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(545, 18));

          DropAsset spawnPirate = new DropAsset();
          spawnPirate.id = "spawnPirate";
          spawnPirate.path_texture = "drops/drop_metal";
          spawnPirate.random_frame = true;
          spawnPirate.default_scale = 0.08f;
          spawnPirate.action_landed = new DropsAction(action_spawnPirateDrop);
          AssetManager.drops.add(spawnPirate);

          GodPower PiratePower = new GodPower();
          PiratePower.id = "PiratePowerbutton";
          PiratePower.name = "PiratePowerbutton";
          PiratePower.showSpawnEffect = "spawn";
          PiratePower.showToolSizes = false;
          PiratePower.forceBrush = "circ_0";
          PiratePower.holdAction = false;
          PiratePower.unselectWhenWindow = true;
          PiratePower.fallingChance = 0.03f;
          PiratePower.actorStatsId = "boat_trading";
          PiratePower.dropID = "spawnPirate";
          PiratePower.click_power_action = new PowerAction(action_Drop);
          AssetManager.powers.add(PiratePower);

          var PirateButton = NCMS.Utils.PowerButtons.CreateButton(
          "PiratePowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.iconPirate.png"),
          "Pirate",
          "Spawn Pirate boat",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          PirateButton,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(545, -18));

          GodPower CrossPower = new GodPower();
          CrossPower.id = "CrossPowerbutton";
          CrossPower.name = "CrossPowerbutton";
          CrossPower.forceBrush = "circ_0";
          CrossPower.holdAction = false;
          CrossPower.showToolSizes = false;
          CrossPower.unselectWhenWindow = false;
          CrossPower.click_action = new PowerActionWithID(action_CrossPowerClick);
          AssetManager.powers.add(CrossPower);

          var CrossPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "CrossPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Cross_wood.png"),
          "Cross",
          "Give a unit a Cross",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          CrossPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(580, 18));

          GodPower philoStonePower = new GodPower();
          philoStonePower.id = "philoStonePowerbutton";
          philoStonePower.name = "philoStonePowerbutton";
          philoStonePower.forceBrush = "circ_0";
          philoStonePower.holdAction = false;
          philoStonePower.showToolSizes = false;
          philoStonePower.unselectWhenWindow = false;
          philoStonePower.click_action = new PowerActionWithID(action_philoStonePowerClick);
          AssetManager.powers.add(philoStonePower);

          var philoStonePowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "philoStonePowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_philostone.png"),
          "Philosopher stone",
          "Give a unit the Philosopher stone",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          philoStonePowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(580, -18));

          GodPower ShieldPower = new GodPower();
          ShieldPower.id = "ShieldPowerbutton";
          ShieldPower.name = "ShieldPowerbutton";
          ShieldPower.forceBrush = "circ_0";
          ShieldPower.holdAction = false;
          ShieldPower.showToolSizes = false;
          ShieldPower.unselectWhenWindow = false;
          ShieldPower.click_action = new PowerActionWithID(action_ShieldPowerClick);
          AssetManager.powers.add(ShieldPower);

          var ShieldPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "ShieldPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Shield_iron.png"),
          "Shield",
          "Give a unit the Magic Shield",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          ShieldPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(616, 18));

          GodPower HGrailPower = new GodPower();
          HGrailPower.id = "HGrailPowerbutton";
          HGrailPower.name = "HGrailPowerbutton";
          HGrailPower.forceBrush = "circ_0";
          HGrailPower.holdAction = false;
          HGrailPower.showToolSizes = false;
          HGrailPower.unselectWhenWindow = false;
          HGrailPower.click_action = new PowerActionWithID(action_HGrailPowerClick);
          AssetManager.powers.add(HGrailPower);

          var HGrailPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "HGrailPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_HGrail_bronze.png"),
          "Holy Grail",
          "Give a unit the Holy Grail Power",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          HGrailPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(616, -18));

          GodPower RingPower = new GodPower();
          RingPower.id = "RingPowerbutton";
          RingPower.name = "RingPowerbutton";
          RingPower.forceBrush = "circ_0";
          RingPower.holdAction = false;
          RingPower.showToolSizes = false;
          RingPower.unselectWhenWindow = false;
          RingPower.click_action = new PowerActionWithID(action_RingPowerClick);
          AssetManager.powers.add(RingPower);

          var RingPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "RingPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_RingPower_bronze.png"),
          "Ring of Power",
          "Give a unit the Ring of Power",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          RingPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(652, 18));

          GodPower GrimoirePower = new GodPower();
          GrimoirePower.id = "GrimoirePowerbutton";
          GrimoirePower.name = "GrimoirePowerbutton";
          GrimoirePower.forceBrush = "circ_0";
          GrimoirePower.holdAction = false;
          GrimoirePower.showToolSizes = false;
          GrimoirePower.unselectWhenWindow = false;
          GrimoirePower.click_action = new PowerActionWithID(action_GrimoirePowerClick);
          AssetManager.powers.add(GrimoirePower);

          var GrimoirePowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "GrimoirePowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Grimoire_bone.png"),
          "Grimoire",
          "Give a unit the Grimoire",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          GrimoirePowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(652, -18));

          GodPower Scp035Power = new GodPower();
          Scp035Power.id = "Scp035Powerbutton";
          Scp035Power.name = "Scp035Powerbutton";
          Scp035Power.forceBrush = "circ_0";
          Scp035Power.holdAction = false;
          Scp035Power.showToolSizes = false;
          Scp035Power.unselectWhenWindow = false;
          Scp035Power.click_action = new PowerActionWithID(action_Scp035PowerClick);
          AssetManager.powers.add(Scp035Power);

          var Scp035PowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "Scp035Powerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Scp035.png"),
          "Scp-035",
          "Give a unit Scp-035",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          Scp035PowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(688, 18));

          GodPower LawsPower = new GodPower();
          LawsPower.id = "LawsPowerbutton";
          LawsPower.name = "LawsPowerbutton";
          LawsPower.forceBrush = "circ_0";
          LawsPower.holdAction = false;
          LawsPower.showToolSizes = false;
          LawsPower.unselectWhenWindow = false;
          LawsPower.click_action = new PowerActionWithID(action_LawsPowerClick);
          AssetManager.powers.add(LawsPower);

          var LawsPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "LawsPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Laws.png"),
          "The tables of Laws",
          "Give a unit the tables of Laws",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          LawsPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(688, -18));

          GodPower TechPower = new GodPower();
          TechPower.id = "TechPowerbutton";
          TechPower.name = "TechPowerbutton";
          TechPower.forceBrush = "circ_0";
          TechPower.holdAction = false;
          TechPower.showToolSizes = false;
          TechPower.unselectWhenWindow = false;
          TechPower.click_action = new PowerActionWithID(action_TechPowerClick);
          AssetManager.powers.add(TechPower);

          var TechPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "TechPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Tech_iron.png"),
          "Acient Tech",
          "Give a unit a piece of acient tech",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          TechPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(724, 18));

          GodPower HornPower = new GodPower();
          HornPower.id = "HornPowerbutton";
          HornPower.name = "HornPowerbutton";
          HornPower.forceBrush = "circ_0";
          HornPower.holdAction = false;
          HornPower.showToolSizes = false;
          HornPower.unselectWhenWindow = false;
          HornPower.click_action = new PowerActionWithID(action_HornPowerClick);
          AssetManager.powers.add(HornPower);

          var HornPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "HornPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Horn_wood.png"),
          "Magic Horn",
          "Give a unit a Magic Horn",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          HornPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(724, -18));

          GodPower FrogWandPower = new GodPower();
          FrogWandPower.id = "FrogWandPowerbutton";
          FrogWandPower.name = "FrogWandPowerbutton";
          FrogWandPower.forceBrush = "circ_0";
          FrogWandPower.holdAction = false;
          FrogWandPower.showToolSizes = false;
          FrogWandPower.unselectWhenWindow = false;
          FrogWandPower.click_action = new PowerActionWithID(action_FrogWandPowerClick);
          AssetManager.powers.add(FrogWandPower);

          var FrogWandPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "FrogWandPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_FrogWand_wood.png"),
          "Acient FrogWand",
          "Give a unit an Acient FrogWand",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          FrogWandPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(796, 18));

          GodPower MidaHandPower = new GodPower();
          MidaHandPower.id = "MidaHandPowerbutton";
          MidaHandPower.name = "MidaHandPowerbutton";
          MidaHandPower.forceBrush = "circ_0";
          MidaHandPower.holdAction = false;
          MidaHandPower.showToolSizes = false;
          MidaHandPower.unselectWhenWindow = false;
          MidaHandPower.click_action = new PowerActionWithID(action_MidaHandPowerClick);
          AssetManager.powers.add(MidaHandPower);

          var MidaHandPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "MidaHandPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_MidaHand.png"),
          "Mida Hand",
          "Give a unit Mida Hand",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          MidaHandPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(796, -18));

          GodPower AizStaffPower = new GodPower();
          AizStaffPower.id = "AizStaffPowerbutton";
          AizStaffPower.name = "AizStaffPowerbutton";
          AizStaffPower.forceBrush = "circ_0";
          AizStaffPower.holdAction = false;
          AizStaffPower.showToolSizes = false;
          AizStaffPower.unselectWhenWindow = false;
          AizStaffPower.click_action = new PowerActionWithID(action_AizStaffPowerClick);
          AssetManager.powers.add(AizStaffPower);

          var AizStaffPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "AizStaffPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_AizStaff_bronze.png"),
          "Aiz Staff",
          "Give a unit Aiz Staff",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          AizStaffPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(830, 18));

          GodPower DragonSlayerPower = new GodPower();
          DragonSlayerPower.id = "DragonSlayerPowerbutton";
          DragonSlayerPower.name = "DragonSlayerPowerbutton";
          DragonSlayerPower.forceBrush = "circ_0";
          DragonSlayerPower.holdAction = false;
          DragonSlayerPower.showToolSizes = false;
          DragonSlayerPower.unselectWhenWindow = false;
          DragonSlayerPower.click_action = new PowerActionWithID(action_DragonSlayerPowerClick);
          AssetManager.powers.add(DragonSlayerPower);

          var DragonSlayerPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "DragonSlayerPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_DragonSlayer_iron.png"),
          "DragonSlayer",
          "Give a unit DragonSlayer blade",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          DragonSlayerPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(830, -18));

          GodPower GrassSwordPower = new GodPower();
          GrassSwordPower.id = "GrassSwordPowerbutton";
          GrassSwordPower.name = "GrassSwordPowerbutton";
          GrassSwordPower.forceBrush = "circ_0";
          GrassSwordPower.holdAction = false;
          GrassSwordPower.showToolSizes = false;
          GrassSwordPower.unselectWhenWindow = false;
          GrassSwordPower.click_action = new PowerActionWithID(action_GrassSwordPowerClick);
          AssetManager.powers.add(GrassSwordPower);

          var GrassSwordPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "GrassSwordPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_GrassSword.png"),
          "Grass Sword",
          "Give a unit Grass Sword",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          GrassSwordPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(866, 18));

          GodPower DurantPower = new GodPower();
          DurantPower.id = "DurantPowerbutton";
          DurantPower.name = "DurantPowerbutton";
          DurantPower.forceBrush = "circ_0";
          DurantPower.holdAction = false;
          DurantPower.showToolSizes = false;
          DurantPower.unselectWhenWindow = false;
          DurantPower.click_action = new PowerActionWithID(action_DurantPowerClick);
          AssetManager.powers.add(DurantPower);

          var DurantPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "DurantPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Durant_iron.png"),
          "Durant",
          "Give a unit Durant sword",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          DurantPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(866, -18));

          GodPower DemonLPower = new GodPower();
          DemonLPower.id = "DemonLPowerbutton";
          DemonLPower.name = "DemonLPowerbutton";
          DemonLPower.forceBrush = "circ_0";
          DemonLPower.holdAction = false;
          DemonLPower.showToolSizes = false;
          DemonLPower.unselectWhenWindow = false;
          DemonLPower.click_action = new PowerActionWithID(action_DemonLPowerClick);
          AssetManager.powers.add(DemonLPower);

          var DemonLPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "DemonLPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_DemonL.png"),
          "Demon Lord Blade",
          "Give a unit the Demon Lord Blade",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          DemonLPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(902, 18));

          GodPower SmitePower = new GodPower();
          SmitePower.id = "SmitePowerbutton";
          SmitePower.name = "SmitePowerbutton";
          SmitePower.forceBrush = "circ_0";
          SmitePower.holdAction = false;
          SmitePower.showToolSizes = false;
          SmitePower.unselectWhenWindow = false;
          SmitePower.click_action = new PowerActionWithID(action_SmitePowerClick);
          AssetManager.powers.add(SmitePower);

          var SmitePowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "SmitePowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Smite.png"),
          "Smite",
          "Give a unit the Smite",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          SmitePowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(902, -18));

          GodPower SicklePower = new GodPower();
          SicklePower.id = "SicklePowerbutton";
          SicklePower.name = "SicklePowerbutton";
          SicklePower.forceBrush = "circ_0";
          SicklePower.holdAction = false;
          SicklePower.showToolSizes = false;
          SicklePower.unselectWhenWindow = false;
          SicklePower.click_action = new PowerActionWithID(action_SicklePowerClick);
          AssetManager.powers.add(SicklePower);

          var SicklePowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "SicklePowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_Sickle_iron.png"),
          "Sickle",
          "Give a unit the Sickle",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          SicklePowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(938, 18));

          GodPower DragonHeadPower = new GodPower();
          DragonHeadPower.id = "DragonHeadPowerbutton";
          DragonHeadPower.name = "DragonHeadPowerbutton";
          DragonHeadPower.forceBrush = "circ_0";
          DragonHeadPower.holdAction = false;
          DragonHeadPower.showToolSizes = false;
          DragonHeadPower.unselectWhenWindow = false;
          DragonHeadPower.click_action = new PowerActionWithID(action_DragonHeadPowerClick);
          AssetManager.powers.add(DragonHeadPower);

          var DragonHeadPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "DragonHeadPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_DragonHead.png"),
          "DragonHead",
          "Give a unit a DragonHead",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          DragonHeadPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(938, -18));

          GodPower DragonPower = new GodPower();
          DragonPower.id = "DragonPowerbutton";
          DragonPower.name = "DragonPowerbutton";
          DragonPower.forceBrush = "circ_0";
          DragonPower.holdAction = false;
          DragonPower.showToolSizes = false;
          DragonPower.unselectWhenWindow = false;
          DragonPower.click_action = new PowerActionWithID(action_DragonPowerClick);
          AssetManager.powers.add(DragonPower);

          var DragonPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "DragonPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_DragonArmor.png"),
          "Dragon Armor",
          "Give a unit the Dragon Armor",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          DragonPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(1010, 18));

          GodPower BerserkPower = new GodPower();
          BerserkPower.id = "BerserkPowerbutton";
          BerserkPower.name = "BerserkPowerbutton";
          BerserkPower.forceBrush = "circ_0";
          BerserkPower.holdAction = false;
          BerserkPower.showToolSizes = false;
          BerserkPower.unselectWhenWindow = false;
          BerserkPower.click_action = new PowerActionWithID(action_BerserkPowerClick);
          AssetManager.powers.add(BerserkPower);

          var BerserkPowerBut = NCMS.Utils.PowerButtons.CreateButton(
          "BerserkPowerbutton",
          Mod.EmbededResources.LoadSprite($"{Mod.Info.Name}.Resources.Icon.icon_BerserkHelmet.png"),
          "Berserker Armor",
          "Give a unit the Berserker Armor",
          Vector2.zero,
          NCMS.Utils.ButtonType.GodPower);

          NCMS.Utils.PowerButtons.AddButtonToTab(
          BerserkPowerBut,
          NCMS.Utils.PowerTab.Kingdoms,
          new Vector2(1010, -18));
        }
        public static bool action_Drop(WorldTile pTile, GodPower pPower){
          AssetManager.powers.CallMethod("spawnDrops", pTile, pPower);
          return true;
        }
        public static void action_spawnBoatDrop(WorldTile pTile = null, string pDropID = null)
        {
          if(pTile.Type.liquid && pTile.zone.city != null && pTile.zone.city.haveBuildingType("docks", true)){
            Actor actor = MapBox.instance.createNewUnit("boat_trading", pTile, "boat_trading", 1f, null);
            Kingdom kingdom = Reflection.GetField(pTile.zone.city.GetType(), pTile.zone.city, "kingdom") as Kingdom;
            actor.CallMethod("setKingdom", kingdom);
            actor.CallMethod("setCity", pTile.zone.city);
            Building buildingType = (Building)pTile.zone.city.CallMethod("getBuildingType", "docks", true, true);
            Docks dockComp = (Docks)Reflection.GetField(typeof(Building), buildingType, "component_docks");
            dockComp.addBoatToDock(actor);
            if (BehCheckArmyBoats.existingGroups.ContainsKey(pTile.zone.city))
            {
              BehCheckArmyBoats.existingGroups[pTile.zone.city].addUnit(actor);
            }
            else
            {
              UnitGroup newGroup = MapBox.instance.unitGroupManager.createNewGroup(pTile.zone.city);
              newGroup.addUnit(actor);
              BehCheckArmyBoats.existingGroups.Add(pTile.zone.city, newGroup);
            }
            actor.ai.nextJobDelegate = new GetNextJobID(BehCheckArmyBoats.getNextBoatJob);
          }
        }
        public static void action_spawnPirateDrop(WorldTile pTile = null, string pDropID = null)
        {
          if(pTile.Type.liquid){
            Actor actor = MapBox.instance.createNewUnit("boat_transport", pTile, "boat_transport", 1f, null);
            actor.addTrait("Pirate Boat");
          }
        }
        public static bool action_CrossPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Cross"), "wood", 0, "", "the pope", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_philoStonePowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("philostone"), "base", 0, "", "Nicholas Flamel", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_ShieldPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Shield"), "iron", 0, "", "Naofumi Iwatani", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_HGrailPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("HGrail"), "bronze", 0, "", "Igniz", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_RingPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("RingPower"), "bronze", 0, "", "Sauron", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Ring);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_GrimoirePowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Grimoire"), "bone", 0, "", "an acient mage", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_Scp035PowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Scp035"), "base", 0, "[Redacted]", "[Redacted]", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_LawsPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Laws"), "base", 0, "", "God (you)", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_TechPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Tech"), "iron", 0, "", "an acient race", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_HornPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Horn"), "wood", 0, "", "an acient warrior", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Amulet);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_FrogWandPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("FrogWand"), "wood", 0, "", "Kermit", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_MidaHandPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("MidaHand"), "base", 0, "", "Mida", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_AizStaffPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("AizStaff"), "bronze", 0, "", "our overlord ainz ooal gown", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_DragonSlayerPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("DragonSlayer"), "iron", 0, "", "Godo", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_GrassSwordPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("GrassSword"), "base", 0, "", "Grassy Wizard", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_DurantPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Durant"), "iron", 0, "", "a blacksmith from another dimension", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_DemonLPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("DemonL"), "base", 0, "", "Demon Lord", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_SmitePowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Smite"), "base", 0, "", "Cyclopes", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_SicklePowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("Sickle"), "iron", 0, "", "Grim Reaper", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
        public static bool action_DragonHeadPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("DragonHead"), "base", 0, "", "the acient WereDragon Tempest", 1, null) as ItemData;
                var pSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                pSlot.CallMethod("setItem", pData);
                Reflection.SetField(actor, "statsDirty", true);
                actor.addTrait("fire_proof");
              }
            }
          return true;
        }
        public static bool action_DragonPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pDataArmor = ItemGenerator.generateItem(AssetManager.items.get("DragonArmor"), "base", 0, "", "the acient WereDragon Tempest", 1, null) as ItemData;
                var pSlotArmor = actor.equipment.getSlot(EquipmentType.Armor);
                pSlotArmor.CallMethod("setItem", pDataArmor);
                ItemData pDataBoots = ItemGenerator.generateItem(AssetManager.items.get("DragonBoots"), "base", 0, "", "the acient WereDragon Tempest", 1, null) as ItemData;
                var pSlotBoots = actor.equipment.getSlot(EquipmentType.Boots);
                pSlotBoots.CallMethod("setItem", pDataBoots);
                ItemData pDataHelmet = ItemGenerator.generateItem(AssetManager.items.get("DragonHelmet"), "base", 0, "", "the acient WereDragon Tempest", 1, null) as ItemData;
                var pSlotHelmet = actor.equipment.getSlot(EquipmentType.Helmet);
                pSlotHelmet.CallMethod("setItem", pDataHelmet);
                Reflection.SetField(actor, "statsDirty", true);
                actor.addTrait("fire_proof");
              }
            }
          return true;
        }
        public static bool action_BerserkPowerClick(WorldTile pTile, string pPowerID){
          for (int i = 0; i < pTile.units.Count; i++)
            {
             Actor actor = pTile.units[i];
              if (actor.base_data.alive != null)
              {
                ItemData pDataArmor = ItemGenerator.generateItem(AssetManager.items.get("BerserkArmor"), "base", 0, "", "Hanarr", 1, null) as ItemData;
                var pSlotArmor = actor.equipment.getSlot(EquipmentType.Armor);
                pSlotArmor.CallMethod("setItem", pDataArmor);
                ItemData pDataBoots = ItemGenerator.generateItem(AssetManager.items.get("BerserkBoots"), "base", 0, "", "Hanarr", 1, null) as ItemData;
                var pSlotBoots = actor.equipment.getSlot(EquipmentType.Boots);
                pSlotBoots.CallMethod("setItem", pDataBoots);
                ItemData pDataHelmet = ItemGenerator.generateItem(AssetManager.items.get("BerserkHelmet"), "base", 0, "", "Hanarr", 1, null) as ItemData;
                var pSlotHelmet = actor.equipment.getSlot(EquipmentType.Helmet);
                pSlotHelmet.CallMethod("setItem", pDataHelmet);
                Reflection.SetField(actor, "statsDirty", true);
              }
            }
          return true;
        }
    }
}
