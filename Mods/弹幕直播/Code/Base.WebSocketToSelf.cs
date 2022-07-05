using System;
using System.Threading;


using Newtonsoft.Json;
using UnityEngine;

namespace BarrageGame
{
    public class WebSocketToSelf
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
            [JsonProperty("uname")]
            public string uname = "";
            [JsonProperty("type")]
            public int type = 0;
            [JsonProperty("headurl")]
            public string headurl = "";
            [JsonProperty("timestamp")]
            public long timestamp = 0;
            // 粉丝牌
            [JsonProperty("medal_name")]
            public string medal_name = "";
            [JsonProperty("medal_room_id")]
            public string medal_room_id = "";
            [JsonProperty("medal_level")]
            public string medal_level = "";

        }

        public class BarrageMsg
        {
            [JsonProperty("msg")]
            public string msg = "";
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
        public WebSocketToSelf()
        {
        }

        ~WebSocketToSelf()
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
            info.name = msgBase.uname;
            info.urlHead = msgBase.headurl;
            info.medal_name = msgBase.medal_name;
            info.medal_room_id = msgBase.medal_room_id;
            int.TryParse(msgBase.medal_level,out info.medal_level);


            switch ((MsgType)msgBase.type)
            {
                case MsgType.Barrage:
                    {
                        var barrageMsg = JsonConvert.DeserializeObject(data, typeof(BarrageMsg)) as BarrageMsg;
                        Log($"[{msgBase.uid}:{msgBase.uname}]{barrageMsg.msg}");
                        MessageDistribute.NormalMsg msg = new MessageDistribute.NormalMsg();
                        msg.msg = barrageMsg.msg;
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
