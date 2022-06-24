using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKingdom
{
    public GameObject goMain = null;
    public GameObject goContent;

    public Image image;
    public Text name;
    public Text cityNumber;
    public Text score;
    public Text kDA;


    public List<UIUnit> itemList = new List<UIUnit>();

    private int uiUnitItemCount = 0;
    // 刷新显示
    public void RefreshDisplay()
    {
        if (goMain == null)
        {
            RectTransform rect;
            goMain = new GameObject("Item");
            rect = goMain.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(400, 32);
            rect.pivot = Vector2.zero;
            goMain.AddComponent<Image>().color = Color.clear;

            var vert = goMain.AddComponent<VerticalLayoutGroup>();
            vert.childForceExpandHeight = false;
            vert.childForceExpandWidth = true;
            vert.childControlHeight = false;
            vert.childControlWidth = true;
            var contentSizeFitter = goMain.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


            var kingdomItem = new GameObject("KingdomItem");
            kingdomItem.transform.SetParent(goMain.transform);
            rect = kingdomItem.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.sizeDelta = new Vector2(0, 32);
            rect.pivot = new Vector2(0.5f, 1);

            var go = new GameObject("Panel");
            go.transform.SetParent(kingdomItem.transform);
            var tempImage = go.AddComponent<Image>();
            tempImage.sprite = Resources.Load("Resources/unity_builtin_extra/Background", typeof(Sprite)) as Sprite;
            tempImage.color = new Color(1, 1, 1, 0.5f);
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = new Vector2(0, 0);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0);

            go = new GameObject("Image");
            go.transform.SetParent(kingdomItem.transform);
            image = go.AddComponent<Image>();
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(28, 28);
            rect.anchoredPosition = new Vector2(18, 0);

            go = new GameObject("Name");
            go.transform.SetParent(kingdomItem.transform);
            name = go.AddComponent<Text>();
            name.font = Font.CreateDynamicFontFromOSFont("Arial", 16);
            name.alignment = TextAnchor.MiddleLeft;
            name.color = Color.black;
            name.text = "name";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.anchoredPosition = new Vector2(38, 0);
            rect.sizeDelta = new Vector2(100, 32);
            rect.pivot = new Vector2(0f, 0.5f);

            go = new GameObject("CityNumber");
            go.transform.SetParent(kingdomItem.transform);
            cityNumber = go.AddComponent<Text>();
            cityNumber.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            cityNumber.alignment = TextAnchor.MiddleCenter;
            cityNumber.color = Color.black;
            cityNumber.text = "CityNumber";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(-220, 0);
            rect.sizeDelta = new Vector2(60, 32);
            rect.pivot = new Vector2(0.5f, 0.5f);

            go = new GameObject("Score");
            go.transform.SetParent(kingdomItem.transform);
            score = go.AddComponent<Text>();
            score.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            score.alignment = TextAnchor.MiddleCenter;
            score.color = Color.black;
            score.text = "score";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(-140, 0);
            rect.sizeDelta = new Vector2(84, 32);
            rect.pivot = new Vector2(0.5f, 0.5f);

            go = new GameObject("KDA");
            go.transform.SetParent(kingdomItem.transform);
            kDA = go.AddComponent<Text>();
            kDA.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            kDA.alignment = TextAnchor.MiddleLeft;
            kDA.color = Color.black;
            kDA.text = "kDA";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(-24, 0);
            rect.sizeDelta = new Vector2(33, 32);
            rect.pivot = new Vector2(0.5f, 0.5f);


            // Verical Layout Group
            goContent = new GameObject("Content");
            goContent.transform.SetParent(goMain.transform);
            vert = goContent.AddComponent<VerticalLayoutGroup>();
            vert.childForceExpandHeight = false;
            vert.childForceExpandWidth = true;
            vert.childControlHeight = false;
            vert.childControlWidth = true;
            rect = goContent.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta=new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(0, 0);

            goContent.AddComponent<Image>().color = Color.clear;
            contentSizeFitter = goContent.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        }
    }


    public UIUnit GetUIUnit()
    {
        if (itemList.Count <= uiUnitItemCount)
        {
            var item = new UIUnit();
            item.RefreshDisplay();
            item.goMain.transform.SetParent(goContent.transform);
            itemList.Add(item);
        }
        UIUnit ret = itemList[uiUnitItemCount];
        ret.goMain.SetActive(true);
        ++uiUnitItemCount;
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(goContent.GetComponent<RectTransform>());
        UIKingdomList.instance.ForceRebuildLayout();
        return ret;
    }

    public void Remove(UIUnit uIUnit)
    {
        if (uIUnit.goMain.transform.parent != goContent.transform)
        {
            return;
        }
        // 开始移除
        itemList.Remove(uIUnit);
        uIUnit.goMain.SetActive(false);
        uIUnit.goMain.transform.SetParent(null);
        uIUnit.goMain.transform.SetParent(goContent.transform);
        itemList.Add(uIUnit);
        --uiUnitItemCount;
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(goContent.GetComponent<RectTransform>());
        UIKingdomList.instance.ForceRebuildLayout();
    }

    public void Clear()
    {
        foreach (var item in itemList)
        {
            item.goMain.SetActive(false);
        }
        uiUnitItemCount = 0;
    }

}
