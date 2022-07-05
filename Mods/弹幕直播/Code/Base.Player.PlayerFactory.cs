using System;
using System.IO;
using UnityEngine;
using NCMS;
using NCMS.Utils;
using Newtonsoft.Json;


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
            player.medal_name = info.medal_name;
            player.medal_room_id = info.medal_room_id;
            player.medal_level = info.medal_level;
            player.headSprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");

            // 加载存档
            string path = $"{SaveManager.generateMainPath("PlayerData")}/{info.uid}.txt";
            if(File.Exists(path))
            {
                player.playerDataInfo = JsonConvert.DeserializeObject(
                    File.ReadAllText(path),
                        typeof(PlayerDataInfo)) as PlayerDataInfo;
            }
            player.playerDataInfo.uid = info.uid;
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