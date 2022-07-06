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

    class BehBoatLeaderCheck : BehBoat
    {
        public static void init()
        {
            BehaviourTaskActor boatTaskBeh = new BehaviourTaskActor();
			boatTaskBeh.id = "boat_leader_attack";
            boatTaskBeh.addBeh(new BehBoatCheckHomeDocks());
			boatTaskBeh.addBeh(new BehBoatLeaderCheck());
			boatTaskBeh.addBeh(new BehBoatFindTileInDock());
			boatTaskBeh.addBeh(new BehGoToTileTarget());
			boatTaskBeh.addBeh(new BehRandomWait(5f, 10f));
			boatTaskBeh.addBeh(new BehSetNextTask("boat_return_to_dock", true));
			boatTaskBeh.addBeh(new BehRestartTask());
            AssetManager.tasks_actor.add(boatTaskBeh);

            ActorJob boatLeaderJob = new ActorJob();
            boatLeaderJob.id = "boat_leader";
            boatLeaderJob.addTask("boat_leader_attack");
			AssetManager.job_actor.add(boatLeaderJob);
        }

        public override BehResult execute(Actor pActor)
		{
            Docks boatTarget = getDockTarget(pActor);
            if (boatTarget != null)
			{
				pActor.beh_building_target = boatTarget.building;
				return BehResult.Continue;
			}
			return BehResult.Stop;
        }

        public static Docks getDockTarget(Actor pActor)
		{
			List<Docks> docks = pActor.currentTile.region.island.docks;
			if (docks.Count == 0)
			{
				return null;
			}
			docks.Shuffle<Docks>();
			for (int i = 0; i < docks.Count; i++)
			{
				Docks docks2 = docks[i];
                if (docks2 == null)
                {
                    return null;
                }
                Building dockBuilding = (Building)Reflection.GetField(typeof(Docks), docks2, "building");

				if (!(dockBuilding == null) && !(pActor.homeBuilding == dockBuilding) && (docks2.isDockGood() || docks2.checkOceanTiles()) && !dockBuilding.isNonUsable() && dockBuilding.city.kingdom.isEnemy(pActor.kingdom))
				{
					return docks2;
				}
			}
			return null;
		}
    }
}
