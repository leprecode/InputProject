using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputService _inputService;
        [SerializeField] private InputServiceView _inputServiceView;
        private Pause _pause;

        private void Start()
        {
            _pause = new Pause(playerController);
            _inputServiceView.Construct(_inputService);
        }
    }
}
