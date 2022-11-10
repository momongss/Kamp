using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInput : MonoBehaviour
{
    public static XRInput I { get; private set; }

    public XRNode node;
    public bool isPrimaryButtonPressed = false;
    public bool isSecondaryButtonPressed = false;

    public UnityEvent primaryButtonEvent = new UnityEvent();
    public UnityEvent secondaryButtonEvent = new UnityEvent();

    private void Awake()
    {
        I = this;
    }

    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(node), InputHelpers.Button.PrimaryButton, out bool primaryButton, 0.5f);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(node), InputHelpers.Button.SecondaryButton, out bool secondaryButton, 0.5f);

        if (primaryButton)
        {
            if (isPrimaryButtonPressed == false)
            {
                primaryButtonEvent.Invoke();
            }

            isPrimaryButtonPressed = true;
        }
        else
        {
            isPrimaryButtonPressed = false;
        }

        if (secondaryButton)
        {
            if (isSecondaryButtonPressed == false)
            {
                secondaryButtonEvent.Invoke();
            }

            isSecondaryButtonPressed = true;
        }
        else
        {
            isSecondaryButtonPressed = false;
        }
    }

    public void SubscribePrimaryButton(UnityAction action)
    {
        primaryButtonEvent.AddListener(action);
    }

    public void SubscribeSecondaryButton(UnityAction action)
    {
        secondaryButtonEvent.AddListener(action);
    }
}
