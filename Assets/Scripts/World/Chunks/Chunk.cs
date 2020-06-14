using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Chunk
{
    private readonly long dimId;
    private readonly ChunkPos pos;
    private readonly List<BlockHolder> blocks = new List<BlockHolder>();

    public static GameObject blockTemplate;

    public static void loadBlockTemplate()
    {
        blockTemplate = Resources.Load<GameObject>("models/blocktemplate");
    }

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
        GameManager.getInstance().StartCoroutine(blockMake());
    }

    IEnumerator blockMake()
    {
        
        for(int x = 0+(pos.getX()*16); x < 16+(pos.getX()*16); x++)
        {
            for(int y = 0; y < 4; y++)
            {
                for(int z = 0+(pos.getZ()*16); z < 16+(pos.getZ()*16); z++)
                {
                    GameObject block = GameObject.Instantiate(blockTemplate);
                    
                    BlockHolder b = block.AddComponent<BlockHolder>();
                    b.pos = new BlockPos(x, y, z);
                    block.transform.position = new Vector3(x, y, z);

                    if (y == 3)
                    {
                        b.blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis = "dirt";
                    }
                    else if (y == 0)
                    {
                        b.blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis = "bedrock";
                    }
                    else
                    {
                        b.blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis = "stone";
                    }
                    
                    b.updateSides();

                    blocks.Add(b);
                    
                    yield return new WaitForSeconds(0.1f);
                }
            }
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

            try { getWorld().getBlockAt(new BlockPos(pos.getX() - 1, pos.getY(), pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getWorld().getBlockAt(new BlockPos(pos.getX() + 1, pos.getY(), pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getWorld().getBlockAt(new BlockPos(pos.getX(), pos.getY() - 1, pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getWorld().getBlockAt(new BlockPos(pos.getX(), pos.getY() + 1, pos.getZ())).updateSides(); } catch(System.Exception e) { }
            try { getWorld().getBlockAt(new BlockPos(pos.getX(), pos.getY(), pos.getZ() - 1)).updateSides(); } catch(System.Exception e) { }
            try { getWorld().getBlockAt(new BlockPos(pos.getX(), pos.getY(), pos.getZ() + 1)).updateSides(); } catch(System.Exception e) { }

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
