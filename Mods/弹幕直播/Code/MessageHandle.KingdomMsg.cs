using System;
using UnityEngine;
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
        }

        static public void MsgJoin(Player player,MessageDistribute.NormalMsg msg)
        {
            Debug.Log($"MsgJoin {msg.msg}");
            if(player.kingdomCivId != null && player.kingdomCivId != "")
            {
                Debug.Log($"玩家 {player.name} 已经加入国家 {player.kingdomCivId}");
                return;
            }
            var comm = msg.msg.Split(' ');
            if(comm.Length < 2)
            {
                // 命令错误
                return;
            }
            var mKingdom = MKingdomManager.instance.GetByKey($"k_{comm[1]}");
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

        }

        static public void MsgSplite(Player player,MessageDistribute.NormalMsg msg)
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
            var comm = msg.msg.Split(' ');
            if(comm.Length < 2)
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

            Debug.Log($"玩家 {player.name} {WarInitiator.id} 宣战 {mKingdom.id}");
            WarInitiator.StartWar(mKingdom);
            //MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", WarInitiator.kingdom, mKingdom.kingdom,false);

            mKingdom.peaceList.Remove(WarInitiator.id);
            WarInitiator.peaceList.Remove(mKingdom.id);
        }

        static public void MsgPeace(Player player,MessageDistribute.NormalMsg msg)
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
            var comm = msg.msg.Split(' ');
            if(comm.Length < 2)
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
    }



}