using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerCtrl : SaiMonoBehaviour
{
    [Header("Grid Manager Ctrl")]
    private static GridManagerCtrl instance;
    public static GridManagerCtrl Instance => instance;

    public BlockSpawner blockSpawner;
    public IPathfinding pathfinding;
    public BlockCtrl firstBlock;
    public BlockCtrl lastBlock;

    protected override void Awake()
    {
        base.Awake();
        if (GridManagerCtrl.instance != null) Debug.LogError("Only 1 GridManagerCtrl allow to exist");
        GridManagerCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadPathfinding();
    }

    protected virtual void LoadSpawner()
    {
        if (this.blockSpawner != null) return;
        this.blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
        Debug.Log(transform.name + " LoadSpawner", gameObject);
    }

    protected virtual void LoadPathfinding()
    {
        if (this.pathfinding != null) return;
        this.pathfinding = transform.GetComponentInChildren<IPathfinding>();
        Debug.Log(transform.name + " LoadPathfinding", gameObject);
    }

    public virtual void SetNode(BlockCtrl blockCtrl)
    {
        if(this.firstBlock != null && this.lastBlock != null)
        {
            this.pathfinding.FindPath(this.firstBlock, this.lastBlock);
            this.firstBlock = null;
            this.lastBlock = null;
            Debug.Log("Reset blocks");
            return;
        }

        Vector3 pos;
        Transform chooseObj;
        if (this.firstBlock == null)
        {
            this.firstBlock = blockCtrl;
            pos = blockCtrl.transform.position;
            chooseObj = this.blockSpawner.Spawn(BlockSpawner.CHOOSE, pos, Quaternion.identity);
            chooseObj.gameObject.SetActive(true);
            return;
        }

        this.lastBlock = blockCtrl;
        pos = blockCtrl.transform.position;
        chooseObj = this.blockSpawner.Spawn(BlockSpawner.CHOOSE, pos, Quaternion.identity);
        chooseObj.gameObject.SetActive(true);
    }
}
