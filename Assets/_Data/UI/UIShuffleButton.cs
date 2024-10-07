using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShuffleButton : SaiMonoBehaviour
{
    [SerializeField] protected Transform shuffleButton;
    [SerializeField] protected Button btnShuffle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShuffleButton();
    }

    protected void FixedUpdate()
    {
        this.ShuffleChecking();
    }

    protected virtual void LoadShuffleButton()
    {
        if (this.shuffleButton != null) return;
        this.shuffleButton = transform.Find("BtnShuffle");
        this.btnShuffle = this.shuffleButton.GetComponent<Button>();
        Debug.Log(transform.name + " LoadModel", gameObject);
    }

    protected virtual void ShuffleChecking()
    {
        bool interactable = true;
        if (GridManagerCtrl.Instance.blockAuto.isNextBlockExist) interactable = false;
        if (GridManagerCtrl.Instance.gridSystem.blocksRemain == 0) interactable = false;
        if (GameManager.Instance.RemainShuffle <= 0) interactable = false;
        this.btnShuffle.interactable = interactable;
    }
}
