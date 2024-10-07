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
        //Debug.Log("SetNode: " + blockCtrl.name);
        if (this.nodeLinking) return;
        if (this.IsBlockRemoved(blockCtrl)) return;

        if (this.firstBlock == null)
        {
            this.ctrl.pathfinding.DataReset();
            this.firstBlock = blockCtrl;
            this.ChooseBlock(blockCtrl);
            return;
        }

        this.lastBlock = blockCtrl;
        this.ChooseBlock(blockCtrl);

        bool isPathFound = false;
        if (this.firstBlock != this.lastBlock
            && this.firstBlock.blockID == this.lastBlock.blockID)
        {
            isPathFound = this.ctrl.pathfinding.FindPath(this.firstBlock, this.lastBlock);
            if (isPathFound) this.LinkNodes();

        }

        if(!isPathFound) Invoke(nameof(this.Unchoose),0.5f);
    }

    protected virtual void ChooseBlock(BlockCtrl blockCtrl)
    {
        //Spawn debug object
        //Transform chooseObj;
        //Vector3 pos = blockCtrl.transform.position;
        //chooseObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.CHOOSE, pos, Quaternion.identity);
        //chooseObj.gameObject.SetActive(true);

        //If you got error here the create new layer with id is 1 and make it over the default layer
        blockCtrl.sortingGroup.sortingOrder = 1;
        blockCtrl.blockBackground.gameObject.SetActive(true);
    }

    protected virtual void UnchooseBlock(BlockCtrl blockCtrl)
    {
        //If you got error here make sure your default layer is 0
        blockCtrl.sortingGroup.sortingOrder = 0;
        blockCtrl.blockBackground.gameObject.SetActive(false);
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

        this.Unchoose();
        this.nodeLinking = false;

        this.ctrl.gameLevel.GetCurrent().MoveBlocks();
    }

    public virtual void Unchoose()
    {
        this.UnchooseBlock(this.firstBlock);
        this.UnchooseBlock(this.lastBlock);

        this.firstBlock = null;
        this.lastBlock = null;
    }
}
