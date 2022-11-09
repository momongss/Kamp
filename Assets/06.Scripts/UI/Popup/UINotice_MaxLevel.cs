using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UINotice_MaxLevel : UINotice
{
    public static UINotice_MaxLevel I { get; private set; }

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }
}
