using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnit
{
    public GameObject goMain = null;
    public Image jobImage;    // 头衔
    public Image image;
    public Text name;
    public Text thisTimeKD;
    // 击杀/队长/国王
    public Text kGlK;
    // 刷新显示
    public void RefreshDisplay()
    {
        if (goMain == null)
        {
            RectTransform rect;
            goMain = new GameObject("UnitItem");
            rect = goMain.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(400, 32);
            rect.pivot = Vector2.zero;

            var go = new GameObject("JobImage");
            go.transform.SetParent(goMain.transform);
            jobImage = go.AddComponent<Image>();
            jobImage.color = Color.clear;
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(28, 28);
            rect.anchoredPosition = new Vector2(18, 0);

            var goPanel = new GameObject("Panel");
            goPanel.transform.SetParent(goMain.transform);
            var tempImage = goPanel.AddComponent<Image>();
            tempImage.sprite = Resources.Load("Resources/unity_builtin_extra/Background", typeof(Sprite)) as Sprite;
            tempImage.color = new Color(1, 1, 1, 0.5f);
            rect = goPanel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = new Vector2(-36, 0);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(18, 0);

            go = new GameObject("Image");
            go.transform.SetParent(goPanel.transform);
            image = go.AddComponent<Image>();
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(28, 28);
            rect.anchoredPosition = new Vector2(18, 0);

            go = new GameObject("Name");
            go.transform.SetParent(goPanel.transform);
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

            var goPanel2 = new GameObject("KillScore");
            goPanel2.transform.SetParent(goPanel.transform);
            tempImage = goPanel2.AddComponent<Image>();
            tempImage.color = Color.clear;
            rect = goPanel2.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = new Vector2(0, 0);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0);

            go = new GameObject("ThisTimeKD");
            go.transform.SetParent(goPanel2.transform);
            thisTimeKD = go.AddComponent<Text>();
            thisTimeKD.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            thisTimeKD.alignment = TextAnchor.MiddleCenter;
            thisTimeKD.color = Color.black;
            thisTimeKD.text = "0/0";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(40, 0);
            rect.sizeDelta = new Vector2(62, 32);
            rect.pivot = new Vector2(0.5f, 0.5f);

            go = new GameObject("KillGroupLeaderKing");
            go.transform.SetParent(goPanel2.transform);
            kGlK = go.AddComponent<Text>();
            kGlK.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            kGlK.alignment = TextAnchor.MiddleCenter;
            kGlK.color = Color.black;
            kGlK.text = "0/0/0";
            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(100, 32);
            rect.pivot = new Vector2(1f, 0.5f);
        }
    }

}
