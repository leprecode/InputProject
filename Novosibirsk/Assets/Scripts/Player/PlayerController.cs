using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _discreteMovementValue = 3f;
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _maxXPosition = 6f;
    [SerializeField] private float _minXPosition = -6f;

    private bool _isPaused;

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void Start()
    {
        _maxXPosition = transform.position.x + _maxXPosition;
        _minXPosition = transform.position.x + _minXPosition;
    }

    private void OnEnable() => InputService.OnNewInputScheme += ChangeSubscribeOnInputScheme;
    private void OnDisable() => InputService.OnNewInputScheme -= ChangeSubscribeOnInputScheme;

    private void ChangeSubscribeOnInputScheme(InputScheme previousScheme, InputScheme newScheme)
    {
        UnsubscribeOnPreviousInputScheme(previousScheme);
        SubscribeOnNewInputScheme(newScheme);
    }

    private void SubscribeOnNewInputScheme(InputScheme newScheme)
    {
        if (newScheme == null)
            return;

        if (newScheme is KeyboardInput)
            newScheme.OnNewInputValue += Move;
        else if (newScheme is SwipeInput)
            newScheme.OnNewInputValue += DiscreteMove;
        else if (newScheme is TouchInput || newScheme is MouseInput)
            newScheme.OnNewInputValue += MoveToPosition;
    }

    private void UnsubscribeOnPreviousInputScheme(InputScheme previousScheme)
    {
        if (previousScheme == null)
            return;

        if (previousScheme is KeyboardInput)
            previousScheme.OnNewInputValue -= Move;
        else if (previousScheme is SwipeInput)
            previousScheme.OnNewInputValue -= DiscreteMove;
        else if (previousScheme is TouchInput || previousScheme is MouseInput)
            previousScheme.OnNewInputValue -= MoveToPosition;
    }

    private void Move(float xValue)
    {
        if (_isPaused)
            return;

        if (xValue == 0)
            return;

        float clampedNewXPosition = Mathf.Clamp(transform.position.x + xValue, _minXPosition, _maxXPosition);
        Vector3 targetPos = new Vector3(clampedNewXPosition, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _movementSpeed);
    }

    private void MoveToPosition(float xPosition)
    {
        if (_isPaused)
            return;

        if (xPosition == 0)
            return;

        float movementDirection = CalculateMoveDirection(xPosition);
        Move(movementDirection);
    }

    private float CalculateMoveDirection(float xPosition)
    {
        float distance = Mathf.Round(xPosition - transform.position.x);

        return NormilizeDirection(distance);
    }

    private float NormilizeDirection(float distance)
    {
        float normilized = 0;

        if (distance < 0)
            normilized = -1;
        else if (distance > 0)
            normilized = 1;

        return normilized;
    }

    private void DiscreteMove(float xValue)
    {
        float newXValue = NormilizeDirection(xValue) * _discreteMovementValue;
        float newPlayerXPosition = transform.position.x + newXValue;

        if (newPlayerXPosition <= _maxXPosition && newPlayerXPosition >= _minXPosition)
        {
            transform.position =
                new Vector3(newPlayerXPosition, transform.position.y, transform.position.z);
        }
    }
}