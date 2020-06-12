using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private readonly long dimId;
    private readonly ChunkPos pos;
    private readonly List<BlockHolder> blocks = new List<BlockHolder>();

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

    public void destroyBlock(BlockHolder block)
    {
        BlockHolder toDestroy = null;

        foreach(BlockHolder b in blocks)
        {
            if(block.getPos().getX()==b.getPos().getX())
            {
                if(block.getPos().getY()==b.getPos().getY())
                {
                    if(block.getPos().getZ()==b.getPos().getZ())
                    {
                        toDestroy = b;
                    }
                }
            }
        }

        if (toDestroy == null) return;

        blocks.Remove(toDestroy);
        GameObject.Destroy(toDestroy.gameObject);
    }

    public BlockHolder getBlockAt(BlockPos pos)
    {
        foreach(BlockHolder block in blocks)
        {
            if (block.getPos().getX() == pos.getX())
            {
                if (block.getPos().getY() == pos.getY())
                {
                    if (block.getPos().getZ() == pos.getZ())
                    {
                        return block;
                    }
                }
            }
        }
        return null;
    }
}
