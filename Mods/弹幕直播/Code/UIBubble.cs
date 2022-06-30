using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 气泡UI
public class UIBubble : MonoBehaviour
{
    public Sprite bottomSprite;
    public Text text;
    public string message = string.Empty;
    public Image headImage;
    private float elapsedTime = 0.0f;
    private RectTransform rectTransform;
    private Image bottomImage;
    private GameObject goBottom;
    private GameObject goChatBubble;
    private bool popCompleted = false;
    void Awake()
    {
        rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(0f, 0f);


        goChatBubble = new GameObject("ChatBubble");
        goChatBubble.transform.SetParent(transform);
        var image = goChatBubble.AddComponent<Image>();
        var rect = image.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(135f, 23f);
        rect.anchoredPosition = new Vector2(0f, 0f);
        rect.pivot = new Vector2(0.5f, 0f);
        //rect.localScale = new Vector3(1f,1f, 1f);

        var contentSizeFitter = goChatBubble.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

        var horizontalLayoutGroup = goChatBubble.AddComponent<HorizontalLayoutGroup>();
        horizontalLayoutGroup.padding = new RectOffset(5, 5, 0, 0);

        // 描边
        var outline = goChatBubble.AddComponent<Outline>();
        outline.effectColor = Color.black;
        outline.effectDistance = Vector2.one;

        var goHead = new GameObject("Head");
        goHead.transform.SetParent(goChatBubble.transform);
        headImage = goHead.AddComponent<Image>();
        headImage.preserveAspect=true;

        var goText = new GameObject("Text");
        goText.transform.SetParent(goChatBubble.transform);
        text = goText.AddComponent<Text>();
        contentSizeFitter = goText.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        text.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
        text.fontSize = 14;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.alignByGeometry = true;
        text.text = message;

        goBottom = new GameObject("Bottom");
        goBottom.transform.SetParent(transform);
        bottomImage = goBottom.AddComponent<Image>();
        rect = bottomImage.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0f, 0f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.localScale = new Vector3(0.6f, 0.6f, 1f);

        rectTransform.localScale = new Vector3(0f, 0f, 0f);
        SetDown(false);
    }


    void Start()
    {
    }

    public void SetDown(bool down)
    {
        if (down == true)
        {
            var rect = bottomImage.GetComponent<RectTransform>();
            rect.localScale = new Vector3(0.6f, -0.6f, 1f);
            rect.anchoredPosition = new Vector3(0f, -10f);
            rect = goChatBubble.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector3(0f, -10f);
        }
        else
        {
            var rect = bottomImage.GetComponent<RectTransform>();
            rect.localScale = new Vector3(0.6f, 0.6f, 1f);
            rect.anchoredPosition = new Vector3(0f, 10f);
            rect = goChatBubble.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0f);
            rect.anchoredPosition = new Vector3(0f, 10f);
        }

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
        if (popCompleted == false)
        {
            rectTransform.localScale += Vector3.Lerp(Vector3.zero, Vector3.one, Time.deltaTime * 10f);
            if (Vector3.one.magnitude - rectTransform.localScale.magnitude <= 0.01f)
            {
                rectTransform.localScale = Vector3.one;
                popCompleted = true;
                Destroy(gameObject, 5f);
            }
        }

    }

}
