using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMS;
using NCMS.Utils;
using ReflectionUtility;

namespace BarrageGame
{
    // 命令：
    // 加入 [国家id] ： 获得国家的控制权
    // 宣战 [国家id] ： 向一个国家进行宣战
    // 和平 [国家id] ： 需要双方都发送才能停止战斗
    // 
    public class KingdomMsg
    {
        static public void MsgAll(Player player,MessageDistribute.NormalMsg msg)
        {
            Debug.Log($"msg {msg.msg}");
            if(player.isKingPlayer == true)
            {
                // 国家掌权者
                var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
                if(mKingdom != null)
                {
                    var mapText = mKingdom.GetMapText();
                    if(mapText == null)
                    {
                        return;
                    }
                    var go = new GameObject("ChatBubble");
                    go.transform.SetParent(MapNamesManager.instance.transform);
                    var uiBubble = go.AddComponent<UIBubble>();
                    uiBubble.SetBottomSprite(Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/bottom.png"));
                    var rect = uiBubble.GetComponent<RectTransform>();
                    rect.anchoredPosition = mapText.GetComponent<RectTransform>().anchoredPosition + new Vector2(0,10);
                    Debug.Log($"rect.anchoredPosition = {rect.anchoredPosition}");
                    uiBubble.SetMessage(msg.msg);

                    
                }
            }
            if(player.unitId != null)
            {
                // 国家协助者
                Debug.Log($"player.unitId = {player.unitId}");
                var unit = UnitManager.instance.GetByKey(player.unitId);
                if(unit == null)
                {
                    return;
                }
                var go = new GameObject("ChatBubble");
                go.transform.SetParent(MapNamesManager.instance.transform);
                var uiBubble = go.AddComponent<UIBubble>();
                uiBubble.SetBottomSprite(Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/bottom.png"));
                var rect = uiBubble.GetComponent<RectTransform>();
                rect.anchoredPosition = GameHelper.MapText.TransformPosition(unit.actor.currentTile.posV3)+ new Vector2(0,10);
                Debug.Log($"rect.anchoredPosition = {rect.anchoredPosition}");
                uiBubble.SetMessage(msg.msg);
            }
        }

        static public void MsgJoin(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            Debug.Log($"MsgJoin {msg.msg}");
            if(player.kingdomCivId != null && player.kingdomCivId != "")
            {
                Debug.Log($"玩家 {player.name} 已经加入国家 {player.kingdomCivId}");
                return;
            }
            MKingdom mKingdom = null;
            if(comm.Count == 2)
            {
                // 命令错误
                mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            }else{
                int allPopulationTotal = 0;
                int avPopulationTotal = 0;
                {
                    // TODO 获取平均人口
                    if(MKingdomManager.instance.allKingdoms.Count > 0)
                    {
                        foreach(var v in MKingdomManager.instance.allKingdoms.Values)
                        {
                            allPopulationTotal += v.kingdom.getPopulationTotal();
                        }
                        avPopulationTotal = allPopulationTotal / MKingdomManager.instance.allKingdoms.Count;
                    }
                }
                // TODO 创建国家，并追平其他国家人口
                var kingdom = GameHelper.KingdomThings.RandomCreate(avPopulationTotal);
                if(kingdom == null)
                {
                    MapBox.instance.addNewText("国家已满，无法加入!", Toolbox.color_log_warning, null);
                    return;
                }
                mKingdom = MKingdomManager.instance.GetByKey(kingdom.id);
                
            }
            
            
            if(mKingdom == null)
            {
                // 国家不存在
                return;
            }
            if(mKingdom.kingPlayerUid != 0)
            {
                // 国家被其他玩家选了
                return;
            }
            // TODO 开始加入国家
            Debug.Log($"玩家 {player.name} 加入国家 {player.kingdomCivId}");
            player.kingdomCivId = mKingdom.id;
            player.isKingPlayer = true;
            mKingdom.kingPlayerUid = player.uid;
            mKingdom.SetName(player.name);
            mKingdom.SetHeadSprite(player.headSprite);
            if(mKingdom.uIKingdom == null)
            {
                mKingdom.uIKingdom = UIKingdomList.instance.GetUIKingdom();
            }
            mKingdom.ReflectionUIKingdom();

        }

        static public void MsgSplite(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            if(player.isKingPlayer == false)
            {
                Debug.Log($"玩家 {player.name} 没有控制权 {player.kingdomCivId}");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var WarInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom == null || WarInitiator == null)
            {
                return;
            }
            if(Main.startWar == false)
            {
                // 还在保护时间
                MapBox.instance.addNewText("保护时间，无法宣战！", Toolbox.color_log_warning, null);
                return;
            }

            Debug.Log($"玩家 {player.name} {WarInitiator.id} 宣战 {mKingdom.id}");
            WarInitiator.StartWar(mKingdom);
            //MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", WarInitiator.kingdom, mKingdom.kingdom,false);

            mKingdom.peaceList.Remove(WarInitiator.id);
            WarInitiator.peaceList.Remove(mKingdom.id);
        }

        static public void MsgPeace(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            if(player.isKingPlayer == false)
            {
                Debug.Log($"玩家 {player.name} 没有控制权 {player.kingdomCivId}");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var PeaceInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom == null || PeaceInitiator == null)
            {
                return;
            }

            if(mKingdom.peaceList.Contains(PeaceInitiator.id))
            {
                Debug.Log($"玩家 {player.name} {PeaceInitiator.id} 和平 {mKingdom.id}");
                mKingdom.StartPeace(PeaceInitiator);
                //MapBox.instance.kingdoms.diplomacyManager.CallMethod("startPeace", mKingdom.kingdom, PeaceInitiator.kingdom, false);
                mKingdom.peaceList.Remove(PeaceInitiator.id);
            }else
            {
                PeaceInitiator.peaceList.Add(mKingdom.id);
            }
        }


        // 协助
        static public void MsgAssist(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.isKingPlayer == true)
            {
                Debug.Log($"玩家 {player.name} 已经加入国家 {player.kingdomCivId}");
                return;
            }
            if(player.unitId != null)
            {
                Debug.Log($"玩家 {player.name} 已经协助国家 {player.kingdomCivId}");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            if(mKingdom == null)
            {
                return;
            }
            City targetCity = mKingdom.kingdom.cities[UnityEngine.Random.Range(0,mKingdom.kingdom.cities.Count)];
            // TODO 协助流程
            var unit = UnitFactory.Create(targetCity.getTile(),"humans");
            player.kingdomCivId = mKingdom.id;
            player.unitId = unit.Id;
            unit.ownerPlayerUid = player.uid;
            unit.actor.CallMethod("becomeCitizen",targetCity);
            {
                // 添加物品，让他变成战斗单位
                ItemAsset pItemAsset = AssetManager.items.get("sword");
                ItemData item = ItemGenerator.generateItem(pItemAsset, "wood", 0, null, null, 1, unit.actor);
                ActorEquipmentSlot slot = unit.actor.equipment.getSlot(EquipmentType.Weapon);
                slot.CallMethod("setItem",item);
            }
            unit.actor.setStatsDirty();
            if(player.headSprite != null)
            {
                unit.head = player.headSprite;
                unit.Apply();
            }
            mKingdom.AddUnit(unit);

            // TODO 创建UI
            if(mKingdom.uIKingdom == null)
            {
                // 不是电脑
                // 创建ui
                unit.uIUnit = mKingdom.uIKingdom.GetUIUnit();
                unit.ReflectionUIUnit();
            }
        }

        static public void MsgToAttack(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var PeaceInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(Main.startWar == false)
            {
                // 还在保护时间
                MapBox.instance.addNewText("保护时间，无法进攻！", Toolbox.color_log_warning, null);
                return;
            }
            if(mKingdom == null || PeaceInitiator == null)
            {
                return;
            }

            if(player.isKingPlayer == true)
            {
                // 拥有国家控制权
                PeaceInitiator.ToAttackKingdom(mKingdom);
            }else if(player.unitId != null){
                // 协助这个国家的玩家
                var unit = UnitManager.instance.GetByKey(player.unitId);
                if(unit != null)
                {
                    unit.ToAttackKingdom(mKingdom);
                }
            }
        }

        static public void MsgMoveToKingdom(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var PeaceInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom == null || PeaceInitiator == null)
            {
                return;
            }

            if(player.isKingPlayer == true)
            {
                // 拥有国家控制权
                PeaceInitiator.MoveToKingdom(mKingdom);
            }else if(player.unitId != null){
                // 协助这个国家的玩家
                var unit = UnitManager.instance.GetByKey(player.unitId);
                if(unit != null)
                {
                    unit.MoveToKingdom(mKingdom);
                }
            }
        }

        static public void MsgToBackArmy(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            var PeaceInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(PeaceInitiator == null)
            {
                return;
            }


            if(player.isKingPlayer == true)
            {
                // 可以控制这个国家
                PeaceInitiator.ToBackArmy();
                return;
            }else if(player.unitId != null){
                // 协助这个国家的玩家
                var unit = UnitManager.instance.GetByKey(player.unitId);
                if(unit != null)
                {
                    unit.ToBack();
                }
            }
        }

        static public void MsgSurrender(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var targetMKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom == null || targetMKingdom == null)
            {
                return;
            }
            if(MKingdomManager.instance.allKingdoms.Count == 2)
            {
                // 只有2人的时候可以投降
                mKingdom.ToSurrender(targetMKingdom);
            }

        }

        static public void MsgToShowDiplomacy(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.kingdomCivId == "")
            {
                Debug.Log($"玩家 {player.name} 还没加入国家");
                return;
            }

            var PeaceInitiator = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(PeaceInitiator == null)
            {
                return;
            }
            PeaceInitiator.ToShowDiplomacy();
        }
        
        // 罢免官职
        static public void MsgFired(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.isKingPlayer == false)
            {
                // 没有控制的国家
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var unit = UnitManager.instance.GetByUnitId(comm[1]);
            if(unit == null)
            {
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom.kingdom != unit.actor.kingdom)
            {
                // 不是一个国家的，没权利罢免
                return;
            }
            if(unit.actor.kingdom.king == unit.actor)
            {
                // 他也是国王，你没权力
                return;
            }

            // TODO 开始罢免官职
            // 检查是不是武将
            var unitGroup = Reflection.GetField(unit.actor.GetType(),unit.actor,"unitGroup") as UnitGroup;
            if(unitGroup != null && unitGroup.groupLeader == unit.actor)
            {
                // 是武将
                unitGroup.CallMethod("setGroupLeader",null);
            }
            
            // 检查是不是侯爷
            if(unit.actor.city.leader == unit.actor)
            {
                // 是侯爷
                unit.actor.city.removeLeader();
            }

        }

        // 提升为武将，战斗队长
        static public void MsgPromotedToGeneral(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.isKingPlayer == false)
            {
                // 没有控制的国家
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var unit = UnitManager.instance.GetByUnitId(comm[1]);
            if(unit == null)
            {
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom.kingdom != unit.actor.kingdom)
            {
                // 不是一个国家的，没权利提升
                return;
            }
            if(unit.actor.kingdom.king == unit.actor)
            {
                // 他也是国王，你没权力
                return;
            }


            // TODO 开始提升官职
            // 检查是不是武将
            var unitGroup = Reflection.GetField(unit.actor.GetType(),unit.actor,"unitGroup") as UnitGroup;
            if(unitGroup != null && unitGroup.groupLeader == unit.actor)
            {
                // 是武将
                return;
            }
            // 检查是不是侯爷
            if(unit.actor.city.leader == unit.actor)
            {
                // 是侯爷
                unit.actor.city.removeLeader();
            }

            List<City> allCity = new List<City>();
            foreach (City city in mKingdom.kingdom.cities)
            {
                if(GameHelper.CityThings.CanAddJob(city))
                {
                    allCity.Add(city);
                }
            }
            if(allCity.Count <= 0)
            {
                // 没有城市了
                return;
            }
            // TODO 任命战斗队长
            City targetCity = allCity[UnityEngine.Random.Range(0,allCity.Count)];
            unit.actor.CallMethod("becomeCitizen",targetCity);
            targetCity.army.CallMethod("setGroupLeader",unit.actor);
            unit.GoTo(targetCity.getTile());
        }

        // 提升为侯爷
        static public void MsgPromotedToLeader(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.kingdomCivId == null || player.isKingPlayer == false)
            {
                // 没有控制的国家
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            var unit = UnitManager.instance.GetByUnitId(comm[1]);
            if(unit == null)
            {
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom.kingdom != unit.actor.kingdom)
            {
                // 不是一个国家的，没权利提升
                return;
            }
            if(unit.actor.kingdom.king == unit.actor)
            {
                // 他也是国王，你没权力
                return;
            }


            // TODO 开始提升官职
            // 检查是不是武将
            var unitGroup = Reflection.GetField(unit.actor.GetType(),unit.actor,"unitGroup") as UnitGroup;
            if(unitGroup != null && unitGroup.groupLeader == unit.actor)
            {
                // 是武将
                unitGroup.CallMethod("setGroupLeader",null);
            }
            // 检查是不是侯爷
            if(unit.actor.city.leader == unit.actor)
            {
                // 是侯爷
                return;
            }

            List<City> allCity = new List<City>();
            foreach (City city in mKingdom.kingdom.cities)
            {
                if(GameHelper.CityThings.CanAddJob(city))
                {
                    allCity.Add(city);
                }
            }
            if(allCity.Count <= 0)
            {
                // 没有城市了
                return;
            }
            // TODO 任命侯爷
            City targetCity = allCity[UnityEngine.Random.Range(0,allCity.Count)];
            unit.actor.CallMethod("becomeCitizen",targetCity);
            City.makeLeader(unit.actor, targetCity);
            unit.GoTo(targetCity.getTile());
        }

        // 投靠
        static public void MsgBetray(Player player,MessageDistribute.NormalMsg msg,List<string> comm)
        {
            if(player.unitId == null)
            {
                return;
            }
            if(comm.Count < 2)
            {
                // 命令错误
                return;
            }
            
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
            var currentMKingdom = MKingdomManager.instance.GetByKey(player.kingdomCivId);
            if(mKingdom == null || currentMKingdom == null)
            {
                return;
            }
            var unit = UnitManager.instance.GetByKey(player.unitId);
            if(unit == null)
            {
                return;
            }

            // 根据不同情况进行处理
            // 检查是不是武将
            var unitGroup = Reflection.GetField(unit.actor.GetType(),unit.actor,"unitGroup") as UnitGroup;
            if(unitGroup != null && unitGroup.groupLeader == unit.actor)
            {
                // 是武将
                unitGroup.CallMethod("setGroupLeader",null);
            }

            // 检查是不是侯爷 或 国王
            if(unit.actor.city.leader == unit.actor || 
            unit.actor.kingdom.king == unit.actor)
            {
                // 是侯爷
                // 也可能是国王
                // 将这块地一起送给投靠的国家
                unit.actor.city.joinAnotherKingdom(mKingdom.kingdom);
                return;
            }else{
                // 一个普普通通的人

                if(mKingdom.kingdom.cities.Count <= 0)
                {
                    // 没有城市了
                    return;
                }
                // TODO 投靠其他国家
                City targetCity = mKingdom.kingdom.cities[UnityEngine.Random.Range(0,mKingdom.kingdom.cities.Count)];
                unit.actor.CallMethod("becomeCitizen",targetCity);
                unit.GoTo(targetCity.getTile());
            }
        }
    }



}