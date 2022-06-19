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
// using DG.Tweening;
// using UnityEngine.EventSystems;
// using System.Collections;
// namespace PowerBox
// {
//     public class EditBannerWindow : SelectableObjects
//     {
//         public EditBannerWindow(Transform inspect_kingdomBackground)
//         {
//             var editBannerWindow = NCMS.Utils.Windows.CreateNewWindow("editBanner", "Edit Banner");
//             //editBannerWindow.titleText.text = "Edit Banner";


//             var editBanner = Helper.GodPowerTab.createButton(
//                 "EditBanner",
//                 Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.flags.png", 0, 0),
//                 inspect_kingdomBackground,
//                 Edit_Banner_Button_Click,
//                 "Edit banner",
//                 "Edit kingdom's banner");


//             //editTraits.transform.localPosition = new Vector3(98f, -15f, editTraits.transform.localPosition.z);
//             editBanner.transform.localPosition = new Vector3(116, 65f, editBanner.transform.localPosition.z);
//             editBanner.transform.Find("Icon").GetComponent<RectTransform>().sizeDelta = new Vector2(70f, 70f);
//             var editBannerRect = editBanner.GetComponent<RectTransform>();
//             editBannerRect.sizeDelta = new Vector2(100f, 100f);


//             var culturesButtonKingdomInspect = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/kingdom/ButtonCulturesBackground");
//             culturesButtonKingdomInspect.transform.localPosition = new Vector3(116.9f, 95, 0);

//             editBanner.GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".other.backgroundBackButtonRev.png", 0, 0);
//             editBanner.GetComponent<Button>().transition = Selectable.Transition.None;


//             var bg = editBannerWindow.transform.Find("Background");

//             changeBannerBtn = Helper.GodPowerTab.createButton(
//                 "changeBannerBtn",
//                 Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.banner_flags.png", 0, 0),
//                 bg,
//                 () => { 
//                     Change_Banner_Edit();
//                     initEditBanner(editBannerWindow); 
//                 },
//                 "Icons",
//                 "");

//             changeBannerBtn.transform.localPosition = new Vector2(30.00f, -100.00f);
//         }

//         static GameObject changeBannerBtn;
//         internal void Change_Banner_Edit()
//         {
//             if (bannerChooseBg)
//             {
//                 bannerChooseBg = false;

//                 changeBannerBtn.transform.Find("Icon").GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.background_flags.png", 0, 0);
//                 NCMS.Utils.Localization.setLocalization("changeBannerBtn", "Backgrounds");
//                 //Helper.Localization.setLocalization("changeBannerBtn", "Backgrounds");
//             }
//             else
//             {
//                 bannerChooseBg = true;
//                 changeBannerBtn.transform.Find("Icon").GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.banner_flags.png", 0, 0);
//                 NCMS.Utils.Localization.setLocalization("changeBannerBtn", "Icons");
//                 //Helper.Localization.setLocalization("changeBannerBtn", "Icons");
//             }
//         }

//         internal void Edit_Banner_Button_Click()
//         {
//             var banner = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/kingdom/Background/Scroll View/Viewport/Content/BannerBackground/Banner");

//             var currentBG = banner.transform.Find("Background").GetComponent<Image>();
//             var currentIcon = banner.transform.Find("Icon").GetComponent<Image>();

//             if (!customBanners.ContainsKey(selectedKingdom.id))
//             {
//                 customBanners.Add(selectedKingdom.id, new customBanner(currentBG, currentIcon));
//             }

//             var window = NCMS.Utils.Windows.GetWindow("editBanner");
//             window.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
//             initEditBanner(window);
//             window.clickShow();
//         }

//         internal static Dictionary<string, customBanner> customBanners = new Dictionary<string, customBanner>();


//         static bool bannerChooseBg = true;
//         internal void initEditBanner(ScrollWindow window)
//         {
//             var Content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");

//             var backgrounds = new List<Sprite>();
//             var icons = new List<Sprite>();

//             for (int i = 0; i < BannerGenerator.list.Count; i++)
//             {
//                 backgrounds.AddRange(BannerGenerator.list[i].backrounds);
//                 icons.AddRange(BannerGenerator.list[i].icons);
//             }

//             for (int i = 0; i < Content.transform.childCount; i++)
//             {
//                 GameObject.Destroy(Content.transform.GetChild(i).gameObject);
//             }


//             startXPos = 47f;//55
//             XStep = 27.75f;//37
//             YStep = -27.75f;
//             countInRow = 7;

//             for (int i = 0; i < backgrounds.Count; i++)
//             {
//                 var highlighted = bannerChooseBg ? backgrounds[i] == customBanners[selectedKingdom.id].CurrentBG.sprite : icons[i] == customBanners[selectedKingdom.id].CurrentIcon.sprite;

//                 var hl = AddHighLight(i, Content, highlighted);

//                 hl.transform.localScale = new Vector2(0.75f, 0.75f);
//                 hl.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

//                 loadBanner(i, hl.transform, backgrounds, icons, window);
//             }

//             ResetWrapVals();
//         }



//         private void loadBanner(int index, Transform parent, List<Sprite> backgrounds, List<Sprite> icons, ScrollWindow window)
//         {

//             var kingdomColor = Reflection.GetField(selectedKingdom.GetType(), selectedKingdom, "kingdomColor") as KingdomColor;

//             var banerObj = new GameObject("banner", typeof(Image));
//             banerObj.transform.SetParent(parent);
//             banerObj.transform.localPosition = new Vector2(0, -5.00f);


//             var iconInObj = new GameObject("banner", typeof(Image));
//             iconInObj.transform.SetParent(banerObj.transform);
//             iconInObj.transform.localPosition = Vector2.zero;

//             if (bannerChooseBg)
//             {
//                 banerObj.GetComponent<Image>().sprite = backgrounds[index];
//                 banerObj.GetComponent<Image>().color = kingdomColor.colorBorderOut;

//                 iconInObj.GetComponent<Image>().sprite = customBanners[selectedKingdom.id].CurrentIcon.sprite;
//                 iconInObj.GetComponent<Image>().color = kingdomColor.colorBorderBannerIcon;
//             }
//             else
//             {
//                 banerObj.GetComponent<Image>().sprite = customBanners[selectedKingdom.id].CurrentBG.sprite;
//                 banerObj.GetComponent<Image>().color = kingdomColor.colorBorderOut;

//                 iconInObj.GetComponent<Image>().sprite = icons[index];
//                 iconInObj.GetComponent<Image>().color = kingdomColor.colorBorderBannerIcon;
//             }

//             var button = banerObj.AddComponent<Button>();

//             button.onClick.AddListener(() => {
//                 customBanners[selectedKingdom.id].CurrentBG = banerObj.GetComponent<Image>();
//                 customBanners[selectedKingdom.id].CurrentIcon = iconInObj.GetComponent<Image>();
//                 initEditBanner(window);
//             });
//         }
//     }

    
//     class customBanner
//     {
//         public Image CurrentBG;
//         public Image CurrentIcon;

//         public customBanner(Image currentBG, Image currentIcon)
//         {
//             CurrentBG = currentBG;
//             CurrentIcon = currentIcon;
//         }
//     }
// }
