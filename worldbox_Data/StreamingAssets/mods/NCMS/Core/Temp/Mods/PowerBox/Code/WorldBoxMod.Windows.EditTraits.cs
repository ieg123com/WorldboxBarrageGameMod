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
//     public class EditTraitsWindow : SelectableObjects
//     {
//         public EditTraitsWindow(Transform inspect_unitContent) : base()
//         {
//             var editTraitsWindow = NCMS.Utils.Windows.CreateNewWindow("editTraits", "Edit Traits");
//             //editTraitsWindow.titleText.text = "Edit Traits";


//             var editTraits = Helper.GodPowerTab.createButton(
//                 "EditTraits",
//                 Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.traits_clear.png", 0, 0),
//                 inspect_unitContent,
//                 Edit_Traits_Button_Click,
//                 "Edit traits",
//                 "Edit unit's traits");

//             var viewport = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{editTraitsWindow.name}/Background/Scroll View/Viewport");
//             var viewportRect = viewport.GetComponent<RectTransform>();
//             viewportRect.sizeDelta = new Vector2(0, 17);

//             //editTraits.transform.localPosition = new Vector3(98f, -15f, editTraits.transform.localPosition.z);
//             editTraits.transform.localPosition = new Vector3(245.50f, -15f, editTraits.transform.localPosition.z);
//             editTraits.transform.Find("Icon").GetComponent<RectTransform>().sizeDelta = new Vector2(65f, 65f);
//             var editTraitsRect = editTraits.GetComponent<RectTransform>();
//             editTraitsRect.sizeDelta = new Vector2(100f, 100f);

//             var culturesButtonCreatureInspect = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/inspect_unit/Background/ButtonCulturesContainer");
//             culturesButtonCreatureInspect.transform.localPosition = new Vector2(5000f, 5000f);
//             //culturesButtonCreatureInspect.gameObject.SetActive(false);
//             //GameObject.Destroy(culturesButtonCreatureInspect);

//             editTraits.GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".other.backgroundBackButtonRev.png", 0, 0);
//             editTraits.GetComponent<Button>().transition = Selectable.Transition.None;
//         }

//         public void Edit_Traits_Button_Click()
//         {
//             var window = NCMS.Utils.Windows.GetWindow("editTraits");
//             window.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
//             initEditTraits(window, editTraitsButtonCallBack);
//             window.clickShow();
//         }

//         private void initEditTraits(ScrollWindow window, Action<TraitButton> callback)
//         {
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
//                 var hl = AddHighLight(i, Content, WorldBoxMod.UNIT.haveTrait(traitsArray[i].id));

//                 loadTraitButton(traitsArray[i].id, i, traitsArray.Count, traitButton, hl.transform, callback);
//             }
//         }

//         private void loadTraitButton(string pID, int pIndex, int pTotal, TraitButton traitButtonPref, Transform parent, Action<TraitButton> callback)
//         {
//             TraitButton traitButton = GameObject.Instantiate<TraitButton>(traitButtonPref, parent);
//             Reflection.CallMethod(traitButton, "load", pID);


//             traitButton.transform.localPosition = Vector3.zero;


//             var button = traitButton.gameObject.GetComponent<Button>();
//             button.onClick.AddListener(() => callback(traitButton));
//         }

//         private void editTraitsButtonCallBack(TraitButton buttonPressed)
//         {
//             var trait = (ActorTrait)Reflection.GetField(buttonPressed.GetType(), buttonPressed, "trait");

//             if (WorldBoxMod.UNIT.haveTrait(trait.id))
//             {
//                 WorldBoxMod.UNIT.removeTrait(trait.id);
//             }
//             else
//             {
//                 WorldBoxMod.UNIT.addTrait(trait.id);
//             }

//             HighlightTrait(WorldBoxMod.data.traits.Contains(trait.id), buttonPressed.transform.parent.gameObject);
//         }
//     }
// }
