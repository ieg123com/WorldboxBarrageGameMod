using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageGame
{
    // 气泡UI
    public class UIBubble : MonoBehaviour
    {
        public Sprite bottomSprite;
        public Text text;
        public string message = string.Empty;
        private float elapsedTime = 0.0f;
        private RectTransform rectTransform;
        private Image bottomImage;
        private bool popCompleted = false;
        void Awake()
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(0f, 0f);


            var go = new GameObject("ChatBubble");
            go.transform.SetParent(transform);
            var image = go.AddComponent<Image>();
            var rect = image.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            //rect.localScale = new Vector3(1f,1f, 1f);

            var contentSizeFitter = go.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var verticalLayoutGroup = go.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.padding = new RectOffset(5, 5, 2, 5);

            // 描边
            var outline = go.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = Vector2.one;


            var goText = new GameObject("Text");
            goText.transform.SetParent(go.transform);
            text = goText.AddComponent<Text>();
            contentSizeFitter = goText.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.black;
            text.text = message;

            var goImage = new GameObject("Bottom");
            goImage.transform.SetParent(transform);
            bottomImage = goImage.AddComponent<Image>();
            rect = bottomImage.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0f, 0f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.localScale = new Vector3(0.6f, 0.6f,1f);

            rectTransform.localScale = new Vector3(0f, 0f, 0f);
            
        }
        void Start()
        {
        }
        public void SetBottomSprite(Sprite sprite)
        {
            bottomSprite = sprite;
            bottomImage.sprite = bottomSprite;
            bottomImage.SetNativeSize();
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

        private void Update()
        {
            if(popCompleted == false)
            {
                rectTransform.localScale += Vector3.Lerp(Vector3.zero, Vector3.one, Time.deltaTime * 10f);
                if (Vector3.one.magnitude - rectTransform.localScale.magnitude < 0f)
                {
                    rectTransform.localScale = Vector3.one;
                    popCompleted = true;
                    Destroy(gameObject, 5f);
                }
            }

        }

    }
}
