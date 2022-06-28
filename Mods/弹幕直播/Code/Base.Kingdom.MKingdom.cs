using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using NCMS;
using NCMS.Utils;

namespace BarrageGame
{
    public class MKingdom : MonoBehaviour
    {
        public string id;

        public bool alive;
        // 国王玩家id
        public long kingPlayerUid = 0;

        public Kingdom kingdom = null;
        // 首都
        public City capital = null;

        public HashSet<string> peaceList = new HashSet<string>();

        // 国家中，拥有的全部玩家 unit
        public Dictionary<string,Unit> allUnit = new Dictionary<string,Unit>();

        // 主动发起开战请求的国家
        public Dictionary<string,MKingdom> AllKingdomAtWar = new Dictionary<string,MKingdom>();

        public float ShowDiplomacyTime = 0f;

        public UIKingdom uIKingdom = null;

        public void ReflectionUIKingdom()
        {
            if(uIKingdom == null)
            {
                return;
            }
            if(this.IsComputer())
            {
                // TODO 电脑控制
                uIKingdom.image.sprite = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/Computer.png");
                uIKingdom.name.text = $"[{id.Substring(2)}]{kingdom.name}";
                uIKingdom.score.text = $"-/-/-";
                uIKingdom.kDA.text = "-.--";
            }else{
                // TODO 玩家控制
                var player = PlayerManager.instance.GetByKey(kingPlayerUid);
                if(player == null)
                {
                    return;
                }
                uIKingdom.image.sprite = player.headSprite;
                uIKingdom.name.text = $"[{id.Substring(2)}]{player.name}";
                if(player.playerDataInfo.configInfo.hideWarRecord == true)
                {
                    // 隐藏战绩显示
                    uIKingdom.score.text = $"-/-/-";
                    uIKingdom.kDA.text = "-.--";
                    return;
                }
                uIKingdom.score.text = $"{player.playerDataInfo.kingdomDataInfo.killNum}/{player.playerDataInfo.kingdomDataInfo.deathNum}/{player.playerDataInfo.kingdomDataInfo.winNum}";
                if(player.playerDataInfo.kingdomDataInfo.deathNum <= 0)
                {
                    uIKingdom.kDA.text = String.Format("{0:F}",player.playerDataInfo.kingdomDataInfo.killNum);
                }else
                {
                    uIKingdom.kDA.text = String.Format("{0:F}",(float)player.playerDataInfo.kingdomDataInfo.killNum / (float)player.playerDataInfo.kingdomDataInfo.deathNum);
                }
            }
            int playerControlCount = 0;
            foreach(var city in kingdom.cities)
            {
                playerControlCount += (GameHelper.CityThings.IsPlayerControl(city) ? 1 : 0);
            }
            uIKingdom.cityNumber.text = $"{playerControlCount}/{kingdom.cities.Count}";
        }

        public void SetHeadSprite(Sprite head)
        {
            kingdom.headSprite = head;
        }
        public void SetName(string name)
        {
            kingdom.name = name;
        }

        public void AddUnit(Unit unit)
        {
            if(allUnit.ContainsKey(unit.Id))
            {
                RemoveUnit(unit.Id);
                allUnit[unit.Id] = unit;
            }else{
                allUnit.Add(unit.Id,unit);
            }
        }

        public void RemoveUnit(string id)
        {
            if(allUnit.ContainsKey(id))
            {
                allUnit.Remove(id);
            }
        }


        public void SecondsUpdate()
        {
            ReflectionUIKingdom();
        }

        public void Clear()
        {
            if(kingdom != null)
            {
                // 清除其他MKingdom中的开战状态
                foreach(var kv in kingdom.getEnemies())
                {
                    if(kv.Value == true)
                    {
                        // 正在开战中
                        var target = MKingdomManager.instance.GetByKey(kv.Key.id);
                        if(target != null)
                        {
                            if(target.AllKingdomAtWar.ContainsKey(id))
                            {
                                target.AllKingdomAtWar.Remove(id);
                            }
                        }
                    }
                }
            }
            AllKingdomAtWar.Clear();

            kingdom = null;
            capital = null;
            uIKingdom = null;
        }

        public void RemoveKingdomUI()
        {
            if(uIKingdom != null)
            {
                UIKingdomList.instance.RemoveUIKingdom(uIKingdom);
                uIKingdom = null;
            }
        }

        public void SetLastUIKingdom()
        {
            if(uIKingdom != null)
            {
                UIKingdomList.instance.SetLastUIKingdom(uIKingdom);
                uIKingdom = null;
            }
        }

        public void Update()
        {
            if(ShowDiplomacyTime > 0f)
            {
                ShowDiplomacyTime -= Time.deltaTime;
            }
        }

    }


}