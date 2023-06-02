using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Spawner
{
    [Header("Block")]
    private static BlockSpawner instance;
    public static BlockSpawner Instance => instance;

    public static string BLOCK = "Block";

    protected override void Awake()
    {
        base.Awake();
        if (BlockSpawner.instance != null) Debug.LogError("Only 1 BlockSpawner allow to exist");
        BlockSpawner.instance = this;
    }
}
