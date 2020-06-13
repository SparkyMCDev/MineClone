using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public List<GameObject> guis;
    public static GuiManager Instance { get; internal set; }

    private void Awake()
    {
        guis = new List<GameObject>();

        Instance = this;

        GameObject gui_crosshair = new GameObject("crosshair");
        gui_crosshair.transform.parent = transform;
        gui_crosshair.AddComponent<Image>().sprite = Resources.Load<Sprite>("gui/img/crosshair");

        gui_crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gui_crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        
        GameObject gui_skins = new GameObject("skins");
        gui_skins.transform.parent = transform;
        
        
    }

    private void Update()
    {
        foreach(Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (!guis.Contains(g.gameObject)) guis.Add(g.gameObject);
        }
        

    }
}
