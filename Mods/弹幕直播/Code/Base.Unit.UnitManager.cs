using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame
{
    public class UnitManager
    {
        static public UnitManager instance;

        static public int lastId = 0;

        public Dictionary<string,Unit> allUnit = new Dictionary<string,Unit>();
        public Dictionary<string,string> unitId2StringId = new Dictionary<string,string>();


        static public string GenerateUnitId()
        {
            ++lastId;
            if(lastId == 7)
            {
                ++lastId;
            }
            return lastId.ToString();
        }

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
                unitId2StringId[unit.unitId] = unit.Id;

            }else{
                allUnit.Add(unit.Id,unit);
                unitId2StringId.Add(unit.unitId,unit.Id);
            }
        }

        public void Remove(string id)
        {
            if(allUnit.TryGetValue(id,out var unit))
            {
                if(unitId2StringId.ContainsKey(unit.unitId))
                {
                    unitId2StringId.Remove(unit.unitId);
                }
                if(allUnit.ContainsKey(id))
                {
                    allUnit.Remove(id);
                }
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

        public Unit GetByUnitId(string unitId)
        {
            if(unitId2StringId.TryGetValue(unitId,out var id))
            {
                return GetByKey(id);
            }
            return null;
        }

        public void Clear()
        {
            allUnit.Clear();
            unitId2StringId.Clear();
            lastId = 0;
        }

        public void SecondsUpdate()
        {
            foreach(var unit in allUnit.Values)
            {
                unit.SecondsUpdate();
            }
        }

    }
}