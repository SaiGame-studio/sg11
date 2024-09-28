using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
    public Node node;

    public BlockData Clone()
    {
        return (BlockData)this.MemberwiseClone();
    }

    public virtual void SetNode(Node node)
    {
        this.node = node;
    }

    public virtual void SetSprite(Sprite sprite)
    {
        this.ctrl.SetSprite(sprite);
        this.ctrl.blockID = sprite.name;
    }
}
