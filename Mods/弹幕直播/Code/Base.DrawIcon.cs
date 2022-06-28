using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;



namespace BarrageGame
{
    public class DrawIcon
    {
        public static Color Color_KingdomAttackTarget;
        static public void Init()
        {
            Color_KingdomAttackTarget = new Color(1f,0f,0f,0.7f);
            // 绘制玩家头像
            AssetManager.map_icons.add(new MapIconAsset
            {
                id = "player_unit",
                id_prefab = "p_mapSprite",
                base_scale = 3f,
                flag_kingdom_color = true,
                render_on_map = true,
                draw_call = new MapIconUpdater(DrawIcon.DrawUnit)
            });

            AssetManager.map_icons.add(new MapIconAsset
            {
                base_scale = 0.25f,
                id = "arrows_kingdom_attack_targets2",
                id_prefab = "p_mapArrow_stroke",
                render_arrow_end = true,
                render_arrow_start = true,
                arrow_animation = true,
                render_on_map = true,
                draw_call = new MapIconUpdater(DrawIcon.DrawKingdomAtWar)
            });
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

        static public void DrawKingdomAtWar(MapIconAsset pAsset)
        {
            foreach(var mKingdom in MKingdomManager.instance.allKingdoms.Values)
            {
                if(mKingdom.capital == null)
                {
                    continue;
                }
                WorldTile tileA = null;
                foreach(var targetMKingdom in mKingdom.AllKingdomAtWar.Values)
                {
                    if(targetMKingdom.capital == null)
                    {
                        continue;
                    }
                    if(mKingdom.ShowDiplomacyTime > 0.1f || targetMKingdom.ShowDiplomacyTime > 0.1f)
                    {
                        if(tileA == null)
                        {
                            tileA = Reflection.GetField(mKingdom.capital.GetType(),mKingdom.capital,"_cityTile") as WorldTile;
                        }
                        var tileB = Reflection.GetField(targetMKingdom.capital.GetType(),targetMKingdom.capital,"_cityTile") as WorldTile;
                        MapIconLibrary.drawArrowMark(pAsset, tileA.posV3, tileB.posV3, ref Color_KingdomAttackTarget, null);
                    }

                }
            }
        }



    }
}