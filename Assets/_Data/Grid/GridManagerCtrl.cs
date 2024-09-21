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
    public BlockAuto blockAuto;
    public GridSystem gridSystem;
    public AbstractPathfinding pathfinding;
    public LinesDrawer linesDrawer;
    public GameLevel gameLevel;

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
        this.LoadBlockAuto();
        this.LoadPathfinding();
        this.LoadBlockHandler();
        this.LoadGridSystem();
        this.LoadLinesDrawer();
        this.LoadGameLevel();
    }

    protected virtual void LoadGameLevel()
    {
        if (this.gameLevel != null) return;
        this.gameLevel = transform.GetComponentInChildren<GameLevel>();
        Debug.LogWarning(transform.name + " LoadSpawner", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.blockSpawner != null) return;
        this.blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
        Debug.LogWarning(transform.name + " LoadSpawner", gameObject);
    }

    protected virtual void LoadBlockAuto()
    {
        if (this.blockAuto != null) return;
        this.blockAuto = transform.Find("BlockAuto").GetComponent<BlockAuto>();
        Debug.LogWarning(transform.name + " LoadBlockAuto", gameObject);
    }

    protected virtual void LoadBlockHandler()
    {
        if (this.blockHandler != null) return;
        this.blockHandler = transform.Find("BlockHandler").GetComponent<BlockHandler>();
        Debug.LogWarning(transform.name + " LoadBlockHandler", gameObject);
    }

    protected virtual void LoadGridSystem()
    {
        if (this.gridSystem != null) return;
        this.gridSystem = transform.Find("GridSystem").GetComponent<GridSystem>();
        Debug.LogWarning(transform.name + " LoadGridSystem", gameObject);
    }

    protected virtual void LoadPathfinding()
    {
        if (this.pathfinding != null) return;
        this.pathfinding = transform.GetComponentInChildren<AbstractPathfinding>();
        Debug.LogWarning(transform.name + " LoadPathfinding", gameObject);
    }

    protected virtual void LoadLinesDrawer()
    {
        if (this.linesDrawer != null) return;
        this.linesDrawer = transform.GetComponentInChildren<LinesDrawer>();
        Debug.LogWarning(transform.name + " LoadLinesDrawer", gameObject);
    }
}
