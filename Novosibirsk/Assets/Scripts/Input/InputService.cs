using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputService : MonoBehaviour, IDataPersistence
{
    public delegate void OnChangeInputScheme(InputScheme previousScheme, InputScheme newScheme);
    public static event OnChangeInputScheme OnNewInputScheme;

    public delegate void OnInputChanged();
    public static event OnInputChanged OnChangeInput;

    public List<InputScheme> inputs { get; private set; }
    [field: SerializeField] public InputScheme activeInput { get;  private set; }

    public InputScheme GetDefaultInput()
    {
        return inputs[0];
    }

    public void SaveData(ref GameData data)
    {
        data.inputScheme = activeInput;
    }

    public void LoadData(GameData data)
    {
        activeInput = data.inputScheme;
    }

    private void Awake()
    {
        inputs = new List<InputScheme>();
        RuntimePlatform currentPlatform = Application.platform;

        RegistrateInputs(currentPlatform);

        InstallDefaultPlatform();
    }

    private void Start()
    {
        OnNewInputScheme?.Invoke(activeInput, activeInput);
    }

    private void OnEnable() => InputServiceView.OnNewInputScheme += ChangeInputScheme;

    private void OnDisable() => InputServiceView.OnNewInputScheme -= ChangeInputScheme;

    private void ChangeInputScheme(int inputNumber)
    {
        var previousScheme = activeInput;

        activeInput = inputs[inputNumber];

        OnNewInputScheme?.Invoke(previousScheme, activeInput);
        OnChangeInput?.Invoke();
    }
    private void InstallDefaultPlatform()
    {
        if (activeInput == null)
        {
            activeInput = inputs[0];
        }
    }

    private void RegistrateInputs(RuntimePlatform currentPlatform)
    {
        if (currentPlatform == RuntimePlatform.WindowsPlayer
            || currentPlatform == RuntimePlatform.WindowsEditor)
        {
            inputs.Add(new KeyboardInput());
            inputs.Add(new MouseInput());
        }
        else if (currentPlatform == RuntimePlatform.Android
            || currentPlatform == RuntimePlatform.IPhonePlayer)
        {
            inputs.Add(new SwipeInput());
            inputs.Add(new TouchInput());
        }
    }

    private void Update()
    {
        if (activeInput == null)
            return;

        activeInput.Update();
    }
}
