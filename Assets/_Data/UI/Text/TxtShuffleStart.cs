using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TxtShuffleStart : BaseText
{
    [SerializeField] protected GameMode gameMode;

    protected override void Start()
    {
        if(GameMode.Classic == gameMode)
        {
            text.text = "x" + GameManager.Instance.ModeData.shuffleClassic;
        }
        if (GameMode.Full == gameMode)
        {
            text.text = "x" + GameManager.Instance.ModeData.shuffleFull + " (+" + GameManager.Instance.ModeData.shuffleEachLevel + "/level)";
        }
    }
}
