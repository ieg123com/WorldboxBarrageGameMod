using System;
using NCMS.Utils;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace WarBox{
    [ModEntry]
    class Main : MonoBehaviour{
        void Awake(){
            Button.init();
            item.init();
            Trait.init();

            ProjectileAsset AcidBall = new ProjectileAsset();
            AcidBall.id = "AcidBall";
            AcidBall.speed = 15f;
            AcidBall.texture = "rock";
            AcidBall.parabolic = true;
            AcidBall.texture_shadow = "shadow_ball";
            AcidBall.hitShake = true;
            AcidBall.startScale = 0.05f;
            AcidBall.targetScale = 0.09f;
            AcidBall.world_actions = (WorldAction)Delegate.Combine(AcidBall.world_actions, new WorldAction(action_AcidBall));
            AssetManager.projectiles.add(AcidBall);

            // Some Behaviour Stuff
            BehCheckArmyBoats.init();
            BehBoatLeaderCheck.init();
            BehBoatFight.init();
        }
        public static bool action_AcidBall(BaseSimObject pTarget = null, WorldTile pTile = null)
      	{
          MapBox.instance.CallMethod("getObjectsInChunks", pTile, 1, MapObjectType.Actor);
          var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
          for (int i = 0; i < temp_map_objects.Count; i++)
           {
             Actor actor = (Actor)temp_map_objects[i];
              {
                if(actor.base_data.alive){
                  ActionLibrary.addPoisonedEffectOnTarget(actor, null);
                }
              }
            }
      		return true;
      	}
        public void Update(){
          Equipment();
        }
        static string civId = "unit_human" + "unit_elf" + "unit_orc" + "unit_dwarf" + "baby_human" + "baby_elf" + "baby_orc" + "baby_dwarf";
        void Equipment(){
          var Units = MapBox.instance.units.getSimpleList();
          foreach(var unit in Units)
            {
              if(civId.Contains(unit.stats.id)){
                var pSlot = unit.equipment.getSlot(EquipmentType.Amulet);
                if(pSlot.data != null){
                  if(pSlot.data.id == "Shield"){
                    ActionLibrary.addShieldEffectOnTarget(unit, null);
                    unit.addTrait("regeneration");
                  }
                  if(pSlot.data.id == "HGrail"){
                    unit.addTrait("immortal");
                    ActorStatus DataUnit = Reflection.GetField(unit.GetType(), unit, "data") as ActorStatus;
                    if(Toolbox.randomChance(0.005f)){
                      unit.restoreHealth(50);
                      unit.removeTrait("crippled");
                      unit.removeTrait("infected");
                      unit.removeTrait("cursed");
                    }
                    DataUnit.mood = "happy";
                  }
                  if(pSlot.data.id == "Grimoire"){
                      ActorStatus Data = Reflection.GetField(unit.GetType(), unit, "data") as ActorStatus;
                      if(Data.health <= 70){
                      WorldTile pTile = unit.currentTile;
                      var act = MapBox.instance.createNewUnit("evilMage", pTile, null, 0f, null);
                      act.kingdom = unit.kingdom;
                      unit.restoreHealth(50);
                      }
                  }
                  if(pSlot.data.id == "Cross"){
                    WorldTile pTile = unit.currentTile;
                    MapBox.instance.CallMethod("getObjectsInChunks", pTile, 5, MapObjectType.Actor);
                    var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
                    for (int i = 0; i < temp_map_objects.Count; i++)
                     {
                       Actor actor = (Actor)temp_map_objects[i];
                        if(actor.stats.id == "demon" || actor.stats.id == "ghost"){
                        actor.killHimself(false, AttackType.Other, false, true);
                        WorldTile pTileActor = actor.currentTile;
                        MapBox.instance.fxDivineLight.playOn(pTileActor);
                        }
                      }
                  }
                  if(pSlot.data.id == "philostone"){
                    unit.addTrait("immortal");
                    ActorStatus DataUnit = Reflection.GetField(unit.GetType(), unit, "data") as ActorStatus;
                    if(Toolbox.randomChance(0.005f)){
                      unit.restoreHealth(200);
                      unit.removeTrait("crippled");
                      unit.removeTrait("infected");
                      unit.removeTrait("cursed");
                      var City = unit.city;
                      CityData dataC = Reflection.GetField(City.GetType(), City, "data") as CityData;
                      dataC.storage.change("gold", 100);
                    }
                  }
                  if(pSlot.data.id == "Scp035"){
                    WorldTile pTile = unit.currentTile;
                    MapBox.instance.CallMethod("getObjectsInChunks", pTile, 5, MapObjectType.Actor);
                    var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
                    for (int i = 0; i < temp_map_objects.Count; i++)
                     {
                       Actor actor = (Actor)temp_map_objects[i];
                       BaseSimObject pTarget = actor;
                       var isEnemy = (bool)actor.kingdom.CallMethod("isEnemy", unit.kingdom);
                        if(isEnemy && actor.kingdom != unit.kingdom && Toolbox.randomChance(0.008f)){
                          BaseStats baseStats = Reflection.GetField(pTarget.GetType(), pTarget, "curStats") as BaseStats;
                          Vector2Int pos = pTile.pos;
                          float pDist = Vector2.Distance(pTarget.currentPosition, pos);
                     		  Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
                     		  Vector3 newPoint2 = Toolbox.getNewPoint(actor.currentPosition.x, actor.currentPosition.y, (float)pos.x, (float)pos.y, baseStats.size, true);
                     		  newPoint2.y += 0.5f;
                          MapBox.instance.stackEffects.CallMethod("startProjectile", newPoint, newPoint2, "AcidBall", 0f);
                        }
                      }
                      unit.addTrait("poison_immune");
                      unit.addTrait("regeneration");
                  }
                  if(pSlot.data.id == "Laws"){
                    var citizens = MapBox.instance.units.getSimpleList();
                    foreach(var citizen in citizens)
                    {
                      if(citizen.kingdom == unit.kingdom){
                          citizen.addTrait("blessed");
                      }
                    }
                  }
                  if(pSlot.data.id == "Tech"){
                    if(Toolbox.randomChance(0.003f)){
                      WorldTile pTile = unit.currentTile;
                      var act = MapBox.instance.createNewUnit("assimilator", pTile, null, 0f, null);
                      act.kingdom = unit.kingdom;
                      }
                  }
                  if(pSlot.data.id == "Horn"){
                      ActorStatus Data = Reflection.GetField(unit.GetType(), unit, "data") as ActorStatus;
                      if(Data.health <= 60){
                      WorldTile pTile = unit.currentTile;
                      var act = MapBox.instance.createNewUnit(unit.stats.id, pTile, null, 0f, null);
                      act.kingdom = unit.kingdom;
                      unit.restoreHealth(5);
                      MapBox.instance.fxDivineLight.playOn(unit.currentTile);
                      }
                  }
                }
              }
            }
        }
    }
}
