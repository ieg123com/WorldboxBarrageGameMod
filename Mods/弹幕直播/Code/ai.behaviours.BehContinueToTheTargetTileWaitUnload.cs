using System;
using UnityEngine;
using life.taxi;
using ReflectionUtility;


namespace ai.behaviours
{
	// 下船后，继续前往目标
    public class BehContinueToTheTargetTileWaitUnload : BehaviourActionActor
    {
        public BehContinueToTheTargetTileWaitUnload()
        {

        }

        public override BehResult execute(Actor pActor)
        {
			Debug.Log("In BehContinueToTheTargetTileWaitUnload");
			Boat boat = pActor.GetComponent<Boat>();

			if(boat.taxiRequest != null)
			{
				Debug.Log("等待 boat.taxiRequest != null");
				pActor.timer_action = Toolbox.randomFloat(3f, 5f);
				return BehResult.RepeatStep;
			}
			if(!pActor.commandTargetTile.isSameIsland(pActor.currentTile))
			{
				// TODO 还没到达目标岛屿
				Debug.Log("还没到达目标岛屿");
				return BehResult.Stop;
			}
            // TODO 到达目标岛屿了
			pActor.beh_tile_target = pActor.commandTargetTile;
			pActor.goTo(pActor.commandTargetTile);
			Debug.Log("到达目标岛屿了");
			return BehResult.Continue;
        }

    }


}