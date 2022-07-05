using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    public static UILoading instance = null;
    public GameObject goMain = null;
    public RectTransform loadRect = null;
    public Text titleText = null;
    public Text titleDownText = null;
    public long load = 0;
    public long loadMax = 0;

    private void Awake()
    {
        instance = this;
    }
    public void RefreshDisplay()
    {

        if (goMain == null)
        {
            RectTransform rect;
            goMain = new GameObject("Loading");
            goMain.transform.SetParent(transform);
            rect = goMain.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0f, -100f);

            var goImage = new GameObject("Image");
            goImage.transform.SetParent(goMain.transform);
            var image = goImage.AddComponent<Image>();
            image.color = new Color(0.75f, 0.75f, 0.75f);
            rect = goImage.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(500f, 10f);
            rect.anchoredPosition = new Vector2(0f, 0f);
                        // 描边
            var outline = goImage.AddComponent<Outline>();
            outline.effectColor = Color.white;
            outline.effectDistance = Vector2.one;

            var goImage2 = new GameObject("Image");
            goImage2.transform.SetParent(goImage.transform);
            image = goImage2.AddComponent<Image>();
            image.color = new Color(0.0f, 0.75f, 0.0f);
            loadRect = goImage2.GetComponent<RectTransform>();
            loadRect.anchorMin = new Vector2(0.0f, 0.5f);
            loadRect.anchorMax = new Vector2(0.0f, 0.5f);
            loadRect.pivot = new Vector2(0.0f, 0.5f);
            loadRect.sizeDelta = new Vector2(500f, 10f);
            loadRect.anchoredPosition = new Vector2(0f, 0f);

            var go = new GameObject("Title");
            go.transform.SetParent(goMain.transform);

            titleText = go.AddComponent<Text>();
            titleText.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.color = Color.white;
            titleText.horizontalOverflow = HorizontalWrapMode.Overflow;
            titleText.supportRichText = true;

            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 20);
            rect.sizeDelta = new Vector2(100, 30);
            rect.pivot = new Vector2(0.5f, 0.5f);
            // 描边
            outline = go.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = Vector2.one;

            go = new GameObject("TitleDown");
            go.transform.SetParent(goMain.transform);

            titleDownText = go.AddComponent<Text>();
            titleDownText.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            titleDownText.alignment = TextAnchor.MiddleCenter;
            titleDownText.color = Color.white;
            titleDownText.horizontalOverflow = HorizontalWrapMode.Overflow;
            titleDownText.supportRichText = true;

            rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, -20);
            rect.sizeDelta = new Vector2(100, 30);
            rect.pivot = new Vector2(0.5f, 0.5f);
            // 描边
            outline = go.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = Vector2.one;

        }

        var scale = new Vector3(1f, 1f, 1f);

        if (loadMax > 0 && load <= loadMax)
        {
            scale.x = (float)load/(float)loadMax;
        }

        loadRect.localScale = scale;
    }

}
