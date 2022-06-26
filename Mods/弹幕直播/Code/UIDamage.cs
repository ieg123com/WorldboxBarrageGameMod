using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    public GameObject goMain = null;

    public Text damageText;
    public RectTransform mainRect;

    public float tempTime = 0f;

    public void Show(string damage, Color color, Vector2 pos)
    {
        if (goMain == null)
        {
            RectTransform rect;
            goMain = new GameObject("Damage");
            goMain.transform.SetParent(transform);

            damageText = goMain.AddComponent<Text>();
            damageText.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            damageText.alignment = TextAnchor.MiddleCenter;
            damageText.color = color;
            damageText.text = damage.ToString();
            damageText.fontStyle = FontStyle.Bold;
            mainRect = goMain.GetComponent<RectTransform>();
            mainRect.anchorMin = new Vector2(0.5f, 0.5f);
            mainRect.anchorMax = new Vector2(0.5f, 0.5f);
            mainRect.anchoredPosition = new Vector2(0, 0);
            mainRect.sizeDelta = new Vector2(50, 20);
            mainRect.pivot = new Vector2(0.5f, 0.5f);

            // 描边
            var outline = goMain.AddComponent<Outline>();
            outline.effectColor = Color.white;
            outline.effectDistance = Vector2.one;

        }

        mainRect.anchoredPosition = Vector2.zero;
        damageText.text = damage.ToString();
        damageText.color = color;
        tempTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {

        tempTime += Time.deltaTime;
        if (tempTime > 0.5f)
        {
            UIDamageManager.instance.Push(this);
            gameObject.SetActive(false);
            return;
        }
        mainRect.anchoredPosition = new Vector2()
        {
            x = 0,
            y = Mathf.Lerp(0, 50f, tempTime/0.5f)
        };

    }


}
