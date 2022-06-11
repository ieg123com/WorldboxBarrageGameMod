using System;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;

namespace ChineseName{
    [ModEntry]
    class Main : MonoBehaviour{
        //注意：Culture.create()已在ChineseNameGenerator添加后置补丁
        void Start()
        {
            //初始化
            ChineseNameGenerator.init();
            //开启拦截

            Harmony.CreateAndPatchAll(typeof(ChineseNameGenerator));

            //添加Asset
            AddAssetManager.addAsset();
        }
    }
}