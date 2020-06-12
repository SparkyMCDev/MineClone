using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private readonly long dimId;
    private readonly ChunkPos pos;

    public Chunk(long dimId, ChunkPos pos)
    {
        this.dimId = dimId;
        this.pos = pos;
    }

    public World getWorld()
    {
        foreach(World world in World.WORLDS)
        {
            if (world.getId() == dimId) return world;
        }
        return null;
    }

    public ChunkPos getPos()
    {
        return pos;
    }

    public void generate()
    {

    }
}
