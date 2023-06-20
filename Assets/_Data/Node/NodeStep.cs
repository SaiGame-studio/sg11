using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeStep
{
    public string nodeId;
    public string stepsString = "";
    public string directionString = "";
    public int changeDirectionCount = 0;
    public NodeDirections direction = NodeDirections.noDirection;
    public Node fromNode;
    public Node toNode;

    public NodeStep(Node toNode, Node fromNode)
    {
        this.nodeId = toNode.x + "x" + toNode.y;

        this.toNode = toNode;
        this.fromNode = fromNode;

        this.direction = this.GetDirection(toNode,fromNode);
    }

    protected virtual NodeDirections GetDirection(Node fromNode, Node toNode)
    {
        if (fromNode.x == toNode.x && fromNode.y < toNode.y) return NodeDirections.up;
        if (fromNode.x == toNode.x && fromNode.y > toNode.y) return NodeDirections.down;

        if (fromNode.x < toNode.x && fromNode.y == toNode.y) return NodeDirections.right;
        if (fromNode.x > toNode.x && fromNode.y == toNode.y) return NodeDirections.left;

        return NodeDirections.noDirection;
    }
}
