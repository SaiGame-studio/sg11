using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnLevel : BaseButton
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected int level = 0;

    protected override void Start()
    {
        base.Start();
        this.ShowLevel();
    }

    private void FixedUpdate()
    {
        this.LevelStatus();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevel();
    }

    protected virtual void ShowLevel()
    {
        this.text.text = this.level.ToString();
    }

    protected virtual void LoadLevel()
    {
        if (this.level > 0) return;
        string name = transform.name.Replace("BtnLevel", "");
        this.level = int.Parse(name);
        this.text = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " LoadLevel", gameObject);
    }

    protected override void OnClick()
    {
        //GameManager.Instance.NextLevel();
    }

    protected virtual void LevelStatus()
    {
        this.button.interactable = GameManager.Instance.CurrentLevel == this.level;
    }
}
