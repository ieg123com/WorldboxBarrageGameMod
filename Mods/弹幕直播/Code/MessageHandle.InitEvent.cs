using System;
using UnityEngine;



namespace BarrageGame
{
    public class MessageHandle
    {
        static public void InitEvent()
        {
            MessageDistribute.instance.OnNormalMsg = new Action<Player, MessageDistribute.NormalMsg>(KingdomMsg.MsgAll);
            MessageDistribute.instance.BindNormalMsgFrameEvent("加入",KingdomMsg.MsgJoin);
            MessageDistribute.instance.BindNormalMsgEvent("宣战",KingdomMsg.MsgSplite);
            MessageDistribute.instance.BindNormalMsgEvent("和平",KingdomMsg.MsgPeace);
            MessageDistribute.instance.BindNormalMsgEvent("协助",KingdomMsg.MsgAssist);
            MessageDistribute.instance.BindNormalMsgEvent("进攻",KingdomMsg.MsgToAttack);
            MessageDistribute.instance.BindNormalMsgEvent("前往",KingdomMsg.MsgMoveToKingdom);
            MessageDistribute.instance.BindNormalMsgEvent("回防",KingdomMsg.MsgToBackArmy);
            MessageDistribute.instance.BindNormalMsgEvent("投降",KingdomMsg.MsgSurrender);
            MessageDistribute.instance.BindNormalMsgEvent("查看外交",KingdomMsg.MsgToShowDiplomacy);
            MessageDistribute.instance.BindNormalMsgEvent("革职",KingdomMsg.MsgFired);
            MessageDistribute.instance.BindNormalMsgEvent("封将",KingdomMsg.MsgPromotedToGeneral);
            MessageDistribute.instance.BindNormalMsgEvent("封侯",KingdomMsg.MsgPromotedToLeader);
            MessageDistribute.instance.BindNormalMsgEvent("投靠",KingdomMsg.MsgBetray);
        }
    }
}