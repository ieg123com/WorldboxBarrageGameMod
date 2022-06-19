using HarmonyLib;
using ReflectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Config;

namespace Helper
{
    class DebugMenu
    {
        public static void toggleDebugButton(bool value)
        {
            if (gameLoaded)
            {
                //PowerButton.get("DebugButton").gameObject.SetActive(value);

                var Buttons = Resources.FindObjectsOfTypeAll<PowerButton>();
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i].gameObject.transform.name == "DebugButton")
                    {
                        Buttons[i].gameObject.SetActive(value);
                    }
                }
            }
        }
    }

    class HisHud
    {
        public static void newText(string message, Color color, Sprite icon = null)
        {
            GameObject gameObject = HistoryHud.instance.GetObject();
            gameObject.name = "HistoryItem " + (object)(HistoryHud.historyItems.Count + 1);
            gameObject.SetActive(true);

            gameObject.transform.Find("CText").GetComponent<Text>();
            gameObject.transform.SetParent(HistoryHud.contentGroup);
            RectTransform component = gameObject.GetComponent<RectTransform>();
            component.localScale = Vector3.one;
            component.localPosition = Vector3.zero;
            component.SetLeft(0.0f);

            float top = (float)HistoryHud.instance.CallMethod("recalcPositions");

            component.SetTop(top);
            component.sizeDelta = new Vector2(component.sizeDelta.x, 15f);
            gameObject.GetComponent<HistoryHudItem>().targetBottom = top;

            gameObject.GetComponent<HistoryHudItem>().textField.color = color;
            gameObject.GetComponent<HistoryHudItem>().textField.text = message;
            HistoryHud.historyItems.Add(gameObject.GetComponent<HistoryHudItem>());
            Reflection.SetField<bool>(HistoryHud.instance, "recalc", true);

            if(icon != null)
            {
                gameObject.transform.Find("Icon").GetComponent<Image>().sprite = icon;
            }

            gameObject.SetActive(true);
        }
    }

    class KingdomThings
    {
        public static void newText(string message, Color color)
        {
            HisHud.newText(message, color);
        }

        public static Color GetKingdomColor(Kingdom kingdom)
        {
            var kingdomColor = (KingdomColor)Reflection.GetField(kingdom.GetType(), kingdom, "kingdomColor");
            return kingdomColor.colorBorderOut;
        }
    }

    class Utils
    {

        public static void HarmonyPatching(Harmony harmony, string type, MethodInfo original, MethodInfo patch)
        {
            switch (type)
            {
                case "prefix":
                    harmony.Patch(original, prefix: new HarmonyMethod(patch));
                    break;
                case "postfix":
                    harmony.Patch(original, postfix: new HarmonyMethod(patch));
                    break;
            }
        }

        public static void CopyClass<T>(T copyFrom, T copyTo, bool copyChildren)
        {
            if (copyFrom == null || copyTo == null)
                throw new Exception("Must not specify null parameters");

            var properties = copyFrom.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                {
                    if (!copyChildren) continue;

                    var destinationClass = Activator.CreateInstance(p.PropertyType);
                    object copyValue = p.GetValue(copyFrom);

                    CopyClass(copyValue, destinationClass, copyChildren);

                    p.SetValue(copyTo, destinationClass);
                }
                else
                {
                    object copyValue = p.GetValue(copyFrom);
                    p.SetValue(copyTo, copyValue);
                }
            }
        }
    }
}
