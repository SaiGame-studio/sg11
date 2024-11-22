using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : GridAbstract
{
    [Header("Block Handler")]
    public BlockCtrl firstBlock;
    public BlockCtrl secondBlock;
    [SerializeField] protected float freeNodesDelay = 0.5f;

    public virtual void SetNode(BlockCtrl blockCtrl)
    {
        if (this.IsBlockRemoved(blockCtrl)) return;

        if (this.firstBlock == null)
        {
            this.ctrl.pathfinding.DataReset();
            this.firstBlock = blockCtrl;
            this.ChooseBlock(blockCtrl);
            SoundManager.Instance.PlaySound(SoundManager.Sound.click);
            return;
        }

        if (this.secondBlock != null)
        {
            return;
        }

        this.secondBlock = blockCtrl;
        this.ChooseBlock(blockCtrl);

        bool isPathFound = false;
        if (this.firstBlock != this.secondBlock
            && this.firstBlock.blockID == this.secondBlock.blockID)
        {
            isPathFound = this.ctrl.pathfinding.FindPath(this.firstBlock, this.secondBlock);
            if (isPathFound)
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.linked);
                this.LinkNodes();
                this.ctrl.blockAuto.ClearBlocks();
            }

        }

        if(!isPathFound)
        {
            Invoke(nameof(this.Unchoose), 0.5f);
            SoundManager.Instance.PlaySound(SoundManager.Sound.oho);
        }
    }

    protected virtual void ChooseBlock(BlockCtrl blockCtrl)
    {
        //If you got error here the create new layer with id is 1 and make it over the default layer
        blockCtrl.sortingGroup.sortingOrder = 1;
        blockCtrl.blockBackground.gameObject.SetActive(true);
    }

    public virtual void UnchooseBlock(BlockCtrl blockCtrl)
    {
        if (blockCtrl == null) return;
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
        this.ctrl.linesDrawer.Drawing(this.ctrl.pathfinding.PathNodes, this.freeNodesDelay);
        Invoke(nameof(this.FreeBlocks), this.freeNodesDelay);
    }

    protected virtual void FreeBlocks()
    {
        this.ctrl.gridSystem.NodeFree(this.firstBlock.blockData.node);
        this.ctrl.gridSystem.NodeFree(this.secondBlock.blockData.node);

        this.Unchoose();
        this.ctrl.gameLevel.GetCurrentLevelObj().MoveBlocks();
        this.ctrl.blockAuto.CheckNextBlock();
    }

    public virtual void Unchoose()
    {
        this.UnchooseBlock(this.firstBlock);
        this.UnchooseBlock(this.secondBlock);

        this.firstBlock = null;
        this.secondBlock = null;
    }
}
