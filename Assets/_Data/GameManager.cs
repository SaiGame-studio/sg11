using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SaiSingleton<GameManager>
{
    private bool isGameEnded = false;
    [SerializeField] protected int maxLevel = 0;
    [SerializeField] protected int gameLevel = 1;
    [SerializeField] protected int remainShuffle = 9;
    public int RemainShuffle => remainShuffle;

    [SerializeField] protected int remainHint = 9;
    public int RemainHint => remainHint;

    public int CurrentLevel => gameLevel;

    protected override void Start()
    {
        base.Start();
        this.LoadMaxLevel();
    }

    protected virtual void Update()
    {
        CheckGameStatus();
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

    protected virtual void CheckGameStatus()
    {
        if (isGameEnded) return;

        int blocksRemain = GridManagerCtrl.Instance.gridSystem.blocksRemain;

        if (blocksRemain <= 0)
        {
            HandleWin();
        }
    }

    protected virtual void HandleWin()
    {
        isGameEnded = true;

        SoundManager.Instance.PlaySound(SoundManager.Sound.win);
    }
}
