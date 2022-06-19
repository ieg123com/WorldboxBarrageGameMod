// using HarmonyLib;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using ReflectionUtility;
// using UnityEngine;
// using static Config;
// using UnityEngine.UI;
// using UnityEngine.Events;

// namespace PowerBox
// {
//     partial class AddRemoveTraitsWindow : SelectableObjects
//     {
//         public AddRemoveTraitsWindow() : base()
//         {
//             var addRemoveTraitsWindow = NCMS.Utils.Windows.CreateNewWindow("addRemoveTraitsWindow", "Add/Remove Traits");

            
//             var viewport = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{addRemoveTraitsWindow.name}/Background/Scroll View/Viewport");
//             var viewportRect = viewport.GetComponent<RectTransform>();
//             viewportRect.sizeDelta = new Vector2(0, 17);
//             //addRemoveTraitsWindow.titleText.text = "Add/Remove Traits";

//             var bg = addRemoveTraitsWindow.transform.Find("Background");

//             var saveButton = Helper.GodPowerTab.createButton(
//                 "Done",
//                 Resources.Load<Sprite>("ui/icons/iconsavelocal"),
//                 bg.transform,
//                 Save_Button_Click,
//                 "Save selected traits",
//                 "");

//             saveButton.transform.localPosition = new Vector2(70.00f, -90.00f);
//         }

//         internal static List<string> SelectedToAdd = new List<string>();
//         internal static List<string> SelectedToRemove = new List<string>();
//         internal void initAddRemoveTraits(ScrollWindow window, PowerType type)
//         {
//             SelectableObjects.TType = type;


//             window.titleText.text = SelectableObjects.TType == PowerType.add ? "Selecting traits to add" : "Selecting traits to remove";

//             var rt = this.spriteHighlighter.GetComponent<RectTransform>();
//             rt.sizeDelta = new Vector2(60f, 60f);

//             var Content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");
//             for (int i = 0; i < Content.transform.childCount; i++)
//             {
//                 GameObject.Destroy(Content.transform.GetChild(i).gameObject);
//             }

//             var traitButton = NCMS.Utils.GameObjects.FindEvenInactive("TraitButton").GetComponent<TraitButton>();

//             var traitsArray = AssetManager.traits.dict.Values.ToList();

            
//             var rect = Content.GetComponent<RectTransform>();
//             rect.pivot = new Vector2(0, 1);
//             rect.sizeDelta = new Vector2(0, Mathf.Abs(GetPosByIndex(traitsArray.Count).y) + 100);

//             for (int i = 0; i < traitsArray.Count; i++)
//             {
//                 var hl = AddHighLight(i, Content, SelectableObjects.TType == PowerType.add ? SelectedToAdd.Contains(traitsArray[i].id) : SelectedToRemove.Contains(traitsArray[i].id));

//                 loadAddRemoveButton(traitsArray[i].id, i, traitsArray.Count, traitButton, hl.transform, addRemoveButtonCallBack);
//             }


//             window.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
//         }

//         private void loadAddRemoveButton(string pID, int pIndex, int pTotal, TraitButton traitButtonPref, Transform parent, Action<TraitButton> callback)
//         {
//             TraitButton traitButton = GameObject.Instantiate<TraitButton>(traitButtonPref, parent);
//             Reflection.CallMethod(traitButton, "load", pID);


//             traitButton.transform.localPosition = Vector3.zero;


//             var button = traitButton.gameObject.GetComponent<Button>();
//             button.onClick.AddListener(() => callback(traitButton));
//         }



//         private void addRemoveButtonCallBack(TraitButton buttonPressed)
//         {
//             var trait = (ActorTrait)Reflection.GetField(buttonPressed.GetType(), buttonPressed, "trait");

//             if (SelectableObjects.TType == PowerType.add ? SelectedToAdd.Contains(trait.id) : SelectedToRemove.Contains(trait.id))
//             {
//                 removeSelected(trait.id);
//                 HighlightTrait(false, buttonPressed.transform.parent.gameObject);
//             }
//             else
//             {
//                 addSelected(trait.id);
//                 HighlightTrait(true, buttonPressed.transform.parent.gameObject);
//             }
//         }

//         private void removeSelected(string id)
//         {
//             if(SelectableObjects.TType == PowerType.add)
//             {
//                 SelectedToAdd.Remove(id);
//             }
//             else
//             {
//                 SelectedToRemove.Remove(id);
//             }
//         }

//         private void addSelected(string id)
//         {
//             if (SelectableObjects.TType == PowerType.add)
//             {
//                 SelectedToAdd.Add(id);
//             }
//             else
//             {
//                 SelectedToRemove.Add(id);
//             }
//         }

//         public void Save_Button_Click()
//         {
//             var addRemoveTraitsWindow = NCMS.Utils.Windows.GetWindow("addRemoveTraitsWindow");
//             addRemoveTraitsWindow.clickHide();

//             if((SelectableObjects.TType == PowerType.add && SelectedToAdd.Count > 0) || (SelectableObjects.TType == PowerType.remove && SelectedToRemove.Count > 0))
//             {
//                 var pbsInstance = Reflection.GetField(typeof(PowerButtonSelector), null, "instance") as PowerButtonSelector;
//                 var pButton = NCMS.Utils.GameObjects.FindEvenInactive(SelectableObjects.TType == PowerType.add ? "addTraits" : "removeTraits");
//                 pbsInstance.clickPowerButton(pButton.GetComponent<PowerButton>());
//             }
//         }
//     }
// }
