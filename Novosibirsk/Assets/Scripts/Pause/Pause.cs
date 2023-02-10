using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pause
{
    [SerializeField] private List<IPauseHandler> _handlers;

    public Pause(params IPauseHandler[] handlers)
    {
        _handlers = new List<IPauseHandler>();
        RegisterSeveral(handlers);

        SubscribeToView();
    }

    public bool isPaused { get; private set; }

    public void OnDestroy()
    { 
        UnsubscribeToView();
    }

    private void SubscribeToView()
    {
        Debug.Log("Subscribe!");
        PauseView.OnPause += PauseGame;
    }

    private void UnsubscribeToView()
    {
        Debug.Log("Unsubscribe!");
        PauseView.OnPause -= PauseGame;
    }

    public void RegisterSeveral(params IPauseHandler[] handlers)
    {
        for (int i = 0; i < handlers.Length; i++)
        {
            _handlers.Add(handlers[i]);
        }
    }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        UpdateHandlers(isPaused);

        if (isPaused)
            Time.timeScale = 0f;
        else 
            Time.timeScale = 1f;
    }

    private void UpdateHandlers(bool isPaused)
    {
        for (int i = 0; i < _handlers.Count; i++)
        {
            _handlers[i].SetPause(isPaused);
        }
    }
}
