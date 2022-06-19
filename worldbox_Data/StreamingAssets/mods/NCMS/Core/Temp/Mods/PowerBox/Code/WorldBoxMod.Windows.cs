using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PowerBox
{
    partial class WorldBoxMod
    {
        //internal static AddRemoveTraitsWindow addRemoveTraitsWindow;
        internal static EditItemsWindow editItemsWindow;
        internal static EditResoucesWindow editResourcesWindow;
        internal static PowerBoxLawsWindow powerBoxLawsWindow;

        private void initWindows()
        {
            #region aboutPowerBoxWindow
            //var aboutPowerBoxWindow = Helper.Windows.createNewWindow("aboutPowerBox");
            var aboutPowerBoxWindow = NCMS.Utils.Windows.CreateNewWindow("aboutPowerBox", "PowerBox!");
            //aboutPowerBoxWindow.titleText.text = "PowerBox!";
            aboutPowerBoxWindow.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);
            var aboutPowerBoxContent = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/aboutPowerBox/Background/Scroll View/Viewport/Content");
            //scrollReact.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;

            #region var description

            var descriptionBytes = Mod.EmbededResources.GetBytes($"{Mod.Info.Name}.Resources.description.txt");
            var description = System.Text.Encoding.Default.GetString(descriptionBytes);
            
            #endregion

            var name = aboutPowerBoxWindow.transform.Find("Background").Find("Name").gameObject;
            //name.GetComponent<RectTransform>().sizeDelta = new Vector2(180, 300);

            var nameText = name.GetComponent<Text>();
            nameText.text = description;
            nameText.color = new Color(0, 0.74f, 0.55f, 1);
            nameText.fontSize = 7;
            nameText.alignment = TextAnchor.UpperLeft;
            nameText.supportRichText = true;
            name.transform.SetParent(aboutPowerBoxWindow.transform.Find("Background").Find("Scroll View").Find("Viewport").Find("Content"));


            name.SetActive(true);

            var nameRect = name.GetComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0.5f, 1);
            nameRect.anchorMax = new Vector2(0.5f, 1);
            nameRect.offsetMin = new Vector2(-90f, nameText.preferredHeight * -1);
            nameRect.offsetMax = new Vector2(90f, -17);
            nameRect.sizeDelta = new Vector2(180, nameText.preferredHeight + 50);
            aboutPowerBoxContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, nameText.preferredHeight + 50);

            name.transform.localPosition = new Vector2(name.transform.localPosition.x, ((nameText.preferredHeight / 2) + 30) * -1);

            #endregion


            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "inspect_unit");
            var inspect_unit = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/inspect_unit");
            var inspect_unitContent = inspect_unit.transform.Find("/Canvas Container Main/Canvas - Windows/windows/inspect_unit/Background/Scroll View/Viewport/Content");
            inspect_unit.SetActive(false);

            #region EditItemsWindow
            editItemsWindow = new EditItemsWindow(inspect_unitContent);
            // //initEditItemsWindow(inspect_unitContent);

            #endregion


            #region EditResoucesWindow

            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "village");
            var inspect_village = NCMS.Utils.GameObjects.FindEvenInactive("village");
            inspect_village.SetActive(false);
            //var inspect_village = Helper.Utils.FindEvenInactive("village");
            Debug.Log(inspect_village);
            var inspect_villageBackground = inspect_village.transform.Find("Background");
            
            editResourcesWindow = new EditResoucesWindow(inspect_villageBackground);
            //initEditResoucesWindow(inspect_villageBackground);

            #endregion


            #region EditBannerWindow

            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "kingdom");
            var inspect_kingdom = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/kingdom");
            var inspect_kingdomBackground = inspect_unit.transform.Find("/Canvas Container Main/Canvas - Windows/windows/kingdom/Background");

            inspect_kingdom.SetActive(false);


            //var editBannerWindow = new EditBannerWindow(inspect_kingdomBackground);
            //initEditBannerWindow(inspect_kingdomBackground);

            #endregion

            #region EditFoodWindow
            var editFoodWindow = new EditFoodWindow(inspect_unitContent);
            //editFoodWindow.initEditFood(inspect_kingdomBackground);
            #endregion

            #region PowerBoxLawsWindow

            Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "world_laws");
            var worldLaws = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/world_laws");
            var worldLaws_unitContent = inspect_unit.transform.Find("/Canvas Container Main/Canvas - Windows/windows/world_laws/Background/Scroll View/Viewport/Content");
            worldLaws.SetActive(false);

            powerBoxLawsWindow = new PowerBoxLawsWindow();
            //initPowerBoxLawsWindow();

            #endregion
        }
    }
}
