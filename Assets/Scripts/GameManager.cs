using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool cLock;
    private static World cWorld = new World(0, "world");

    private static GameManager Instance { get; set; }

    public static GameManager getInstance() { return Instance; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!cLock && Input.GetMouseButtonDown(0)) cLock = true;
        else if (cLock && Input.GetKeyDown(KeyCode.Escape)) cLock = false;
    }

    public static World getCurrentWorld()
    {
        return cWorld;
    }

    public static bool cursorLocked()
    {
        return cLock;
    }
}
