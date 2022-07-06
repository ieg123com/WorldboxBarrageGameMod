using ReflectionUtility;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using NCMS;
using NCMS.Utils;
using Newtonsoft.Json;
using ai;
using ai.behaviours;
using HarmonyLib;

namespace WarBox
{
    
    class BehBoatFight : BehBoat
    {
        public static void init()
        {
            BehaviourTaskActor boatTaskBeh = new BehaviourTaskActor();
			boatTaskBeh.id = "boat_fight";
            // boatTaskBeh.addBeh(new BehBoatFindWaterTile());
            boatTaskBeh.addBeh(new BehBoatFight());
            boatTaskBeh.addBeh(new BehGoToTileTarget());
            boatTaskBeh.addBeh(new BehRandomWait(0f, 1f));
            // boatTaskBeh.addBeh();
            boatTaskBeh.addBeh(new BehRestartTask());
            AssetManager.tasks_actor.add(boatTaskBeh);
        }

        public override BehResult execute(Actor pActor)
		{
            if (pActor.attackTarget != null)
            {
                WorldTile randomTileForBoat = getRandomTileAroundTarget(pActor.attackTarget.a);
			    pActor.beh_tile_target = randomTileForBoat;
			    return BehResult.Continue;
            }
            pActor.ai.setTask("boat_leader_attack", true, false);
            return BehResult.Stop;
        }

        public static WorldTile getRandomTileAroundTarget(Actor pTarget)
		{
			MapRegion mapRegion = pTarget.currentTile.region;
			if (mapRegion.neighbours.Count > 0 && Toolbox.randomBool())
			{
				mapRegion = mapRegion.neighbours.GetRandom<MapRegion>();
			}
			if (mapRegion.tiles.Count > 0)
			{
				return mapRegion.tiles.GetRandom<WorldTile>();
			}
			return null;
		}
    }
}