using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightWithScale : SaiMonoBehaviour
{
    private Vector3 originalScale;
    public Vector3 highlightScale = new Vector3(1.2f, 1.2f, 1.2f);  // T?ng kích th??c ?? highlight
    void Start()
    {
        originalScale = transform.localScale;
    }
    public void Highlight()
    {
        transform.localScale = highlightScale; 
    }

    public void ResetScale()
    {
        transform.localScale = originalScale; 
    }
}
