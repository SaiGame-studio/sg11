using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SaiSingleton<GameManager>
{
    private bool isWin = false;
    private bool isLoss = false;
    private bool isCountdownShuffle = false;

    [SerializeField] protected int maxLevel = 0;
    [SerializeField] protected int gameLevel = 1;
    [SerializeField] protected int remainShuffle = 9;
    public int RemainShuffle => remainShuffle;

    [SerializeField] protected int remainHint = 9;
    public int RemainHint => remainHint;

    public int CurrentLevel => gameLevel;

    // Event
    public event Action OnGameOver;
    public event Action OnFinishGame;

    protected override void Start()
    {
        base.Start();
        this.InitializeData();
    }

    protected virtual void Update()
    {
        CheckWinStatus();
        CheckGameStatus();
        CheckShouldCountdownShuffle();
    }

    private void CheckShouldCountdownShuffle()
    {
        if (isCountdownShuffle) return;
        if (!InputManager.Instance.isDebug) return;
        if (CountdownShuffleCtrl.Instance == null) return;
        if (CountdownShuffleCtrl.Instance.IsCountingDown()) return;

        CountdownShuffleCtrl.Instance.SetShouldCountingDown();
        isCountdownShuffle = true;
    }

    [ProButton]
    public virtual void NextLevel()
    {
        this.gameLevel++;
        if (this.gameLevel > this.maxLevel) this.gameLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected virtual void LoadMaxLevel()
    {
        this.maxLevel = GridManagerCtrl.Instance.gameLevel.Levels.Count;
    }

    public virtual void UseHint()
    {
        this.remainHint--;
        if (this.remainHint < 0) this.remainHint = 0;
    }

    public virtual void UseShuffle()
    {
        this.remainShuffle--;
        if (this.remainShuffle < 0) this.remainShuffle = 0;
    }

    #region Game State Handlers

    private void CheckWinStatus()
    {
        if (GridManagerCtrl.Instance.gridSystem.blocksRemain > 0)
        {
            isWin = false;
        }
    }

    protected virtual void CheckGameStatus()
    {
        int blocksRemain = GridManagerCtrl.Instance.gridSystem.blocksRemain;

        if (blocksRemain <= 0 && isWin == false)
        {
            HandleWin();
        }

        if(remainShuffle <= 0 && !GridManagerCtrl.Instance.blockAuto.isNextBlockExist && isLoss == false && blocksRemain > 0)
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        isLoss = true;

        OnGameOver?.Invoke();
        SoundManager.Instance.PlaySound(SoundManager.Sound.no_move);
    }

    protected virtual void HandleWin()
    {
        isWin = true;

        if (gameLevel == maxLevel)
        {
            OnFinishGame?.Invoke();
            return;
        }

        SoundManager.Instance.PlaySound(SoundManager.Sound.win);
    }

    #endregion

    public void ResetGameOverState()
    {
        gameLevel = 1;
        remainShuffle = 9;
        remainHint = 9;

        // Clear all event listeners
        OnGameOver = null;

        this.InitializeData();
    }

    private void InitializeData()
    {
        this.LoadMaxLevel();
        isLoss = false;
        isWin = false;
        isCountdownShuffle = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        OnGameOver = null; // Clear event listeners
    }
}
