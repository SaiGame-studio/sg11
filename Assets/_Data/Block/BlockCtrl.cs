using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : SaiMonoBehaviour
{
    [Header("Block Ctrl")]
    public SpriteRenderer sprite;
    public string blockID = "#";
    public BlockData blockData;
    public List<BlockCtrl> neighbors = new List<BlockCtrl>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadBlockData();
    }

    protected virtual void LoadModel()
    {
        if (this.sprite != null) return;
        Transform model = transform.Find("Model");
        this.sprite = model.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + " LoadModel", gameObject);
    }

    protected virtual void LoadBlockData()
    {
        if (this.blockData != null) return;
        this.blockData = transform.Find("BlockData").GetComponent<BlockData>();
        Debug.Log(transform.name + " LoadBlockData", gameObject);
    }
}
