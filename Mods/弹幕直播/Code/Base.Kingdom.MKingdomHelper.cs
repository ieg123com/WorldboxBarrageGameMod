using System;
using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;



namespace BarrageGame
{
    static public class MKingdomHelper
    {
        static public void Init()
        {
            // 绑定所有城市
            foreach(var v in MapBox.instance.kingdoms.list_civs)
            {
                var mkingbom = MKingdomFactory.Create(v);
                MKingdomManager.instance.Add(mkingbom);
            }
        }

        static public void InitEvent()
        {
            ModEvent.destroyKingdom = new Action<Kingdom>(DestroyKingdom);
            //ModEvent.destroyKingdom += DestroyKingdom;

            ModEvent.makeNewCivKingdom = new Action<Kingdom, City, Race, string>(MakeNewCivKingdom);
            //ModEvent.makeNewCivKingdom = MakeNewCivKingdom;


        }


        static public void DestroyKingdom(Kingdom kingdom)
        {
            var nKingdom = MKingdomManager.instance.GetByKey(kingdom.id);
            if(nKingdom == null)
            {
                return;
            }
            Debug.Log($"灭亡一个国家 [{kingdom.id}]{kingdom.name}");
            nKingdom.alive = false;
            UnityEngine.Object.Destroy(nKingdom.gameObject);
            MKingdomManager.instance.Remove(kingdom.id);
        }

        static public void MakeNewCivKingdom(Kingdom kingdom,City city,Race race,string id)
        {
            Debug.Log($"建立新国家 [{id}]{kingdom.name}");
            var mkingbom = MKingdomFactory.Create(kingdom);
            MKingdomManager.instance.Add(mkingbom);
        }

        static public void CheckNewKingdom()
        {
            if(Main.startGame == false)
            {
                return;
            }

            // 检测有没有新国家,并添加进去
            if(MapBox.instance.kingdoms.list_civs.Count > MKingdomManager.instance.allKingdoms.Count)
            {
                foreach(var v in MapBox.instance.kingdoms.list_civs)
                {
                    if(!MKingdomManager.instance.allKingdoms.ContainsKey(v.id))
                    {
                        var mkingbom = MKingdomFactory.Create(v);
                        MKingdomManager.instance.Add(mkingbom);
                    }
                }
            }
        }
        // 和平
        static public void StartPeace(this MKingdom self,MKingdom mKingdom)
        {
            MapBox.instance.kingdoms.diplomacyManager.CallMethod("startPeace", self.kingdom, mKingdom.kingdom, false);
        }
        // 宣战
        static public void StartWar(this MKingdom self,MKingdom mKingdom)
        {
            MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", self.kingdom, mKingdom.kingdom,false);

        }

        // 是敌对的
        static public bool IsEnemy(this MKingdom self,MKingdom mKingdom)
        {
            bool ret = false;
            self.kingdom.getEnemies().TryGetValue(mKingdom.kingdom,out ret);
            return ret;
        }
        // 有敌人
        static public bool HasEnemies(this MKingdom self)
        {
            return self.kingdom.hasEnemies();
        }
        // 武力比较 true.比目标厉害 false.没他厉害
        static public bool ForceComparison(this MKingdom self,MKingdom targetMKingdom)
        {
            if(self.kingdom.getPopulationTotal() > targetMKingdom.kingdom.getPopulationTotal() &&
            self.kingdom.getArmy() > targetMKingdom.kingdom.getArmy())
            {
                return true;
            }
            return false;
        }
    }
}