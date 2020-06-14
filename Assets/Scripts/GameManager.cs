using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Jobs;

public class GameManager : MonoBehaviour
{
    private static bool cLock;
    private static World cWorld;

    public static long seed;

    private static GameManager Instance { get; set; }

    public static GameManager getInstance() { return Instance; }

    public static bool Playing = true;
    public static bool InGui = false;

    public Settings settings;
    
    public Thread MainThread = Thread.CurrentThread;

    private void Awake()
    {
        Chunk.loadBlockTemplate();
        
        KeyCode KeyForward;
        KeyCode KeyBackward;
        KeyCode KeyLeft;
        KeyCode KeyRight;
        KeyCode KeyJump;
        KeyCode KeySneak;
        KeyCode KeySprint;
        KeyCode KeyBlockBreak;
        KeyCode KeyBlockPlace;
        int RenderDistance;
        int Fov;
        
        Enum.TryParse(PlayerPrefs.GetString("KeyForward", "W"), out KeyForward);
        Enum.TryParse(PlayerPrefs.GetString("KeyBackward", "S"), out KeyBackward);
        Enum.TryParse(PlayerPrefs.GetString("KeyLeft", "A"), out KeyLeft);
        Enum.TryParse(PlayerPrefs.GetString("KeyRight", "D"), out KeyRight);
        Enum.TryParse(PlayerPrefs.GetString("KeyJump", "Space"), out KeyJump);
        Enum.TryParse(PlayerPrefs.GetString("KeySneak", "LeftShift"), out KeySneak);
        Enum.TryParse(PlayerPrefs.GetString("KeySprint", "LeftControl"), out KeySprint);
        Enum.TryParse(PlayerPrefs.GetString("KeyBlockBreak", "Mouse0"), out KeyBlockBreak);
        Enum.TryParse(PlayerPrefs.GetString("KeyBlockPlace", "Mouse1"), out KeyBlockPlace);

        RenderDistance = PlayerPrefs.GetInt("RenderDistance", 10);
        Fov = PlayerPrefs.GetInt("Fov", 60);
        
        settings = new Settings(KeyForward, KeyBackward, KeyLeft, KeyRight, KeyJump, KeySneak, KeySprint, KeyBlockBreak, KeyBlockPlace, RenderDistance, Fov);
        
        Instance = this;

        seed = (long) Random.Range(0, 10000);

        cWorld = new World(0, "world", seed);
     
    }

    private void Update()
    {
        if (Playing && !InGui)
        {
            if (!cLock && Input.GetMouseButtonDown(0)) cLock = true;
            else if (cLock && Input.GetKeyDown(KeyCode.Escape)) cLock = false;
        } else if (Playing)
        {
            cLock = true;
        }

        if (InGui)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 playerPosition = PlayerController.getInstance().transform.position;
        
        BlockPos playerPos = new BlockPos(Mathf.FloorToInt(playerPosition.x), Mathf.FloorToInt(playerPosition.y), Mathf.FloorToInt(playerPosition.z));
        
        
        
        ChunkPos pos = cWorld.getChunkPosAt(playerPos);
        
        Debug.Log("x: "+pos.getX()+" - y: "+playerPos.getY()+" - y: "+pos.getZ());

        for (int x = pos.getX() - 1; x < pos.getX() + 1; x++)
        {
            for (int z = pos.getZ() - 1; z < pos.getZ() + 1; z++)
            {
                ChunkPos thisOne = new ChunkPos(x,z);
                if (cWorld.getChunkAt(thisOne) == null)
                {
                    cWorld.loadChunk(pos);
                }
            }
        }
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
