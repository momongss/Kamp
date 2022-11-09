using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class UIPlayerNotice : UINotice
{
    public static UIPlayerNotice I { get; private set; }

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }
}
