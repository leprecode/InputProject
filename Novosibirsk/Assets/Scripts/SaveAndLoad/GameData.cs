using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using static FileDataHandler;

[System.Serializable]
public class GameData
{
    [JsonConverter(typeof(InputJsonConverter))]
    public InputScheme inputScheme;

    public GameData(InputScheme currentInputScheme)
    {
        this.inputScheme = currentInputScheme;
    }
}
