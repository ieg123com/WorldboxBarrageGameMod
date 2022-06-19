using System;
using System.Collections.Generic;
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
            //Camera.main.orthographicSize = MapBox.width/2f;
            Reflection.SetField<float>(moveCamera, "targetZoom", MapBox.width/2f);
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

            // 随机建国
            public static Kingdom RandomCreate()
            {
                var zoneCalculator = Reflection.GetField(MapBox.instance.GetType(), MapBox.instance, "zoneCalculator") as ZoneCalculator;
                if(zoneCalculator == null)
                {
                    return null;
                }
                City city = null;
                List<TileZone> gooldZones = new List<TileZone>();
                for(int i = 0;i<zoneCalculator.zones.Count;++i)
                {
                    if(zoneCalculator.zones[i].goodForNewCity == true)
                    {
                        gooldZones.Add(zoneCalculator.zones[i]);
                        if(gooldZones.Count > 10)
                        {
                            break;
                        }
                    }
                }
                if(gooldZones.Count == 0)
                {
                    return null;
                }
                {
                    var pTile = gooldZones[UnityEngine.Random.Range(0,gooldZones.Count)].centerTile;
                    var race = AssetManager.raceLibrary.get("human");
                    city = MapBox.instance.buildNewCity(pTile.zone,null,race,false,null);
                    if(city != null)
                    {
                        city.newCityEvent();
                        var actor = GameHelper.spawnUnit(pTile,"humans");
                        actor.CallMethod("becomeCitizen",city);
                    }
                }
                if(city == null)
                {
                    for(int i = 0;i<zoneCalculator.zones.Count;++i)
                    {
                        if(zoneCalculator.zones[i].goodForNewCity == true)
                        {
                            var pTile = zoneCalculator.zones[i].centerTile;
                            var race = AssetManager.raceLibrary.get("human");
                            city = MapBox.instance.buildNewCity(pTile.zone,null,race,false,null);
                            if(city == null)
                            {
                                continue;
                            }
                            city.newCityEvent();
                            var actor = GameHelper.spawnUnit(pTile,"humans");
                            actor.CallMethod("becomeCitizen",city);
                            break;
                        }
                    }
                }
                if(city == null)
                {
                    return null;
                }
                return Reflection.GetField(city.GetType(), city, "kingdom") as Kingdom;
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