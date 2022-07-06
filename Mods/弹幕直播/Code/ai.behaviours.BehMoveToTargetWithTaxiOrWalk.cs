using System;
using UnityEngine;
using life.taxi;
using ReflectionUtility;


namespace ai.behaviours
{
    public class BehMoveToTargetWithTaxiOrWalk : BehaviourActionActor
    {
        public BehMoveToTargetWithTaxiOrWalk()
        {

        }

        public override BehResult execute(Actor pActor)
        {
            if(pActor.commandTargetTile == null)
            {
                return BehResult.Stop;
            }
            if(pActor.commandTargetTile.isSameIsland(pActor.currentTile))
            {
                // 无需乘船
                // TODO 直接移动过去
                pActor.beh_tile_target = pActor.commandTargetTile;
                pActor.goTo(pActor.commandTargetTile, false, false);
                pActor.timer_action = Toolbox.randomFloat(1f, 4f);
                return BehResult.Stop;
            }else{
                // 需要乘船
                // TODO 乘船前往
                TaxiManager.newRequest(pActor, pActor.commandTargetTile);
				return BehResult.Continue;
            }

        }

    }


}