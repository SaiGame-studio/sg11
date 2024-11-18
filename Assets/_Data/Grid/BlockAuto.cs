using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAuto : GridAbstract
{
    [Header("Block Auto")]
    public bool isNextBlockExist = false;
    public BlockCtrl firstBlock;
    public BlockCtrl secondBlock;
    public BlockCtrl hintBlock;
    public BlockCtrl lastBlock;

    public virtual void ClearBlocks()
    {
        this.hintBlock = this.firstBlock = this.secondBlock = null;
    }

    public virtual void CheckNextBlock()
    {
        this.isNextBlockExist = true;
        this.ClearBlocks();
        List<BlockCtrl> sameBlocks = new();
        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            sameBlocks.Clear();
            sameBlocks = this.GetSameBlocks(blockCtrl);
            foreach (BlockCtrl sameBlock in sameBlocks)
            {
                this.ctrl.pathfinding.DataReset();
                bool found = GridManagerCtrl.Instance.pathfinding.FindPath(blockCtrl, sameBlock);
                if (!found) continue;
                this.firstBlock = blockCtrl;
                this.secondBlock = sameBlock;
            }
        }

        if (this.firstBlock == null) this.isNextBlockExist = false;
    }

    public virtual BlockCtrl LoadHintBlock()
    {
        this.CheckNextBlock();
        this.hintBlock = this.firstBlock;
        if(hintBlock != null)
        {
            GameManager.Instance.UseHint();
        }
        return this.hintBlock;
    }

    protected virtual List<BlockCtrl> GetSameBlocks(BlockCtrl checkBlock)
    {
        List<BlockCtrl> sameBlocks = new();
        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            if (blockCtrl == checkBlock) continue;
            if (!blockCtrl.blockData.node.occupied) continue;
            if (!checkBlock.blockData.node.occupied) continue;
            if (blockCtrl.blockID == checkBlock.blockID) sameBlocks.Add(blockCtrl);
        }

        return sameBlocks;
    }

    public virtual void ShuffleBlocks()
    {
        BlockCtrl randomBlock;

        foreach (BlockCtrl blockCtrl in this.ctrl.gridSystem.blocks)
        {
            if (!blockCtrl.IsOccupied()) continue;
            randomBlock = this.ctrl.gridSystem.GetRandomBlock();
            if (randomBlock.name == blockCtrl.name) continue;
            this.SwapBlocks(blockCtrl, randomBlock);
        }
        this.CheckNextBlock();
        GameManager.Instance.UseShuffle();
    }

    public bool SwapBlocks(BlockCtrl firstBlock, BlockCtrl secondBlock)
    {
        if (this.lastBlock == firstBlock) return false;
        if (firstBlock == secondBlock) return false;
        if (firstBlock == null && secondBlock == null) return false;
        //Debug.Log(firstBlock.blockData.node.Name() + " / " + secondBlock.blockData.node.Name());

        this.SwapBlockData(firstBlock, secondBlock);
        this.SwapModel(firstBlock, secondBlock);
        this.SwapNodeData(firstBlock.blockData.node, secondBlock.blockData.node);
        this.lastBlock = firstBlock;
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

        //Debug.Log("secondNode: " + secondNode.Name() + " / " + secondNode.occupied, secondNode.blockCtrl.gameObject);
        //Debug.Log("firstNode: " + firstNode.Name() + " / " + firstNode.occupied, firstNode.blockCtrl.gameObject);
        //Debug.Log("tempNode: " + tempNode.Name() + " / " + tempNode.occupied, tempNode.blockCtrl.gameObject);

        secondNode.occupied = firstNode.occupied;
        firstNode.occupied = tempNode.occupied;

        //Debug.Log("secondNode: " + secondNode.Name() + " / " + secondNode.occupied, secondNode.blockCtrl.gameObject);
        //Debug.Log("firstNode: " + firstNode.Name() + " / " + firstNode.occupied, firstNode.blockCtrl.gameObject);
        //Debug.Log("tempNode: " + tempNode.Name() + " / " + tempNode.occupied, tempNode.blockCtrl.gameObject);
    }
}
