using UnityEngine;

public class PauseView : MonoBehaviour
{
    public delegate void OnPauseButtonPressed();
    public static event OnPauseButtonPressed OnPause;

    [SerializeField] private GameObject _pauseMenu;

    public void OnPauseButtonClick()
    {
        OnPause?.Invoke();
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }
}
