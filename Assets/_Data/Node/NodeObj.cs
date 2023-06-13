using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeObj : SaiMonoBehaviour
{
    public TextMeshPro text;
    public Color textColor = Color.red;
    public BlockCtrl blockCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
    }

    protected virtual void LoadTextMeshPro()
    {
        if (this.text != null) return;
        this.text = transform.GetComponentInChildren<TextMeshPro>();
        Debug.Log(transform.name + " LoadTextMeshPro", gameObject);
    }

    public virtual void SetText(string text)
    {
        this.text.text = text;
    }

    public virtual void SetColor(Color color)
    {
        this.text.color = color;
    }
}
