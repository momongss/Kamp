using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : UINotice
{
    public static UIEquipment I { get; private set; }

    protected override void Awake()
    {
        I = this;
        base.Awake();
    }
}
