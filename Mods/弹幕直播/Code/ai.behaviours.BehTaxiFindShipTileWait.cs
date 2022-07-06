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
			Actor taxiActor = Reflection.GetField(requestForActor.taxi.GetType(),requestForActor.taxi,"actor") as Actor;
			Boat component = taxiActor.GetComponent<Boat>();
			WorldTile worldTile = null;
			if (component.pickup_near_dock)
			{
				Actor componentActor = Reflection.GetField(component.GetType(),component,"actor") as Actor;
				Building homeBuilding = Reflection.GetField(componentActor.GetType(),componentActor,"homeBuilding") as Building;
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
				worldTile = PathfinderTools.raycastTileForUnitToEmbark(pActor.currentTile, taxiActor.currentTile);
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