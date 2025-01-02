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
            if (neighbordId == -1) continue;
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
                return GetDirectionTowards(node, Direction.TopLeft);
            case LevelCodeName.level7:
                return GetDirectionTowards(node, Direction.TopRight);
            case LevelCodeName.level8:
                return GetDirectionTowards(node, Direction.BottomRight);
            case LevelCodeName.level9:
                return GetDirectionTowards(node, Direction.BottomLeft);
            case LevelCodeName.level10:
                return GetDirectionTowardsCenter(node);
            case LevelCodeName.level11:
                return GetDirectionTowardsHalfGrid(node);
            case LevelCodeName.level1:
            default:
                return 0;
        }
    }

    private enum Direction
    {
        BottomRight,
        BottomLeft,
        TopRight,
        TopLeft,
    }

    private int GetDirectionTowards(Node node, Direction direction)
    {
        Node targetNeighbor = null;

        switch (direction)
        {
            case Direction.BottomRight:
                targetNeighbor = GetBottomRightNeighbor(node);
                break;
            case Direction.BottomLeft:
                targetNeighbor = GetBottomLeftNeighbor(node);
                break;
            case Direction.TopRight:
                targetNeighbor = GetTopRightNeighbor(node);
                break;
            case Direction.TopLeft:
                targetNeighbor = GetTopLeftNeighbor(node);
                break;
        }

        if (targetNeighbor == node.up) return 0;
        if (targetNeighbor == node.right) return 1;
        if (targetNeighbor == node.down) return 2;
        if (targetNeighbor == node.left) return 3;
        if (targetNeighbor == node.topLeft) return 4;
        if (targetNeighbor == node.topRight) return 5;
        if (targetNeighbor == node.bottomRight) return 6;
        if (targetNeighbor == node.bottomLeft) return 7;

        return -1;
    }

    private Node GetBottomRightNeighbor(Node node)
    {
        if (node.bottomRight != null && !node.bottomRight.occupied && node.occupied && node.bottomRight.blockCtrl) return node.bottomRight;
        if (node.down != null && !node.down.occupied && node.occupied && node.down.blockCtrl) return node.down;
        if (node.right != null && !node.right.occupied && node.occupied && node.right.blockCtrl) return node.right;
        return null;
    }

    private Node GetBottomLeftNeighbor(Node node)
    {
        if (node.bottomLeft != null && !node.bottomLeft.occupied && node.occupied && node.bottomLeft.blockCtrl) return node.bottomLeft;
        if (node.down != null && !node.down.occupied && node.occupied && node.down.blockCtrl) return node.down;
        if (node.left != null && !node.left.occupied && node.occupied && node.left.blockCtrl) return node.left;
        return null;
    }

    private Node GetTopRightNeighbor(Node node)
    {
        if (node.topRight != null && !node.topRight.occupied && node.topRight.blockCtrl && node.occupied) return node.topRight;
        if (node.up != null && !node.up.occupied && node.occupied && node.up.blockCtrl) return node.up;
        if (node.right != null && !node.right.occupied && node.occupied && node.right.blockCtrl) return node.right;
        return null;
    }

    private Node GetTopLeftNeighbor(Node node)
    {
        if (node.topLeft != null && !node.topLeft.occupied && node.topLeft.blockCtrl && node.occupied) return node.topLeft;
        if (node.up != null && !node.up.occupied && node.occupied && node.up.blockCtrl) return node.up;
        if (node.left != null && !node.left.occupied && node.occupied && node.left.blockCtrl) return node.left;
        return null;
    }

    private int GetDirectionTowardsHalfGrid(Node node)
    {
        int centerX = gridCtrl.gridSystem.width / 2;

        if (node.x < centerX)
        {
            // Toward Left
            return 3;
        }
        else
        {
            // Toward Right
            return 1;
        }
    }

    private int GetDirectionTowardsCenter(Node node)
    {
        int centerX = gridCtrl.gridSystem.width / 2;

        if (node.x < centerX)
        {
            // Toward Left
            return 1;
        }
        else
        {
            // Toward Right
            return 3;
        }
    }

}
