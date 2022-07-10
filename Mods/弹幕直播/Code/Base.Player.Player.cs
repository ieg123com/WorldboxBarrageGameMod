using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace BarrageGame
{
    // 玩家信息
    public class PlayerInfo
    {
        public long uid;
        public string name;
        public string urlHead;
        // 粉丝牌
        public string medal_name = "";
        public string medal_room_id = "";
        public int medal_level = 0;

    }

    // 选择国家游玩信息
    public class KingdomDataInfo
    {
        [JsonProperty("killNum")]
        public long killNum = 0;
        [JsonProperty("deathNum")]
        public long deathNum = 0;
        [JsonProperty("winNum")]
        public long winNum = 0;
    }
    // 单位游玩信息
    public class UnitDataInfo
    {
        [JsonProperty("killBabyNum")]
        public long killBabyNum = 0;
        [JsonProperty("killUnitNum")]
        public long killUnitNum = 0;
        // 击杀民众数
        [JsonProperty("killWarriorNum")]
        public long killWarriorNum = 0;
        // 击杀领袖数
        [JsonProperty("killLeaderNum")]
        public long killLeaderNum = 0;
        // 击杀国王数
        [JsonProperty("killKingNum")]
        public long killKingNum = 0;
        // 自己死亡数
        [JsonProperty("deathNum")]
        public long deathNum = 0;
    }
    // 基础配置信息存档
    public class ConfigInfo
    {
        // 隐藏战绩
        [JsonProperty("ShowWarRecord")]
        public bool hideWarRecord = false;
    }
    // 玩家存档
    public class PlayerDataInfo
    {
        [JsonProperty("uid")]
        public long uid = 0;
        [JsonProperty("kingdomDataInfo")]
        public KingdomDataInfo kingdomDataInfo = new KingdomDataInfo();
        [JsonProperty("unitDataInfo")]
        public UnitDataInfo unitDataInfo = new UnitDataInfo();
        [JsonProperty("configInfo")]
        public ConfigInfo configInfo = new ConfigInfo();
    }


    public class Player : MonoBehaviour
    {
        public long uid;
        public string name;
        public string urlHead;
        public Sprite headSprite;
        // 所属国家id
        public string kingdomCivId;
        // 是国王玩家，掌权者
        public bool isKingPlayer = false;
        // 控制的单位uid
        public string unitId = null;

        public long lastSpeechTime = 0;
        // 粉丝牌
        public string medal_name = "";
        public string medal_room_id = "";
        public int medal_level = 0;


        // 玩家存档信息
        public PlayerDataInfo playerDataInfo = new PlayerDataInfo();
        public UnitDataInfo currentUnitDataInfo = new UnitDataInfo();

        public bool dataChanged = false;

        public void Start()
        {
            StartCoroutine(DownloadHeadImage());
        }

        // 获取自己粉丝的粉丝牌等级
        public int GetCurrentMedalLevel()
        {
            if(medal_room_id == Main.conf.medalRoomId ||
            medal_room_id == "25191145")    
            /* 恳求保留，让我房间的粉丝牌玩家，去哪个房间都可享有人口加成
             * 没有这些粉丝在支持，就不会有当前公开的ai开盘mod。
             */
            {
                return medal_level;
            }
            return 0;
        }


        
        IEnumerator DownloadHeadImage()
        {
            using (UnityWebRequest request = new UnityWebRequest())
            {
                request.url = urlHead;
                DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
                request.downloadHandler = texDl;
                yield return request.SendWebRequest();
                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    headSprite = Sprite.Create(texDl.texture, new Rect(0, 0, texDl.texture.width, texDl.texture.height), Vector2.one * 0.5f);
                }
            }

            if(isKingPlayer == true)
            {
                var mKingdom = MKingdomManager.instance.GetByKey(kingdomCivId);
                mKingdom.SetHeadSprite(headSprite);
                mKingdom.uIKingdom.image.sprite = headSprite;
            }
            if(unitId != null)
            {
                var unit = UnitManager.instance.GetByKey(unitId);
                unit.head = headSprite;
                unit.Apply();
                unit.uIUnit.image.sprite = headSprite;
            }

        }

        

        void OnDisable()
        {
            if(dataChanged == false)
            {
                return;
            }
            File.WriteAllText(
                $"{SaveManager.generateMainPath("PlayerData")}/{uid}.txt",
                JsonConvert.SerializeObject(playerDataInfo)
            );
        }

    }
}