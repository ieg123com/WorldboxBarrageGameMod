using System;
using UnityEngine;
using UnityEngine.UI;


public static class UIHelper
{
    public static void RefreshSizeDelta(this VerticalLayoutGroup self)
    {
        RectTransform rectTransform = self.transform as RectTransform;
        Vector2 sizeDelta = rectTransform.sizeDelta;

        int childCount = 0;
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            if (child.gameObject.activeSelf == false) continue;
            childCount++;
        }

        sizeDelta.y = (((rectTransform.GetChild(0) as RectTransform).sizeDelta.y + self.spacing) * childCount) + self.padding.top;

        rectTransform.sizeDelta = sizeDelta;
    }
}
