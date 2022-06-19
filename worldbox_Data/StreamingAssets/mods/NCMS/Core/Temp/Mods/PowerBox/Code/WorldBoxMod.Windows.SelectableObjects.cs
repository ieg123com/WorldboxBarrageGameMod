using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;
using UnityEngine;
using static Config;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PowerBox
{
    public class SelectableObjects
    {
        internal static PowerType TType;

        protected GameObject spriteHighlighter;
        protected float rC = 0.314f;
        protected float gC = 0.78f;
        protected float bC = 0;
        protected float aC = 0.565f;
        
        public SelectableObjects(){
            this.spriteHighlighter = new GameObject("spriteHighlighter");
            this.spriteHighlighter.transform.localScale = new Vector2(1.0f, 1.0f);
            this.spriteHighlighter.layer = 5;

            var imageH = this.spriteHighlighter.AddComponent<Image>();
            var texture = Resources.Load<Texture2D>("ui/icons/iconbrush_circ_5");
            imageH.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f), 1f);
            imageH.color = new Color(this.rC, this.gC, this.bC, this.aC);
            imageH.raycastTarget = false;

            this.spriteHighlighter.SetActive(false);
        }

        protected GameObject AddHighLight(int index, GameObject Content, bool enabled = false)
        {
            var spriteHL = GameObject.Instantiate(spriteHighlighter, Content.transform);
            spriteHL.transform.localPosition = GetPosByIndex(index);
            spriteHL.SetActive(true);

            if (enabled)
            {
                spriteHL.GetComponent<Image>().color = new Color(rC, gC, bC, aC);
            }
            else
            {
                spriteHL.GetComponent<Image>().color = new Color(rC, gC, bC, 0);
            }

            return spriteHL;
        }

        protected void HighlightTrait(bool enable, GameObject HighLight)
        {
            if (enable)
            {
                HighLight.GetComponent<Image>().color = new Color(rC, gC, bC, aC);
            }
            else
            {
                HighLight.GetComponent<Image>().color = new Color(rC, gC, bC, 0);
            }
        }

        protected void HighlightButton(bool enable, GameObject HighLight) => HighlightTrait(enable, HighLight);

        protected void UnhilightAll(GameObject Content)
        {
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                HighlightButton(false, Content.transform.GetChild(i).gameObject);
            }
        }


        protected float startXPos = 44.4f;
        protected float XStep = 28f;
        protected int countInRow = 7;
        protected float startYPos = -22.5f;
        protected float YStep = -28.5f;
        protected Vector2 GetPosByIndex(int index)
        {
            float x = (index % countInRow) * XStep + startXPos;
            float y = (Mathf.RoundToInt(index / countInRow) * YStep) + startYPos;

            return new Vector2(x, y);
        }

        protected void ResetWrapVals()
        {
            startXPos = 40f;
            XStep = 22f;
            countInRow = 9;
            startYPos = -22.5f;
            YStep = -22.5f;
        }
    }

    public enum PowerType
    {
        add,
        remove,
        unset
    }
}
