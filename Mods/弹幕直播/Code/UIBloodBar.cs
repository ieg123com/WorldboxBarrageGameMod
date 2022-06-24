using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBloodBar : MonoBehaviour
{
    public GameObject goMain = null;
    public Image hpImage = null;
    public RectTransform hpRect = null;
    public RectTransform rootRect = null;
    public long hp = 0;
    public long hpMax = 0;
    public void RefreshDisplay()
    {
        float maxWidth = 35f;
        if (goMain == null)
        {

            rootRect = GetComponent<RectTransform>();
            RectTransform rect;
            goMain = new GameObject("BloodBar");
            goMain.transform.SetParent(transform);
            rect = goMain.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100, 100);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0);

            var goBackground = new GameObject("Background");
            goBackground.transform.SetParent(goMain.transform);
            var image = goBackground.AddComponent<Image>();
            image.color = new Color(0.75f, 0.75f, 0.75f);
            rect = goBackground.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(maxWidth, 3.72f);
            rect.anchoredPosition = new Vector2(0, 20);

            var go = new GameObject("HP");
            go.transform.SetParent(goBackground.transform);
            hpImage = go.AddComponent<Image>();
            hpImage.color = new Color(0.0f, 1f, 0.0f);
            hpRect = go.GetComponent<RectTransform>();
            hpRect.anchorMin = new Vector2(0.0f, 0.5f);
            hpRect.anchorMax = new Vector2(0.0f, 0.5f);
            hpRect.pivot = new Vector2(0.0f, 0.5f);
            hpRect.sizeDelta = new Vector2(maxWidth, 3.72f);
            hpRect.anchoredPosition = new Vector2(0, 0);
        }
        float hpPct = 0f;

        if (hpMax <= 0)
        {
            hpPct = 1f;
        }
        else
        {
            hpPct = (float)((double)hp/(double)hpMax);
        }
        hpPct = (hpPct > 1f) ? 1f : hpPct;
        hpRect.sizeDelta = new Vector2(maxWidth * hpPct, 3.72f);
        if (hpPct > 0.5f)
        {
            hpImage.color = new Color((1f - hpPct) * 2f, 1f, 0f);
        }
        else
        {
            hpImage.color = new Color(1f, hpPct * 2f, 0f);
        }
    }

    public void Clear()
    {
        Destroy(gameObject);
    }

    static public UIBloodBar Create(Transform parent)
    {
        var go = new GameObject("BloodBar");
        go.transform.SetParent(parent);
        go.AddComponent<RectTransform>();
        var ui = go.AddComponent<UIBloodBar>();
        return ui;
    }

}
