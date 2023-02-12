using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace _Game.Code.Infrastructure.SaveAndLoadSystem
{
    [Serializable]
    public class DataPersistenceManager
    {
        private GameData _gameData;
        private List<IDataPersistence> _allDataPersistents;
        private FileDataHandler _fileDataHandler;
        private InputService _inputService;

        public DataPersistenceManager(FileDataHandler fileDataHandler, InputService inputService)
        {
            _allDataPersistents = new List<IDataPersistence>();

            _fileDataHandler = fileDataHandler;
            _inputService = inputService;
            AddDataPersistence(_inputService);

            Subscribe();
        }

        ~DataPersistenceManager() 
        {
            Unsubscribe();
        }

        public void LoadGame()
        {
            _gameData = _fileDataHandler.Load();

            if (this._gameData == null)
            {
                NewGame();
            }
            else
            {
                for (int i = 0; i < _allDataPersistents.Count; i++)
                {
                    _allDataPersistents[i].LoadData(_gameData);
                }
            }
        }

        public void NewGame() 
        {
            this._gameData = new GameData(_inputService.GetDefaultInput());
        }

        public void SaveGame()
        {
            for (int i = 0; i < _allDataPersistents.Count; i++)
            {
                 _allDataPersistents[i].SaveData(ref _gameData);
            }

            _fileDataHandler.Save(_gameData);
        }

        private void AddDataPersistence(IDataPersistence newDataPersistent)
        {
            _allDataPersistents.Add(newDataPersistent);
        }

        private void Subscribe()
        {
            InputService.OnChangeInput += SaveGame;
        }

        private void Unsubscribe()
        {
            InputService.OnChangeInput -= SaveGame;
        }
    }
}