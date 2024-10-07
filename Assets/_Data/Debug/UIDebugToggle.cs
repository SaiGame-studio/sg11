using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebugToggle : SaiMonoBehaviour
{
    [SerializeField] protected Transform debug;

    protected void FixedUpdate()
    {
        this.Showing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDebug();
    }

    protected virtual void LoadDebug()
    {
        if (this.debug != null) return;
        this.debug = transform.Find("Debug");
        Debug.Log(transform.name + " LoadDebug", gameObject);
    }

    protected virtual void Showing()
    {
        this.debug.gameObject.SetActive(InputManager.Instance.isDebug);
    }
}
