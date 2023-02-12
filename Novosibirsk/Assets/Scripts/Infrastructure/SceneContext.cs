using _Game.Code.Infrastructure.SaveAndLoadSystem;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class SceneContext : MonoBehaviour
    {
        private const string _dataPlayersInput = "PlayerInput";
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputService _inputService;
        [SerializeField] private InputServiceView _inputServiceView;

        private Pause _pause;
        private DataPersistenceManager _dataPersistenceManager;

        private void Awake()
        {
            _pause = new Pause(playerController);

            _dataPersistenceManager = 
                new DataPersistenceManager(new FileDataHandler(Application.persistentDataPath, _dataPlayersInput), _inputService);
            _dataPersistenceManager.LoadGame();

            _inputServiceView.Construct(_inputService);
        }
    }
}
