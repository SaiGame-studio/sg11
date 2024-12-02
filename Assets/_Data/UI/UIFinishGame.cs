using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinishGame : SaiMonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject winFx;

    protected override void Start()
    {
        GameManager.Instance.OnFinishGame += GameManager_OnFinishGame;
    }

    private void GameManager_OnFinishGame()
    {
        ShowUI();
        SoundManager.Instance.PlaySound(SoundManager.Sound.finish);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContainer();
    }

    private void LoadContainer()
    {
        if (this.container != null) return;
        this.container = transform.Find("Container").gameObject;
        Debug.Log(transform.name + ": LoadContainer", gameObject);
    }

    private void ShowUI()
    {
        container.SetActive(true);
        winFx.SetActive(true);
    }
}
