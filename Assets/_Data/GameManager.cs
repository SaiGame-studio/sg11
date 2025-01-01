using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : SaiSingleton<GameManager>
{
    private GameState currentState;
    public GameState CurrentState => currentState;

    #region Game Logic Variables
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
    #endregion

    // Event
    public event Action OnGameOver;
    public event Action OnFinishGame;
    public event Action<GameState> OnGameStateChanged;

    protected override void Start()
    {
        base.Start();
        SetInitialState();
    }

    #region Game State Handlers
    private void SetInitialState()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        ChangeState(currentSceneName.ToLower().Contains("mainmenu") ? GameState.MainMenu : GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        if (currentState == newState) return;

        ExitCurrentState();
        currentState = newState;
        EnterNewState();

        OnGameStateChanged?.Invoke(currentState);
    }

    private void ExitCurrentState()
    {
        switch (currentState)
        {
            case GameState.Playing:
                // Clean up any ongoing game processes
                break;
            case GameState.Paused:
                Time.timeScale = 1f;
                break;
        }
    }

    private void EnterNewState()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                //ResetGameData();
                break;
            case GameState.Playing:
                this.InitializeData();
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
        }
    }
    #endregion

    protected virtual void Update()
    {
        if (currentState != GameState.Playing) return;

        UpdateGameplay();
    }

    private void UpdateGameplay()
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

    public void AddMoreHint(int hintNum)
    {
        this.remainHint += hintNum;
    }

    #region Game State Handlers

    private void CheckWinStatus()
    {
        if (GridManagerCtrl.Instance?.gridSystem == null) return;

        if (GridManagerCtrl.Instance.gridSystem.blocksRemain > 0)
        {
            isWin = false;
        }
    }

    protected virtual void CheckGameStatus()
    {
        if (GridManagerCtrl.Instance?.gridSystem == null) return;

        int blocksRemain = GridManagerCtrl.Instance.gridSystem.blocksRemain;
        bool noMovesLeft = remainShuffle <= 0 && !GridManagerCtrl.Instance.blockAuto.isNextBlockExist;

        if (blocksRemain == 0 && isWin == false)
        {
            ChangeState(GameState.Victory);
        }

        if (noMovesLeft && blocksRemain > 0 && isLoss == false)
        {
            ChangeState(GameState.GameOver);
        }
    }

    private void HandleGameOver()
    {
        OnGameOver?.Invoke();
        SoundManager.Instance?.PlaySound(SoundManager.Sound.no_move);
    }

    private void HandleVictory()
    {
        if (gameLevel == maxLevel)
        {
            OnFinishGame?.Invoke();
            return;
        }

        SoundManager.Instance?.PlaySound(SoundManager.Sound.win);
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
