using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectCharacter : UIPanel
{
    public static UISelectCharacter I { get; private set; }

    private void Awake()
    {
        I = this;
    }
}
