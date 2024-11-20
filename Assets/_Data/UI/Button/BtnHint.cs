using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHint : BaseButton
{
    [SerializeField] protected SpriteRenderer spriteHint;
    [SerializeField] protected BlockCtrl hintBlock;

    private void FixedUpdate()
    {
        this.ShowSpriteHint();
        this.CheckHintAvailability();
    }

    protected override void OnClick()
    {
        BlockDebug.Instance.ClearDebug();
        GridManagerCtrl.Instance.blockAuto.LoadHintBlock();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRender();
    }

    protected virtual void LoadSpriteRender()
    {
        if (this.spriteHint != null) return;
        this.spriteHint = transform.parent.Find("SpriteHint").GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + " LoadSpriteRender", gameObject);
    }

    protected virtual void ShowSpriteHint()
    {
        this.hintBlock = GridManagerCtrl.Instance.blockAuto.hintBlock;
        if (this.hintBlock == null || !this.hintBlock.blockData.node.occupied)
        {
            this.spriteHint.sprite = null;
            this.hintBlock = null;
            return;
        }

        this.spriteHint.sprite = this.hintBlock.sprite;
    }

    private void CheckHintAvailability()
    {
        bool interactable = true;

        if (!GridManagerCtrl.Instance.blockAuto.isNextBlockExist) interactable = false;
        if (GridManagerCtrl.Instance.gridSystem.blocksRemain == 0) interactable = false;
        if (GameManager.Instance.RemainHint <= 0) interactable = false;

        this.button.interactable = interactable;
    }
}
