using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAuto : GridAbstract
{
    //[Header("Block Auto")]
    
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
                    Debug.Log("==================================");
                    Debug.Log("blockCtrl: " + blockCtrl.blockData.node.Name());
                    Debug.Log("sameBlock: " + sameBlock.blockData.node.Name());
                    return;
                }
                else
                {
                    Debug.Log("NotFound");
                }
            }
        }
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



}
