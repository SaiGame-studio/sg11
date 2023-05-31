using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : GridAbstract
{
    [Header("Grid System")]
    public int width = 18;
    public int height = 11;
    private float offsetX = 0.19f;
    public List<Node> nodes;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.InitGridSystem();
    }

    protected override void Start()
    {
        this.SpawnBlocks();
    }

    protected virtual void InitGridSystem()
    {
        if (this.nodes.Count > 0) return;
        for(int x = 0; x < this.width; x++)
        {
            for(int y = 0; y < this.height; y++)
            {
                Node node = new Node
                {
                    x = x,
                    y = y,
                    posX = x-(this.offsetX*x),
                };
                this.nodes.Add(node);
            }
        }
    }

    protected virtual void SpawnBlocks()
    {
        Vector3 pos = Vector3.zero;
        foreach(Node node in this.nodes)
        {
            if (node.x == 0) continue;
            if (node.y == 0) continue;
            if (node.x == this.width-1) continue;
            if (node.y == this.height-1) continue;

            pos.x = node.posX;
            pos.y = node.y;
            Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
            block.gameObject.SetActive(true);
        }
    }
}
