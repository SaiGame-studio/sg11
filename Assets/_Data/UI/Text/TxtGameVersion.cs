using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtGameVersion : BaseText
{
    protected override void Start()
    {
        base.Start();
        this.text.text = "v" + Application.version;
    }
}
