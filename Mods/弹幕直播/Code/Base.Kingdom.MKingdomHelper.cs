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
            var unit = UnitManager.instance.GetByKey(pActor.GetID());
            if(unit == null)
            {
                return;
            }
            Debug.Log($"KillHimself id = {pActor.GetID()}, pAttackType = {pType.ToString()}");
            if(unit.ownerPlayerUid != 0)
            {
                var player= PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                if(player != null)
                {
                    player.kingdomCivId = "";
                    player.unitId = null;
                }
                unit.ownerPlayerUid = 0;
            }
            UnitManager.instance.Remove(unit.Id);
        }

        static public void DestroyActor(Actor pActor)
        {
            var unit = UnitManager.instance.GetByKey(pActor.GetID());
            if(unit == null)
            {
                return;
            }
            Debug.Log($"DestroyActor id = {pActor.GetID()}");
            if(unit.ownerPlayerUid != 0)
            {
                var player= PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                if(player != null)
                {
                    player.kingdomCivId = "";
                    player.unitId = null;
                }
                unit.ownerPlayerUid = 0;
            }
            UnitManager.instance.Remove(unit.Id);
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


        // 让所有士兵进攻目标国家最近的城市
        public static void ToAttackKingdom(this MKingdom self,MKingdom targetKingdom)
        {
            if(self == targetKingdom)
            {
                // 不能自己向自己宣战
                return;
            }
            if(!self.IsEnemy(targetKingdom))
            {
                // 不是敌人 开始宣战
                self.StartWar(targetKingdom);
            }
            foreach(var city in self.kingdom.cities)
            {
                var _cityTile = Reflection.GetField(city.GetType(),city,"_cityTile") as WorldTile;
                if (city.army != null && _cityTile != null)
                {
                    WorldTile worldTile;
                    if (city.army.groupLeader != null)
                    {
                        worldTile = city.army.groupLeader.currentTile;
                    }
                    else
                    {
                        worldTile = _cityTile;
                    }
                    WorldTile cityTile =  Reflection.GetField(targetKingdom.kingdom.capital.GetType(),targetKingdom.kingdom.capital,"_cityTile") as WorldTile;
                   // WorldTile cityTile =  targetKingdom.kingdom.capital._cityTile;
                    foreach (City city2 in  targetKingdom.kingdom.cities)
                    {
                        var _city2Tile = Reflection.GetField(city2.GetType(),city2,"_cityTile") as WorldTile;
                        if (_city2Tile == null || Toolbox.DistVec2(worldTile.pos, _city2Tile.pos) < Toolbox.DistVec2(worldTile.pos, cityTile.pos))
                        {
                            cityTile = _city2Tile;
                        }
                    }
                    if (cityTile != null)
                    {
                        city.army.moveTo(cityTile);
                    }
                }
            }
            WorldLog.logNewMessage(self.kingdom, "对", targetKingdom.kingdom, "发起了进攻");
        }
        
        public static void MoveToKingdom(this MKingdom self,MKingdom targetKingdom)
        {
            foreach(var city in self.kingdom.cities)
            {
                WorldTile cityTile =  Reflection.GetField(targetKingdom.kingdom.capital.GetType(),targetKingdom.kingdom.capital,"_cityTile") as WorldTile;
                city.army.moveTo(cityTile);
            }
        }


        // 回防，召回军队
        public static bool ToBackArmy(this MKingdom self)
        {
            Kingdom kingdom = self.kingdom;
            WorldLog.logNewMessage(kingdom, "召回了它的军队");
            foreach (City city in kingdom.cities)
            {
                city.army.moveTo(Reflection.GetField(city.GetType(),city,"_cityTile") as WorldTile);
            }
            return true;
        }

        // 显示外交状态
        public static bool ToShowDiplomacy(this MKingdom self)
        {
            Kingdom kingdom = self.kingdom;
            string text = string.Concat(new string[]
            {
                " <color=",
                Toolbox.colorToHex(GameHelper.KingdomThings.GetKingdomColor(kingdom), true),
                ">",
                kingdom.name,
                "</color> "
            });
            if (kingdom.civs_enemies.Count == 0)
            {
                text += "处于和平状态";
            }
            else
            {
                text += "正在跟";
                Kingdom[] array = new Kingdom[kingdom.civs_enemies.Count];
                kingdom.civs_enemies.Keys.CopyTo(array, 0);
                for (int i = 0; i < array.Length; i++)
                {
                    string str = string.Concat(new string[]
                    {
                        " <color=",
                        Toolbox.colorToHex(GameHelper.KingdomThings.GetKingdomColor(array[i]), true),
                        ">",
                        array[i].name,
                        "</color> "
                    });
                    text += str;
                }
                text += "交战。";
            }
            MapBox.instance.addNewText(text, Toolbox.color_log_good, null);
            return true;
        }
    }
}