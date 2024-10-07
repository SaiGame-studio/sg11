using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : GridAbstract
{
    [SerializeField] protected List<LevelAbstract> levels;
    public List<LevelAbstract> Levels => levels;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevels();
    }

    protected virtual void LoadLevels()
    {
        if (this.levels.Count > 0) return;
        foreach (Transform child in transform)
        {
            LevelAbstract level = child.GetComponent<LevelAbstract>();
            if (level == null) continue;
            this.levels.Add(level);
        }
        Debug.Log(transform.name + ": LoadLevels", gameObject);
    }

    public virtual LevelAbstract GetCurrentLevelObj()
    {
        int level = GameManager.Instance.CurrentLevel;
        return this.levels[level - 1];
    }
}
