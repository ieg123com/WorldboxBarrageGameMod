using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageManager : MonoBehaviour
{
    static public UIDamageManager instance;

    public Queue<UIDamage> uiPool = new Queue<UIDamage>();

    void Awake()
    {
        instance = this;
    }

    public void Show(string damage, Color color, Vector2 pos)
    {
        var ui = GetOrCreate();
        ui.Show(damage, color, pos);
        ui.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    public UIDamage GetOrCreate()
    {
        UIDamage retUI = null;
        Debug.Log($"uiPool.Count = {uiPool.Count}");
        if (uiPool.Count > 0)
        {
            retUI = uiPool.Dequeue();
        }
        else
        {
            var go = new GameObject("Damage");
            go.transform.SetParent(transform);
            var rect = go.AddComponent<RectTransform>();
            retUI = go.AddComponent<UIDamage>();
        }
        return retUI;
    }

    public void Push(UIDamage ui)
    {
        uiPool.Enqueue(ui);
        Debug.Log($"Push uiPool.Count = {uiPool.Count}");
    }
}
