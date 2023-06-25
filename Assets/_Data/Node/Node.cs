using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node
{
    public int x = 0;
    public int y = 0;
    public float posX = 0;
    public int weight = 1;
    public bool occupied = false;
    public bool blockPlaced = false;
    public int nodeId;
    public Node up;
    public Node right;
    public Node down;
    public Node left;
    public NodeObj nodeObj;
    public BlockCtrl blockCtrl;

    public virtual List<Node> Neighbors()
    {
        List<Node> nodes = new List<Node>
        {
            up,
            right,
            down,
            left
        };
        return nodes;
    }

    public virtual string Name()
    {
        return this.x + "x" + this.y;
    }
}
