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
    GameOver,
    Victory
}

public enum GameMode
{
    Classic,
    Full
}

public class GameManager : SaiSingleton<GameManager>
{
    protected GameState currentState;
    public GameState CurrentState => currentState;

    protected GameMode currentMode;
    public GameMode CurrentMode => currentMode;

    protected GameModeData modeData = new GameModeData();
    public GameModeData ModeData => modeData;

    #region Game Logic Variables
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

    protected virtual void FixedUpdate()
    {
        if (currentState != GameState.Playing) return;

        UpdateGameplay();
    }

    protected virtual void UpdateGameplay()
    {
        CheckGameStatus();
        CheckShouldCountdownShuffle();
    }

    #region Game State Handlers
    protected virtual void SetInitialState()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.ToLower().Contains("mainmenu"))
        {
            ChangeState(GameState.MainMenu);
        }
        else
        {
            this.StartNewGame();
        }
    }

    public virtual void ChangeState(GameState newState)
    {
        if (currentState == newState) return;

        ExitCurrentState();
        currentState = newState;
        EnterNewState();

        OnGameStateChanged?.Invoke(currentState);
    }

    protected virtual void ExitCurrentState()
    {
        switch (currentState)
        {
            case GameState.Playing:
                // Clean up any ongoing game processes
                break;
        }
    }

    protected virtual void EnterNewState()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                //ResetGameData();
                break;
            case GameState.Playing:
                //StartCoroutine(WaitForGameSceneLoad());
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

    public virtual void StartNewGame()
    {
        StartCoroutine(WaitForGameSceneLoad());
    }

    protected virtual IEnumerator WaitForGameSceneLoad()
    {
        SceneManager.LoadScene("game");

        yield return null;

        while (GridManagerCtrl.Instance == null || GridManagerCtrl.Instance.gridSystem.blocksRemain == 0)
        {
            yield return null;
        }

        InitializeData();
        this.ChangeState(GameState.Playing);
    }

    protected virtual void CheckShouldCountdownShuffle()
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

        if (currentMode == GameMode.Classic && this.gameLevel == 6)
        {
            // Convert from 6 to 10 for classic mode
            this.gameLevel = 10;
        }

        if(currentMode == GameMode.Full)
        {
            AddMoreHint(modeData.hintEachLevel);
            AddMoreShuffle(modeData.shuffleEachLevel);
        }

        if (this.gameLevel > this.maxLevel) this.gameLevel = 1;
        StartCoroutine(WaitForGameSceneLoad());
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

    public virtual void AddMoreHint(int hintNum)
    {
        this.remainHint += hintNum;
    }

    public virtual void AddMoreShuffle(int shuffleNum)
    {
        this.remainShuffle += shuffleNum;
    }

    public virtual void SetGameMode(GameMode mode)
    {
        this.currentMode = mode;
    }

    #region Game State Handlers

    protected virtual void CheckGameStatus()
    {
        if (GridManagerCtrl.Instance?.gridSystem == null) return;

        int blocksRemain = GridManagerCtrl.Instance.gridSystem.blocksRemain;
        bool noMovesLeft = remainShuffle <= 0 && !GridManagerCtrl.Instance.blockAuto.isNextBlockExist;

        if (blocksRemain == 0)
        {
            ChangeState(GameState.Victory);
        }

        else if (noMovesLeft && blocksRemain > 0)
        {
            ChangeState(GameState.GameOver);
        }
    }

    protected virtual void HandleGameOver()
    {
        OnGameOver?.Invoke();
        SoundManager.Instance?.PlaySound(SoundManager.Sound.no_move);
    }

    protected virtual void HandleVictory()
    {
        if (gameLevel == maxLevel)
        {
            OnFinishGame?.Invoke();
            return;
        }

        SoundManager.Instance?.PlaySound(SoundManager.Sound.win);
    }

    #endregion

    public virtual void ResetGameOverState()
    {
        if(currentMode == GameMode.Classic)
        {
            remainShuffle = modeData.shuffleClassic;
            remainHint = modeData.hintClassic;
        }

        if(currentMode == GameMode.Full)
        {
            remainShuffle = modeData.shuffleFull;
            remainHint = modeData.hintFull;
        }
        gameLevel = 1;

        // Clear all event listeners
        OnGameOver = null;
        OnFinishGame = null;
    }

    protected virtual void InitializeData()
    {
        this.LoadMaxLevel();
        isCountdownShuffle = false;
    }

    #region event registration
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

    protected virtual void OnSceneUnloaded(Scene scene)
    {
        OnGameOver = null;
        OnFinishGame = null;
    }
    #endregion
}
