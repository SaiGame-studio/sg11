using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelAbstract
{
    public override void MoveBlocks()
    {
        Debug.Log("Move Up ====================");

        BlockCtrl upperBlock;
        bool isSwap;
        foreach (Node node in this.gridCtrl.gridSystem.nodes)
        {
            BlockCtrl blockCtrl = node.blockCtrl;
            if (blockCtrl == null) continue;
            Debug.Log("Block " + blockCtrl.name, blockCtrl.gameObject);
            upperBlock = blockCtrl.neighbors[0];
            if (upperBlock == null || upperBlock.blockData.node.occupied) continue;
            Debug.Log("blockCtrl: " + blockCtrl.blockData.node.Name(), blockCtrl.gameObject);
            isSwap = this.gridCtrl.blockAuto.SwapBlocks(blockCtrl, upperBlock);
           // this.MoveBlocks();
        }
    }

    public virtual void _MoveBlocks()
    {
        Debug.Log("Move Up ====================");

        BlockCtrl upperBlock;
        bool isSwap;
        foreach (BlockCtrl blockCtrl in this.gridCtrl.gridSystem.blocks)
        {
            Debug.Log("Block " + blockCtrl.name, blockCtrl.gameObject);
            upperBlock = blockCtrl.neighbors[0];
            if (upperBlock == null || upperBlock.blockData.node.occupied) continue;
            Debug.Log("blockCtrl: " + blockCtrl.blockData.node.Name(), blockCtrl.gameObject);
            isSwap = this.gridCtrl.blockAuto.SwapBlocks(blockCtrl, upperBlock);
        }
    }
}
