using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseName
{
    class ChineseNameAsset : Asset
    {
        //采用模板法命名
        public string[] templates = { "addition_start","part","addition_end"};//命名模板
        /*
         * 大致模板为addition_start,parts,addition_end
         * 模板元素前缀加上R.6F表示60%产生
         */

        public List<string> addition_startList = new List<string>();//前缀,也用作姓氏

        public List<string> addition_endList = new List<string>();//后缀，也用作名
        
        public List<string> partsList = new List<string>();//中间1，也用作辈分

        public List<string> partsList2 = new List<string>();//中间2

        public List<string> fixedList = new List<string>();//可作为固定生成的名字

        public List<string> special1 = new List<string>();//特殊词缀一组

        public List<string> special2 = new List<string>();//特殊词缀二组

        public bool onlyByTemplate = false;//是否只按照模板生成（即不生成预订的名字

        public float fixedNameChance = 0.5f;//表示产生固定名字的概率（仅在上一项为false时有效），0.5f表示50%

        public ChineseNameAsset()
        {
        }
        static ChineseNameAsset()
        {

        }
    }
}
