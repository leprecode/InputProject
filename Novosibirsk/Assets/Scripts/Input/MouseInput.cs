using System;
using UnityEngine;

[Serializable]
public class MouseInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    public override void Update()
    {
        float currentMouseXPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        OnNewInputValue?.Invoke(currentMouseXPosition);
    }
}
