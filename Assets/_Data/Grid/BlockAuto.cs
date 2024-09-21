using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAuto : GridAbstract
{
    [Header("Block Auto")]
    public BlockCtrl firstBlock;
    public BlockCtrl secondBlock;


    public virtual void ShowHint()
    {
        Debug.LogWarning("ShowHint");

        List<BlockCtrl> sameBlocks = new List<BlockCtrl>();
        foreach(BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            sameBlocks.Clear();
            sameBlocks = this.GetSameBlocks(blockCtrl);
            foreach(BlockCtrl sameBlock in sameBlocks)
            {
                this.ctrl.pathfinding.DataReset();
                bool found = GridManagerCtrl.Instance.pathfinding.FindPath(blockCtrl, sameBlock);
                if (found)
                {
                    this.firstBlock = blockCtrl;
                    this.secondBlock = sameBlock;
                    return;
                }
            }
        }

        Debug.Log("Not Found");
    }

    protected virtual List<BlockCtrl> GetSameBlocks(BlockCtrl checkBlock)
    {
        List<BlockCtrl> sameBlocks = new List<BlockCtrl>();
        foreach(BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            if (blockCtrl == checkBlock) continue;
            if (blockCtrl.blockID == checkBlock.blockID) sameBlocks.Add(blockCtrl);
        }

        return sameBlocks;
    }

    public virtual void ShuffleBlocks()
    {
        BlockCtrl randomBlock;

        foreach(BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            randomBlock = this.ctrl.gridSystem.GetRandomBlock();
            if (randomBlock.name == blockCtrl.name) continue;
            this.SwapBlocks(blockCtrl, randomBlock);
        }
    }

    protected virtual void SwapBlocks(BlockCtrl blockCtrl, BlockCtrl randomBlock) {
        if (blockCtrl == randomBlock) return;
        BlockCtrl temp = blockCtrl.Clone();
        //BlockCtrl temp = blockCtrl;
        Node tempNode = temp.blockData.node;

        blockCtrl.sprite = randomBlock.sprite;
        blockCtrl.blockData.node = randomBlock.blockData.node;
        blockCtrl.blockData.SetSprite(blockCtrl.sprite);

        randomBlock.sprite = temp.sprite;
        randomBlock.blockData.node = tempNode;
        randomBlock.blockData.SetSprite(randomBlock.sprite);
    }
}
