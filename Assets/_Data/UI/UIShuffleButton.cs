using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShuffleButton : SaiMonoBehaviour
{
    [SerializeField] protected Transform shuffleButton;
    [SerializeField] protected Button btnShuffle;
    [SerializeField] protected Animator btnAnim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShuffleButton();
        this.LoadButtonAnimator();
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

    protected virtual void LoadButtonAnimator()
    {
        if (this.btnAnim != null) return;
        if (this.shuffleButton == null) return;
        this.btnAnim = this.shuffleButton.GetComponent<Animator>();
        Debug.Log(transform.name + " LoadButtonAnimator", gameObject);
    }

    protected virtual void ShuffleChecking()
    {
        bool interactable = true;
        if (GridManagerCtrl.Instance.blockAuto.isNextBlockExist) interactable = false;
        if (GridManagerCtrl.Instance.gridSystem.blocksRemain == 0) interactable = false;
        if (GameManager.Instance.RemainShuffle <= 0) interactable = false;

        UpdateButtonState(interactable);
    }

    private void UpdateButtonState(bool interactable)
    {
        if (this.btnShuffle.interactable != interactable)
        {
            btnAnim.SetBool("EnablePulse", interactable);
            if (interactable) SoundManager.Instance.PlaySound(SoundManager.Sound.no_move);
        }
        this.btnShuffle.interactable = interactable;
    }
}
