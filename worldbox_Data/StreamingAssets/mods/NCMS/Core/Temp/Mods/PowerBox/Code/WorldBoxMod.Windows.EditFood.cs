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

namespace PowerBox
{
    public class EditFoodWindow : SelectableObjects
    {
        private static GameObject content;
        public EditFoodWindow(Transform inspect_unitContent) : base()
        {
            var editFoodWindow = NCMS.Utils.Windows.CreateNewWindow("editFood", "Select Preferred Food");

            var viewport = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{editFoodWindow.name}/Background/Scroll View/Viewport");
            var viewportRect = viewport.GetComponent<RectTransform>();
            viewportRect.sizeDelta = new Vector2(0, 17);
//HungerBar/
            var button = inspect_unitContent.transform.Find("HungerBar").gameObject.GetComponent<Button>();
            button.onClick.AddListener(Edit_Food_Button_Click);
        }

        public void Edit_Food_Button_Click()
        {
            var window = NCMS.Utils.Windows.GetWindow("editFood");
            window.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
            initEditFood(window, editFoodsButtonCallBack);
            window.clickShow();
        }

        private void initEditFood(ScrollWindow window, Action<ResourceAsset, GameObject> callback)
        {
            var rt = this.spriteHighlighter.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(60f, 60f);

            content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");
            for (int i = 0; i < content.transform.childCount; i++)
            {
                GameObject.Destroy(content.transform.GetChild(i).gameObject);
            }


            //var traitButton = NCMS.Utils.GameObjects.FindEvenInactive("TraitButton").GetComponent<TraitButton>();

            var foodResources = AssetManager.resources.list.Where(x => x.type == ResType.Food).ToList();

            var rect = content.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(0, Mathf.Abs(GetPosByIndex(foodResources.Count).y) + 100);

            var actorData = Reflection.GetField(typeof(Actor), WorldBoxMod.UNIT, "data") as ActorStatus;
            var currentFavoriteFood = actorData.favoriteFood;

            for (int i = 0; i < foodResources.Count; i++)
            {
                var hl = AddHighLight(i, content, currentFavoriteFood == foodResources[i].id);

                loadFoodButton(foodResources[i], i, foodResources.Count, hl.transform, callback);
            }
        }

        private void loadFoodButton(ResourceAsset asset, int pIndex, int pTotal, Transform parent, Action<ResourceAsset, GameObject> callback)
        {
            var foodButtonObj = new GameObject("food", typeof(Image));
            foodButtonObj.transform.SetParent(parent);
            foodButtonObj.transform.localPosition = new Vector2(0, 0);
            var button = foodButtonObj.AddComponent<Button>();

            var foodSprite = Resources.Load<Sprite>($"ui/icons/{asset.path_icon}");
            foodButtonObj.GetComponent<Image>().sprite = foodSprite;


            //traitButton.transform.localPosition = Vector3.zero;


            button.onClick.AddListener(() => callback(asset, foodButtonObj));
        }

        private void editFoodsButtonCallBack(ResourceAsset asset, GameObject button)
        {
            var actorData = Reflection.GetField(typeof(Actor), WorldBoxMod.UNIT, "data") as ActorStatus;
            actorData.favoriteFood = asset.id;

            UnhilightAll(content);
            HighlightButton(true, button.transform.parent.gameObject);
        }
    }
}
