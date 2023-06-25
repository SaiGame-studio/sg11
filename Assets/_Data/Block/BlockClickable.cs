using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BlockClickable : BlockAbstract
{
    [Header("Block Clickable")]
    public BoxCollider _collider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadColider();
    }

    protected virtual void LoadColider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<BoxCollider>();
        this._collider.isTrigger = true;
        this._collider.size = new Vector3(0.79f, 0.99f, 0.5f);
        Debug.Log(transform.name + " LoadColider", gameObject);
    }

    protected void OnMouseUp()
    {
        GridManagerCtrl.Instance.blockHandler.SetNode(this.ctrl);
    }
}
