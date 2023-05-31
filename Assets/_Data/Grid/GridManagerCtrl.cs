using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerCtrl : SaiMonoBehaviour
{
    public BlockSpawner blockSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.blockSpawner != null) return;
        this.blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
        Debug.Log(transform.name + " LoadSpawner", gameObject);
    }

}
