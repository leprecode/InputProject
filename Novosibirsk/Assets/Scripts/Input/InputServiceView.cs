using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

        _inputsDropdown.value =  UpdateDropdownOption(inputService, options);
    }

    public void ChangeDropdownValue()
    {
        OnNewInputScheme?.Invoke(_inputsDropdown.value);
    }

    private int UpdateDropdownOption(InputService inputService, List<TMP_Dropdown.OptionData> options)
    {
        var avtiveInput = inputService.activeInput.ToString();

        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].text == avtiveInput)
            {
                return i;
            }
        }

        return 0;
    }
}
