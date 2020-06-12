using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPos
{
    private readonly int x;
    private readonly int z;

    public ChunkPos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public int getX()
    {
        return x;
    }

    public int getZ()
    {
        return z;
    }
}
