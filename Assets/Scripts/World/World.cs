using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public static readonly List<World> WORLDS = new List<World>();

    private readonly List<Chunk> chunks;
    private readonly long dimId;
    private readonly string registryName;

    public World(long dimId, string registryName)
    {
        chunks = new List<Chunk>();
        this.dimId = dimId;
        this.registryName = registryName;

        WORLDS.Add(this);
    }

    public long getId()
    {
        return dimId;
    }

    public string getRegistryName()
    {
        return registryName;
    }

    public List<Chunk> getChunks()
    {
        return new List<Chunk>(chunks);
    }

    public Block getBlockAt(BlockPos pos)
    {
        foreach(Chunk c in chunks)
        {
            
        }
        return null;
    }

    public Chunk getChunkAt(ChunkPos pos)
    {
        foreach(Chunk c in chunks)
        {
            if (c.getPos().getX() == pos.getX() && c.getPos().getZ() == pos.getZ()) return c;
        }
        return null;
    }

    public void generate(ChunkPos pos)
    {
        for(int x = pos.getX()-5; x < pos.getX()+5; x++)
        {
            for(int z = pos.getZ()-5; z < pos.getZ()+5; z++)
            {
                generateChunk(new ChunkPos(x, z));
            }
        }
    }

    private void generateChunk(ChunkPos pos)
    {
        Chunk chunk = new Chunk(getId(), pos);
        chunk.generate();

        chunks.Add(chunk);
    }
}
