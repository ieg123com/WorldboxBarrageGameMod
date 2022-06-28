using System;
using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;



namespace BarrageGame
{
    static public class MKingdomHelper
    {
        //static public Color colorHit = new Color(0.99f,0.31f,0.29f,1f);
        //static public Color colorAttack = new Color(0.93f,0.77f,0.51f,1f);
        static public Color colorAttack = new Color(0.99f,0.31f,0.29f,1f);

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
            ModEvent.capitalChange = new Action<Kingdom,City,City>(CapitalChange);
            // Actor 国家变动
            ModEvent.actorKingdomChanged = new Action<Actor,Kingdom,Kingdom>(ActorKingdomChanged);
            // ActorBase 属性更新
            ModEvent.actorBaseUpdataStats = new Action<ActorBase>(ActorBaseUpdataStats);
            ModEvent.actorGetHit = new Action<Actor, float, bool, AttackType, BaseSimObject, bool>(Event_ActorGetHit);
            ModEvent.kingdomStartPeace = new Action<Kingdom, Kingdom>(Event_KingdomStartPeace);
            ModEvent.kingdomStartWar = new Action<Kingdom, Kingdom>(Event_KingdomStartWar);
        }


        static public void DestroyKingdom(Kingdom kingdom)
        {
            var mKingdom = MKingdomManager.instance.GetByKey(kingdom.id);
            if(mKingdom == null)
            {
                return;
            }
            Debug.Log($"灭亡一个国家 [{kingdom.id}]{kingdom.name}");
            MKingdom kingdomKiller = null;
            if(mKingdom.capital != null)
            {
                var last_kingdom = Reflection.GetField(mKingdom.capital.GetType(),mKingdom.capital,"kingdom") as Kingdom;
                if(last_kingdom != null)
                {
                    kingdomKiller = MKingdomManager.instance.GetByKey(last_kingdom.id);
                }
            }else{
                Debug.Log($"国家首都没有 [{kingdom.id}]{kingdom.name}");
            }
            

            
            {
                // TODO 计分
                if(kingdomKiller != null)
                {
                    Debug.Log($"死于 [{kingdomKiller.kingdom.id}]{kingdomKiller.kingdom.name}");
                    var playerKiller = PlayerManager.instance.GetByKey(kingdomKiller.kingPlayerUid); // 凶手
                    if(playerKiller != null)
                    {
                        playerKiller.playerDataInfo.kingdomDataInfo.killNum += 1;
                        playerKiller.dataChanged = true;
                    }
                    kingdomKiller.ReflectionUIKingdom();
                }
            }

            
            var player = PlayerManager.instance.GetByKey(mKingdom.kingPlayerUid);
            if(player != null)
            {
                {
                    // TODO 计分
                    player.playerDataInfo.kingdomDataInfo.deathNum += 1;
                    player.dataChanged = true;
                    mKingdom.ReflectionUIKingdom();
                }

                player.kingdomCivId = "";
                player.isKingPlayer = false;
            }

            mKingdom.alive = false;
            mKingdom.SetLastUIKingdom();
            mKingdom.Clear();
            UnityEngine.Object.Destroy(mKingdom.gameObject);
            MKingdomManager.instance.Remove(mKingdom.id);

        }

        static public void MakeNewCivKingdom(Kingdom kingdom,City city,Race race,string id)
        {
            Debug.Log($"建立新国家 [{id}]{kingdom.name}");
            var mkingbom = MKingdomFactory.Create(kingdom);
            MKingdomManager.instance.Add(mkingbom);
        }
        // 国家首都变动
        static public void CapitalChange(Kingdom kingdom,City newCity,City oldCity)
        {
            var mKingdom = MKingdomManager.instance.GetByKey(kingdom.id);
            if(mKingdom == null)
            {
                return;
            }
            Debug.Log($"国家首都变动 {kingdom.id}");
            mKingdom.capital = newCity;
        }

        // Actor 所属的国家变动
        static public void ActorKingdomChanged(Actor actor,Kingdom newKingdom,Kingdom oldKingdom)
        {
            if(newKingdom == null || oldKingdom == null)
            {
                return;
            }

            if(actor.isPlayerControl == false)return;// 不是玩家控制的
            var unit = UnitManager.instance.GetByKey(actor.GetID());
            if(unit == null)
            {
                return;
            }


            // 更新 unit 所属的国家
            {
                // 先清除ui
                var oldMKingdom = MKingdomManager.instance.GetByKey(oldKingdom.id);
                if(oldMKingdom != null && oldMKingdom.uIKingdom != null && unit.uIUnit != null)
                {
                    oldMKingdom.uIKingdom.Remove(unit.uIUnit);
                    unit.uIUnit = null;

                }
                // 将ui移到其他国家下
                var newMKingdom = MKingdomManager.instance.GetByKey(newKingdom.id);
                if(newMKingdom != null)
                {
                    if(newMKingdom.uIKingdom == null)
                    {
                        // 这是电脑，还没创建UI
                        newMKingdom.uIKingdom = UIKingdomList.instance.GetUIKingdom();
                        newMKingdom.ReflectionUIKingdom();
                    }
                    unit.uIUnit = newMKingdom.uIKingdom.GetUIUnit();
                    unit.ReflectionUIUnit();
                }
            }
            
            // TODO 将 unit 转移到其他MKingdom下
            {
                var oldMKingdom = MKingdomManager.instance.GetByKey(oldKingdom.id);
                if(oldMKingdom != null)
                {
                    oldMKingdom.RemoveUnit(unit.Id);
                }
                var newMKingdom = MKingdomManager.instance.GetByKey(newKingdom.id);
                if(newMKingdom != null)
                {
                    newMKingdom.AddUnit(unit);
                    var player = PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                    if(player != null)
                    {
                        player.kingdomCivId = newMKingdom.id;
                    }
                }

            }


            // 国家亡，永不背叛
            if(unit.changeKingdom == false){
                // TODO 检查原来的国家是否灭亡
                if(oldKingdom.cities.Count > 0)
                {
                    // 还有城市
                    var targetCity = GameHelper.CityThings.GetShotestDistance(oldKingdom.cities,unit.actor.currentTile);
                    try{
                        unit.changeKingdom = true;
                        unit.actor.CallMethod("becomeCitizen",targetCity);
                    }finally{
                        unit.changeKingdom = false;
                    }
                }

            }
        }

        static public void ActorBaseUpdataStats(ActorBase pActorBase)
        {
            if(pActorBase.isPlayerControl == false)return;// 不是玩家控制的
            var unit = UnitManager.instance.GetByKey(pActorBase.GetID());
            if(unit == null)
            {
                return;
            }
            // 是玩家控制的单位
            var curStats = Reflection.GetField(pActorBase.GetType(),pActorBase,"curStats") as BaseStats;
            curStats.health += 4000;
            curStats.damage += 10;
        }

        static public void RemoveCitizen(Actor pActor, bool pKilled, AttackType pAttackType)
        {
            
        }

        static public void KillHimself(Actor pActor,bool pDestroy, AttackType pType, bool pCountDeath, bool pLaunchCallbacks)
        {
            // Actor死亡
            var attackActor = pActor.GetAttackActor();
            if(attackActor != null)
            {
                // 可能是某个玩家击杀的
                var attackUnit = UnitManager.instance.GetByKey(attackActor.GetID());
                if(attackUnit != null)
                {
                    // 是玩家击杀的
                    var attackPlayer = PlayerManager.instance.GetByKey(attackUnit.ownerPlayerUid);
                    if(attackPlayer != null)
                    {
                        // TODO 判断击杀的单位是？
                        ActorStatus actorStatus = Reflection.GetField(pActor.GetType(),pActor,"data") as ActorStatus;
                        Debug.Log($"杀死的单位是 [{pActor.GetID()}]{actorStatus.profession.ToString()}");
                        switch(actorStatus.profession)
                        {
                            case UnitProfession.King:
                            attackPlayer.playerDataInfo.unitDataInfo.killKingNum +=1;
                            attackPlayer.currentUnitDataInfo.killKingNum +=1;
                            break;
                            case UnitProfession.Leader:
                            attackPlayer.playerDataInfo.unitDataInfo.killLeaderNum +=1;
                            attackPlayer.currentUnitDataInfo.killLeaderNum +=1;
                            break;
                            case UnitProfession.Warrior:
                            attackPlayer.playerDataInfo.unitDataInfo.killWarriorNum +=1;
                            attackPlayer.currentUnitDataInfo.killWarriorNum +=1;
                            break;
                            case UnitProfession.Unit:
                            attackPlayer.playerDataInfo.unitDataInfo.killUnitNum +=1;
                            attackPlayer.currentUnitDataInfo.killUnitNum +=1;
                            break;
                            case UnitProfession.Baby:
                            attackPlayer.playerDataInfo.unitDataInfo.killBabyNum +=1;
                            attackPlayer.currentUnitDataInfo.killBabyNum +=1;
                            break;
                            default:
                            break;
                        }
                        attackPlayer.dataChanged = true;
                        attackUnit.ReflectionUIUnit();
                    }
                }
                
            }

            if(pActor.isPlayerControl == false)return;// 不是玩家控制的
            var unit = UnitManager.instance.GetByKey(pActor.GetID());
            if(unit == null)
            {
                return;
            }
            // 死亡的是自己
            var player = PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
            if(player != null)
            {
                player.playerDataInfo.unitDataInfo.deathNum += 1;
                player.currentUnitDataInfo.deathNum +=1;
                player.dataChanged = true;
                unit.ReflectionUIUnit();
            }


            DestroyActor(pActor);
        }

        static public void DestroyActor(Actor pActor)
        {
            if(pActor.isPlayerControl == false)return;// 不是玩家控制的
            var unit = UnitManager.instance.GetByKey(pActor.GetID());
            if(unit == null)
            {
                return;
            }
            Debug.Log($"KillHimself id = {pActor.GetID()}");
            // 先清除ui
            {
                var mKingdom = MKingdomManager.instance.GetByKey(pActor.kingdom.id);
                if(mKingdom != null && mKingdom.uIKingdom != null && unit.uIUnit != null)
                {
                    mKingdom.uIKingdom.Remove(unit.uIUnit);
                    unit.uIUnit = null;
                }
                if(unit.uIBloodBar != null)
                {
                    unit.uIBloodBar.Clear();
                    unit.uIBloodBar = null;
                }
            }


            if(unit.ownerPlayerUid != 0)
            {
                var player= PlayerManager.instance.GetByKey(unit.ownerPlayerUid);
                if(player != null)
                {
                    player.kingdomCivId = "";
                    player.unitId = null;
                    var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
                    if(mKingdom != null)
                    {
                        mKingdom.RemoveUnit(unit.Id);
                    }
                }
                unit.ownerPlayerUid = 0;
 
            }
            UnitManager.instance.Remove(unit.Id);
        }

        static public void Event_ActorGetHit(Actor self,float pDamage, bool pFlash = true, AttackType pType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true)
        {
            if(pAttacker != null && pAttacker.isPlayerControl == true)
            {
                // 玩家攻击的
                Vector2 pos = GameHelper.MapText.TransformPosition(self.currentTile.posV3);
                UIDamageManager.instance.Show(((int)pDamage).ToString(),colorAttack,pos);
            }
        }

        static public void Event_KingdomStartWar(Kingdom pKingdom,Kingdom pTarget)
        {
            if(pKingdom == pTarget)
            {
                return;
            }

            var mKingdom = MKingdomManager.instance.GetByKey(pKingdom.id);
            var targetMKingdom = MKingdomManager.instance.GetByKey(pTarget.id);
            if(mKingdom == null || targetMKingdom == null || mKingdom.IsEnemy(targetMKingdom))
            {
                return;
            }
            if(!mKingdom.AllKingdomAtWar.ContainsKey(targetMKingdom.id))
            {
                mKingdom.AllKingdomAtWar.Add(targetMKingdom.id,targetMKingdom);
            }
        }

        static public void Event_KingdomStartPeace(Kingdom pKingdom,Kingdom pTarget)
        {
            if(pKingdom == pTarget)
            {
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey(pKingdom.id);
            var targetMKingdom = MKingdomManager.instance.GetByKey(pTarget.id);
            if(mKingdom == null || targetMKingdom == null || !mKingdom.IsEnemy(targetMKingdom))
            {
                return;
            }
            if(mKingdom.AllKingdomAtWar.ContainsKey(targetMKingdom.id))
            {
                mKingdom.AllKingdomAtWar.Remove(targetMKingdom.id);
            }
            if(targetMKingdom.AllKingdomAtWar.ContainsKey(mKingdom.id))
            {
                targetMKingdom.AllKingdomAtWar.Remove(mKingdom.id);
            }

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
                    return false;
                }else{
                    return kingdom.id == self.id;
                }
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
                if(GameHelper.CityThings.IsPlayerControl(city))continue;    // 玩家控制的城市，国王无法命令
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
                if(GameHelper.CityThings.IsPlayerControl(city))continue;    // 玩家控制的城市，国王无法命令

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
                if(GameHelper.CityThings.IsPlayerControl(city))continue;    // 玩家控制的城市，国王无法命令
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

        // 向pkingdom投降
        public static void ToSurrender(this MKingdom self,MKingdom pKingdom)
        {
            List<City> list = new List<City>();
            foreach (City item in self.kingdom.cities)
            {
                list.Add(item);
            }
            foreach (City city in list)
            {
                city.joinAnotherKingdom(pKingdom.kingdom);
            }
            list.Clear();
        }

        // 是电脑
        public static bool IsComputer(this MKingdom self)
        {
            return (self.kingPlayerUid == 0);
        }
    }
}