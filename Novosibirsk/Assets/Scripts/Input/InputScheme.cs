using System;

[Serializable]
public abstract class InputScheme
{
    public delegate void OnChangeValue(float xAxisValue);
    public abstract event OnChangeValue OnNewInputValue;
    public abstract void Update();
}
