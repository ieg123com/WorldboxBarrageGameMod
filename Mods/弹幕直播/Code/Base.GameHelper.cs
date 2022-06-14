using System;
using UnityEngine;
using UnityEngine.UI;
using NCMS;
using NCMS.Utils;
using Beebyte.Obfuscator;
using ReflectionUtility;

namespace BarrageGame
{

    public class GameHelper
    {
        // 重置Camera
        static public void ResetCamera()
        {
            Camera.main.transform.position = new Vector3(){
                x=MapBox.width/2f,
                y=MapBox.height/2f
            };
            var moveCamera = Camera.main.GetComponent<MoveCamera>();
            Camera.main.orthographicSize = MapBox.width/2f;
            GameObjects.FindEvenInactive("BottomElements").SetActive(false);
        }
        // 加载地图
        static public void LoadMapStore(int id)
        {
            SaveManager.setCurrentSlot(id);
            MapBox.instance.saveManager.startLoadSlot();
        }
        // 暂停游戏 true.暂停游戏 false.恢复游戏
        static public void Paused(bool v)
        {
            Config.paused = v;
        }
        // 变速
        static public void SetTimeScale(float scale)
        {
            Config.timeScale = scale;
        }

        static public Actor spawnUnit(WorldTile pTile, string pPowerID)
        {
            GodPower godPower = AssetManager.powers.get(pPowerID);
            Sfx.play("spawn", true, -1f, -1f);
            if (godPower.spawnSound != "")
            {
                Sfx.play(godPower.spawnSound, true, -1f, -1f);
            }
            if (godPower.id == "sheep")
            {
                Sfx.timeout("sheep");
                if (pTile.Type.lava)
                {
                    AchievementLibrary.achievementSacrifice.check(null, null, null);
                }
            }
            if (godPower.showSpawnEffect != string.Empty)
            {
                MapBox.instance.stackEffects.CallMethod("startSpawnEffect",pTile, godPower.showSpawnEffect);
            }
            string pStatsID;
            if (godPower.actorStatsId.Contains(","))
            {
                pStatsID = Toolbox.getRandom<string>(godPower.actorStatsId.Split(new char[]
                {
                    ','
                }));
            }
            else
            {
                pStatsID = godPower.actorStatsId;
            }
            return MapBox.instance.spawnNewUnit(pStatsID, pTile, "", godPower.actorSpawnHeight);
        }


        public class KingdomThings
        {

            public static Color GetKingdomColor(Kingdom kingdom)
            {
                var kingdomColor = (KingdomColor)Reflection.GetField(kingdom.GetType(), kingdom, "kingdomColor");
                return kingdomColor.colorBorderOut;
            }

            public static WorldTile GetCapitalTile(Kingdom kingdom)
            {
                return kingdom.capital.getTile();
            }



        }



        public class MapText
        {
            public static Vector2 TransformPosition(Vector3 pVec)
            {
                Vector2 vector = Camera.main.WorldToViewportPoint(pVec);
                var canvasRect = (RectTransform)Reflection.GetField(MapNamesManager.instance.GetType(),MapNamesManager.instance,"canvasRect");
                return new Vector2(vector.x * canvasRect.sizeDelta.x - canvasRect.sizeDelta.x * 0.5f, vector.y * canvasRect.sizeDelta.y - canvasRect.sizeDelta.y * 0.5f);
            }
        }
    }


}