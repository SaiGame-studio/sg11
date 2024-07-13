using System.Collections.Generic;
using UnityEngine;

public class LinesSearch : AbstractPathfinding
{
    [Header("Lines Search")]
    public GridManagerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<GridManagerCtrl>();
        Debug.LogWarning(transform.name + " LoadCtrl", gameObject);
    }

    public override void DataReset()
    {

    }

    public override bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        if (this.IsLineBlocked(startBlock, targetBlock))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsLineBlocked(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        if (!this.IsLineUp(startBlock, targetBlock)) return true;
        Debug.Log("== IsLineUp ============================");
        Debug.Log("startBlock: " + startBlock.blockData.node.x + "/" + startBlock.blockData.node.y);
        Debug.Log("targetBlock: " + targetBlock.blockData.node.x + "/" + targetBlock.blockData.node.y);

        if (startBlock.blockData.node.y == targetBlock.blockData.node.y)
        {
            if (this.IsLineBlockedX(startBlock, targetBlock)) return true;
        }

        if (startBlock.blockData.node.x == targetBlock.blockData.node.x)
        {
            if (this.IsLineBlockedY(startBlock, targetBlock)) return true;
        }

        return false;
    }

    protected virtual List<int> GetNumberBettwen(int start, int end)
    {
        List<int> numbers = new List<int>();
        if (start > end)
        {
            for (int i = start - 1; i > end; i--)
            {
                numbers.Add(i);
            }
            return numbers;
        }

        for (int i = start + 1; i < end; i++)
        {
            numbers.Add(i);
        }
        return numbers;
    }

    protected virtual bool IsLineBlockedX(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        int y = startBlock.blockData.node.y;
        List<int> numbers = this.GetNumberBettwen(startBlock.blockData.node.x, targetBlock.blockData.node.x);
        if (numbers.Count == 0) return false;
        foreach (var x in numbers)
        {
            Node node = this.GetNode(x, y);
            if (node.occupied)
            {
                Debug.Log(node.Name() + " node.occupied: " + node.occupied);
                return true;
            }
        }
        return false;
    }

    protected virtual bool IsLineBlockedY(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        int x = startBlock.blockData.node.x;
        List<int> numbers = this.GetNumberBettwen(startBlock.blockData.node.y, targetBlock.blockData.node.y);
        if (numbers.Count == 0) return false;
        foreach (var y in numbers)
        {
            Node node = this.GetNode(x, y);
            if (node.occupied)
            {
                Debug.Log(node.Name() + " node.occupied: " + node.occupied);
                return true;
            };
        }
        return false;
    }

    protected virtual bool IsLineUp(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        if (startBlock.blockData.node.x == targetBlock.blockData.node.x) return true;
        if (startBlock.blockData.node.y == targetBlock.blockData.node.y) return true;
        return false;
    }

    protected virtual Node GetNode(int x, int y)
    {
        return GridManagerCtrl.Instance.gridSystem.GetNodeByXY(x, y);
    }
}
