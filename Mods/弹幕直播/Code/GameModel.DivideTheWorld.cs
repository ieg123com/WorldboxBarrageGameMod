using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




namespace BarrageGame
{
    // 兵分天下
    // A.暂停 30 秒
    // B.40倍加速到200年
    // C.4倍速到只剩一个国家
    // 剩余2个国家时，主动敌对
    public class DivideTheWorld : MonoBehaviour
    {
        public enum StageType
        {
            None,
            // A.暂停 30 秒
            A,
            // B.40倍加速到200年
            B,
            // C.2倍速到只剩一个国家
            C,
        }
        static public DivideTheWorld instance;
        // 阶段
        public StageType stageType = StageType.None;

        public float secondsTime = 0f;

        public float aTimer = 0f;

        private bool taskCompleted = false;

        private int createkingdomCount = 0;

        void Awake()
        {
            instance = this;
            Debug.Log(">>>>>>>>>>> DivideTheWorld.Awake");

        }

        void Start()
        {
            stageType = StageType.A;
            GameHelper.SetTimeScale(0.1f);
            //GameHelper.Paused(true);
        }

        void Update()
        {
            secondsTime += Time.deltaTime;
            if(secondsTime >= 1f)
            {
                secondsTime -= 1f;
                // 流逝1秒
                SecondsUpdate();
            }

            if(createkingdomCount > 0)
            {
                createkingdomCount--;
                GameHelper.KingdomThings.RandomCreate();

            }

            if(stageType == StageType.B)
            {
                UILoading.instance.titleText.text = $"发展时间，禁止开战...(<color=#00c000>{MapBox.instance.mapStats.year}.{MapBox.instance.mapStats.month}/150</color>)";
                UILoading.instance.load = MapBox.instance.mapStats.year * 12 + MapBox.instance.mapStats.month;
                UILoading.instance.RefreshDisplay();
            }

        }


        void SecondsUpdate()
        {
            switch(stageType)
            {
                case StageType.A:
                {
                    aTimer += 1f;
                    if(aTimer >= 9f)
                    {
                        stageType = StageType.B;
                        //GameHelper.Paused(false);
                        if(MKingdomManager.instance.allKingdoms.Count < 20)
                        {
                            createkingdomCount = 20 - MKingdomManager.instance.allKingdoms.Count;
                        }

                        GameHelper.SetTimeScale(40f);
                        UILoading.instance.loadMax = 150 * 12;
                        UILoading.instance.load = MapBox.instance.mapStats.year * 12;
                        UILoading.instance.RefreshDisplay();
                        UILoading.instance.titleDownText.text = "";
                        UILoading.instance.goMain.SetActive(true);
                    }
                    break;
                }
                case StageType.B:
                {
                    UILoading.instance.titleText.text = $"发展时间，禁止开战...(<color=#00c000>{MapBox.instance.mapStats.year}.{MapBox.instance.mapStats.month}/150</color>)";
                    UILoading.instance.titleDownText.text = MapBox.instance.mapStats.description;
                    if(MapBox.instance.mapStats.year >= 150)
                    {
                        stageType = StageType.C;
                        GameHelper.SetTimeScale(5f);
                        // 可以宣战了
                        Main.startWar = true;
                        UILoading.instance.goMain.SetActive(false);
                        MapBox.instance.addNewText("可以相互宣战了", Toolbox.color_log_good, null);
                    }
                    break;
                }
                case StageType.C:
                {
                    if(MKingdomManager.instance.allKingdoms.Count <= 1)
                    {
                        if(taskCompleted == false)
                        {
                            taskCompleted = true;
                            OnCompeleted();
                        }
                    }
                    if(MKingdomManager.instance.allKingdoms.Count == 2)
                    {
                        if(MKingdomManager.instance.allKingdoms.ElementAt(0).Value.IsEnemy(MKingdomManager.instance.allKingdoms.ElementAt(1).Value) == false)
                        {
                            MKingdomManager.instance.allKingdoms.ElementAt(0).Value.StartWar(MKingdomManager.instance.allKingdoms.ElementAt(1).Value);
                        }
                    }
                    break;
                }
            }




            if(stageType == StageType.C)
            {
                // 时间流速会慢慢变快
                int diffYear = MapBox.instance.mapStats.year - 150;
                if(diffYear<0)
                {
                    diffYear =0;
                }
                diffYear = (diffYear > 200)?200:diffYear;
                GameHelper.SetTimeScale(5f + 15f * (diffYear / 200f));
                return;
                if(MKingdomManager.instance.allKingdoms.Count > 2){
                    // 简易ai
                    {
                        int index = UnityEngine.Random.Range(0,MKingdomManager.instance.allKingdoms.Count);
                        var mKingdom =MKingdomManager.instance.allKingdoms.ElementAt(index).Value;
                        if(mKingdom.kingPlayerUid != 0)
                        {
                            // 这是玩家控制的
                            return;
                        }
                        if(mKingdom.HasEnemies())
                        {
                            // 有敌人了，先把现在的敌人解决掉
                            return;
                        }
                        index = UnityEngine.Random.Range(0,MKingdomManager.instance.allKingdoms.Count);
                        var targetMKingdom = MKingdomManager.instance.allKingdoms.ElementAt(index).Value;
                        if(mKingdom.IsEnemy(targetMKingdom))
                        {
                            // 已经是敌人了
                            return;
                        }
                        if(!mKingdom.ForceComparison(targetMKingdom))
                        {
                            // 没他厉害，和他宣战，我傻吗？
                            return;
                        }
                        mKingdom.StartWar(targetMKingdom);
                    }

                }
            }
        }

        void OnCompeleted()
        {
            // 任务完成
            // TODO 记录谁赢了
            if(MKingdomManager.instance.allKingdoms.Count == 1)
            {
                var mKingdom = MKingdomManager.instance.allKingdoms.ElementAt(0).Value;
                var winedPlayer = PlayerManager.instance.GetByKey(mKingdom.kingPlayerUid);
                if(winedPlayer != null)
                {
                    winedPlayer.playerDataInfo.kingdomDataInfo.winNum += 1;
                    winedPlayer.dataChanged = true;
                }
            }


            Destroy(gameObject);
            ControlHelper.GameOver();
        }
        void OnDisable()
        {
            Debug.Log(">>>>>>>>>>> DivideTheWorld.OnDisable");
        }
    }
} 