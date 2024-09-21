using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : GridAbstract
{
    [SerializeField] protected int current;
    [SerializeField] protected List<LevelAbstract> levels;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevels();
    }

    protected virtual void LoadLevels()
    {
        if (this.levels.Count > 0) return;
        foreach(Transform child in transform)
        {
            LevelAbstract level = child.GetComponent<LevelAbstract>();
            if (level == null) continue;
            this.levels.Add(level);
        }
        Debug.Log(transform.name + ": LoadLevels", gameObject);
    }

    public virtual void NextLevel()
    {
        this.current++;
    }

    public virtual LevelAbstract GetCurrent()
    {
        return this.levels[this.current];
    }
}
