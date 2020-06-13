﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        FastNoise noise = getWorld().noise;

        for(int x = 0+(pos.getX()*16); x < 16+(pos.getX()*16); x++)
        {
            for(int y = 0; y < 2; y++)
            {
                for(int z = 0+(pos.getZ()*16); z < 16+(pos.getZ()*16); z++)
                {
                    
                    float g = noise.GetSimplex(x, y, z);
                    Debug.Log(g);

                    GameObject block = GameObject.Instantiate(Resources.Load<GameObject>("models/blocktemplate"));
                    BlockHolder b = block.AddComponent<BlockHolder>();
                    b.pos = new BlockPos(x, y, z);
                    block.transform.position = new Vector3(x, y, z);
                    if(g<-0.03)
                    {
                        b.blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis = "dirt";
                    } else
                    {
                        b.blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis = "stone";
                    }
                    blocks.Add(b);
                    b.updateSides();
                }
            }
        }
        
        foreach(BlockHolder b in blocks)
        {
            b.updateSides();
        }
    }

    public void destroyBlock(BlockHolder block)
    {
        BlockHolder toDestroy = null;
        BlockPos pos = block.getPos();

        foreach (BlockHolder b in blocks)
        {
            if (block.getPos().getX() == b.getPos().getX())
            {
                if (block.getPos().getY() == b.getPos().getY())
                {
                    if (block.getPos().getZ() == b.getPos().getZ())
                    {
                        toDestroy = b;
                    }
                }
            }
        }

        if (toDestroy == null) { return; }
        else
        {

            blocks.Remove(toDestroy);
            GameObject.Destroy(toDestroy.gameObject);

            try { getBlockAt(new BlockPos(pos.getX() - 1, pos.getY(), pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getBlockAt(new BlockPos(pos.getX() + 1, pos.getY(), pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getBlockAt(new BlockPos(pos.getX(), pos.getY() - 1, pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getBlockAt(new BlockPos(pos.getX(), pos.getY() + 1, pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getBlockAt(new BlockPos(pos.getX(), pos.getY(), pos.getZ() - 1)).updateSides(); } catch(System.Exception e) { }
            try { getBlockAt(new BlockPos(pos.getX(), pos.getY(), pos.getZ() + 1)).updateSides(); } catch(System.Exception e) { }

        }
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

    public List<BlockHolder> getBlocks()
    {
        return blocks;
    }
}
