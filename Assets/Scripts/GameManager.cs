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

    

    private void Awake()
    {
        Chunk.loadBlockTemplate();
        
        
        
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
