using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoResizeBackground : SaiMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private float padding = 5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadBackgroundRect();
    }

    private void LoadText()
    {
        if (this.text != null) return;
        this.text = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }

    private void LoadBackgroundRect()
    {
        if (this.backgroundRect != null) return;
        this.backgroundRect = transform.GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + ": LoadBackgroundRect", gameObject);
    }

    void Update()
    {
        float maxDimension = Mathf.Max(text.preferredWidth, text.preferredHeight);

        backgroundRect.sizeDelta = new Vector2(maxDimension + padding, maxDimension + padding);
    }
}
