using System;
using UnityEngine;
using UnityEngine.UI;
using NCMS;
using NCMS.Utils;

namespace BarrageGame
{
    public class MKingdomFactory
    {
        static public MKingdom Create(Kingdom kingdom)
        {
            var go = new GameObject("MKingdom");
            MKingdom mkingdom = go.AddComponent<MKingdom>();
            mkingdom.id = kingdom.id;
            mkingdom.alive = true;
            mkingdom.kingdom = kingdom;
            mkingdom.capital = kingdom.capital;
            kingdom.headSprite = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/Computer.png");
            return mkingdom;
        }

    }


}