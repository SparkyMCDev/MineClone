using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPos
{
    private readonly int x;
    private readonly int y;
    private readonly int z;

    public BlockPos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public int getZ()
    {
        return z;
    }
}
