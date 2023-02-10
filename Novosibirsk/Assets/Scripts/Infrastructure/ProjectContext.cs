using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private Pause _pause;

        private void Awake()
        {
            _pause = new Pause(playerController);
        }

        private void OnDestroy()
        {
            _pause.OnDestroy();
        }
    }
}
