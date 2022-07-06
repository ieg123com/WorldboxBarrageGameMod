using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace WarBox
{
    class Trait
    {
        public static void init()
        {
          //Don't blame me for the my bad coding skill
          var boat_trading = AssetManager.unitStats.get("boat_trading");
          boat_trading.traits.Add("Cannon");

          ActorTrait CannonBoat = new ActorTrait();
          CannonBoat.id = "Cannon";
          CannonBoat.inherit = 0f;
          CannonBoat.birth = 0f;
          CannonBoat.path_icon = "ui/Icons/iconCannon";
          PlayerConfig.unlockTrait("Cannon");
          CannonBoat.action_special_effect = (WorldAction)Delegate.Combine(CannonBoat.action_special_effect, new WorldAction(CannonBoatEffect));
          CannonBoat.action_death = (WorldAction)Delegate.Combine(CannonBoat.action_death, new WorldAction(Crew));
          AssetManager.traits.add(CannonBoat);
          addTraitToLocalizedLibrary(CannonBoat.id, "#Shoot shoot");

          ActorTrait PirateBoat = new ActorTrait();
          PirateBoat.id = "Pirate Boat";
          PirateBoat.inherit = 0f;
          PirateBoat.birth = 0f;
          PirateBoat.path_icon = "ui/Icons/iconPirate";
          PlayerConfig.unlockTrait("Pirate Boat");
          PirateBoat.action_special_effect = (WorldAction)Delegate.Combine(PirateBoat.action_special_effect, new WorldAction(PirateBoatEffect));
          PirateBoat.action_death = (WorldAction)Delegate.Combine(PirateBoat.action_death, new WorldAction(PirateCrew));
          AssetManager.traits.add(PirateBoat);
          addTraitToLocalizedLibrary(PirateBoat.id, "#PirateBoat");

          ProjectileAsset CannonBall = new ProjectileAsset();
          CannonBall.id = "CannonBall";
    			CannonBall.speed = 20f;
    			CannonBall.texture = "firebomb";
    			CannonBall.parabolic = false;
    			CannonBall.texture_shadow = "shadow_ball";
    			CannonBall.terraformOption = "demon_fireball";
    			CannonBall.endEffect = "fireSmoke";
    			CannonBall.hitShake = true;
    			CannonBall.startScale = 0.05f;
    			CannonBall.targetScale = 0.09f;
    			CannonBall.playImpactSound = true;
    			CannonBall.impactSoundID = "explosion medium";
    			CannonBall.terraformRange = 0;
          CannonBall.world_actions = (WorldAction)Delegate.Combine(CannonBall.world_actions, new WorldAction(CannonBallEffect));
          AssetManager.projectiles.add(CannonBall);
        }
        public static void addTraitToLocalizedLibrary(string id, string description)
    		{
    			Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
    				localizedText.Add("trait_" + id, id);
    				localizedText.Add("trait_" + id + "_info", description);
        }
        public static bool CannonBoatEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          BaseSimObject targetObj = a.findEnemyObjectTarget();
          if(targetObj != null && pTile != null){
            Actor actor = targetObj.a;
            if(/*Toolbox.randomChance(0.3f) && */actor != null)
            {
              if (actor.stats.isBoat && a.ai.task != a.ai.task_library.get("boat_fight"))
              {
                a.stopMovement();
                a.ai.setTask("boat_fight", true, true);
                a.attackTarget = targetObj;
                if (actor.stats.id == "boat_trading")
                {
                  actor.stopMovement();
                  actor.ai.setTask("boat_fight", true, true);
                  actor.attackTarget = pTarget;
                }
              }
              BaseStats baseStats = Reflection.GetField(pTarget.GetType(), pTarget, "curStats") as BaseStats;
              Vector2Int pos = pTile.pos;
              float pDist = Vector2.Distance(pTarget.currentPosition, pos);
              Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
              Vector3 newPoint2 = Toolbox.getNewPoint(actor.currentPosition.x, actor.currentPosition.y, (float)pos.x, (float)pos.y, baseStats.size, true);
              newPoint2.y += 0.5f;
              MapBox.instance.stackEffects.CallMethod("startProjectile", newPoint, newPoint2, "CannonBall", 0f);
            }
          }
          // var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
          // for (int i = 0; i < temp_map_objects.Count; i++)
          // {
          //   if(actor.haveTrait("Pirate Boat") && Toolbox.randomChance(0.3f)){
          //     BaseStats baseStats = Reflection.GetField(pTarget.GetType(), pTarget, "curStats") as BaseStats;
          //     Vector2Int pos = pTile.pos;
          //     float pDist = Vector2.Distance(pTarget.currentPosition, pos);
          //   	Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
          //   	Vector3 newPoint2 = Toolbox.getNewPoint(actor.currentPosition.x, actor.currentPosition.y, (float)pos.x, (float)pos.y, baseStats.size, true);
          //   	newPoint2.y += 0.5f;
          //     MapBox.instance.stackEffects.CallMethod("startProjectile", newPoint, newPoint2, "CannonBall", 0f);
          //   }
          // }
           return true;
        }
        public static bool Crew(BaseSimObject pTarget, WorldTile pTile = null)
        {
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          for (int i = 0; i < Toolbox.randomInt(3, 8); i++)
           {
             string Unit = a.kingdom.raceID;
             Actor crew = MapBox.instance.createNewUnit("unit_" + Unit, pTile, "", 0f, null);
             crew.kingdom = a.kingdom;
           }
           return true;
        }
        public static bool PirateBoatEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
          Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
          AiSystemActor ai = Reflection.GetField(a.GetType(), a, "ai") as AiSystemActor;
          ai.setTask("Pirate_fight", true, false);
          MapBox.instance.CallMethod("getObjectsInChunks", pTile, 10, MapObjectType.Actor);
          var temp_map_objects = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "temp_map_objects") as List<BaseSimObject>;
          for (int i = 0; i < temp_map_objects.Count; i++)
           {
              Actor actor = (Actor)temp_map_objects[i];
               if(!actor.haveTrait("Pirate Boat") && actor.stats.id != "bandit" && Toolbox.randomChance(0.3f)){
                 BaseStats baseStats = Reflection.GetField(pTarget.GetType(), pTarget, "curStats") as BaseStats;
                 Vector2Int pos = pTile.pos;
                 float pDist = Vector2.Distance(pTarget.currentPosition, pos);
            		 Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
            		 Vector3 newPoint2 = Toolbox.getNewPoint(actor.currentPosition.x, actor.currentPosition.y, (float)pos.x, (float)pos.y, baseStats.size, true);
            		 newPoint2.y += 0.5f;
                 MapBox.instance.stackEffects.CallMethod("startProjectile", newPoint, newPoint2, "CannonBall", 0f);
               }
           }
           return true;
        }
        public static bool PirateCrew(BaseSimObject pTarget, WorldTile pTile = null)
        {
          for (int i = 0; i < Toolbox.randomInt(3, 8); i++)
           {
             MapBox.instance.createNewUnit("bandit", pTile, "", 0f, null);
           }
           return true;
        }
        public static bool CannonBallEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
          MapBox.instance.applyForce(pTile, 2, 0.1f, true, false, 250, null, null, null);
          return true;
        }
    }
}
