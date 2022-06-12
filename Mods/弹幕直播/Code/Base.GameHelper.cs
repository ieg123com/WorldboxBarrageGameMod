using System;
using UnityEngine;
using UnityEngine.UI;
using NCMS;
using NCMS.Utils;

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
            //GameObjects.FindEvenInactive("BottomElements").SetActive(false);
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
    }


}