using System;
using UnityEngine;


namespace ai.behaviours
{
    public class Manager
    {
        public static void Init()
        {
            BehaviourTaskActor actorTaskBeh = new BehaviourTaskActor();
			actorTaskBeh.id = "actor_move_to_target";
            // boatTaskBeh.addBeh(new BehBoatFindWaterTile());
            actorTaskBeh.addBeh(new BehMoveToTargetWithTaxiOrWalk());
            actorTaskBeh.addBeh(new BehTaxiFindShipTileWait());
            actorTaskBeh.addBeh(new BehGoToTileTarget());
			actorTaskBeh.addBeh(new BehTaxiEmbark());
			actorTaskBeh.addBeh(new BehContinueToTheTargetTileWaitUnload());
            actorTaskBeh.addBeh(new BehRandomWait(1f, 4f));
            AssetManager.tasks_actor.add(actorTaskBeh);
        }
    }

}