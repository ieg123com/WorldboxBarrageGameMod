using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




namespace BarrageGame
{
    public class DrawIcon
    {
        static public MapIconAsset mapIconAssetPlayerUnit;
        static public void Init()
        {
            // 绘制玩家头像
            mapIconAssetPlayerUnit = new MapIconAsset
            {
                id = "player_unit",
                id_prefab = "p_mapSprite",
                base_scale = 3f,
                flag_kingdom_color = true,
                render_on_map = true,
                draw_call = new MapIconUpdater(DrawIcon.DrawUnit)
            };
            AssetManager.map_icons.add(mapIconAssetPlayerUnit);
        }

        
        static public void DrawUnit(MapIconAsset pAsset)
        {
            foreach(var unit in UnitManager.instance.allUnit.Values)
            {
                if(unit.head != null)
                {
                    MapMark mapMark = MapIconLibrary.drawMark(pAsset,unit.actor.currentTile, null, null, null, null);
                    mapMark.spriteRenderer.sprite = unit.head;


                }
            }
        }

    }
}