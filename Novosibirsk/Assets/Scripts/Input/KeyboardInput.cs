using System;
using UnityEngine;

[Serializable]
public class KeyboardInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    public override void Update()
    {
        OnNewInputValue?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
