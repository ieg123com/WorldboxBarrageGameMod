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
            var unit = new Unit();
            actor.isPlayerControl = true;
            unit.Id = actor.GetID();
            unit.unitId = UnitManager.GenerateUnitId();
            unit.actor = actor;
            unit.actorCurStats = Reflection.GetField(actor.GetType(),actor,"curStats") as BaseStats;
            unit.actorData = Reflection.GetField(actor.GetType(),actor,"data") as ActorStatus;
            UnitManager.instance.Add(unit);
            Reflection.SetField<bool>(actor, "event_full_heal", true);
            return unit;
        }
    }
}