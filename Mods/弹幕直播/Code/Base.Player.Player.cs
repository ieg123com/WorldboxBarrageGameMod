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
    // 玩家存档
    public class PlayerDataInfo
    {
        [JsonProperty("uid")]
        public long uid = 0;
        [JsonProperty("kingdomDataInfo")]
        public KingdomDataInfo kingdomDataInfo = new KingdomDataInfo();
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

        public UIKingdom uIKingdom = null;

        // 玩家存档信息
        public PlayerDataInfo playerDataInfo = new PlayerDataInfo();

        public bool dataChanged = false;

        public void Start()
        {
            StartCoroutine(DownloadHeadImage());
        }

        public void ReflectionUIKingdom()
        {
            if(uIKingdom == null)
            {
                return;
            }
            uIKingdom.image.sprite = headSprite;
            uIKingdom.name.text = $"[{kingdomCivId.Substring(2)}]{name}";
            uIKingdom.fraction.text = $"{playerDataInfo.kingdomDataInfo.killNum}/{playerDataInfo.kingdomDataInfo.deathNum}/{playerDataInfo.kingdomDataInfo.winNum}";
            if(playerDataInfo.kingdomDataInfo.deathNum <= 0)
            {
                uIKingdom.kDA.text = String.Format("{0:F}",playerDataInfo.kingdomDataInfo.killNum);
            }else
            {
                uIKingdom.kDA.text = String.Format("{0:F}",(float)playerDataInfo.kingdomDataInfo.killNum / (float)playerDataInfo.kingdomDataInfo.deathNum);
            }
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
            }
            if(unitId != null)
            {
                var unit = UnitManager.instance.GetByKey(unitId);
                unit.head = headSprite;
                unit.Apply();
            }
            if(uIKingdom != null)
            {
                uIKingdom.image.sprite = headSprite;
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