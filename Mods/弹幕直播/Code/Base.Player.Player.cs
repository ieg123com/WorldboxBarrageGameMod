using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace BarrageGame
{
    // 玩家信息
    public class PlayerInfo
    {
        public long uid;
        public string name;
        public string urlHead;
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

        public void Start()
        {
            StartCoroutine(DownloadHeadImage());
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
        }
        

    }
}