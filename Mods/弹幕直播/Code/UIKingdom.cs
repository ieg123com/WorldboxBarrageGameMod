using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMS;
using NCMS.Utils;
using ReflectionUtility;

public class UIKingdom
{
    public GameObject goMain = null;
    public Image image;
    public Text name;
    public Text fraction;
    public Text kDA;
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
            rect.sizeDelta = new Vector2(350, 32);
            rect.pivot = Vector2.zero;

            var kingdomItem = new GameObject("KingdomItem");
            kingdomItem.transform.SetParent(goMain.transform);
            rect = kingdomItem.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 1);

            var go = new GameObject("Panel");
            go.transform.SetParent(kingdomItem.transform);
            var tempImage = go.AddComponent<Image>();
            //tempImage.sprite = Sprites.LoadSprite("Resources/unity_builtin_extra/Background");
            tempImage.color = new Color(1,1,1,0.5f);
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

            go = new GameObject("Fraction");
            go.transform.SetParent(kingdomItem.transform);
            fraction = go.AddComponent<Text>();
            fraction.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            fraction.alignment = TextAnchor.MiddleLeft;
            fraction.color = Color.black;
            fraction.text = "fraction";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(-77, 0);
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
            rect.anchoredPosition = new Vector2(-16, 0);
            rect.sizeDelta = new Vector2(33, 32);
            rect.pivot = new Vector2(0.5f, 0.5f);
        }
    }



}
