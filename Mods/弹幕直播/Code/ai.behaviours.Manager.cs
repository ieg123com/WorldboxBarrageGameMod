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
			actorTaskBeh.addBeh(new BehTaxiEmbark2());
            actorTaskBeh.addBeh(new BehGoToTileTarget());
            actorTaskBeh.addBeh(new BehRandomWait(1f, 2f));
			actorTaskBeh.addBeh(new BehTaxiEmbark2());
            actorTaskBeh.addBeh(new BehGoToTileTarget());
            actorTaskBeh.addBeh(new BehRandomWait(1f, 2f));
			actorTaskBeh.addBeh(new BehTaxiEmbark2());
            AssetManager.tasks_actor.add(actorTaskBeh);
        }
    }

}