using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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



    }


}