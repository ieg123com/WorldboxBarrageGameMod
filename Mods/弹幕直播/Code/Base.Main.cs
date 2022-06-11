using System;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using UnityEngine.UI;
using ReflectionUtility;

namespace BarrageGame{

    [ModEntry]
    class Main : MonoBehaviour{

        static public Main instance;

        static public bool startGame = false;

        public MessageDistribute messageDistribute;
        public PlayerManager playerManager;

        public MKingdomManager mKingdomManager;

        public WebSocketToCCWSS webSocketToCCWSS;

        public LoadStatus loadStatus;

        static public GameObject GameModel;

        
        static public Sprite sprite = null;
        static public List<MapText> mapTextList = new List<MapText>();


        static public int grenadeNum = 0;
        void Awake(){
            instance = this;

            messageDistribute = new MessageDistribute();
            playerManager = new PlayerManager();
            mKingdomManager = new MKingdomManager();


            loadStatus = new LoadStatus();


            GameModel = new GameObject("GameModel");

            Debug.Log("Test Start");

            var sprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");
            //var sprite = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/chat_bubble2.png");
            var button = PowerButtons.CreateButton("exampleButton", sprite, "Example Button", "This is the example button", Vector2.zero, NCMS.Utils.ButtonType.Click, null, SpawnTest);
            PowerButtons.AddButtonToTab(button, NCMS.Utils.PowerTab.Main, new Vector2(332*2, -18));

            var tr = button.transform;
            for(int i =0;i<10;++i)
            {
                tr = tr.parent;
                Debug.Log($"name:{tr.name}");
            }


            Debug.Log("Test Over");

            webSocketToCCWSS = new WebSocketToCCWSS();
            webSocketToCCWSS.Connect("ws://127.0.0.1:8080");



            MKingdomHelper.InitEvent();
            MessageHandle.InitEvent();

        }

        

        public float tempTime = 0f;
        void Update()
        {
            webSocketToCCWSS.Update();
            loadStatus.Update();

            tempTime += Time.deltaTime;
            if(tempTime >= 1f)
            {
                tempTime -= 1f;
                //Debug.Log($"Config.timeScale {Config.timeScale}");
                //Debug.Log($"Config.paused {Config.paused}");
            }

            if(grenadeNum > 0)
            {
                --grenadeNum;
                var worldTile = MapBox.instance.GetTile(UnityEngine.Random.Range(0,MapBox.width),UnityEngine.Random.Range(0,MapBox.width));

                var drop = MapBox.instance.dropManager.spawn(worldTile, "napalmBomb", -1f, -1f);
                Reflection.SetField<bool>(drop, "soundOn", true);
            }
        }

        

        static void SpawnTest()
        {
            Camera.main.transform.position = new Vector3(){
                x=MapBox.width/2f,
                y=MapBox.height/2f
            };

            var moveCamera = Camera.main.GetComponent<MoveCamera>();
            Camera.main.orthographicSize = MapBox.width/2f;
            Debug.Log($"width:{MapBox.width},height:{MapBox.height} | {Camera.main.orthographicSize}");
            Reflection.SetField<float>(moveCamera, "targetZoom", Camera.main.orthographicSize);
            Reflection.SetField<float>(moveCamera, "focusZoom", Camera.main.orthographicSize);
            Reflection.SetField<float>(moveCamera, "focusUnitZoom", Camera.main.orthographicSize);
            Debug.Log($"ScrollWindow.currentWindows {ScrollWindow.currentWindows.Count}");
            foreach(var (k,v) in  ScrollWindow.allWindows)
            {
                Debug.Log($"Windows: {k}={v.screen_id}");
            }
            GameObjects.FindEvenInactive("BottomElements").SetActive(false);
            
            //PowerButtonSelector.instance.sizeButtMover.setVisible(false, false);
            //PowerButtonSelector.instance.cancelButtMover.setVisible(false, false);
            //PowerButtonSelector.instance.bottomElementsMover.setVisible(false, false);
            //ScrollWindow.moveAllToLeftAndRemove(true);
            //NCMS.Utils.Windows.AllWindows["aboutPowerBox"].show(null, "right", "right", false);
           //NCMS.Utils.Windows.AllWindows["aboutPowerBox"].setActive(true, "right", null, "right", true);
            //ScrollWindow.addCurrentWindow(NCMS.Utils.Windows.AllWindows["aboutPowerBox"]);
            //grenadeNum+=100;
            //WorldTip.hideNow();
		    //Tooltip.hideTooltip();
            //Reflection.SetField<bool>(drop, "ScrollWindow._isWindowActive", true);
        }

        static void exampleMethod(){
            Debug.Log("Test Button Start");

            Debug.Log("=============================================");
            Debug.Log($"dict_hidden.Count = {MapBox.instance.kingdoms.dict_hidden.Count}");

            Debug.Log($"list.Count = {MapBox.instance.kingdoms.list.Count}");
            Debug.Log($"list_civs.Count = {MapBox.instance.kingdoms.list_civs.Count}");
            Debug.Log($"list_hidden.Count = {MapBox.instance.kingdoms.list_hidden.Count}");
            Debug.Log("=============================================");
            foreach(var v in MapBox.instance.kingdoms.list_civs)
            {
                Debug.Log($"[{v.id}]{v.name}");
                v.name = v.id;
            }
            Debug.Log("=============================================");
            if(MapBox.instance.kingdoms.list_civs.Count > 0)
            {
                var WarInitiator = MapBox.instance.kingdoms.list_civs[0];
                for(int i = 1;i<MapBox.instance.kingdoms.list_civs.Count;++i)
                {
                    var kingdom = MapBox.instance.kingdoms.list_civs[i];
                    Debug.Log($"kingdom.id={WarInitiator.id}|target.id={kingdom.id}");
                    MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", WarInitiator, kingdom,false);
                }
            }

            Debug.Log("=============================================");
            Debug.Log("Test Button Over");

        }


        static void TestUI()
        {
            Debug.Log("TestUI Start");
            
            GameObject goImage = new GameObject();
            goImage.transform.SetParent(CanvasMain.instance.canvas_tooltip.transform);
            goImage.AddComponent<Image>().sprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");;
            goImage.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            goImage.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            goImage.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            goImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);


            goImage.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 200f);
            Debug.Log("TestUI Over");
        }

        static void TestMapName()
        {
            //MapBox.instance.saveManager.CallMethod("loadWorld","save2",false);
            //SaveManager.setCurrentSlot(2);
            //MapBox.instance.saveManager.startLoadSlot();
            //MapBox.instance.kingdoms.diplomacyManager.CallMethod("startWar", WarInitiator, kingdom,false);
            //return ;
            sprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");
            mapTextList = Reflection.GetField(MapNamesManager.instance.GetType(),MapNamesManager.instance,"list") as List<MapText>;
            Debug.Log("=============================================");
            Debug.Log($"list.Count = {mapTextList.Count}");
            for(int i = 0;i<mapTextList.Count;++i)
            {
               
                //GameObject goImage = new GameObject();
                //goImage.transform.SetParent(mapTextList[i].transform);
                 /*
                goImage.AddComponent<Image>().sprite = Sprites.LoadSprite($"{Mod.Info.Path}/icon.png");;
                goImage.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                goImage.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                goImage.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                goImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);


                goImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                */
                
                //UIBubble uiBubble = goImage.AddComponent<UIBubble>();
                //uiBubble.sprite = Sprites.LoadSprite($"{Mod.Info.Path}/GameResources/chat_bubble.png");
                //uiBubble.SetMessage("结盟 123455 你在干嘛");

                //mapTextList[i].capitalIcon.sprite = sprite;
            }
            Debug.Log("=============================================");

        }

        void LateUpdate()
        {
            //MKingdomManager.instance.LateUpdate();
            if(sprite)
            {
                for(int i = 0;i<mapTextList.Count;++i)
                {
                    if(!mapTextList[i].capitalIcon.sprite.Equals(sprite))
                    {
                        mapTextList[i].capitalIcon.sprite = sprite;
                    }
                }
            }
        }
    }
}