using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerCtrl : SaiMonoBehaviour
{
    [Header("Grid Manager Ctrl")]
    private static GridManagerCtrl instance;
    public static GridManagerCtrl Instance => instance;

    public BlockSpawner blockSpawner;
    public BlockHandler blockHandler;
    public GridSystem gridSystem;
    public IPathfinding pathfinding;

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
        this.LoadBlockHandler();
        this.LoadGridSystem();
    }

    protected virtual void LoadSpawner()
    {
        if (this.blockSpawner != null) return;
        this.blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
        Debug.Log(transform.name + " LoadSpawner", gameObject);
    }

    protected virtual void LoadBlockHandler()
    {
        if (this.blockHandler != null) return;
        this.blockHandler = transform.Find("BlockHandler").GetComponent<BlockHandler>();
        Debug.Log(transform.name + " LoadBlockHandler", gameObject);
    }

    protected virtual void LoadGridSystem()
    {
        if (this.gridSystem != null) return;
        this.gridSystem = transform.Find("GridSystem").GetComponent<GridSystem>();
        Debug.Log(transform.name + " LoadGridSystem", gameObject);
    }

    protected virtual void LoadPathfinding()
    {
        if (this.pathfinding != null) return;
        this.pathfinding = transform.GetComponentInChildren<IPathfinding>();
        Debug.Log(transform.name + " LoadPathfinding", gameObject);
    }
}
