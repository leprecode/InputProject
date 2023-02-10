using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputServiceView : MonoBehaviour
{
    public delegate void OnChangeInputScheme(int inputNumber);
    public static event OnChangeInputScheme OnNewInputScheme;

    [SerializeField] private TMP_Dropdown _inputsDropdown;

    public void Construct(InputService inputService)
    {
        var options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < inputService.inputs.Count; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(inputService.inputs[i].ToString()));
        }

        _inputsDropdown.ClearOptions();
        _inputsDropdown.AddOptions(options);
    }

    public void ChangeDropdownValue()
    {
        OnNewInputScheme?.Invoke(_inputsDropdown.value);
    }
}
