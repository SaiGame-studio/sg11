using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtShuffleCount : BaseText
{
    private void FixedUpdate()
    {
        this.ShowCount();
    }

    protected virtual void ShowCount()
    {
        this.text.text = GameManager.Instance.RemainShuffle.ToString();
    }
}
