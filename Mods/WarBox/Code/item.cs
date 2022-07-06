using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using NCMS.Utils;

namespace WarBox
{
    class item
    {
        public static void init()
        {
          //Don't blame me for the my bad coding skill
          ItemAsset FrogWand = AssetManager.items.clone("FrogWand", "sword");
          FrogWand.id = "FrogWand";
          FrogWand.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          FrogWand.materials = List.Of<string>(new string[]{"wood"});
          FrogWand.baseStats.range = 169f;
          FrogWand.baseStats.attackSpeed = 60f;
          FrogWand.equipment_value = 400;
          FrogWand.slash = "sword";
          FrogWand.equipmentType = EquipmentType.Weapon;
          FrogWand.quality = ItemQuality.Legendary;
          FrogWand.name_class = "item_class_weapon";
          FrogWand.attackAction = (WorldAction)Delegate.Combine(FrogWand.attackAction, new WorldAction(FrogMagic));
          AssetManager.items.list.AddItem(FrogWand);
          Localization.addLocalization("item_FrogWand", "Frog Wand");
          addItemSprite(FrogWand.id, FrogWand.materials[0]);

          ItemAsset MidaHand = AssetManager.items.clone("MidaHand", "hands");
          MidaHand.id = "MidaHand";
          MidaHand.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
            "sword_name_king#3",
            "weapon_name_city",
            "weapon_name_kingdom",
            "weapon_name_culture",
            "weapon_name_enemy_king",
            "weapon_name_enemy_kingdom"
          });
          MidaHand.materials = List.Of<string>(new string[]{"base"});
          MidaHand.attackAction = (WorldAction)Delegate.Combine(MidaHand.attackAction, new WorldAction(MidaPower));
          MidaHand.baseStats.attackSpeed = 20f;
          MidaHand.equipment_value = 300;
          MidaHand.slash = "punch";
          MidaHand.quality = ItemQuality.Legendary;
          AssetManager.items.list.AddItem(MidaHand);
          Localization.addLocalization("item_MidaHand", "Mida Hand");
          addItemSprite(MidaHand.id, MidaHand.materials[0]);

          ItemAsset AizStaff = AssetManager.items.clone("AizStaff", "_range");
          AizStaff.id = "AizStaff";
          AizStaff.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
            "sword_name_king#3",
            "weapon_name_city",
            "weapon_name_kingdom",
            "weapon_name_culture",
            "weapon_name_enemy_king",
            "weapon_name_enemy_kingdom"
          });
          AizStaff.materials = List.Of<string>(new string[]{"bronze"});
          AizStaff.projectile = "dark_orb";
          AizStaff.baseStats.range = 169f;
          AizStaff.baseStats.accuracy = 999;
          AizStaff.baseStats.attackSpeed = 20f;
          AizStaff.baseStats.damage = 200;
          AizStaff.baseStats.health = 1000;
          AizStaff.equipment_value = 800;
          AizStaff.slash = "punch";
          AizStaff.quality = ItemQuality.Legendary;
          AssetManager.items.list.AddItem(AizStaff);
          Localization.addLocalization("item_AizStaff", "Aiz Staff");
          addStaffSprite(AizStaff.id, AizStaff.materials[0]);

          ItemAsset DragonSlayer = AssetManager.items.clone("DragonSlayer", "sword");
          DragonSlayer.id = "DragonSlayer";
          DragonSlayer.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          DragonSlayer.materials = List.Of<string>(new string[]{"iron"});
          DragonSlayer.baseStats.attackSpeed = -20f;
          DragonSlayer.baseStats.damage = 169;
          DragonSlayer.baseStats.health = 400;
          DragonSlayer.baseStats.targets = 5;
          DragonSlayer.equipment_value = 500;
          DragonSlayer.baseStats.knockbackReduction = 100f;
          DragonSlayer.slash = "sword";
          DragonSlayer.equipmentType = EquipmentType.Weapon;
          DragonSlayer.quality = ItemQuality.Legendary;
          DragonSlayer.name_class = "item_class_weapon";
          DragonSlayer.attackAction = (WorldAction)Delegate.Combine(DragonSlayer.attackAction, new WorldAction(DragonSlayerPower));
          AssetManager.items.list.AddItem(DragonSlayer);
          Localization.addLocalization("item_DragonSlayer", "DragonSlayer");
          addItemSprite(DragonSlayer.id, DragonSlayer.materials[0]);

          ItemAsset GrassSword = AssetManager.items.clone("GrassSword", "sword");
          GrassSword.id = "GrassSword";
          GrassSword.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          GrassSword.materials = List.Of<string>(new string[]{"base"});
          GrassSword.baseStats.attackSpeed = 50f;
          GrassSword.baseStats.damage = 20;
          GrassSword.equipment_value = 400;
          GrassSword.slash = "sword";
          GrassSword.equipmentType = EquipmentType.Weapon;
          GrassSword.quality = ItemQuality.Legendary;
          GrassSword.name_class = "item_class_weapon";
          GrassSword.attackAction = (WorldAction)Delegate.Combine(GrassSword.attackAction, new WorldAction(GrassSwordPower));
          AssetManager.items.list.AddItem(GrassSword);
          Localization.addLocalization("item_GrassSword", "GrassSword");
          addItemSprite(GrassSword.id, GrassSword.materials[0]);

          ItemAsset Durant = AssetManager.items.clone("Durant", "sword"); //Hero as returned (rly cool)
          Durant.id = "Durant";
          Durant.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          Durant.materials = List.Of<string>(new string[]{"iron"});
          Durant.baseStats.attackSpeed = -25f;
          Durant.baseStats.damage = 120;
          Durant.baseStats.health = 300;
          Durant.baseStats.targets = 3;
          Durant.equipment_value = 700;
          Durant.baseStats.knockbackReduction = 100f;
          Durant.slash = "sword";
          Durant.equipmentType = EquipmentType.Weapon;
          Durant.quality = ItemQuality.Legendary;
          Durant.name_class = "item_class_weapon";
          Durant.attackAction = (WorldAction)Delegate.Combine(Durant.attackAction, new WorldAction(DurantPower));
          AssetManager.items.list.AddItem(Durant);
          Localization.addLocalization("item_Durant", "Durant");
          addItemSprite(Durant.id, Durant.materials[0]);

          ItemAsset DemonL = AssetManager.items.clone("DemonL", "sword");
          DemonL.id = "DemonL";
          DemonL.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          DemonL.materials = List.Of<string>(new string[]{"base"});
          DemonL.baseStats.attackSpeed = -10f;
          DemonL.baseStats.damage = 150;
          DemonL.baseStats.health = 500;
          DemonL.baseStats.targets = 3;
          DemonL.equipment_value = 800;
          DemonL.baseStats.knockbackReduction = 100f;
          DemonL.slash = "sword";
          DemonL.quality = ItemQuality.Legendary;
          DemonL.equipmentType = EquipmentType.Weapon;
          DemonL.name_class = "item_class_weapon";
          DemonL.attackAction = (WorldAction)Delegate.Combine(DemonL.attackAction, new WorldAction(DemonLPower));
          AssetManager.items.list.AddItem(DemonL);
          Localization.addLocalization("item_DemonL", "Demon Lord Blade");
          addItemSprite(DemonL.id, DemonL.materials[0]);

          ItemAsset Smite = AssetManager.items.clone("Smite", "sword");
          Smite.id = "Smite";
          Smite.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          Smite.materials = List.Of<string>(new string[]{"base"});
          Smite.baseStats.range = 169f;
          Smite.baseStats.attackSpeed = 60f;
          Smite.baseStats.knockbackReduction = 100f;
          Smite.equipment_value = 700;
          Smite.slash = "sword";
          Smite.equipmentType = EquipmentType.Weapon;
          Smite.quality = ItemQuality.Legendary;
          Smite.name_class = "item_class_weapon";
          Smite.attackAction = (WorldAction)Delegate.Combine(Smite.attackAction, new WorldAction(SmitePower));
          AssetManager.items.list.AddItem(Smite);
          Localization.addLocalization("item_Smite", "Smite");
          addItemSprite(Smite.id, Smite.materials[0]);

          ItemAsset Sickle = AssetManager.items.clone("Sickle", "sword");
          Sickle.id = "Sickle";
          Sickle.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
      			"sword_name_king#3",
      			"weapon_name_city",
      			"weapon_name_kingdom",
      			"weapon_name_culture",
      			"weapon_name_enemy_king",
      			"weapon_name_enemy_kingdom"
          });
          Sickle.materials = List.Of<string>(new string[]{"iron"});
          Sickle.baseStats.damage = 9999;
          Sickle.baseStats.attackSpeed = 60f;
          Sickle.baseStats.knockbackReduction = 100f;
          Sickle.equipment_value = 90000;
          Sickle.slash = "sword";
          Sickle.quality = ItemQuality.Legendary;
          Sickle.equipmentType = EquipmentType.Weapon;
          Sickle.name_class = "item_class_weapon";
          AssetManager.items.list.AddItem(Sickle);
          Localization.addLocalization("item_Sickle", "Sickle");
          addStaffSprite(Sickle.id, Sickle.materials[0]);

          ItemAsset DragonHead = AssetManager.items.clone("DragonHead", "_range");
          DragonHead.id = "DragonHead";
          DragonHead.name_templates = Toolbox.splitStringIntoList(new string[]
          {
            "sword_name#30",
            "sword_name_king#3",
            "weapon_name_city",
            "weapon_name_kingdom",
            "weapon_name_culture",
            "weapon_name_enemy_king",
            "weapon_name_enemy_kingdom"
          });
          DragonHead.materials = List.Of<string>(new string[]{"base"});
          DragonHead.projectile = "red_orb";
          DragonHead.baseStats.projectiles = 50;
          DragonHead.baseStats.range = 999f;
          DragonHead.baseStats.damage = 200;
          DragonHead.baseStats.attackSpeed = -20f;
          DragonHead.baseStats.health = 500;
          DragonHead.equipment_value = 900;
          DragonHead.slash = "punch";
          DragonHead.quality = ItemQuality.Legendary;
          AssetManager.items.list.AddItem(DragonHead);
          Localization.addLocalization("item_DragonHead", "DragonHead");
          addItemSprite(DragonHead.id, DragonHead.materials[0]);

          ItemAsset Shield = AssetManager.items.clone("Shield", "_accessory");
          Shield.id = "Shield";
          Shield.quality = ItemQuality.Legendary;
          Shield.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Shield.equipmentType = EquipmentType.Amulet;
          Shield.baseStats.armor = 100;
          Shield.baseStats.knockbackReduction = 100f;
          Shield.materials = List.Of<string>(new string[]{"iron"});
          Shield.equipment_value = 600;
          AssetManager.items.list.AddItem(Shield);
          Localization.addLocalization("item_Shield", "Shield");

          ItemAsset HGrail = AssetManager.items.clone("HGrail", "_accessory");
          HGrail.id = "HGrail";
          HGrail.quality = ItemQuality.Legendary;
          HGrail.name_templates = List.Of<string>(new string[]{"amulet_name"});
          HGrail.equipmentType = EquipmentType.Amulet;
          HGrail.baseStats.armor = 20;
          HGrail.baseStats.health = 1000;
          HGrail.materials = List.Of<string>(new string[]{"bronze"});
          HGrail.equipment_value = 300;
          AssetManager.items.list.AddItem(HGrail);
          Localization.addLocalization("item_HGrail", "Holy Grail");

          ItemAsset RingPower = AssetManager.items.clone("RingPower", "_accessory");
          RingPower.id = "RingPower";
          RingPower.quality = ItemQuality.Legendary;
          RingPower.name_templates = List.Of<string>(new string[]{"ring_name"});
          RingPower.equipmentType = EquipmentType.Ring;
          RingPower.baseStats.damage = 120;
          RingPower.baseStats.health = 2000;
          RingPower.baseStats.knockbackReduction = 100f;
          RingPower.materials = List.Of<string>(new string[]{"bronze"});
          RingPower.equipment_value = 700;
          AssetManager.items.list.AddItem(RingPower);
          Localization.addLocalization("item_RingPower", "Ring of Power");

          ItemAsset Grimoire = AssetManager.items.clone("Grimoire", "_accessory");
          Grimoire.id = "Grimoire";
          Grimoire.quality = ItemQuality.Legendary;
          Grimoire.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Grimoire.equipmentType = EquipmentType.Amulet;
          Grimoire.materials = List.Of<string>(new string[]{"bone"});
          Grimoire.equipment_value = 500;
          AssetManager.items.list.AddItem(Grimoire);
          Localization.addLocalization("item_Grimoire", "Grimoire");

          ItemAsset Cross = AssetManager.items.clone("Cross", "_accessory");
          Cross.id = "Cross";
          Cross.quality = ItemQuality.Legendary;
          Cross.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Cross.equipmentType = EquipmentType.Amulet;
          Cross.materials = List.Of<string>(new string[]{"wood"});
          Cross.equipment_value = 200;
          AssetManager.items.list.AddItem(Cross);
          Localization.addLocalization("item_Cross", "Cross");

          ItemAsset philostone = AssetManager.items.clone("philostone", "_accessory");
          philostone.id = "philostone";
          philostone.quality = ItemQuality.Legendary;
          philostone.name_templates = List.Of<string>(new string[]{"amulet_name"});
          philostone.equipmentType = EquipmentType.Amulet;
          philostone.materials = List.Of<string>(new string[]{"base"});
          philostone.equipment_value = 400;
          AssetManager.items.list.AddItem(philostone);
          Localization.addLocalization("item_philostone", "Philostone stone");

          ItemAsset Scp035 = AssetManager.items.clone("Scp035", "_accessory"); //ScpBox when?
          Scp035.id = "Scp035";
          Scp035.quality = ItemQuality.Legendary;
          Scp035.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Scp035.equipmentType = EquipmentType.Amulet;
          Scp035.materials = List.Of<string>(new string[]{"base"});
          Scp035.baseStats.health = 200;
          Scp035.equipment_value = 300;
          AssetManager.items.list.AddItem(Scp035);
          Localization.addLocalization("item_Scp035", "Scp-035");

          ItemAsset Laws = AssetManager.items.clone("Laws", "_accessory");
          Laws.id = "Laws";
          Laws.quality = ItemQuality.Legendary;
          Laws.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Laws.equipmentType = EquipmentType.Amulet;
          Laws.materials = List.Of<string>(new string[]{"base"});
          Laws.baseStats.diplomacy = 200;
          Laws.baseStats.warfare = 200;
          Laws.baseStats.stewardship = 200;
          Laws.baseStats.cities = 10;
          Laws.equipment_value = 200;
          AssetManager.items.list.AddItem(Laws);
          Localization.addLocalization("item_Laws", "The tables of Laws");

          ItemAsset Tech = AssetManager.items.clone("Tech", "_accessory");
          Tech.id = "Tech";
          Tech.quality = ItemQuality.Legendary;
          Tech.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Tech.equipmentType = EquipmentType.Amulet;
          Tech.materials = List.Of<string>(new string[]{"iron"});
          Tech.baseStats.diplomacy = 200;
          Tech.baseStats.warfare = 200;
          Tech.baseStats.stewardship = 200;
          Tech.baseStats.cities = 10;
          Tech.equipment_value = 700;
          AssetManager.items.list.AddItem(Tech);
          Localization.addLocalization("item_Tech", "Acient Tech");

          ItemAsset Horn = AssetManager.items.clone("Horn", "_accessory");
          Horn.id = "Horn";
          Horn.quality = ItemQuality.Legendary;
          Horn.name_templates = List.Of<string>(new string[]{"amulet_name"});
          Horn.equipmentType = EquipmentType.Amulet;
          Horn.baseStats.knockbackReduction = 100f;
          Horn.materials = List.Of<string>(new string[]{"wood"});
          Horn.equipment_value = 400;
          AssetManager.items.list.AddItem(Horn);
          Localization.addLocalization("item_Horn", "Magic Horn");

          ItemAsset DragonArmor = AssetManager.items.clone("DragonArmor", "_equipment");
          DragonArmor.id = "DragonArmor";
          DragonArmor.quality = ItemQuality.Legendary;
          DragonArmor.name_class = "item_class_armor";
          DragonArmor.name_templates = List.Of<string>(new string[]{"Armor_name"});
          DragonArmor.equipmentType = EquipmentType.Armor;
          DragonArmor.baseStats.knockbackReduction = 100f;
          DragonArmor.baseStats.armor = 20;
          DragonArmor.baseStats.damage = 80;
          DragonArmor.baseStats.health = 400;
          DragonArmor.materials = List.Of<string>(new string[]{"base"});
          DragonArmor.equipment_value = 900;
          AssetManager.items.list.AddItem(DragonArmor);
          Localization.addLocalization("item_DragonArmor", "Dragon Armor");

          ItemAsset DragonHelmet = AssetManager.items.clone("DragonHelmet", "_equipment");
          DragonHelmet.id = "DragonHelmet";
          DragonHelmet.quality = ItemQuality.Legendary;
          DragonHelmet.name_class = "item_class_armor";
          DragonHelmet.name_templates = List.Of<string>(new string[]{"helmet_name"});
          DragonHelmet.equipmentType = EquipmentType.Helmet;
          DragonHelmet.baseStats.knockbackReduction = 100f;
          DragonHelmet.baseStats.armor = 5;
          DragonHelmet.baseStats.health = 300;
          DragonHelmet.materials = List.Of<string>(new string[]{"base"});
          DragonHelmet.equipment_value = 900;
          AssetManager.items.list.AddItem(DragonHelmet);
          Localization.addLocalization("item_DragonHelmet", "Dragon Helmet");

          ItemAsset DragonBoots = AssetManager.items.clone("DragonBoots", "_equipment");
          DragonBoots.id = "DragonBoots";
          DragonBoots.quality = ItemQuality.Legendary;
          DragonBoots.name_class = "item_class_armor";
          DragonBoots.name_templates = List.Of<string>(new string[]{"boots_name"});
          DragonBoots.equipmentType = EquipmentType.Boots;
          DragonBoots.baseStats.knockbackReduction = 100f;
          DragonBoots.baseStats.armor = 10;
          DragonBoots.baseStats.speed = 60;
          DragonBoots.baseStats.health = 300;
          DragonBoots.materials = List.Of<string>(new string[]{"base"});
          DragonBoots.equipment_value = 900;
          AssetManager.items.list.AddItem(DragonBoots);
          Localization.addLocalization("item_DragonBoots", "Dragon Boots");

          ItemAsset BerserkArmor = AssetManager.items.clone("BerserkArmor", "_equipment");
          BerserkArmor.id = "BerserkArmor";
          BerserkArmor.quality = ItemQuality.Legendary;
          BerserkArmor.name_class = "item_class_armor";
          BerserkArmor.name_templates = List.Of<string>(new string[]{"Armor_name"});
          BerserkArmor.equipmentType = EquipmentType.Armor;
          BerserkArmor.baseStats.knockbackReduction = 100f;
          BerserkArmor.baseStats.attackSpeed = 100;
          BerserkArmor.baseStats.armor = 20;
          BerserkArmor.baseStats.health = 170;
          BerserkArmor.materials = List.Of<string>(new string[]{"base"});
          BerserkArmor.equipment_value = 900;
          AssetManager.items.list.AddItem(BerserkArmor);
          Localization.addLocalization("item_BerserkArmor", "Berserk Armor");

          ItemAsset BerserkHelmet = AssetManager.items.clone("BerserkHelmet", "_equipment");
          BerserkHelmet.id = "BerserkHelmet";
          BerserkHelmet.quality = ItemQuality.Legendary;
          BerserkHelmet.name_class = "item_class_armor";
          BerserkHelmet.name_templates = List.Of<string>(new string[]{"helmet_name"});
          BerserkHelmet.equipmentType = EquipmentType.Helmet;
          BerserkHelmet.baseStats.knockbackReduction = 100f;
          BerserkHelmet.baseStats.armor = 10;
          BerserkHelmet.baseStats.damage = 20;
          BerserkHelmet.baseStats.health = 197;
          BerserkHelmet.materials = List.Of<string>(new string[]{"base"});
          BerserkHelmet.equipment_value = 900;
          AssetManager.items.list.AddItem(BerserkHelmet);
          Localization.addLocalization("item_BerserkHelmet", "Berserk Helmet");

          ItemAsset BerserkBoots = AssetManager.items.clone("BerserkBoots", "_equipment");
          BerserkBoots.id = "BerserkBoots";
          BerserkBoots.quality = ItemQuality.Legendary;
          BerserkBoots.name_class = "item_class_armor";
          BerserkBoots.name_templates = List.Of<string>(new string[]{"boots_name"});
          BerserkBoots.equipmentType = EquipmentType.Boots;
          BerserkBoots.baseStats.knockbackReduction = 100f;
          BerserkBoots.baseStats.armor = 10;
          BerserkBoots.baseStats.health = 199;
          BerserkBoots.materials = List.Of<string>(new string[]{"base"});
          BerserkBoots.equipment_value = 900;
          AssetManager.items.list.AddItem(BerserkBoots);
          Localization.addLocalization("item_BerserkBoots", "Berserk Boots");
        }
        public static void addItemSprite(string id, string material)
    		{
          var dictItems = Reflection.GetField(typeof(ActorAnimationLoader), null, "dictItems") as Dictionary<string, Sprite>;
          var sprite = Resources.Load<Sprite>("actors/races/items/w_" + id + "_" + material);
          dictItems.Add(sprite.name, sprite);
        }
        public static void addStaffSprite(string id, string material)
    		{
          var dictItems = Reflection.GetField(typeof(ActorAnimationLoader), null, "dictItems") as Dictionary<string, Sprite>;
          var sprite = Resources.Load<Sprite>("actors/races/items/Staff/w_" + id + "_" + material);
          dictItems.Add(sprite.name, sprite);
        }
        public static bool FrogMagic(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTarget != null){
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          a.killHimself(false, AttackType.Other, false, true);
          MapBox.instance.createNewUnit("frog", pTile, "frog", 1f, null);
          }
      		return true;
      	}
        public static bool MidaPower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTarget != null){
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          a.killHimself(false, AttackType.Other, false, true);
          MapBox.instance.dropManager.spawn(pTile, "gold", 0f, -1f);
          }
      		return true;
      	}
        public static bool DragonSlayerPower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTarget != null){
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if(a.stats.id == "demon" || a.stats.id == "ghost" || a.stats.id == "dragon"){
              a.killHimself(false, AttackType.Other, false, true);
            }
          }
      		return true;
      	}
        public static bool GrassSwordPower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTarget != null){
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          a.killHimself(false, AttackType.Other, false, true);
          ActionLibrary.tryToGrowTree(pTarget, pTile);
          }
      		return true;
      	}
        public static bool DurantPower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(Toolbox.randomChance(0.4f)){
          MapBox.instance.spawnFlash(pTile, 2);
          MapBox.instance.applyForce(pTile, 10, 0.05f, true, false, 3, null, null, null);
          }
      		return true;
      	}
        public static bool DemonLPower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTarget != null){
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          ActionLibrary.addBurningEffectOnTarget(a, pTile);
          a.addTrait("cursed");
          }
      		return true;
      	}
        public static bool SmitePower(BaseSimObject pTarget, WorldTile pTile = null)
      	{
          if(pTile != null){
          MapBox.spawnLightning(pTile, 0.05f);
          }
      		return true;
      	}
    }
}
