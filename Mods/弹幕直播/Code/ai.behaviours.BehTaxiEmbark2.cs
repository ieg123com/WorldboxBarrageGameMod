using System;
using life.taxi;
using UnityEngine;
using ReflectionUtility;

namespace ai.behaviours
{
	public class BehTaxiEmbark2 : BehCity
	{
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		public override BehResult execute(Actor pActor)
		{
			TaxiRequest requestForActor = TaxiManager.getRequestForActor(pActor);
			if (requestForActor == null || requestForActor.taxi == null || requestForActor.state != TaxiRequestState.Loading)
			{
				return BehResult.Stop;
			}
			Boat component = requestForActor.taxi.actor.GetComponent<Boat>();
			bool flag = component.isNearDock();
			if (Toolbox.DistTile(component.actor.currentTile, pActor.currentTile) < 5f || flag)
			{
				pActor.beh_tile_target = null;
                pActor.CallMethod("embarkInto",requestForActor.taxi);
				return BehResult.Continue;
			}
			return BehResult.Continue;
		}
	}
}
