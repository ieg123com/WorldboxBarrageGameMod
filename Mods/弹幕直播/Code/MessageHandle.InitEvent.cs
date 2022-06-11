using System;
using UnityEngine;



namespace BarrageGame
{
    public class MessageHandle
    {
        static public void InitEvent()
        {
            MessageDistribute.instance.OnNormalMsg = new Action<Player, MessageDistribute.NormalMsg>(KingdomMsg.MsgAll);
            MessageDistribute.instance.BindNormalMsgEvent("加入",KingdomMsg.MsgJoin);
            MessageDistribute.instance.BindNormalMsgEvent("宣战",KingdomMsg.MsgSplite);
            MessageDistribute.instance.BindNormalMsgEvent("和平",KingdomMsg.MsgPeace);
        }
    }
}