using System;
using System.Collections.Generic;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame
{
    public class MKingdomManager
    {
        static public MKingdomManager instance;

        public Dictionary<string,MKingdom> allKingdoms = new Dictionary<string,MKingdom>();

        public MKingdomManager()
        {
            MKingdomManager.instance = this;
        }
        
        public void Add(MKingdom  kingdom)
        {
            if(allKingdoms.ContainsKey(kingdom.id))
            {
                Remove(kingdom.id);
                allKingdoms[kingdom.id] = kingdom;
            }else{
                allKingdoms.Add(kingdom.id,kingdom);
            }
        }

        public void Remove(string id)
        {
            if(allKingdoms.ContainsKey(id))
            {
                allKingdoms.Remove(id);
            }
        }

        public MKingdom GetByKey(string id)
        {
            MKingdom kingdom;
            if(allKingdoms.TryGetValue(id,out kingdom))
            {
                return kingdom;
            }
            return null;
        }

        public void Clear()
        {
            foreach(var v in allKingdoms.Values)
            {
                v.Clear();
                UnityEngine.Object.Destroy(v.gameObject);
            }
            allKingdoms.Clear();
        }




    }


}