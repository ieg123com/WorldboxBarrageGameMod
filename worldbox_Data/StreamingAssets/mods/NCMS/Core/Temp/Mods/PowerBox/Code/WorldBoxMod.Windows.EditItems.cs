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
    partial class EditItemsWindow : SelectableObjects
    {
        private GameObject changeItemType;
        //private GameObject changeItemModifierF;
        private ScrollWindow editItemsWindow;
        public EditItemsWindow(Transform inspect_unitContent) : base()
        {
            initAddRemoveChoosen();

            editItemsWindow = NCMS.Utils.Windows.CreateNewWindow("editItems", "Edit Items");
            //editItemsWindow.titleText.text = "Edit Items";

            editItemsWindow.transform.Find("Background").Find("Scroll View").gameObject.SetActive(true);

            var editItems = Helper.GodPowerTab.createButton(
                "EditItems",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.items.png", 0, 0),
                inspect_unitContent,
                Edit_Items_Button_Click,
                "Edit items",
                "Edit unit's items");

            var viewport = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{editItemsWindow.name}/Background/Scroll View/Viewport");
            var viewportRect = viewport.GetComponent<RectTransform>();
            viewportRect.sizeDelta = new Vector2(0, 17);


            editItems.transform.localPosition = new Vector3(245.50f, -105f, editItems.transform.localPosition.z);
            editItems.transform.Find("Icon").GetComponent<RectTransform>().sizeDelta = new Vector2(65f, 65f);
            var editItemsRect = editItems.GetComponent<RectTransform>();
            editItemsRect.sizeDelta = new Vector2(100f, 100f);
            editItems.GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".other.backgroundBackButtonRev.png", 0, 0);
            editItems.GetComponent<Button>().transition = Selectable.Transition.None;

            var bg = editItemsWindow.transform.Find("Background");


            var saveButton = Helper.GodPowerTab.createButton(
                "DoneItems",
                Resources.Load<Sprite>("ui/icons/iconsavelocal"),
                bg.transform,
                Items_Save_Button_Click,
                "Save selected items",
                "");
            saveButton.transform.localPosition = new Vector2(70.00f, -150.00f);

            changeItemType = Helper.GodPowerTab.createButton(
                "ChangeType",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.armor.png", 0, 0),
                bg,
                Change_Type_Button_Click,
                "Armors",
                "");
            changeItemType.transform.localPosition = new Vector2(35.00f, -150.00f);


            var changeItemModifierF = Helper.GodPowerTab.createButton(
                "ChangeModifier0",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.prefix.png", 0, 0),
                bg,
                () => Change_Modifier_Button_Click(0),
                "First modifier",
                "");
            changeItemModifierF.transform.localPosition = new Vector2(-70.00f, -150.00f);


            var changeItemModifierS = Helper.GodPowerTab.createButton(
                "ChangeModifier1",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.prefix.png", 0, 0),
                bg,
                () => Change_Modifier_Button_Click(1),
                "Second modifier",
                "");
            changeItemModifierS.transform.localPosition = new Vector2(-35.00f, -150.00f);


            var changeItemModifierT = Helper.GodPowerTab.createButton(
                "ChangeModifier2",
                Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.prefix.png", 0, 0),
                bg,
                () => Change_Modifier_Button_Click(2),
                "Third modifier",
                "");
            changeItemModifierT.transform.localPosition = new Vector2(0.00f, -150.00f);
        }

        internal void initEditItems(ScrollWindow window, Action<EquipmentButton> callback, PowerType Ttype = PowerType.unset)
        {
            SelectableObjects.TType = Ttype;

            if(SelectableObjects.TType != PowerType.unset)
            {
                callback = addRemoveItemsButtonCallBack;
            }

            var rt = this.spriteHighlighter.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(60f, 60f);

            var Content = GameObject.Find("/Canvas Container Main/Canvas - Windows/windows/" + window.name + "/Background/Scroll View/Viewport/Content");
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                GameObject.Destroy(Content.transform.GetChild(i).gameObject);
            }

            EquipmentButton itemButton = NCMS.Utils.GameObjects.FindEvenInactive("EquipmentButton").GetComponent<EquipmentButton>();

            List<ItemAsset> itemsList = AssetManager.items.list.FindAll(c => choosenType == EquipmentType.Weapon ? c.equipmentType == EquipmentType.Weapon : c.equipmentType != EquipmentType.Weapon);

            List<ItemAsset> materials = null;
            switch(choosenType){
                case EquipmentType.Weapon:
                    materials = AssetManager.items_material_weapon.list;
                    break;
                default: 
                    materials = AssetManager.items_material_armor.list;
                    break;
                // case EquipmentType.Armor:
                // case EquipmentType.Helmet:
                // case EquipmentType.Boots:
                //     materials = AssetManager.items_material_armor.list;
                //     break;
                // case EquipmentType.Ring:
                // case EquipmentType.Amulet:
                //     materials = AssetManager.items_material_accessory.list;
                //     break;
            }

            
            this.XStep = 24.75f;
            this.countInRow = 8;

            int index = 0;

            List<string> skipList = new List<string>{
                "_equipment", "_accessory", "_weapon", "_melee", "_range", "base", "hands", "jaws", "claws"
            };

            for(int i = 0; i < itemsList.Count; i++){
                if(skipList.Contains(itemsList[i].id)){
                    continue;
                }

                for(int k = 0; k < materials.Count; k++){
                    var itemData = new ItemData();
                    itemData.id = itemsList[i].id;
                    itemData.by = "Nikon";
                    bool ringAndAmulet = ((itemData.id == "ring" || itemData.id == "amulet") && materials[k].id == "leather");
                    itemData.material = ringAndAmulet ? "bone" : materials[k].id;
                    itemData.modifiers = new List<string>();
                    itemData.modifiers.Add(choosenModifierId[0]);
                    itemData.modifiers.Add(choosenModifierId[1]);
                    itemData.modifiers.Add(choosenModifierId[2]);

                    
                    if(Resources.Load<Sprite>($"ui/Icons/items/icon_{itemData.id}_{itemData.material}") is null && Resources.Load<Sprite>($"ui/Icons/items/icon_{itemData.id}") is null){
                        continue;
                    }

                    if(!itemsList[i].materials.Contains(materials[k].id) && !ringAndAmulet){
                        continue;
                    }

                    GameObject hl;
                    if(SelectableObjects.TType == PowerType.unset)
                    {
                        var actorsItem = ActorEquipment.getList(WorldBoxMod.UNIT.equipment).Find(c => c.data.id == itemData.id);
                        var compare = actorsItem?.data is not null ? compareItems(actorsItem.data, itemData) : false;
                        hl = AddHighLight(index, Content, compare);
                    }
                    else{
                        bool addCond = false;
                        bool removeCond = false;

                        if(choosenForAddSlots[itemsList[i].equipmentType] is not null){
                            addCond = compareItems(choosenForAddSlots[itemsList[i].equipmentType], itemData);
                        }

                        if(choosenForRemoveSlots[itemsList[i].equipmentType] is not null){
                            removeCond = compareItems(choosenForRemoveSlots[itemsList[i].equipmentType], itemData);
                        }

                        hl = AddHighLight(index,  Content, SelectableObjects.TType == PowerType.add ? addCond : removeCond);
                    }
                        
                    loadItemButton(itemData, itemsList[i].equipmentType, i, itemsList.Count * materials.Count, itemButton, hl.transform, callback);
                    index++;
                }
            }
        }

        private void loadItemButton(ItemData item, EquipmentType eqType, int pIndex, int pTotal, EquipmentButton itemButtonPref, Transform parent, Action<EquipmentButton> callback)
        {

            string path = $"ui/Icons/items/icon_{item.id}_{item.material}";
            Sprite spite = Resources.Load<Sprite>(path);
            if(spite is null){
                path = $"ui/Icons/items/icon_{item.id}";
                spite = Resources.Load<Sprite>(path);
            }

            if(spite is null){
                return;
            }

            EquipmentButton itemButton = GameObject.Instantiate<EquipmentButton>(itemButtonPref, parent);
            
            itemButton.GetComponent<Image>().sprite = spite;
            itemButton.transform.localPosition = Vector3.zero;

            // ActorEquipmentSlot slot = new ActorEquipmentSlot();
            // slot.data = item;
            // slot.type = eqType;

            Reflection.SetField(itemButton, "item_data", item);

            var button = itemButton.gameObject.GetComponent<Button>();
            button.onClick.AddListener(() => callback(itemButton));
        }

        internal static bool compareItems(ItemData item1, ItemData item2){
            bool result = true;
            result = result && item1.id == item2.id;
            result = result && item1.by == item2.by;
            result = result && item1.material == item2.material;

            for(int i = 0; i < item1.modifiers.Count; i++){
                if(item2.modifiers.Contains(item1.modifiers[i])){
                    continue;
                }

                result = false;
            }

            return result;
        }

        private EquipmentType choosenType = EquipmentType.Weapon;
        private void Change_Type_Button_Click()
        {
            switch(choosenType){
                case EquipmentType.Weapon:
                    choosenType = EquipmentType.Armor;
                    break;
                default:
                    choosenType = EquipmentType.Weapon;
                    break;

                // case EquipmentType.Helmet:
                //     choosenType = EquipmentType.Armor;
                //     break;
                // case EquipmentType.Armor:
                //     choosenType = EquipmentType.Boots;
                //     break;
                // case EquipmentType.Boots:
                //     choosenType = EquipmentType.Ring;
                //     break;
                // case EquipmentType.Ring:
                //     choosenType = EquipmentType.Amulet;
                //     break;
                // case EquipmentType.Amulet:
                //     choosenType = EquipmentType.Weapon;
                //     break;
            }

            if(choosenType == EquipmentType.Weapon){
                changeItemType.transform.Find("Icon").GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.weapons.png", 0, 0);
                NCMS.Utils.Localization.setLocalization("ChangeType", "Weapons");
            }
            else{
                changeItemType.transform.Find("Icon").GetComponent<Image>().sprite = Mod.EmbededResources.LoadSprite(WorldBoxMod.resources + ".powers.armor.png", 0, 0);
                NCMS.Utils.Localization.setLocalization("ChangeType", "Armors");
            }

            if (SelectableObjects.TType == PowerType.unset)
            {
                initEditItems(editItemsWindow, editItemsButtonCallBack);
            }
            else
            {
                initEditItems(editItemsWindow, null, SelectableObjects.TType);
            }
        }

        private int[] choosenModifierIndex = new int[]{0, 0 ,0};
        private string[] choosenModifierId = new string[]{"normal", "normal", "normal"};
        private void Change_Modifier_Button_Click(int modifierIndexNumber)
        {
            string poolName = "weapon";

            switch(choosenType){
                case EquipmentType.Weapon:
                    poolName = "weapon";
                    break;
                default:
                    poolName = "armor";
                    break;
            }

            List<ItemAsset> pool = AssetManager.items_modifiers.pools[poolName];


            while(true){
                choosenModifierIndex[modifierIndexNumber]++;
            

                if(choosenModifierIndex[modifierIndexNumber] >= pool.Count){
                    choosenModifierIndex[modifierIndexNumber] = 0;
                }

                if(choosenModifierId[modifierIndexNumber] != pool[choosenModifierIndex[modifierIndexNumber]].id){
                    choosenModifierId[modifierIndexNumber] = pool[choosenModifierIndex[modifierIndexNumber]].id;
                    break;
                }
            }

            //Debug.LogWarning($"choosenModifierIndex[modifierIndexNumber]: {choosenModifierIndex[modifierIndexNumber]}, pool.Count: {pool.Count}, pool[choosenModifierIndex].id: {pool[choosenModifierIndex[modifierIndexNumber]].id}");

            NCMS.Utils.Localization.setLocalization($"ChangeModifier{modifierIndexNumber}", $"{numberToName(modifierIndexNumber)} modifier: {pool[choosenModifierIndex[modifierIndexNumber]].id}");

            if (SelectableObjects.TType == PowerType.unset)
            {
                initEditItems(editItemsWindow, editItemsButtonCallBack);
            }
            else
            {
                initEditItems(editItemsWindow, null, SelectableObjects.TType);
            }
        }

        public void Edit_Items_Button_Click()
        {
            if (WorldBoxMod.UNIT.stats.use_items)
            {
                initEditItems(editItemsWindow, editItemsButtonCallBack);
                editItemsWindow.clickShow();
            }
        }

        private void editItemsButtonCallBack(EquipmentButton buttonPressed)
        {
            var data = Reflection.GetField(buttonPressed.GetType(), buttonPressed, "item_data") as ItemData;
            var unitSlot = WorldBoxMod.UNIT.equipment.getSlot(AssetManager.items.get(data.id).equipmentType);

            if(unitSlot.data != null)
            {
                if (compareItems(data, data))
                {
                    unitSlot.emptySlot();
                    //unitSlot.CallMethod("setItem", null);
                }
                else
                { 
                    unitSlot.CallMethod("setItem", data);
                }
            }
            else
            {
                unitSlot.CallMethod("setItem", data);
            }

            Reflection.SetField(WorldBoxMod.UNIT, "statsDirty", true);


            var window = NCMS.Utils.Windows.GetWindow("editItems");
            initEditItems(window, editItemsButtonCallBack);
        }

        internal static Dictionary<EquipmentType, ItemData> choosenForAddSlots = new Dictionary<EquipmentType, ItemData>();
        internal static Dictionary<EquipmentType, ItemData> choosenForRemoveSlots = new Dictionary<EquipmentType, ItemData>();

        private void addRemoveItemsButtonCallBack(EquipmentButton buttonPressed)
        {
            var data = Reflection.GetField(buttonPressed.GetType(), buttonPressed, "item_data") as ItemData;
            var type = AssetManager.items.get(data.id).equipmentType;
            //var slot = Reflection.GetField(buttonPressed.GetType(), buttonPressed, "slot") as ActorEquipmentSlot;

            if(SelectableObjects.TType == PowerType.add)
            {
                choosenForAddSlots[type] = data;
            }
            else if (TType == PowerType.remove)
            {
                choosenForRemoveSlots[type] = data;
            }

            var window = NCMS.Utils.Windows.GetWindow("editItems");
            initEditItems(window, null, SelectableObjects.TType);
        }

        private void initAddRemoveChoosen()
        {
            choosenForAddSlots.Add(EquipmentType.Amulet, null);
            choosenForAddSlots.Add(EquipmentType.Armor, null);
            choosenForAddSlots.Add(EquipmentType.Boots, null);
            choosenForAddSlots.Add(EquipmentType.Helmet, null);
            choosenForAddSlots.Add(EquipmentType.Ring, null);
            choosenForAddSlots.Add(EquipmentType.Weapon, null);
            choosenForRemoveSlots.Add(EquipmentType.Amulet, null);
            choosenForRemoveSlots.Add(EquipmentType.Armor, null);
            choosenForRemoveSlots.Add(EquipmentType.Boots, null);
            choosenForRemoveSlots.Add(EquipmentType.Helmet, null);
            choosenForRemoveSlots.Add(EquipmentType.Ring, null);
            choosenForRemoveSlots.Add(EquipmentType.Weapon, null);
        }

        private void Items_Save_Button_Click()
        {
            var addRemoveTraitsWindow = NCMS.Utils.Windows.GetWindow("editItems");
            addRemoveTraitsWindow.clickHide();

            if ((SelectableObjects.TType == PowerType.add && choosenForAddSlots.Count > 0) || (SelectableObjects.TType == PowerType.remove && choosenForRemoveSlots.Count > 0))
            {
                var pbsInstance = Reflection.GetField(typeof(PowerButtonSelector), null, "instance") as PowerButtonSelector;
                var pButton = NCMS.Utils.GameObjects.FindEvenInactive(SelectableObjects.TType == PowerType.add ? "addItems" : "removeItems");
                pbsInstance.clickPowerButton(pButton.GetComponent<PowerButton>());
            }
        }

        

        private string numberToName(int number)
        {
            switch (number)
            {
                case 0:
                    return "First";
                case 1:
                    return "Second";
                case 2:
                    return "Third";
            }

            return "First";
        }
    }
}
