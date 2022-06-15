using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame
{
    public class UnitManager
    {
        static public UnitManager instance;

        public Dictionary<string,Unit> allUnit = new Dictionary<string,Unit>();
        public UnitManager()
        {
            instance = this;
            
        }


        public void Add(Unit  unit)
        {
            if(allUnit.ContainsKey(unit.Id))
            {
                Remove(unit.Id);
                allUnit[unit.Id] = unit;
            }else{
                allUnit.Add(unit.Id,unit);
            }
        }

        public void Remove(string id)
        {
            if(allUnit.ContainsKey(id))
            {
                allUnit.Remove(id);
            }
        }

        public Unit GetByKey(string id)
        {
            Unit unit;
            if(allUnit.TryGetValue(id,out unit))
            {
                return unit;
            }
            return null;
        }

        public void Clear()
        {
            allUnit.Clear();
        }



    }
}