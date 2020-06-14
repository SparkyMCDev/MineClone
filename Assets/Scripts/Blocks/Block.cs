using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public static List<Block> BLOCKS = new List<Block>();

    public static Block DIRT = new Block("dirt", "dirt");
    public static Block STONE = new Block("stone", "stone");
    public static Block BEDROCK = new Block("bedrock", "bedrock");

    public static Block parse(string registryName)
    {
        foreach(Block block in BLOCKS) {
            if (block.getRegistryName().Equals(registryName)) return block;
        }
        return null;
    }

    private readonly string registryName;
    private readonly string texturePath;

    public Block(string regName, string tPath)
    {
        registryName = regName;
        texturePath = tPath;

        BLOCKS.Add(this);
    }

    public Sprite getTexture()
    {
        return Resources.Load<Sprite>("textures/blocks/" + texturePath);
    }

    public string getTexturePath()
    {
        return texturePath;
    }

    public string getRegistryName()
    {
        return registryName;
    }


}
