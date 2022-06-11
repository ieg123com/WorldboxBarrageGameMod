using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseName
{
    class AddAssetManager
    {
        internal static ChineseNameLibrary chineseNameGenerator;

        public static void addAsset()
        {
            chineseNameGenerator = new ChineseNameLibrary();

            add(chineseNameGenerator, "chineseNameGenerator");

        }

        private static void add(BaseAssetLibrary pLibrary,string pID)
        {
            pLibrary.init();
            pLibrary.id = pID;
            AssetManager.instance.dict.Add(pID, pLibrary);
            AssetManager.instance.list.Add(pLibrary);
            
        }
    }
}
