using System;
using UnityEngine;
using UnityEngine.UI;


namespace BarrageGame
{
    public class ControlHelper
    {
        static public void GameStart()
        {

            // 删除之前的全部规则
            Debug.Log($"清理上一张图的游戏规则 count={Main.GameModel.transform.childCount}");
            for(int i = 0;i<Main.GameModel.transform.childCount;++i)
            {
                var child = Main.GameModel.transform.GetChild(i);
                Debug.Log($"[{i}]{child.gameObject.name}");
                UnityEngine.Object.Destroy(child.gameObject);
            }
            
            // 加载本局游戏规则
            Debug.Log("加载本局游戏规则");
            var go = new GameObject("DivideTheWorld");
            go.transform.SetParent(Main.GameModel.transform);
            go.AddComponent<DivideTheWorld>();


            PlayerManager.instance.Clear();
            MKingdomManager.instance.Clear();
            UnitManager.instance.Clear();
            UIKingdomList.instance.Clear();

            MKingdomHelper.Init();

            DebugConfig.setOption(DebugOption.ShowAmountNearArmy,true);
            DebugConfig.setOption(DebugOption.ShowWarriorsCityText,true);
            DebugConfig.setOption(DebugOption.CityFastConstruction,true);
            DebugConfig.setOption(DebugOption.CityFastUpgrades,true);
            //DebugConfig.setOption(DebugOption.CityUnlimitedHouses,true);


            Main.startGame = true;
            Main.startWar = false;


            GameHelper.ResetCamera();
            //GameObjects.FindEvenInactive("BottomElements").SetActive(false);
        }

        // 加载完成
        static public void LoadingCompleted()
        {
            Main.startGame = false;
            Debug.Log("地图加载完成...");


            GameStart();
        }
        
        static public void GameOver()
        {
            Main.startGame = false;



            PlayerManager.instance.Clear();
            MKingdomManager.instance.Clear();

            GameHelper.LoadMapStore(UnityEngine.Random.Range(1,23));
        }


    }
}