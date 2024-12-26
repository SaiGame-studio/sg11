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
                return GetDirectionTowards(node, Direction.Center);
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
        Center
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
            case Direction.Center:
                targetNeighbor = GetCenterNeighbor(node);
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

    private Vector2 GetGridCenter()
    {
        // Calculate center based on grid dimensions
        float centerX = (gridCtrl.gridSystem.width + 1) * (1 - gridCtrl.gridSystem.offsetX) / 2f;
        float centerY = (gridCtrl.gridSystem.height - 1) / 2f;
        return new Vector2(centerX, centerY);
    }

    private Node GetCenterNeighbor(Node node)
    {
        if (!node.occupied) return null;

        Vector2 centerPos = GetGridCenter();
        Vector2 nodePos = new Vector2(node.x, node.y);

        // Lấy phần tư
        bool isAboveCenter = node.y > centerPos.y;
        bool isRightOfCenter = node.x > centerPos.x;

        // Check các node lân cận theo thứ tự ưu tiên 
        List<Node> priorityNeighbors = new List<Node>();

        if (isAboveCenter && isRightOfCenter) // Góc phần tư trên phải
        {
            Node priorityNode = GetBottomLeftNeighbor(node);
            if (priorityNode != null) priorityNeighbors.Add(priorityNode);
        }
        else if (isAboveCenter && !isRightOfCenter) // Góc phần tư trên trái
        {
            Node priorityNode = GetBottomRightNeighbor(node);
            if (priorityNode != null) priorityNeighbors.Add(priorityNode);
        }
        else if (!isAboveCenter && isRightOfCenter) // Góc phần tư dưới phải
        {
            Node priorityNode = GetTopLeftNeighbor(node);
            if (priorityNode != null) priorityNeighbors.Add(priorityNode);
        }
        else // Góc phần tư dưới trái
        {
            Node priorityNode = GetTopRightNeighbor(node);
            if (priorityNode != null) priorityNeighbors.Add(priorityNode);
        }

        // Kiểm tra xem có node lân cận nào gần center hơn không
        foreach (Node neighbor in priorityNeighbors)
        {
            Vector2 neighborPos = new Vector2(neighbor.x, neighbor.y);

            if (Vector2.Distance(neighborPos, centerPos) < Vector2.Distance(nodePos, centerPos))
            {
                bool hasNearbyBlocks = CheckNearbyBlocks(neighbor);
                if (hasNearbyBlocks) return neighbor;
            }
        }

        foreach (Node neighbor in priorityNeighbors)
        {
            if (HasEmptySpaceNearby(neighbor)) return neighbor;
        }

        return null;
    }

    private bool CheckNearbyBlocks(Node node)
    {
        // Kiểm tra xem có ít nhất một block lân cận không
        if (node.up != null && node.up.occupied) return true;
        if (node.down != null && node.down.occupied) return true;
        if (node.left != null && node.left.occupied) return true;
        if (node.right != null && node.right.occupied) return true;
        if (node.topLeft != null && node.topLeft.occupied) return true;
        if (node.topRight != null && node.topRight.occupied) return true;
        if (node.bottomLeft != null && node.bottomLeft.occupied) return true;
        if (node.bottomRight != null && node.bottomRight.occupied) return true;

        return false;
    }

    private bool HasEmptySpaceNearby(Node node)
    {
        int emptyCount = 0;
        int occupiedCount = 0;

        void CheckNode(Node n)
        {
            if (n != null)
            {
                if (n.occupied) occupiedCount++;
                else if (n.blockCtrl != null) emptyCount++;
            }
        }

        CheckNode(node.up);
        CheckNode(node.down);
        CheckNode(node.left);
        CheckNode(node.right);
        CheckNode(node.topLeft);
        CheckNode(node.topRight);
        CheckNode(node.bottomLeft);
        CheckNode(node.bottomRight);

        // Nếu có nhiều node trống hơn node có block, ưu tiên lấp chỗ trống
        return emptyCount > occupiedCount;
    }

}
