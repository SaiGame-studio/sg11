using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : GridAbstract
{
    [Header("Block Handler")]
    public BlockCtrl firstBlock;
    public BlockCtrl lastBlock;

    public virtual void SetNode(BlockCtrl blockCtrl)
    {
        Vector3 pos;
        Transform chooseObj;
        if (this.firstBlock == null)
        {
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

        if(this.firstBlock.blockID == this.lastBlock.blockID)
        {
            bool isPathFound = this.ctrl.pathfinding.FindPath(this.firstBlock, this.lastBlock);
            if (isPathFound) this.FreeBlocks();
        }

        this.firstBlock = null;
        this.lastBlock = null;
        this.ctrl.pathfinding.DataReset();
    }

    protected virtual void FreeBlocks()
    {
        this.ctrl.gridSystem.NodeFree(this.firstBlock.blockData.node);
        this.ctrl.gridSystem.NodeFree(this.lastBlock.blockData.node);
    }
}
