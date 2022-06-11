using System;
using UnityEngine;


namespace BarrageGame
{
    public class ControlHelper
    {
        static public void GameStart()
        {
            // 加载本局游戏规则
            Main.GameModel.AddComponent<DivideTheWorld>();


            MKingdomHelper.Init();

            DebugConfig.setOption(DebugOption.ShowAmountNearArmy,true);
            DebugConfig.setOption(DebugOption.ShowWarriorsCityText,true);

            Main.startGame = true;

            //GameObjects.FindEvenInactive("BottomElements").SetActive(false);
        }

        // 加载完成
        static public void LoadingCompleted()
        {
            Debug.Log("地图加载完成...");


            GameStart();
        }
        
        static public void GameOver()
        {
            Main.startGame = false;



            PlayerManager.instance.Clear();
            MKingdomManager.instance.Clear();

            GameHelper.LoadMapStore(1);
        }


    }
}