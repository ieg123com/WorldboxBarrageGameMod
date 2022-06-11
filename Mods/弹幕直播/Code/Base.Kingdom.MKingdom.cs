using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BarrageGame
{
    public class MKingdom : MonoBehaviour
    {
        public string id;

        public bool alive;
        // 国王玩家id
        public long kingPlayerUid = 0;

        public Kingdom kingdom = null;

        public HashSet<string> peaceList = new HashSet<string>();

        public void SetHeadSprite(Sprite head)
        {
            kingdom.headSprite = head;
        }
        public void SetName(string name)
        {
            kingdom.name = name;
        }



    }


}