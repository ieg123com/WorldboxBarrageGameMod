using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using ReflectionUtility;


namespace BarrageGame
{

    public class Unit
    {
        // 圆形通道
        static public Texture2D RoundChannel;
        public string Id;
        public string unitId;
        public string unitDataInfo;
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
            uIUnit.name.text = $"[{unitId}]{player.name}";
            long killNum = player.playerDataInfo.unitDataInfo.killUnitNum;
            killNum += player.playerDataInfo.unitDataInfo.killWarriorNum;
            killNum += player.playerDataInfo.unitDataInfo.killBabyNum;
            uIUnit.kGlK.text = $"{killNum}/{player.playerDataInfo.unitDataInfo.killLeaderNum}/{player.playerDataInfo.unitDataInfo.killKingNum}";
            
            // 本局
            killNum = player.currentUnitDataInfo.killUnitNum;
            killNum += player.currentUnitDataInfo.killWarriorNum;
            killNum += player.currentUnitDataInfo.killBabyNum;
            killNum += player.currentUnitDataInfo.killLeaderNum;
            killNum += player.currentUnitDataInfo.killKingNum;
            uIUnit.thisTimeKD.text = $"{killNum}/{player.currentUnitDataInfo.deathNum}";

            ActorStatus actorStatus = Reflection.GetField(actor.GetType(),actor,"data") as ActorStatus;
            switch(actorStatus.profession)
            {
                case UnitProfession.King:
                // 国王
                uIUnit.jobImage.sprite = SpriteManager.iconKings;
                uIUnit.jobImage.color = Color.white;
                break;
                case UnitProfession.Leader:
                // 领袖
                uIUnit.jobImage.sprite = SpriteManager.iconLeaders;
                uIUnit.jobImage.color = Color.white;
                break;
                default:
                {
                    var unitGroup = Reflection.GetField(actor.GetType(),actor,"unitGroup") as UnitGroup;
                    if(unitGroup != null && unitGroup.groupLeader == actor)
                    {
                        uIUnit.jobImage.sprite = SpriteManager.map_mark_flag;
                        uIUnit.jobImage.color = Color.white;
                    }else{
                        uIUnit.jobImage.color = Color.clear;
                    }
                }
                break;
            }
        }

        public void SecondsUpdate()
        {
            ReflectionUIUnit();
        }
    }

}