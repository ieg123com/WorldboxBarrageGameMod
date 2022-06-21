using System;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;



namespace BarrageGame
{
    class UnitFactory
    {
        static public Unit Create(WorldTile pTile, string pPowerID)
        {
            var actor = GameHelper.spawnUnit(pTile,pPowerID);
            //actor.isPlayer = true;
            var unit = new Unit();
            unit.Id = actor.GetID();
            unit.actor = actor;
            UnitManager.instance.Add(unit);
            Reflection.SetField<bool>(actor, "event_full_heal", true);
            return unit;
        }
    }
}