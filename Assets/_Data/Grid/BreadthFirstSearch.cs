using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract, IPathfinding
{
    [Header("Breadth First Search")]
    public Queue<BlockCtrl> queue = new Queue<BlockCtrl>();
    public Dictionary<BlockCtrl, BlockCtrl> cameFrom = new Dictionary<BlockCtrl, BlockCtrl>();

    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");

        this.queue.Enqueue(startBlock);
        this.cameFrom[startBlock] = startBlock;

        while (this.queue.Count > 0)
        {
            BlockCtrl current = this.queue.Dequeue();

            if (current == targetBlock)
                break;

            foreach (BlockCtrl neighbor in current.neighbors)
            {

                if (this.IsValidPosition(neighbor) && !cameFrom.ContainsKey(neighbor))
                {
                    this.queue.Enqueue(neighbor);
                    this.cameFrom[neighbor] = current;
                }
            }
        }

        this.ShowCameFrom();
    }

    protected virtual void ShowCameFrom()
    {
        foreach (var pair in cameFrom)
        {
            BlockCtrl key = pair.Key;
            BlockCtrl value = pair.Value;

            Debug.Log("Left: " + key.ToString() + ", Right: " + value.ToString());
        }
    }

    private bool IsValidPosition(BlockCtrl block)
    {
        return true;//TODO: tinh sau
    }
}
