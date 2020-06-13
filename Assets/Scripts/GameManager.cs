using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool cLock;
    private static World cWorld;

    public static long seed;

    private static GameManager Instance { get; set; }

    public static GameManager getInstance() { return Instance; }

    private void Awake()
    {
        Instance = this;

        seed = (long) Random.Range(0, 10000);

        cWorld = new World(0, "world", seed);

        cWorld.generate(new ChunkPos(0, 0));
     
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
