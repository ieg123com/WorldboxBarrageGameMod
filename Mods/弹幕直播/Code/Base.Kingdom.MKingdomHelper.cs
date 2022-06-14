using System;
using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;



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
            ModEvent.destroyActor = new Action<Actor>(DestroyActor);
            ModEvent.killHimself = new Action<Actor, bool, AttackType, bool, bool>(KillHimself);
        }


        static public void DestroyKingdom(Kingdom kingdom)
        {
            var mKingdom = MKingdomManager.instance.GetByKey(kingdom.id);
            if(mKingdom == null)
            {
                return;
            }
            Debug.Log($"灭亡一个国家 [{kingdom.id}]{kingdom.name}");
            mKingdom.alive = false;
            UnityEngine.Object.Destroy(mKingdom.gameObject);
            MKingdomManager.instance.Remove(kingdom.id);
            // 复活玩家
            var player = PlayerManager.instance.GetByKey(mKingdom.kingPlayerUid);
            if(player == null)
            {
                return;
            }
            player.kingdomCivId = "";
            player.isKingPlayer = false;

            
        }

        static public void MakeNewCivKingdom(Kingdom kingdom,City city,Race race,string id)
        {
            Debug.Log($"建立新国家 [{id}]{kingdom.name}");
            var mkingbom = MKingdomFactory.Create(kingdom);
            MKingdomManager.instance.Add(mkingbom);
        }

        static public void RemoveCitizen(Actor pActor, bool pKilled, AttackType pAttackType)
        {
            
        }

        static public void KillHimself(Actor pActor,bool pDestroy, AttackType pType, bool pCountDeath, bool pLaunchCallbacks)
        {
            var unit = UnitManager.instance.GetByKey(pActor.GetInstanceID());
            if(unit == null)
            {
                return;
            }
            Debug.Log($"KillHimself id = {pActor.GetInstanceID().ToString()}, pAttackType = {pType.ToString()}");
            if(unit.ownerPlayerUid != 0)
            {
                var player= PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                if(player != null)
                {
                    player.kingdomCivId = "";
                    player.unitInstanceId = 0;
                }
                unit.ownerPlayerUid = 0;
            }
            UnitManager.instance.Remove(unit.instanceId);
        }

        static public void DestroyActor(Actor pActor)
        {
            var unit = UnitManager.instance.GetByKey(pActor.GetInstanceID());
            if(unit == null)
            {
                return;
            }
            Debug.Log($"DestroyActor id = {pActor.GetInstanceID().ToString()}");
            if(unit.ownerPlayerUid != 0)
            {
                var player= PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                if(player != null)
                {
                    player.kingdomCivId = "";
                    player.unitInstanceId = 0;
                }
                unit.ownerPlayerUid = 0;
            }
            UnitManager.instance.Remove(unit.instanceId);
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

        static public MapText GetMapText(this MKingdom self)
        {
            var list = Reflection.GetField(MapNamesManager.instance.GetType(),MapNamesManager.instance,"list") as List<MapText>;
            if(list == null)
                {
                    // 未知原因
                    Debug.Log($"[error] list == null in GetMapText");
                }
            return list.Find(delegate(MapText mapText){
                var kingdom = Reflection.GetField(mapText.GetType(),mapText,"Kingdom") as Kingdom;
                if(kingdom == null)
                {
                    // 未知原因
                    Debug.Log($"[error] list.Count = {list.Count} in GetMapText");
                }

                return kingdom.id == self.id;
            });
        }
    }
}