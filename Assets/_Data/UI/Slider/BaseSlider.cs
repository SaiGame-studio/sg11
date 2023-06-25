using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : SaiMonoBehaviour
{
    [Header("Base Slider")]
    [SerializeField] protected Slider slider;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected virtual void FixedUpdate()
    {
        //For override
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        this.slider.onValueChanged.AddListener(this.OnChanged);
    }

    protected abstract void OnChanged(float newValue);
}
