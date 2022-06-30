using System;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;


namespace BarrageGame
{
    public class SpriteManager
    {
        static public SpriteManager instance;

        static public Sprite iconKings;
        static public Sprite iconLeaders;
        static public Sprite map_mark_flag;
        static public Sprite iconKingslayer;
        static public Sprite iconKingdom;



        public SpriteManager()
        {
            instance = this;

        }

        public void Init()
        {
            iconKings = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/icon/iconKings.png");
            iconLeaders = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/icon/iconLeaders.png");
            map_mark_flag = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/icon/map_mark_flag.png");
            iconKingslayer = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/icon/iconKingslayer.png");
            iconKingdom = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/icon/iconKingdom.png");
        }

        

    }
}