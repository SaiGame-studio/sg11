using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtHintStart : BaseText
{
    [SerializeField] protected GameMode gameMode;

    protected override void Start()
    {
        if (GameMode.Classic == gameMode)
        {
            text.text = "x" + GameManager.Instance.ModeData.hintClassic;
        }
        if (GameMode.Full == gameMode)
        {
            text.text = "x" + GameManager.Instance.ModeData.hintFull + " (+" + GameManager.Instance.ModeData.hintEachLevel + "/level)";
        }
    }
}
