using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : GridAbstract
{
    [Header("Block Handler")]
    public BlockCtrl firstBlock;
    public BlockCtrl lastBlock;
    protected bool nodeLinking = false;
    protected float freeNodesDelay = 0.5f;

    public virtual void SetNode(BlockCtrl blockCtrl)
    {
        Debug.Log("SetNode: " + blockCtrl.name);
        if (this.nodeLinking) return;
        if (this.IsBlockRemoved(blockCtrl)) return;

        Vector3 pos;
        Transform chooseObj;
        if (this.firstBlock == null)
        {
            this.ctrl.pathfinding.DataReset();
            this.firstBlock = blockCtrl;
            pos = blockCtrl.transform.position;
            chooseObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.CHOOSE, pos, Quaternion.identity);
            chooseObj.gameObject.SetActive(true);
            return;
        }

        this.lastBlock = blockCtrl;
        pos = blockCtrl.transform.position;
        chooseObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.CHOOSE, pos, Quaternion.identity);
        chooseObj.gameObject.SetActive(true);

        if (this.firstBlock != this.lastBlock
            && this.firstBlock.blockID == this.lastBlock.blockID)
        {
            bool isPathFound = this.ctrl.pathfinding.FindPath(this.firstBlock, this.lastBlock);
            if (isPathFound) this.LinkNodes();
        }
    }

    protected virtual bool IsBlockRemoved(BlockCtrl blockCtrl)
    {
        Node node = blockCtrl.blockData.node;
        return !node.occupied && node.blockPlaced;
    }

    protected virtual void LinkNodes()
    {
        this.nodeLinking = true;
        this.ctrl.linesDrawer.Drawing(this.ctrl.pathfinding.PathNodes, this.freeNodesDelay);
        Invoke(nameof(this.FreeBlocks), this.freeNodesDelay);
    }

    protected virtual void FreeBlocks()
    {
        this.ctrl.gridSystem.NodeFree(this.firstBlock.blockData.node);
        this.ctrl.gridSystem.NodeFree(this.lastBlock.blockData.node);

        this.firstBlock = null;
        this.lastBlock = null;
        this.nodeLinking = false;

        this.ctrl.gameLevel.GetCurrent().MoveBlocks();
    }

    public virtual void Unchoose()
    {
        this.firstBlock = null;
        this.lastBlock = null;
    }
}
