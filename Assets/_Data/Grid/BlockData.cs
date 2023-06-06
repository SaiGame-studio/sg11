using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
    public TextMeshPro text;
    public Node node;

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

    public virtual void SetNode(Node node)
    {
        this.node = node;
        //this.text.text = this.node.nodeId.ToString();
        this.text.text = this.node.x.ToString() + "\n" + this.node.y.ToString();
    }

    public virtual void SetSprite(Sprite sprite)
    {
        this.ctrl.sprite.sprite = sprite;
    }
}
