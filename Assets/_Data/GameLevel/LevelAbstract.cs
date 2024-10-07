using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelAbstract : SaiMonoBehaviour
{
    [SerializeField] protected GridManagerCtrl gridCtrl;

    public abstract void MoveBlocks();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGridManagerCtrl();
    }

    protected virtual void LoadGridManagerCtrl()
    {
        if (this.gridCtrl != null) return;
        this.gridCtrl = transform.GetComponentInParent<GridManagerCtrl>();
        Debug.LogWarning(transform.name + " LoadGridManagerCtrl", gameObject);
    }

    protected virtual void LoopToMoveBlocks(LevelCodeName levelCode)
    {
        this.LoopToMoveBlocks(levelCode, 99);
    }

    protected virtual void LoopToMoveBlocks(LevelCodeName levelCode, int loopCount)
    {
        if (loopCount <= 0) return;
        Debug.Log("== Move Down " + loopCount + "====================");

        BlockCtrl upperBlock;
        bool isSwap = false;
        foreach (Node node in this.gridCtrl.gridSystem.nodes)
        {
            BlockCtrl blockCtrl = node.blockCtrl;
            if (blockCtrl == null) continue;
            int neighbordId = this.GetNeighbordId(node, levelCode);
            upperBlock = blockCtrl.neighbors[neighbordId];
            if (upperBlock == null || upperBlock.blockData.node.occupied) continue;
            if (!node.occupied && !upperBlock.blockData.node.occupied) continue;

            isSwap = this.gridCtrl.blockAuto.SwapBlocks(blockCtrl, upperBlock);
        }
        if (isSwap) this.LoopToMoveBlocks(levelCode, loopCount -= 1);
    }

    protected virtual int GetNeighbordId(Node node, LevelCodeName levelCode)
    {
        switch (levelCode)
        {
            case LevelCodeName.level2:
                return 0;
            case LevelCodeName.level3:
                return 2;
            case LevelCodeName.level4:
                return 1;
            case LevelCodeName.level5:
                return 3;
            case LevelCodeName.level6:
                //TODO: you can do it
                //From node check how to go in
                return 3;
            case LevelCodeName.level7:
                //TODO: you can do it
                //From node check how to go out
                return 3;
            case LevelCodeName.level1:
            default:
                return 0;
        }
    }
}
