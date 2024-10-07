using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDebug : GridAbstract
{
    [Header("Block Debug")]
    private static BlockDebug instance;
    public static BlockDebug Instance => instance;
    public bool continuePlay = true;
    public float autoPlaySpeed = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        if (BlockDebug.instance != null) Debug.LogError("Only 1 BlockDebug allow to exist");
        BlockDebug.instance = this;
    }

    public virtual void DeleteFirstBlock()
    {
        if (!InputManager.Instance.isDebug) return;
        if (this.ctrl.blockHandler.firstBlock == null) return;
        BlockCtrl block = this.ctrl.blockHandler.firstBlock;
        this.ctrl.gridSystem.NodeFree(block.blockData.node);
        this.ctrl.blockHandler.firstBlock = null;
    }

    public virtual void NodeUndo()
    {
        int lastIndex = this.ctrl.gridSystem.freeNodes.Count - 1;
        if (lastIndex < 0) return;
        Node node = this.ctrl.gridSystem.freeNodes[lastIndex];
        this.ctrl.gridSystem.freeNodes.RemoveAt(lastIndex);

        node.occupied = true;
        Sprite sprite = node.blockCtrl.sprite;
        node.blockCtrl.SetSprite(sprite);
        this.ctrl.gridSystem.blocks.Add(node.blockCtrl);
    }

    public virtual void ClearDebug()
    {
        List<string> names = new();
        names.Add(BlockSpawner.LINKER);
        names.Add(BlockSpawner.SCAN);
        names.Add(BlockSpawner.SCAN_STEP);
        names.Add(BlockSpawner.CHOOSE);

        foreach (Transform clone in BlockSpawner.Instance.Holder)
        {
            if (names.Contains(clone.name)) BlockSpawner.Instance.Despawn(clone);
        }
    }

    public virtual void AutoPlay()
    {
        GridManagerCtrl.Instance.blockAuto.CheckNextBlock();

        Invoke(nameof(this.AutoClickBlocks), this.autoPlaySpeed);
    }

    protected virtual void AutoClickBlocks()
    {
        BlockCtrl firstBlock = GridManagerCtrl.Instance.blockAuto.firstBlock;
        BlockCtrl secondBlock = GridManagerCtrl.Instance.blockAuto.secondBlock;

        if (!this.IsBlocksMatching(firstBlock, secondBlock))
        {
            Debug.Log("== No more Move ==============");
            CancelInvoke();
            return;
        }

        //Debug.Log("==== AutoPlay ==============================");
        //Debug.Log("blockCtrl: " + firstBlock.blockData.node.Name(), firstBlock.gameObject);
        //Debug.Log("sameBlock: " + secondBlock.blockData.node.Name(), secondBlock.gameObject);

        GridManagerCtrl.Instance.blockHandler.SetNode(firstBlock);
        GridManagerCtrl.Instance.blockHandler.SetNode(secondBlock);

        this.ClearDebug();

        if (this.continuePlay) Invoke(nameof(this.AutoPlay), this.autoPlaySpeed);
    }

    protected virtual bool IsBlocksMatching(BlockCtrl firstBlock, BlockCtrl secondBlock)
    {
        if (firstBlock == null || secondBlock == null) return false;
        if (firstBlock.blockID != secondBlock.blockID) return false;
        if (!firstBlock.IsOccupied() || !secondBlock.IsOccupied()) return false;

        return true;
    }

}
