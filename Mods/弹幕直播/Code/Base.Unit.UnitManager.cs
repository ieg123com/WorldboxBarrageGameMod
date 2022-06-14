using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame
{
    public class UnitManager
    {
        static public UnitManager instance;

        public Dictionary<int,Unit> allUnit = new Dictionary<int,Unit>();
        public UnitManager()
        {
            instance = this;
            
        }


        public void Add(Unit  unit)
        {
            if(allUnit.ContainsKey(unit.instanceId))
            {
                Remove(unit.instanceId);
                allUnit[unit.instanceId] = unit;
            }else{
                allUnit.Add(unit.instanceId,unit);
            }
        }

        public void Remove(int instanceId)
        {
            if(allUnit.ContainsKey(instanceId))
            {
                allUnit.Remove(instanceId);
            }
        }

        public Unit GetByKey(int instanceId)
        {
            Unit unit;
            if(allUnit.TryGetValue(instanceId,out unit))
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