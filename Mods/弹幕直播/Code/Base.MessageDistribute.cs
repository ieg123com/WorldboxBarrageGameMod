using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public Dictionary<string,Action<Player,NormalMsg,List<string>>> AllNormalMsg = new Dictionary<string,Action<Player,NormalMsg,List<string>>>();
        // 帧消息，每帧只执行一次
        public Dictionary<string,Action<Player,NormalMsg,List<string>>> AllNormalFrameMsg = new Dictionary<string,Action<Player,NormalMsg,List<string>>>();
        public Action<Player,NormalMsg> OnNormalMsg;
        public Action<Player,GiftMsg> OnGiftMsg;

        private Regex reg;


        public MessageDistribute()
        {
            MessageDistribute.instance = this;
            reg = new Regex("^[a-zA-Z\u4e00-\u9fa5]+|[0-9]+");
        }

        public void DistributeNormalMsg(PlayerInfo info,NormalMsg msg)
        {
            var player = PlayerFactory.GetOrCreate(info);
            player.lastSpeechTime = TimeHelper.ClientNow();
            if(OnNormalMsg != null)
            {
                OnNormalMsg(player,msg);
            }

            List<string> list = new List<string>();
            var match = reg.Matches(msg.msg);
            for (int i = 0; i < match.Count; ++i)
            {
                list.Add(match[i].Value);
            }
            if(list.Count > 0)
            {
                Action<Player,NormalMsg,List<string>> action;
                if(AllNormalMsg.TryGetValue(list[0],out action))
                {
                    action(player,msg,list);
                }
                if(AllNormalFrameMsg.TryGetValue(list[0],out action))
                {
                    Main.frameActions.Enqueue(()=>{
                        action(player,msg,list);
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

        public void BindNormalMsgEvent(string comm,Action<Player,NormalMsg,List<string>> action)
        {
            if(AllNormalMsg.ContainsKey(comm))
            {
                AllNormalMsg[comm] = action;
            }else{
                AllNormalMsg.Add(comm,action);
            }
        }

        public void BindNormalMsgFrameEvent(string comm,Action<Player,NormalMsg,List<string>> action)
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