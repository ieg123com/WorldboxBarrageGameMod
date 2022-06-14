using System;
using UnityEngine;
using UnityEngine.UI;




namespace BarrageGame
{
    class UnitFactory
    {
        static public Unit Create(WorldTile pTile, string pPowerID)
        {
            var actor = GameHelper.spawnUnit(pTile,pPowerID);
            var unit = new Unit();
            unit.instanceId = actor.GetInstanceID();
            unit.actor = actor;
            UnitManager.instance.Add(unit);
            return unit;
        }
    }
}