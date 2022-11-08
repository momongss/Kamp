using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class UINotice_End : UINotice
{
    public static UINotice_End I { get; private set; }

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }
}
