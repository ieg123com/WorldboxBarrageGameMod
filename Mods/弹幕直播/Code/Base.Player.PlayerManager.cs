using System;
using System.Collections.Generic;

namespace BarrageGame
{
    public class PlayerManager
    {
        static public PlayerManager instance;

        public Dictionary<long,Player> allPlayers = new Dictionary<long,Player>();

        public PlayerManager()
        {
            PlayerManager.instance = this;
        }

        public void Add(Player  player)
        {
            if(allPlayers.ContainsKey(player.uid))
            {
                allPlayers[player.uid] = player;
            }else{
                allPlayers.Add(player.uid,player);
            }
        }

        public void Remove(long uid)
        {
            if(allPlayers.ContainsKey(uid))
            {
                allPlayers.Remove(uid);
            }
        }

        public Player GetByKey(long uid)
        {
            Player player;
            if(allPlayers.TryGetValue(uid,out player))
            {
                return player;
            }
            return null;
        }

        public void Clear()
        {
            foreach(var v in allPlayers.Values)
            {
                UnityEngine.Object.Destroy(v.gameObject);
            }
            allPlayers.Clear();
        }


    }


}