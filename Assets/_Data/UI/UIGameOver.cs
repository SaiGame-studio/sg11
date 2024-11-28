using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : SaiMonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
        }
    }

    private void GameManager_OnGameOver()
    {
        gameOverUI.SetActive(true);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGameOverUI();
    }

    private void LoadGameOverUI()
    {
        if (this.gameOverUI != null) return;
        this.gameOverUI = transform.Find("GameOverPanel").gameObject;
        Debug.Log(transform.name + " LoadGameOverUI", gameObject);
    }
}
