using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ChineseName
{
    class ChineseNameGenerator
    {
        public static ChineseNameGenerator instance;

        public static bool isChinese = true;

        public static void init()
        {
            instance = new ChineseNameGenerator();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NameGenerator),"getName")]
        public static bool getChineseName(string pAssetID,ref string __result)
        {
            if(!isChinese){
                return true;
            }
            ChineseNameAsset chineseNameAsset;
            if (AddAssetManager.chineseNameGenerator.dict.ContainsKey(pAssetID))
            {
                chineseNameAsset = AddAssetManager.chineseNameGenerator.get(pAssetID);
            }
            else
            {
                chineseNameAsset = AddAssetManager.chineseNameGenerator.get("default_name");

            }
            __result = instance.getNameFromTemplate(chineseNameAsset);

            return false;
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Culture),"create")]
        public static void create_Postfix(Race pRace,City pCity,ref Culture __instance)
        {
            //原代码 
            /*this.race = pRace.id;
            this.list_tech_ids = new List<string>();
            this.id = MapBox.instance.mapStats.getNextId("culture");
            NameGeneratorAsset pAsset = AssetManager.nameGenerator.get(pRace.name_template_culture);
            this.name = NameGenerator.generateNameFromTemplate(pAsset);
            if (pCity != null)
            {
                this.village_origin = pCity.data.cityName;
            }
            else
            {
                this.village_origin = "??";
            }
            this.year = MapBox.instance.mapStats.year;
            this.prepare();
            */
            if(!isChinese){
                return;
            }
            ChineseNameAsset chineseNameAsset = AddAssetManager.chineseNameGenerator.get(pRace.name_template_culture);

            __instance.name = instance.getNameFromTemplate(chineseNameAsset);
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(LocalizedTextManager),"setLanguage")]
        public static void setLanguage_Postfix(string pLanguage)
        {
            if (pLanguage != "cz" &&pLanguage!="ch")
            {
                isChinese = false;
            }
            else
            {
                isChinese = true;
            }
        }
        public string getNameFromTemplate(ChineseNameAsset pAsset)
        {
            StringBuilder nameBuilder = new StringBuilder();
            //如果该命名可使用固定名
            if (!pAsset.onlyByTemplate && Toolbox.randomChance(pAsset.fixedNameChance))
            {
                return pAsset.fixedList.GetRandom();
            }
            //否则进行随机
            int length = pAsset.templates.Length;
            string[] template = pAsset.templates;
            /*
             * 遍历整个模板，若不存在'.'则直接添加，否则对该项遍历至'F'，确定概率，添加
             * 添加采用外置函数
             * 外置函数的参数为：
             *      概率(float)，类型(string)
             */
            for(int i = 0; i < length; i++)
            {
                addPartsByTemplate(nameBuilder, template[i],pAsset);
            }
            return nameBuilder.ToString();
        }
        //根据模板内元素确定概率
        private void addPartsByTemplate(StringBuilder nameBuilder, string partTemplate, ChineseNameAsset pAsset)
        {
            
            if (partTemplate.StartsWith("R."))
            {
                float chance = 0;
                int pos = 2;
                while (partTemplate[pos] != 'F')
                {
                    float k = 1f;
                    for(int i = 0; i < pos - 1; i++)
                    {
                        k *= 0.1f;
                    }
                    chance += (partTemplate[pos] - '0') * k;
                    pos++;
                }
                addPartsByChance(nameBuilder, partTemplate, pAsset,chance, true);
            }
            else
            {
                addPartsByChance(nameBuilder, partTemplate, pAsset,1, false);
            }
            //再使用上述外置函数
        }
        
        //按照概率添加文本
        private void addPartsByChance(StringBuilder nameBuilder,string partTemplate, ChineseNameAsset pAsset,double chance = 1,bool random = false)
        {
            if (random)
            {
                if (Toolbox.randomChance((float)chance))
                {
                    nameBuilder.Append(getPart(partTemplate, pAsset));
                }
            }
            else
            {
                nameBuilder.Append(getPart(partTemplate, pAsset));
            }
        }
        //按照模板获取文本
        private string getPart(string partTemplate, ChineseNameAsset pAsset)
        {
            if (partTemplate.Contains("addition_start"))
            {
                return pAsset.addition_startList.GetRandom();
            }
            else if (partTemplate.Contains("addition_end"))
            {
                return pAsset.addition_endList.GetRandom();
            }
            else if (partTemplate.Contains("part1"))
            {
                return pAsset.partsList.GetRandom();
            }
            else if (partTemplate.Contains("part2"))
            {
                return pAsset.partsList2.GetRandom();
            }
            else if (partTemplate.Contains("special1"))
            {
                return pAsset.special1.GetRandom();
            }
            else if (partTemplate.Contains("special2"))
            {
                return pAsset.special2.GetRandom();
            }
            else
            {
                return "请检查该生物命名模板";
            }

        }
    }
}
