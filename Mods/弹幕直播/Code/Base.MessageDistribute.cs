using System;
using System.Collections.Generic;
using UnityEngine;



namespace BarrageGame
{
    public class MessageDistribute
    {
        public class NormalMsg
        {
            public string msg;
        }

        public class GiftMsg
        {
            public int giftId = 0;
            public string giftName = "";
            public int giftNum = 0;
            public int giftPrice = 0;
        }


        static public MessageDistribute instance;

        public Dictionary<string,Action<Player,NormalMsg>> AllNormalMsg = new Dictionary<string,Action<Player,NormalMsg>>();
        // 帧消息，每帧只执行一次
        public Dictionary<string,Action<Player,NormalMsg>> AllNormalFrameMsg = new Dictionary<string,Action<Player,NormalMsg>>();
        public Action<Player,NormalMsg> OnNormalMsg;
        public Action<Player,GiftMsg> OnGiftMsg;


        public MessageDistribute()
        {
            MessageDistribute.instance = this;
        }

        public void DistributeNormalMsg(PlayerInfo info,NormalMsg msg)
        {
            var player = PlayerFactory.GetOrCreate(info);
            player.lastSpeechTime = TimeHelper.ClientNow();
            if(OnNormalMsg != null)
            {
                OnNormalMsg(player,msg);
            }
            var list = msg.msg.Split(' ');
            if(list.Length > 0)
            {
                Action<Player,NormalMsg> action;
                if(AllNormalMsg.TryGetValue(list[0],out action))
                {
                    action(player,msg);
                }
                if(AllNormalFrameMsg.TryGetValue(list[0],out action))
                {
                    Main.frameActions.Enqueue(()=>{
                        action(player,msg);
                    });
                }
            }
        }

        public void DistributeGiftMsg(PlayerInfo info,GiftMsg msg)
        {
            var player = PlayerFactory.GetOrCreate(info);
            player.lastSpeechTime = TimeHelper.ClientNow();
            if(OnGiftMsg != null)
            {
                OnGiftMsg(player,msg);
            }
        }

        public void BindNormalMsgEvent(string comm,Action<Player,NormalMsg> action)
        {
            if(AllNormalMsg.ContainsKey(comm))
            {
                AllNormalMsg[comm] = action;
            }else{
                AllNormalMsg.Add(comm,action);
            }
        }

        public void BindNormalMsgFrameEvent(string comm,Action<Player,NormalMsg> action)
        {
            if(AllNormalFrameMsg.ContainsKey(comm))
            {
                AllNormalFrameMsg[comm] = action;
            }else{
                AllNormalFrameMsg.Add(comm,action);
            }
        }

    }
}