using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour
{
    private GameObject Interface;
    void Awake()
    {
        if (Interface == null) Interface = Resources.Load<GameObject>("prefabs/gui/unknown");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
