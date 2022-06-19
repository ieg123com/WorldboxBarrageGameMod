using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;
using UnityEngine;
using static Config;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections;

namespace PowerBox
{
    public class buttonActivation : MonoBehaviour
    {
        void Awake()
        {
            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(EditResoucesWindow.Edit_Resouces_Button_Click);
        }
    }

    class EditResoucesWindow : SelectableObjects
    {
        public EditResoucesWindow(Transform inspect_villageContent){
            var editResourcesWindow = NCMS.Utils.Windows.CreateNewWindow("editResources", "Edit Resources");
            //editResourcesWindow.titleText.text = "Edit Resources";


            var editResouces = Helper.GodPowerTab.createButton(
                "EditResouces",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.res_clear.png", 0, 0),
                inspect_villageContent,
                Edit_Resouces_Button_Click,
                "Edit resouces",
                "Edit village's resources");

            editResouces.AddComponent(typeof(buttonActivation));


            //editTraits.transform.localPosition = new Vector3(98f, -15f, editTraits.transform.localPosition.z);
            editResouces.transform.localPosition = new Vector3(117.50f, 49f, editResouces.transform.localPosition.z);
            editResouces.transform.Find("Icon").GetComponent<RectTransform>().sizeDelta = new Vector2(75f, 75f);
            var editResoucesRect = editResouces.GetComponent<RectTransform>();
            editResoucesRect.sizeDelta = new Vector2(100f, 100f);

            editResouces.GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".other.backgroundBackButtonRev.png", 0, 0);
            editResouces.GetComponent<Button>().transition = Selectable.Transition.None;
        }

        public static void Edit_Resouces_Button_Click()
        {
            var window = NCMS.Utils.Windows.GetWindow("editResources");
            window.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
            WorldBoxMod.editResourcesWindow.initEditResources(window, editResourseButtonCallBack);
            window.clickShow();
        }

        private void initEditResources(ScrollWindow window, Action<ButtonResource> callback)
        {
            var rt = spriteHighlighter.GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(60f, 60f);

            var Content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");

            var resourceArray = AssetManager.resources.dict.Values.ToList();
            resourceArray.Remove(resourceArray.Find(c => c.id == "honey"));


            if (Content.transform.childCount > 0)
            {
                for (int i = 0; i < Content.transform.childCount; i++)
                {
                    var resBtn = Content.transform.GetChild(i).gameObject.GetComponentInChildren<ButtonResource>();
                    var inpt = Content.transform.GetChild(i).gameObject.GetComponentInChildren<NameInput>();

                    var resBtnAsset = Reflection.GetField(resBtn.GetType(), resBtn, "asset") as ResourceAsset;

                    var data = Reflection.GetField(selectedCity.GetType(), selectedCity, "data") as CityData;
                    Reflection.CallMethod(resBtn, "load", resBtnAsset, data.storage.get(resBtnAsset.id));


                    var inputComponent = inpt.GetComponent<NameInput>();
                    inputComponent.setText(data.storage.get(resBtnAsset.id).ToString());

                    inputComponent.inputField.onEndEdit.AddListener(_param1 => CheckInput(inputComponent, resBtnAsset, data));
                }

                return;
            }

            var resourceButton = NCMS.Utils.GameObjects.FindEvenInactive("ResourceElement").GetComponent<ButtonResource>();

            startXPos = 40.4f;
            XStep = 99f;
            YStep = -20f;
            countInRow = 2;

            
            var rect = Content.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(0, Mathf.Abs(GetPosByIndex(resourceArray.Count).y) + 100);

            for (int i = 0; i < resourceArray.Count; i++)
            {
                var hl = AddHighLight(i, Content, false);

                loadResourceButton(resourceArray[i], 999, i, resourceArray.Count, resourceButton, hl.transform, callback);
            }

            ResetWrapVals();
        }

        private void loadResourceButton(ResourceAsset asset, int quantity, int pIndex, int pTotal, ButtonResource resourceButtonPref, Transform parent, Action<ButtonResource> callback)
        {
            ButtonResource resourceButton = GameObject.Instantiate<ButtonResource>(resourceButtonPref, parent);

            var data = Reflection.GetField(selectedCity.GetType(), selectedCity, "data") as CityData;
            Reflection.CallMethod(resourceButton, "load", asset, data.storage.get(asset.id));
            resourceButton.transform.Find("Text").gameObject.SetActive(false);

            resourceButton.StartCoroutine(InitResourceButtonEvents(resourceButton, asset, data.storage.get(asset.id), callback));
            resourceButton.transform.localPosition = Vector3.zero;

            var nameInputElement = GameObject.Instantiate(GameObject.Find("NameInputElement"), parent);
            //nameInputElement.GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".other.windowNameEdit.png", 0, 0);
            nameInputElement.transform.localPosition = new Vector2(50f, 0);
            nameInputElement.transform.localScale = new Vector2(0.75f, 0.75f);
            var inputComponent = nameInputElement.GetComponent<NameInput>();
            inputComponent.setText(data.storage.get(asset.id).ToString());

            inputComponent.inputField.onEndEdit.AddListener(_param1 => CheckInput(inputComponent, asset, data));
        }

        public IEnumerator InitResourceButtonEvents(ButtonResource resButton, ResourceAsset asset, int quantity, Action<ButtonResource> callback)
        {
            yield return new WaitForSeconds(1);



            var eventTrigger = resButton.GetComponents<EventTrigger>();

            foreach (var trigger in eventTrigger)
            {
                GameObject.Destroy(trigger);
            }

            var button = resButton.gameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(() => { callback(resButton); showTooltip(asset, quantity, resButton.gameObject); });

            button.OnHover(new UnityAction(() => showTooltip(asset, quantity, resButton.gameObject)));
            button.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
        }

        private void showTooltip(ResourceAsset asset, int quantity, GameObject resourceButton)
        {
            if (!tooltipsActive)
                return;

            Tooltip.info_resource = asset;
            Tooltip.instance.show(resourceButton.gameObject, "normal", Tooltip.info_resource.id);


            resourceButton.transform.localScale = new Vector3(1f, 1f, 1f);
            resourceButton.transform.DOKill(false);
            resourceButton.transform.DOScale(0.8f, 0.1f).SetEase<Tweener>(Ease.InBack);
        }

        private void CheckInput(NameInput text, ResourceAsset asset, CityData cityData)
        {
            int number;
            if(int.TryParse(text.textField.text, out number))
            {
                if(number >= 0 && number < 10000)
                {
                    cityData.storage.set(asset.id, number);
                    return;
                }
            }
            
            text.setText(cityData.storage.get(asset.id).ToString());
        }

        private static void editResourseButtonCallBack(ButtonResource buttonPressed)
        {
            var resource = Reflection.GetField(buttonPressed.GetType(), buttonPressed, "asset") as ResourceAsset;
        }
    }
}
