using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer 
            || Application.platform == RuntimePlatform.WindowsEditor)
        {

        }
        else if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer)
        {

        }
    }
}
