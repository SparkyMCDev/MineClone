using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public static readonly List<World> WORLDS = new List<World>();

    private readonly List<Chunk> chunks;
    private readonly long dimId;
    private readonly string registryName;
    private readonly long seed;

    public readonly FastNoise noise = new FastNoise();

    public World(long dimId, string registryName, long seed)
    {
        chunks = new List<Chunk>();
        this.dimId = dimId;
        this.registryName = registryName;
        this.seed = seed;
        noise.SetSeed((int)seed);

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

    public BlockHolder getBlockAt(BlockPos pos)
    {
        foreach(Chunk chunk in chunks)
        {
            BlockHolder block = chunk.getBlockAt(pos);
            if (block != null) return block;
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
        generateChunk(pos);
        generateChunk(new ChunkPos(pos.getX() + 1, pos.getZ()));

        foreach(Chunk c in chunks)
        {
            foreach(BlockHolder b in c.getBlocks()) {
                b.updateSides();
            }
        }
    }

    private void generateChunk(ChunkPos pos)
    {
        Chunk chunk = new Chunk(getId(), pos);
        chunk.generate();

        chunks.Add(chunk);
    }

    public void destroyBlockAt(BlockPos pos)
    {
        foreach(Chunk chunk in chunks)
        {
            BlockHolder block = chunk.getBlockAt(pos);


            if (block != null) { chunk.destroyBlock(block); return; }
        }
    }

    public long getSeed()
    {
        return seed;
    }
}
