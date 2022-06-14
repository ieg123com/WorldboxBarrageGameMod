using System;
using System.Threading;


using Newtonsoft.Json;
using UnityEngine;

namespace BarrageGame
{
    public class WebSocketToCCWSS
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        enum MsgType
        {
            Barrage = 1,    // 弹幕
            Gift = 2,   // 礼物
            FocusOn = 3,    // 关注
            Enter = 4,  // 进入直播间
        }

        public class MsgBase
        {
            [JsonProperty("uid")]
            public long uid = 0;
            [JsonProperty("name")]
            public string name = "";
            [JsonProperty("type")]
            public int type = 0;
            [JsonProperty("head")]
            public string head = "";
        }

        public class BarrageMsg
        {
            [JsonProperty("data")]
            public string data = "";
        }

        public class GiftMsg
        {
            [JsonProperty("gif_id")]
            public int gif_id = 0;
            [JsonProperty("gif_name")]
            public string gif_name = "";
            [JsonProperty("gif_num")]
            public int gif_num = 0;
            [JsonProperty("gif_price")]
            public int gif_price = 0;
        }


        private WSocketClientHelp client;
        public WebSocketToCCWSS()
        {
        }

        ~WebSocketToCCWSS()
        {
        }

        public void Update()
        {
            client.Update();
        }

        public void Connect(string url)
        {

            client = new WSocketClientHelp(url);
            client.OnMessage += DistributeMessage;
            client.Open();
        }

        public void DistributeMessage(object sender, string data)
        {
            Debug.Log(data);
            var msgBase = JsonConvert.DeserializeObject(data, typeof(MsgBase)) as MsgBase;
            PlayerInfo info = new PlayerInfo();
            info.uid = msgBase.uid;
            info.name = msgBase.name;
            info.urlHead = msgBase.head;

            switch ((MsgType)msgBase.type)
            {
                case MsgType.Barrage:
                    {
                        var barrageMsg = JsonConvert.DeserializeObject(data, typeof(BarrageMsg)) as BarrageMsg;
                        Log($"[{msgBase.uid}:{msgBase.name}]{barrageMsg.data}");
                        MessageDistribute.NormalMsg msg = new MessageDistribute.NormalMsg();
                        msg.msg = barrageMsg.data;
                        if(MessageDistribute.instance == null)
                        {
                            Debug.Log("MessageDistribute.instance == null");
                        }
                        MessageDistribute.instance.DistributeNormalMsg(info,msg);
                    }
                    break;
                case MsgType.Gift:
                    {
                        var giftMsg = JsonConvert.DeserializeObject(data, typeof(GiftMsg)) as GiftMsg;
                        MessageDistribute.GiftMsg msg = new MessageDistribute.GiftMsg();
                        msg.giftId = giftMsg.gif_id;
                        msg.giftName = giftMsg.gif_name;
                        msg.giftNum = giftMsg.gif_num;
                        msg.giftPrice = giftMsg.gif_price;
                        MessageDistribute.instance.DistributeGiftMsg(info,msg);
                    }
                    break;
            }

        }

        public void Log(string msg)
        {
            Debug.Log(msg);
        }

        


    }
}
