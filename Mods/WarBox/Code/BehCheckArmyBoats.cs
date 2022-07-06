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

    class BehCheckArmyBoats : BehaviourActionCity
    {
        public static Dictionary<City, UnitGroup> existingGroups = new Dictionary<City, UnitGroup>();
        public static void init()
        {
            BehaviourTaskCity newBehTaskCity = new BehaviourTaskCity();
			      newBehTaskCity.id = "check_army_boats";
            newBehTaskCity.addBeh(new BehCheckArmyBoats());
            AssetManager.tasks_city.add(newBehTaskCity);
            var city_jobs = AssetManager.job_city.get("city");
            city_jobs.addTask("check_army_boats");
        }

        public override BehResult execute(City pCity)
		{
            if (!(bool)pCity.CallMethod("haveBuildingType", "docks", true))
			{
				return BehResult.Continue;
			}
			Building buildingType = (Building)pCity.CallMethod("getBuildingType", "docks", true, true);
			if (buildingType == null)
			{
				return BehResult.Continue;
			}
            Docks dockComp = (Docks)Reflection.GetField(typeof(Building), buildingType, "component_docks");
            List<Actor> armyBoatList = (List<Actor>)Reflection.GetField(typeof(Docks), dockComp, "boats_trading");
            if (armyBoatList.Count <= 0)
            {
                return BehResult.Continue;
            }
            UnitGroup armyGroup = null;
            if (!existingGroups.ContainsKey(pCity))
            {
                armyGroup = MapBox.instance.unitGroupManager.createNewGroup(pCity);
                existingGroups.Add(pCity, armyGroup);
            }else
            {
                armyGroup = existingGroups[pCity];
            }
            foreach(Actor actor in armyBoatList)
            {
                var actorGroup = (UnitGroup)Reflection.GetField(typeof(Actor), actor, "unitGroup");
                if (actorGroup == armyGroup)
                {
                    if (actor.ai.job != actor.ai.jobs_library.get("boat_leader") || actor.ai.task != actor.ai.task_library.get("boat_fight"))
                    {
                        actor.ai.setJob("boat_leader");
                    }
                    continue;
                }
                armyGroup.addUnit(actor);
                actor.ai.nextJobDelegate = new GetNextJobID(getNextBoatJob);
            }
            return BehResult.Continue;
        }

        public static string getNextBoatJob()
      		{
              return "boat_leader";
      		}
    }
}
