using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDebug : GridAbstract
{
    [Header("Block Debug")]
    private static BlockDebug instance;
    public static BlockDebug Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (BlockDebug.instance != null) Debug.LogError("Only 1 BlockDebug allow to exist");
        BlockDebug.instance = this;
    }

    protected virtual void Update()
    {
        this.DeleteBlock();
    }

    protected virtual void DeleteBlock()
    {
        if (!Input.GetKey(KeyCode.Delete)) return;
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
        List<string> names = new List<string>();
        names.Add(BlockSpawner.LINKER);
        names.Add(BlockSpawner.SCAN);
        names.Add(BlockSpawner.SCAN_STEP);
        names.Add(BlockSpawner.CHOOSE);

        foreach (Transform clone in BlockSpawner.Instance.Holder)
        {
            if (names.Contains(clone.name)) BlockSpawner.Instance.Despawn(clone);
        }
    }
}
