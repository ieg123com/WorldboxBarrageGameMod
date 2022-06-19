using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKingdomList : MonoBehaviour
{
    static public UIKingdomList instance;
    public GameObject goMain;
    public GameObject goContent;

    public List<UIKingdom> itemList = new List<UIKingdom>();

    private int uiKingdomItemCount = 0;

    void Awake()
    {
        instance = this;
    }

    // 刷新显示
    public void RefreshDisplay()
    {
        if (goMain == null)
        {
            goMain = new GameObject("KingdomList");
            goMain.transform.SetParent(transform);
            goMain.AddComponent<RectTransform>();
            var rect = goMain.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.one;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta=new Vector2(300, 400);
            rect.anchoredPosition = new Vector2(0, -20);
            rect.pivot = new Vector2(1, 1f);
            goMain.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;


            goContent = new GameObject("Content");
            goContent.transform.SetParent(goMain.transform);
            var vert = goContent.AddComponent<VerticalLayoutGroup>();
            vert.childForceExpandHeight = false;
            vert.childForceExpandWidth = true;
            vert.childControlHeight = false;
            rect = goContent.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta=new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(0, 0);

        }

    }

    public UIKingdom GetUIKingdom()
    {
        if (itemList.Count <= uiKingdomItemCount)
        {
            var item = new UIKingdom();
            item.RefreshDisplay();
            item.goMain.transform.SetParent(goContent.transform);
            itemList.Add(item);
        }
        UIKingdom ret = itemList[uiKingdomItemCount];
        ret.goMain.SetActive(true);
        ++uiKingdomItemCount;
        return ret;
    }


    public void Clear()
    {
        foreach (var item in itemList)
        {
            item.goMain.SetActive(false);
        }
        uiKingdomItemCount = 0;
    }


}
