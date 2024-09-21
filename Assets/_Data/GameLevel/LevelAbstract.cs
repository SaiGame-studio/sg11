using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelAbstract : SaiMonoBehaviour
{
    [SerializeField] protected GridManagerCtrl gridCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGridManagerCtrl();
    }

    protected virtual void LoadGridManagerCtrl()
    {
        if (this.gridCtrl != null) return;
        this.gridCtrl = transform.GetComponentInParent<GridManagerCtrl>();
        Debug.LogWarning(transform.name + " LoadGridManagerCtrl", gameObject);
    }

    public abstract void MoveBlocks();
}
