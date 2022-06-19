using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;

namespace PowerBox
{
    partial class WorldBoxMod
    {
        private static void initActorsAssets()
        {
            var wolf = AssetManager.unitStats.get("wolf");

            ActorStats burgerSpiderActorStats = AssetManager.unitStats.clone("burgerSpider", "wolf");
            Helper.Utils.CopyClass(wolf, burgerSpiderActorStats, true);
            burgerSpiderActorStats.id = "burgerSpider";
            burgerSpiderActorStats.maxAge = 250;
            burgerSpiderActorStats.race = "burgerSpider";
            burgerSpiderActorStats.kingdom = "burgers";
            burgerSpiderActorStats.unit = false;
            burgerSpiderActorStats.shadow = false;
            burgerSpiderActorStats.canAttackBuildings = true;
            burgerSpiderActorStats.canTurnIntoZombie = false;
            burgerSpiderActorStats.canBeMovedByPowers = true;
            burgerSpiderActorStats.canBeKilledByStuff = true;
            burgerSpiderActorStats.canReceiveTraits = true;
            burgerSpiderActorStats.canBeHurtByPowers = true;
            burgerSpiderActorStats.texture_path = "t_burgerSpider";
            burgerSpiderActorStats.icon = "iconWolf";
            burgerSpiderActorStats.job = "animal_herd";
            burgerSpiderActorStats.playRandomSound = true;
            burgerSpiderActorStats.playRandomSound_id = "goo";
            burgerSpiderActorStats.diet_meat_same_race = true;
            burgerSpiderActorStats.diet_meat = true;
            burgerSpiderActorStats.texture_heads = "";
            burgerSpiderActorStats.use_items = false;
            burgerSpiderActorStats.baseStats.damage = 25;
            burgerSpiderActorStats.traits.Add("regeneration");
            burgerSpiderActorStats.traits.Add("ugly");
            burgerSpiderActorStats.traits.Add("cursed");
            burgerSpiderActorStats.nameTemplate = "burger_spider_name";
            //AssetManager.unitStats.add(burgerSpiderActorStats);

            texturePathes.Add(burgerSpiderActorStats.texture_path);

            NameGeneratorAsset burgerSpiderName = new NameGeneratorAsset();
            burgerSpiderName.id = "burger_spider_name";
            burgerSpiderName.part_groups.Add("Burger,Spider");
            burgerSpiderName.part_groups.Add("-");
            burgerSpiderName.part_groups.Add("spider,burger");
            burgerSpiderName.templates.Add("part_group");
            AssetManager.nameGenerator.add(burgerSpiderName);


            KingdomAsset burgerKingdom = new KingdomAsset();
            burgerKingdom.id = "burgers";
            burgerKingdom.mobs = true;
            burgerKingdom.addTag("burgers");
            burgerKingdom.addTag("nature_creature");
            burgerKingdom.addFriendlyTag("nature_creature");
            burgerKingdom.addFriendlyTag("neutral");
            burgerKingdom.addEnemyTag("civ");
            burgerKingdom.addEnemyTag("bandits");
            burgerKingdom.addEnemyTag("developers");
            AssetManager.kingdoms.add(burgerKingdom);

            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", burgerKingdom);

            var human = AssetManager.kingdoms.get("human");
            human.addEnemyTag("burgers");
            human.addFriendlyTag("developers");
            var elf = AssetManager.kingdoms.get("elf");
            elf.addEnemyTag("burgers");
            elf.addFriendlyTag("developers");
            var dwarf = AssetManager.kingdoms.get("dwarf");
            dwarf.addEnemyTag("burgers");
            dwarf.addFriendlyTag("developers");
            var orc = AssetManager.kingdoms.get("orc");
            orc.addEnemyTag("burgers");
            orc.addFriendlyTag("developers");
            var bandit = AssetManager.kingdoms.get("bandits");
            bandit.addEnemyTag("burgers");
            bandit.addFriendlyTag("developers");



            var mage = AssetManager.unitStats.get("whiteMage");

            ActorStats MaximCreature = AssetManager.unitStats.clone("MaximCreature", "whiteMage");
            Helper.Utils.CopyClass(mage, MaximCreature, true);
            MaximCreature.id = "MaximCreature";
            MaximCreature.maxAge = 1000;
            MaximCreature.race = "good";
            MaximCreature.kingdom = "developers";
            MaximCreature.unit = false;
            MaximCreature.canAttackBuildings = false;
            MaximCreature.canTurnIntoZombie = false;
            MaximCreature.canBeMovedByPowers = true;
            MaximCreature.canBeKilledByStuff = true;
            MaximCreature.canReceiveTraits = true;
            MaximCreature.canBeHurtByPowers = true;
            MaximCreature.canAttackBuildings = false;
            MaximCreature.shadow = false;
            MaximCreature.texture_path = "t_MaximCreature";
            MaximCreature.job = "white_mage";
            MaximCreature.playRandomSound_id = "human";
            MaximCreature.diet_meat_same_race = false;
            MaximCreature.diet_meat = false;
            MaximCreature.baseStats.damage = 100;
            MaximCreature.baseStats.health = 1000;
            MaximCreature.use_items = false;
            MaximCreature.defaultWeapons = null;
            MaximCreature.source_meat = false;
            MaximCreature.source_meat_insect = false;
            MaximCreature.traits = new List<string>();
            MaximCreature.traits.Add("immortal");
            MaximCreature.traits.Add("blessed");
            MaximCreature.traits.Add("wise");
            MaximCreature.nameTemplate = "maxim_creature_name";
            //AssetManager.unitStats.add(MaximCreature);

            texturePathes.Add(MaximCreature.texture_path);

            NameGeneratorAsset MaximCreatureName = new NameGeneratorAsset();
            MaximCreatureName.id = "maxim_creature_name";
            MaximCreatureName.part_groups.Add("Maxim,Max,Greg,dev");
            MaximCreatureName.part_groups.Add(" ");
            MaximCreatureName.part_groups.Add("Karpenko,dev,greg");
            MaximCreatureName.templates.Add("part_group");
            AssetManager.nameGenerator.add(MaximCreatureName);

            ActorStats MastefCreature = AssetManager.unitStats.clone("MastefCreature", "MaximCreature");
            Helper.Utils.CopyClass(MaximCreature, MastefCreature, true);
            MastefCreature.id = "MastefCreature";
            MastefCreature.texture_path = "t_MastefCreature";
            MastefCreature.unit = false;
            MastefCreature.shadow = false;
            MastefCreature.traits = new List<string>();
            MastefCreature.traits.Add("immortal");
            MastefCreature.traits.Add("blessed");
            MastefCreature.traits.Add("fast");
            MastefCreature.nameTemplate = "mastef_creature_name";
            //AssetManager.unitStats.add(MastefCreature);

            texturePathes.Add(MastefCreature.texture_path);

            NameGeneratorAsset MastefCreatureName = new NameGeneratorAsset();
            MastefCreatureName.id = "mastef_creature_name";
            MastefCreatureName.part_groups.Add("Mastef,Markus,Big Lebovski,Greg,dev");
            MastefCreatureName.part_groups.Add(" ");
            MastefCreatureName.part_groups.Add("Stefanko,dev,greg");
            MastefCreatureName.templates.Add("part_group");
            AssetManager.nameGenerator.add(MastefCreatureName);

            KingdomAsset developersKingdom = new KingdomAsset();
            developersKingdom.id = "developers";
            developersKingdom.mobs = true;
            developersKingdom.addTag("developers");
            developersKingdom.addTag("good");
            developersKingdom.addFriendlyTag("neutral");
            developersKingdom.addFriendlyTag("civ");
            developersKingdom.addEnemyTag("burgers");
            AssetManager.kingdoms.add(developersKingdom);


            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", developersKingdom);
        }
    }
}
