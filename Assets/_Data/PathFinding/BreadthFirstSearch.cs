using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : AbstractPathfinding
{
    [Header("Breadth First Search")]
    public GridManagerCtrl ctrl;
    public List<Node> queue = new List<Node>();
    public List<Node> finalPath = new List<Node>();
    public List<NodeStep> cameFromNodes = new List<NodeStep>();
    public List<Node> visited = new List<Node>();

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
        this.queue = new List<Node>();
        this.finalPath = new List<Node>();
        this.cameFromNodes = new List<NodeStep>();
        this.visited = new List<Node>();
    }

    public override bool FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        //Debug.Log("FindPath");
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;

        this.Enqueue(startNode);
        this.cameFromNodes.Add(new NodeStep(startNode, startNode));
        this.visited.Add(startNode);

        NodeStep nodeStep;
        List<NodeStep> steps;
        while (this.queue.Count > 0)
        {
            Node current = this.Dequeue();

            if (current == targetNode)
            {
                this.finalPath = this.BuildFinalPath(startNode, targetNode);
                break;
            }

            foreach (Node neighbor in current.Neighbors())
            {
                if (neighbor == null) continue;
                if (this.visited.Contains(neighbor)) continue;
                if (!this.IsValidPosition(neighbor, targetNode)) continue;

                nodeStep = new NodeStep(neighbor, current);
                this.cameFromNodes.Add(nodeStep);
                this.visited.Add(neighbor);

                steps = this.BuildNodeStepPath(neighbor, startNode);
                nodeStep.stepsString = this.GetStringFromSteps(steps);
                nodeStep.directionString = this.GetDirectionsFromSteps(steps);
                nodeStep.changeDirectionCount = this.CountDirectionFrom2Nodes(neighbor, startNode);
                if (nodeStep.changeDirectionCount > 3) continue;

                this.Enqueue(neighbor);
            }

        }

        //this.ShowVisited();
        this.ShowPath();

        return this.IsPathFound();
    }

    protected virtual bool IsPathFound()
    {
        int nodeCount = this.finalPath.Count;
        return nodeCount > 0;
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

    protected virtual List<Node> BuildFinalPath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node toNode = targetNode;

        while (toNode != startNode)
        {
            path.Add(toNode);
            toNode = this.GetFromNode(toNode);
        }

        path.Add(startNode);
        path.Reverse();

        return path;
    }

    protected virtual Node GetFromNode(Node toNode)
    {
        return this.GetNodeStepByToNode(toNode).fromNode;
    }

    protected virtual NodeStep GetNodeStepByToNode(Node toNode)
    {
        return this.cameFromNodes.Find(item => item.toNode == toNode);
    }

    protected virtual void ShowPath()
    {
        Vector3 pos;
        foreach (Node node in this.finalPath)
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

    protected virtual string GetStringFromSteps(List<NodeStep> steps)
    {
        string stepsString = "";
        foreach (NodeStep nodeStep in steps)
        {
            stepsString += nodeStep.toNode.Name() + "=>";
        }
        return stepsString;
    }

    protected virtual string GetDirectionsFromSteps(List<NodeStep> steps)
    {
        string stepsString = "";
        foreach (NodeStep nodeStep in steps)
        {
            stepsString += nodeStep.direction + "=>";
        }
        return stepsString;
    }

    protected virtual int CountDirectionFrom2Nodes(Node currentNode, Node startNode)
    {
        int count = 0;
        List<NodeStep> steps = this.BuildNodeStepPath(currentNode, startNode);
        count = this.CountDirectionFromSteps(steps);
        return count;
    }

    protected virtual int CountDirectionFromSteps(List<NodeStep> steps)
    {
        NodeDirections lastDirection = NodeDirections.noDirection;
        NodeDirections currentDirection;
        int turnCount = 0;
        foreach (NodeStep nodeStep in steps)
        {
            currentDirection = nodeStep.direction;
            if(currentDirection != lastDirection)
            {
                lastDirection = currentDirection;
                turnCount++;
            }
        }

        return turnCount;
    }

    protected virtual List<NodeStep> BuildNodeStepPath(Node currentNode, Node startNode)
    {
        List<NodeStep> steps = new List<NodeStep>();

        Node checkNode = currentNode;
        for (int i = 0; i < this.cameFromNodes.Count; i++)
        {
            NodeStep step = this.GetNodeStepByToNode(checkNode);
            steps.Add(step);
            checkNode = step.fromNode;
            if (step.fromNode == startNode) break;
        }

        //this.ShowScanStep(currentNode);
        return steps;
    }

    protected virtual void ShowStepsDebug(List<NodeStep> steps)
    {
        Debug.LogError("Steps Count: " + steps.Count);

        foreach (NodeStep step in steps)
        {
            Debug.Log("stepsDebug: " + step.toNode.Name());
        }
        Debug.LogError("=========================");
    }

    protected virtual void ShowScanStep(Node currentNode)
    {
        Vector3 pos = currentNode.nodeObj.transform.position;
        Transform obj = BlockSpawner.Instance.Spawn(BlockSpawner.SCAN_STEP, pos, Quaternion.identity);
        obj.gameObject.SetActive(true);
    }
}
