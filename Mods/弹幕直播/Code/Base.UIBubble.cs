using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame{
    // 气泡UI
    public class UIBubble : MonoBehaviour
    {
        public Image image;
        public Text text;
        public GameObject goText;
        private float elapsedTime = 0.0f;
        private string message = string.Empty;
        void Start()
        {


            image = gameObject.AddComponent<Image>();
            var rect = image.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 200f);
            rect.anchoredPosition = new Vector2(0f, 10f);
            rect.pivot = new Vector2(0.5f, 0f);

            var contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.padding = new RectOffset(5, 5, 5, 5);

            goText = new GameObject("Text");
            goText.transform.SetParent(transform);
            text = goText.AddComponent<Text>();
            contentSizeFitter = goText.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.black;
            text.text = message;

            // 描边
            var outline = gameObject.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = Vector2.one;


            Destroy(gameObject, 5f);
        }

        public void SetMessage(string msg)
        {
            message = msg;
            if (text == null)
            {
                return;
            }
            text.text = message;
        }


    }
}
