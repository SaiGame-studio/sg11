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
        List<BlockCtrl> sameBlocks = new List<BlockCtrl>();
        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            sameBlocks.Clear();
            sameBlocks = this.GetSameBlocks(blockCtrl);
            foreach (BlockCtrl sameBlock in sameBlocks)
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
        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            if (blockCtrl == checkBlock) continue;
            if (blockCtrl.blockID == checkBlock.blockID) sameBlocks.Add(blockCtrl);
        }

        return sameBlocks;
    }

    public virtual void ShuffleBlocks()
    {
        BlockCtrl randomBlock;

        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            randomBlock = this.ctrl.gridSystem.GetRandomBlock();
            if (randomBlock.name == blockCtrl.name) continue;
            this.SwapBlocks(blockCtrl, randomBlock);
        }
    }

    public bool SwapBlocks(BlockCtrl firstBlock, BlockCtrl secondBlock)
    {
        if (firstBlock == secondBlock) return false;
        Debug.Log("== SwapBlocks ====================");

        this.SwapBlockData(firstBlock, secondBlock);
        this.SwapModel(firstBlock, secondBlock);
        this.SwapNodeData(firstBlock.blockData.node, secondBlock.blockData.node);
        return true;
    }

    protected virtual void SwapModel(BlockCtrl firstBlock, BlockCtrl secondBlock)
    {

        Transform firstModel = firstBlock.model;
        Transform secondModel = secondBlock.model;

        Transform firstParent = firstModel.parent;
        Transform secondParent = secondModel.parent;

        firstModel.parent = secondParent;
        secondModel.parent = firstParent;

        firstBlock.ReloadModel();
        secondBlock.ReloadModel();

        firstModel.localPosition = Vector3.zero;
        secondModel.localPosition = Vector3.zero;
    }

    protected virtual void SwapBlockData(BlockCtrl firstBlock, BlockCtrl secondBlock)
    {
        Sprite tempSprite = firstBlock.sprite;
        string tempBlockId = firstBlock.blockID;

        firstBlock.sprite = secondBlock.sprite;
        firstBlock.blockID = secondBlock.blockID;

        secondBlock.sprite = tempSprite;
        secondBlock.blockID = tempBlockId;
    }


    protected virtual void SwapNodeData(Node firstNode, Node secondNode)
    {
        Node tempNode = secondNode.Clone();

        Debug.Log("secondNode: " + secondNode.Name() + " / " + secondNode.occupied, secondNode.blockCtrl.gameObject);
        Debug.Log("firstNode: " + firstNode.Name() + " / " + firstNode.occupied, firstNode.blockCtrl.gameObject);
        Debug.Log("tempNode: " + tempNode.Name() + " / " + tempNode.occupied, tempNode.blockCtrl.gameObject);

        secondNode.occupied = firstNode.occupied;
        firstNode.occupied = tempNode.occupied;

        Debug.Log("secondNode: " + secondNode.Name() + " / " + secondNode.occupied, secondNode.blockCtrl.gameObject);
        Debug.Log("firstNode: " + firstNode.Name() + " / " + firstNode.occupied, firstNode.blockCtrl.gameObject);
        Debug.Log("tempNode: " + tempNode.Name() + " / " + tempNode.occupied, tempNode.blockCtrl.gameObject);
    }
}
