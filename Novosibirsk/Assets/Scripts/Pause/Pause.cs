using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pause
{
    [SerializeField] private List<IPauseHandler> _handlers;
    public bool isPaused { get; private set; }

    public Pause(params IPauseHandler[] handlers)
    {
        _handlers = new List<IPauseHandler>();
        RegisterSeveral(handlers);

        SubscribeToView();
    }

    ~Pause()
    { 
        UnsubscribeToView();
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

    private void SubscribeToView()
    {
        PauseView.OnPause += PauseGame;
    }

    private void UnsubscribeToView()
    {
        PauseView.OnPause -= PauseGame;
    }

    private void UpdateHandlers(bool isPaused)
    {
        for (int i = 0; i < _handlers.Count; i++)
        {
            _handlers[i].SetPause(isPaused);
        }
    }
}
