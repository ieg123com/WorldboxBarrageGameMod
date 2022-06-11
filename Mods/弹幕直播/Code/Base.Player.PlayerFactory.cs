using System;
using UnityEngine;
using NCMS;
using NCMS.Utils;


namespace BarrageGame
{

    public class PlayerFactory
    {

        static public Player Create(PlayerInfo info)
        {
            var go = new GameObject("Player");
            var player = go.AddComponent<Player>();
            player.uid = info.uid;
            player.name = info.name;
            player.urlHead = info.urlHead;
            player.headSprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");
            return player;
        }

        static public Player GetOrCreate(PlayerInfo info)
        {
            var player = PlayerManager.instance.GetByKey(info.uid);
            if(player == null)
            {
                player = Create(info);
                PlayerManager.instance.Add(player);
            }
            return  player;
        }
    }
}