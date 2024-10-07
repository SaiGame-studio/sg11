using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtBlockCount : BaseText
{
    private void FixedUpdate()
    {
        this.ShowBlockCount();
    }

    protected virtual void ShowBlockCount()
    {
        int count = GridManagerCtrl.Instance.gridSystem.blocksRemain;
        this.text.text = count.ToString();
    }


}
