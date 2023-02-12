using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Windows;
using System.ComponentModel;

[Serializable]
public partial class FileDataHandler
{
    [SerializeField] private string _dataDirPath;
    [SerializeField] private string _dataFileName;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullFilePath = Path.Combine(_dataDirPath, _dataFileName);

        GameData loadedData;

        if (!System.IO.File.Exists(fullFilePath))
            return null;

        try
        {
            string dataToLoad;

            using (FileStream stream = new FileStream(fullFilePath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            return loadedData;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when trying to load GameData: " + fullFilePath + "\n" + e);

            return null;
        }
    }

    public void Save(GameData data)
    {
        string fullFilePath = Path.Combine(_dataDirPath, _dataFileName);
        Debug.Log("File path: " + fullFilePath);

        try
        {
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath));
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Cant save data to file: " + fullFilePath + "\n" + e);
        }
    }
}
