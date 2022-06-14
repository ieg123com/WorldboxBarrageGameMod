using HarmonyLib;
using ReflectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Config;

namespace Helper
{
    partial class GodPowerTab
    {
        public static GameObject additionalPowersTab;
        public static PowersTab powersTabComponent;
        public static void init()
        {
            if (gameLoaded)
            {
                var OtherTabButton = NCMS.Utils.GameObjects.FindEvenInactive("Button_Other");
                if (OtherTabButton != null)
                {
                    NCMS.Utils.Localization.addLocalization("Button_Additional_Powers", "PowerBox");
                    NCMS.Utils.Localization.addLocalization("tab_additional", "PowerBox");


                    var newTabButton = GameObject.Instantiate(OtherTabButton);
                    newTabButton.transform.SetParent(OtherTabButton.transform.parent);
                    var buttonComponent = newTabButton.GetComponent<Button>();


                    newTabButton.transform.localPosition = new Vector3(150f, 49.62f);
                    newTabButton.transform.localScale = new Vector3(1f, 1f);
                    newTabButton.name = "Button_Additional_Powers";

                    // Assembly sas = Assembly.GetExecutingAssembly();
                    // foreach (var item in sas.GetManifestResourceNames())
                    // {
                    //     Debug.Log(item);
                    // }

                    var spriteForTab = Mod.EmbededResources.LoadSprite(PowerBox.WorldBoxMod.resources + ".powers.tabIcon.png");
                    newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = spriteForTab;
                    //newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = Helper.Utils.LoadSprite(PowerBox.WorldBoxMod.resources + ".powers.tabIcon.png", 0, 0);




                    var OtherTab = NCMS.Utils.GameObjects.FindEvenInactive("Tab_Other");
                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(false);
                    }

                    additionalPowersTab = GameObject.Instantiate(OtherTab);

                    foreach (Transform child in additionalPowersTab.transform)
                    {
                        if (child.gameObject.name == "tabBackButton" || child.gameObject.name == "-space")
                        {
                            child.gameObject.SetActive(true);
                            continue;
                        }

                        GameObject.Destroy(child.gameObject);
                    }

                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(true);
                    }


                    additionalPowersTab.transform.SetParent(OtherTab.transform.parent);
                    powersTabComponent = additionalPowersTab.GetComponent<PowersTab>();
                    powersTabComponent.powerButton = buttonComponent;


                    additionalPowersTab.name = "Tab_Additional_Powers";
                    powersTabComponent.powerButton.onClick = new Button.ButtonClickedEvent();
                    powersTabComponent.powerButton.onClick.AddListener(Button_Additional_Powers_Click);
                    Reflection.SetField<GameObject>(powersTabComponent, "parentObj", OtherTab.transform.parent.parent.gameObject);
                    powersTabComponent.tipKey = "tab_additional";

                    additionalPowersTab.SetActive(true);



                }
            }
        }

        private static int createdButtons = 0;
        private static bool lined = false;
        private static float startX = 72f;
        private static float plusX = 18f;
        private static float evenY = 18f;
        private static float oddY = -18f;
        private static float lineStep = 23.4f;

        public static GameObject createButton(string name, Sprite sprite, Transform parent, UnityAction call, string localName, string localDescription, GameObject instantinateFrom = null)
        {
            ToInstantinatePowerButton = name;

            NCMS.Utils.Localization.addLocalization(name, localName);
            NCMS.Utils.Localization.addLocalization(name + " Description", localDescription);


            GameObject newButton;
            if (instantinateFrom != null)
            {
                instantinateFrom.SetActive(false);
                newButton = GameObject.Instantiate(instantinateFrom, parent);
                instantinateFrom.SetActive(true);
            }
            else
            {
                var tempObject = NCMS.Utils.GameObjects.FindEvenInactive("WorldLaws");
                tempObject.SetActive(false);
                newButton = GameObject.Instantiate(tempObject);
                tempObject.SetActive(true);
            }

            ButtonsToRename.Add(newButton.GetInstanceID(), name);
            newButton.SetActive(true);

            newButton.transform.SetParent(parent);

            var image = newButton.transform.Find("Icon").GetComponent<Image>();
            image.sprite = sprite;

            float x = startX + (createdButtons != 0 ? (plusX *
                    (createdButtons % 2 == 0 ? createdButtons : createdButtons - 1))
                : 0);
            float y = (createdButtons % 2 == 0 ? evenY : oddY);


            newButton.transform.localPosition = new Vector3(x, y);


            var powerButtonComponent = newButton.GetComponent<PowerButton>();
            powerButtonComponent.open_window_id = string.Empty;
            powerButtonComponent.type = PowerButtonType.Active;

            var newButtonButton = newButton.GetComponent<Button>();


            if (call != null)
            {
                //powerButtonComponent.type = PowerButtonType.Library;
                newButtonButton.onClick = new Button.ButtonClickedEvent();
                newButtonButton.onClick.AddListener(call);
            }

            if(!AssetManager.powers.dict.ContainsKey(name)){
                powerButtonComponent.type = PowerButtonType.Library;
            }

            powersTabComponent.CallMethod("setNewWidth");

            createdButtons++;
            lined = false;
            return newButton;
        }

        public static void AddLine()
        {
            if (!lined)
            {
                var line = CanvasMain.instance.canvas_ui.transform.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Main/LINE");



                var newLine = GameObject.Instantiate(line, additionalPowersTab.transform);
                var countBtns = (createdButtons%2 != 0 ? createdButtons - 1 : createdButtons - 2);

                //var x = startX + (countBtns != 0 ? (plusX *
                //        (countBtns % 2 == 0 ? countBtns : countBtns - 1))
                //    : 0);

                float x = startX + lineStep + (countBtns != 0 ? (plusX * countBtns) : 0);
                newLine.transform.localPosition = new Vector2(x, newLine.transform.localPosition.y);

                startX += (lineStep - plusX) * 2;

                if (createdButtons%2 != 0)
                {
                    createdButtons++;
                }

                lined = true;
            }
        }

        public static Dictionary<string, bool> CustomWorldLaws = new Dictionary<string, bool>();

        private static Dictionary<int, string> ButtonsToRename = new Dictionary<int, string>();
        public static GameObject createWorldLaw(string name, Sprite sprite, Transform parent, UnityAction<WorldLawElement> call, string localName, string localDescription, GameObject instantinateFrom = null)
        {
            if (!CustomWorldLaws.ContainsKey(name)) CustomWorldLaws.Add(name, false);

            ToInstantinatePowerButton = name;

            NCMS.Utils.Localization.addLocalization(name + "_title", localName);
            NCMS.Utils.Localization.addLocalization(name + "_description", localDescription);


            GameObject newButton;
            if (instantinateFrom != null)
            {
                instantinateFrom.SetActive(false);
                newButton = GameObject.Instantiate(instantinateFrom, parent);
                instantinateFrom.SetActive(true);
            }
            else
            {
                var tempObject = NCMS.Utils.GameObjects.FindEvenInactive("WorldLaws");
                tempObject.SetActive(false);
                newButton = GameObject.Instantiate(tempObject);
                tempObject.SetActive(true);
            }
            
            ButtonsToRename.Add(newButton.GetInstanceID(), name);

            newButton.SetActive(true);

            newButton.transform.SetParent(parent);

            var image = newButton.transform.Find("Button").Find("LawIcon").GetComponent<Image>();
            image.sprite = sprite;


            newButton.transform.localPosition = Vector2.zero;

            var newButtonButton = newButton.transform.Find("Button").GetComponent<Button>();

            var worldLawEl = newButton.GetComponent<WorldLawElement>();
            if (call != null)
            {
                newButtonButton.onClick = new Button.ButtonClickedEvent();
                newButtonButton.onClick.AddListener(() => call(worldLawEl));
            }


            updateIcon(worldLawEl);

            return newButton;
        }
        public static void updateIcon(WorldLawElement elem)
        {
            var id = Reflection.GetField(elem.GetType(), elem, "lawID") as string;

            elem.toggleIcon.CallMethod("updateIcon", Helper.GodPowerTab.CustomWorldLaws[id]);

            if (Helper.GodPowerTab.CustomWorldLaws[id])
            {
                elem.icon.color = (Color)Reflection.GetField(elem.GetType(), elem, "colorNormal");
            }
            else
            {
                elem.icon.color = (Color)Reflection.GetField(elem.GetType(), elem, "colorDisabled");
            }
        }

        public static void Button_Additional_Powers_Click()
        {
            var AdditionalTab = NCMS.Utils.GameObjects.FindEvenInactive("Tab_Additional_Powers");
            var AdditionalPowersTab = AdditionalTab.GetComponent<PowersTab>();

            AdditionalPowersTab.showTab(AdditionalPowersTab.powerButton);
        }


        public static void patch(Harmony harmony)
        {
            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(PowerButton), "init"), AccessTools.Method(typeof(GodPowerTab), "init_Prefix"));
            Debug.Log("Pre patch: PowerButton.init");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(WorldLawElement), "Awake"), AccessTools.Method(typeof(GodPowerTab), "WorldLawElement_Awake_Prefix"));
            Debug.Log("Prefix: WorldLawElement.Awake");

            Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(WorldLawElement), "updateStatus"), AccessTools.Method(typeof(GodPowerTab), "updateStatus_Prefix"));
            Debug.Log("Prefix: WorldLawElement.updateStatus");
        }

        internal static string ToInstantinatePowerButton = "";

        public static void init_Prefix(PowerButton __instance, RectTransform ___rectTransform, Image ___image, Button ___button, GodPower ___godPower)
        {
            var key = __instance.gameObject.GetInstanceID();
            if (ButtonsToRename.ContainsKey(key))
            {
                __instance.name = ButtonsToRename[key];
            }
            
            // if (__instance.name == "Button_Other(Clone)") __instance.name = "Button_Additional_Powers";


            // if (new string[] { "crab(Clone)", "WorldLaws(Clone)", "WorldLaws(Clone)", "DebugButton(Clone)", "world_law_diplomacy(Clone)" }.Contains(__instance.name))
            // {
            //     __instance.name = ToInstantinatePowerButton;
            // }

            // //if (__instance.name == "crab(Clone)") __instance.name = ToInstantinatePowerButton;

            // //if (__instance.name == "WorldLaws(Clone)")  __instance.name = ToInstantinatePowerButton;

            // //if (__instance.name == "WorldLaws(Clone)") __instance.name = ToInstantinatePowerButton;

            return;
        }

        public static bool setNewWidth_Prefix(PowersTab __instance)
        {
            if (__instance.name == "Tab_Additional_Powers") return false;


            return true;
        }

        public static void WorldLawElement_Awake_Prefix(WorldLawElement __instance)
        {
            if (__instance.name == "world_law_diplomacy(Clone)") __instance.name = Helper.GodPowerTab.ToInstantinatePowerButton;

            return;
        }

        public static bool updateStatus_Prefix(WorldLawElement __instance, string ___lawID)
        {
            if (CustomWorldLaws.ContainsKey(___lawID))
            {
                __instance.toggleIcon.CallMethod("updateIcon", CustomWorldLaws[___lawID]);

                return false;
            }

            return true;
        }
    }
}
