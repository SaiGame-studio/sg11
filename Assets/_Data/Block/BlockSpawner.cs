using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Spawner
{
    [Header("Block")]
    private static BlockSpawner instance;
    public static BlockSpawner Instance => instance;

    public static string BLOCK = "Block";
    public static string LINKER = "Linker";
    public static string CHOOSE = "Choose";
    public static string SCAN = "Scan";
    public static string SCAN_STEP = "ScanStep";
    public static string NODE_OBJ = "NodeObj";

    protected override void Awake()
    {
        base.Awake();
        if (BlockSpawner.instance != null) Debug.LogError("Only 1 BlockSpawner allow to exist");
        BlockSpawner.instance = this;
    }
}
