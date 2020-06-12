using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    public string blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis;
    private Block type;
    private BlockPos pos;
    private Transform[] sides;

    void Awake()
    {
        type = Block.parse(blockNameForTestingBecauseImTooLazyTooWriteMoreCodeForThis);

        pos = new BlockPos(0, 0, 0);

        updateSides();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateSides()
    {
        sides = transform.GetComponentsInChildren<Transform>();

        foreach (Transform side in sides)
        {
            if (!side.name.Equals("BlockHolder"))
            {
                side.gameObject.SetActive(false);
            }
        }

        if (pos == null) return;

        BlockPos leftPeripheral = new BlockPos(pos.getX() - 1, pos.getY(), pos.getZ());
        BlockPos rightPeripheral = new BlockPos(pos.getX() + 1, pos.getY(), pos.getZ());
        BlockPos topPeripheral = new BlockPos(pos.getX(), pos.getY() + 1, pos.getZ());
        BlockPos bottomPeripheral = new BlockPos(pos.getX(), pos.getY() - 1, pos.getZ());
        BlockPos frontPeripheral = new BlockPos(pos.getX(), pos.getY(), pos.getZ() + 1);
        BlockPos backPeripheral = new BlockPos(pos.getX(), pos.getY(), pos.getZ() - 1);

        if (GameManager.getCurrentWorld().getBlockAt(leftPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("left")) side.gameObject.SetActive(true);
            }
        }
        if (GameManager.getCurrentWorld().getBlockAt(rightPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("right")) side.gameObject.SetActive(true);
            }
        }
        if (GameManager.getCurrentWorld().getBlockAt(topPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("top")) side.gameObject.SetActive(true);
            }
        }
        if (GameManager.getCurrentWorld().getBlockAt(bottomPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("bottom")) side.gameObject.SetActive(true);
            }
        }
        if (GameManager.getCurrentWorld().getBlockAt(frontPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("front")) side.gameObject.SetActive(true);
            }
        }
        if (GameManager.getCurrentWorld().getBlockAt(backPeripheral) == null)
        {
            foreach (Transform side in sides)
            {
                if (side.name.Equals("back")) side.gameObject.SetActive(true);
            }
        }

        foreach (Transform side in sides)
        {
            if (!side.name.Equals("BlockHolder"))
            {
                side.gameObject.GetComponent<SpriteRenderer>().sprite = type.getTexture();
            }
        }
    }

    public BlockPos getPos()
    {
        return pos;
    }
}
