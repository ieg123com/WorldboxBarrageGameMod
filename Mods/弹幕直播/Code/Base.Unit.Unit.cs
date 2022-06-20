using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;



namespace BarrageGame
{

    public class Unit
    {
        // 圆形通道
        static public Texture2D RoundChannel;
        public string Id;
        public long ownerPlayerUid = 0;
        public Sprite head;
        public Actor actor;

        public UIUnit uIUnit = null;

        public void Apply()
        {
            var temp_head = head;
            head = null;
            int width = 64;
            int height = 64;
            Texture2D temp = new Texture2D(width,height);
            Task.Run(
                async ()=>{
                    
                    for(int x =0 ;x<width;++x)
                    {
                        for(int y = 0;y<height;++y)
                        {
                            var color = temp_head.texture.GetPixel(x,y);
                            color.a = RoundChannel.GetPixel(x,y).r;
                            Debug.Log( color.a);
                            temp.SetPixel(x,y,color);
                            //temp.SetPixel(x,y,Color.red);
                        }
                    }
                    Main.actions.Enqueue(
                        ()=>{
                            temp.Apply();
                            head = Sprite.Create(temp, new Rect(0,0,temp.width, temp.height), new Vector2(0.5f, 0.5f));
                        }
                    );
                }
            );
            
            //headSpriteRenderer.sprite = head;
            
            Debug.Log("设置 head");

        }

        public void ReflectionUIUnit()
        {
            if(uIUnit == null)
            {
                return;
            }
            var player = PlayerManager.instance.GetByKey(ownerPlayerUid);
            if(player == null)
            {
                return;
            }
            uIUnit.image.sprite = player.headSprite;
            uIUnit.name.text = $"[{Id.Substring(2)}]{player.name}";
            uIUnit.kGlK.text = $"{player.playerDataInfo.unitDataInfo.killUnitNum}/{player.playerDataInfo.unitDataInfo.killWarriorNum}/{player.playerDataInfo.unitDataInfo.killLeaderNum}/{player.playerDataInfo.unitDataInfo.killKingNum}";
        }
    }

}