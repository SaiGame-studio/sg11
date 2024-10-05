using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlockCtrl : SaiMonoBehaviour
{
    [Header("Block Ctrl")]
    public Transform model;
    public Transform blockBackground;
    public SortingGroup sortingGroup;
    public SpriteRenderer spriteRender;
    public Sprite sprite;
    public string blockID = "#";
    public BlockData blockData;
    public List<BlockCtrl> neighbors = new List<BlockCtrl>();

    public BlockCtrl Clone()
    {
        return (BlockCtrl)this.MemberwiseClone();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadBlockBackground();
        this.LoadBlockData();
        this.LoadSortingGroup();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.ReloadModel();
        Debug.Log(transform.name + " LoadModel", gameObject);
    }

    protected virtual void LoadBlockBackground()
    {
        if (this.blockBackground != null) return;
        this.blockBackground = transform.Find("BlockBackground");
        Debug.Log(transform.name + " LoadBlockBackground", gameObject);
    }

    protected virtual void LoadSortingGroup()
    {
        if (this.sortingGroup != null) return;
        this.sortingGroup = GetComponent<SortingGroup>();
        Debug.Log(transform.name + " LoadSortingGroup", gameObject);
    }

    public virtual void ReloadModel() {
        this.model = transform.Find("Model");
        this.spriteRender = this.model.GetComponent<SpriteRenderer>();
    }

    protected virtual void LoadBlockData()
    {
        if (this.blockData != null) return;
        this.blockData = transform.Find("BlockData").GetComponent<BlockData>();
        Debug.Log(transform.name + " LoadBlockData", gameObject);
    }

    public virtual void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        this.spriteRender.sprite = sprite;
    }
}
