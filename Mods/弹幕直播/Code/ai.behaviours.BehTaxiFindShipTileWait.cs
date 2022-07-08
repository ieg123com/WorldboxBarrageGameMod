using System;
using UnityEngine;
using life.taxi;
using ReflectionUtility;


namespace ai.behaviours
{
    public class BehTaxiFindShipTileWait : BehCity
    {
        public BehTaxiFindShipTileWait()
        {

        }

        public override BehResult execute(Actor pActor)
        {
			TaxiRequest requestForActor = TaxiManager.getRequestForActor(pActor);
			if (requestForActor == null)
			{
				return BehResult.Stop;
			}
            if(requestForActor.taxi == null || requestForActor.state != TaxiRequestState.Loading)
            {
                pActor.timer_action = Toolbox.randomFloat(3f, 5f);
                return BehResult.RepeatStep;
            }
			Boat component = requestForActor.taxi.actor.GetComponent<Boat>();
			WorldTile worldTile = null;
			if (component.pickup_near_dock)
			{
				Building homeBuilding = Reflection.GetField(component.actor.GetType(),component.actor,"homeBuilding") as Building;
				if (homeBuilding != null)
				{
					WorldTile constructionTile = homeBuilding.getConstructionTile();
					if (constructionTile != null)
					{
						worldTile = constructionTile.region.tiles.GetRandom<WorldTile>();
					}
				}
			}
			if (worldTile == null)
			{
				worldTile = PathfinderTools.raycastTileForUnitToEmbark(pActor.currentTile, requestForActor.taxi.actor.currentTile);
			}
			if (worldTile == null)
			{
                pActor.timer_action = Toolbox.randomFloat(3f, 5f);
				return BehResult.RepeatStep;
			}
            pActor.beh_tile_target = worldTile;
			return BehResult.Continue;

        }

    }


}