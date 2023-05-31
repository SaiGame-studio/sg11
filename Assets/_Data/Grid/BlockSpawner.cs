using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Spawner
{
    [Header("Block")]
    private static BlockSpawner instance;
    public static BlockSpawner Instance => instance;

    public static string BLOCK = "Block";
    public BlocksProfile blocksProfile;

    protected override void Awake()
    {
        base.Awake();
        if (BlockSpawner.instance != null) Debug.LogError("Only 1 BlockSpawner allow to exist");
        BlockSpawner.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBlockProflie();
    }

    protected virtual void LoadBlockProflie()
    {
        if (this.blocksProfile != null) return;
        this.blocksProfile = Resources.Load<BlocksProfile>("Pikachu");
        //MyScriptableObject myScriptableObject = Resources.Load<MyScriptableObject>("MyScriptableObject");
        Debug.Log(transform.name + " LoadBlockProflie", gameObject);
    }
}
