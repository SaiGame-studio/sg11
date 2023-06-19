using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract, IPathfinding
{
    [Header("Breadth First Search")]
    public List<Node> queue = new List<Node>();
    public List<Node> path = new List<Node>();
    public List<NodeCameFrom> cameFromNodes = new List<NodeCameFrom>();
    public List<Node> visited = new List<Node>();

    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;

        this.Enqueue(startNode);
        this.cameFromNodes.Add(new NodeCameFrom(startNode, startNode));
        this.visited.Add(startNode);

        while (this.queue.Count > 0)
        {
            Node current = this.Dequeue();

            if (current == targetNode)
            {
                this.path = BuildNodePath(startNode, targetNode);
                break;
            }

            foreach (Node neighbor in current.Neighbors())
            {
                if (neighbor == null) continue;
                if (this.visited.Contains(neighbor)) continue;
                if (!this.IsValidPosition(neighbor, targetNode)) continue;

                this.Enqueue(neighbor);
                this.visited.Add(neighbor);
                this.cameFromNodes.Add(new NodeCameFrom(neighbor, current));
            }
        }

        this.ShowVisited();
        this.ShowPath();
    }

    protected virtual void ShowVisited()
    {
        foreach (Node node in this.visited)
        {
            Vector3 pos = node.nodeObj.transform.position;
            Transform keyObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.SCAN, pos, Quaternion.identity);
            keyObj.gameObject.SetActive(true);
        }
    }

    protected virtual List<Node> BuildNodePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentCell = targetNode;

        while (currentCell != startNode)
        {
            path.Add(currentCell);
            currentCell = this.GetCameFrom(currentCell);
        }

        path.Add(startNode);
        path.Reverse();

        return path;
    }

    protected virtual Node GetCameFrom(Node node)
    {
        return this.GetNodeCameFrom(node).fromNode;
    }

    protected virtual NodeCameFrom GetNodeCameFrom(Node node)
    {
        return this.cameFromNodes.Find(item => item.toNode == node);
    }

    protected virtual void ShowPath()
    {
        Vector3 pos;
        foreach (Node node in this.path)
        {
            pos = node.nodeObj.transform.position;
            Transform linker = this.ctrl.blockSpawner.Spawn(BlockSpawner.LINKER, pos, Quaternion.identity);
            linker.gameObject.SetActive(true);
        }
    }

    protected virtual void Enqueue(Node blockCtrl)
    {
        this.queue.Add(blockCtrl);
    }

    protected virtual Node Dequeue()
    {
        Node node = this.queue[0];
        this.queue.RemoveAt(0);
        return node;
    }

    private bool IsValidPosition(Node node, Node startNode)
    {
        if (node == startNode) return true;

        return !node.occupied;
    }
}
