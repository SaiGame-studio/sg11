using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridAbstract : SaiMonoBehaviour
{
    [Header("Grid Abstract")]
    public GridManagerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<GridManagerCtrl>();
        Debug.Log(transform.name + " LoadCtrl", gameObject);
    }

}
