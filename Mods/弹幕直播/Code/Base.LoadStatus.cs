using System;
using UnityEngine;


namespace BarrageGame
{
    // 地图加载状态
    public class LoadStatus
    {
        public bool lastStatus = false;
        public float tempTime = 0f;
        public void Update()
        {
            tempTime += Time.deltaTime;
            if(tempTime >= 1f)
            {
                tempTime -= 1f;

                if(Config.worldLoading != lastStatus)
                {
                    lastStatus = Config.worldLoading;
                    if(lastStatus == false)
                    {
                        // 加载地图完成
                        ControlHelper.LoadingCompleted();
                    }
                }
            }
        }
    }
}