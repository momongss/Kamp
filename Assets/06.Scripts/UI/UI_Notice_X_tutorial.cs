using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Notice_X_tutorial : UINotice
{
    public static UI_Notice_X_tutorial I { get; private set; }

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }
}
