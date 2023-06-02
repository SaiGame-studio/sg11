using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
    public Node node;

    public virtual void SetNode(Node node)
    {
        this.node = node;
    }

    public virtual void SetSprite(Sprite sprite)
    {
        this.ctrl.sprite.sprite = sprite;
    }
}
