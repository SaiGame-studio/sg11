using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelAbstract
{
    public override void MoveBlocks()
    {
        Debug.Log("Move Up");

        BlockCtrl upperBlock;
        foreach (BlockCtrl blockCtrl in this.gridCtrl.gridSystem.blocks)
        {
            upperBlock = blockCtrl.neighbors[0];
            if (upperBlock == null || upperBlock.blockData.node.occupied) continue;
            Debug.Log("blockCtrl: " + blockCtrl.blockData.node.Name());
            this.gridCtrl.blockAuto.SwapBlocks(blockCtrl, upperBlock);
        }
    }
}
