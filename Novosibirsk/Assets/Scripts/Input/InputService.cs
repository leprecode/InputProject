using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public delegate void OnChangeInputScheme(InputScheme previousScheme,
        InputScheme newScheme);
    public static event OnChangeInputScheme OnNewInputScheme;

    public List<InputScheme> inputs { get; private set; }
    public InputScheme activeInput { get; private set; }

    private void Awake()
    {
        inputs = new List<InputScheme>();
        RuntimePlatform currentPlatform = Application.platform;

        RegistrateInputs(currentPlatform);

        //TODO: сделать проверку сохранений платформы
        InstallDefaultPlatform();
    }

    private void Start()
    {
        OnNewInputScheme?.Invoke(activeInput, activeInput);
    }

    private void OnEnable() => InputServiceView.OnNewInputScheme += ChangeInputScheme;

    private void OnDisable() => InputServiceView.OnNewInputScheme -= ChangeInputScheme;

    private void ChangeInputScheme(int inputNumber)
    {
        var previousScheme = activeInput;

        activeInput = inputs[inputNumber];

        OnNewInputScheme?.Invoke(previousScheme, activeInput);
    }

    private void InstallDefaultPlatform()
    {
        activeInput = inputs[0];
    }

    private void RegistrateInputs(RuntimePlatform currentPlatform)
    {
        if (currentPlatform == RuntimePlatform.WindowsPlayer
            || currentPlatform == RuntimePlatform.WindowsEditor)
        {
            inputs.Add(new KeyboardInput());
            inputs.Add(new MouseInput());
        }
        else if (currentPlatform == RuntimePlatform.Android
            || currentPlatform == RuntimePlatform.IPhonePlayer)
        {
            inputs.Add(new SwipeInput());
            inputs.Add(new TouchInput());
        }
    }

    private void Update()
    {
        if (activeInput == null)
            return;

        activeInput.Update();
        //OnNewInputValue?.Invoke(inputXValue);
    }
}

public abstract class InputScheme
{
    public delegate void OnChangeValue(float xAxisValue);
    public abstract event OnChangeValue OnNewInputValue;
    public abstract void Update();
}

public class TouchInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    public override void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Canceled
                || touch.phase != TouchPhase.Ended)
            {
                Vector3 touchPosition = touch.position;
                Vector3 worldPosition =
                    Camera.main.ScreenToWorldPoint
                    (new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));

                OnNewInputValue?.Invoke(worldPosition.x);
            }
            else
            {
                OnNewInputValue?.Invoke(0);
            }
        }
        else
        {
            OnNewInputValue?.Invoke(0);
        }
    }
}

public class SwipeInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _minSwipeDistance = 50.0f;

    public override void Update()
    {
        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    _endTouchPosition = touch.position;
                    CalculateSwipeDirection();
                    break;
            }
        }
    }

    private void CalculateSwipeDirection()
    {
        float swipeDistance = Vector2.Distance(_startTouchPosition, _endTouchPosition);

        if (CheckSwipeDistance(swipeDistance))
        {
            Vector2 swipeDirection = _endTouchPosition - _startTouchPosition;

            HandleSwipe(swipeDirection.x);
        }
    }

    private void HandleSwipe(float swipeDirectionOnXAxis)
    {
        OnNewInputValue?.Invoke(swipeDirectionOnXAxis);
    }

    private bool CheckSwipeDistance(float swipeDistance)
    {
        return swipeDistance > _minSwipeDistance;
    }
}

public class KeyboardInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    public override void Update()
    {
        Debug.Log("KeyboardInput");
        OnNewInputValue?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}

public class MouseInput : InputScheme
{
    public override event OnChangeValue OnNewInputValue;

    private float _previousMousePositionOnX;

    public override void Update()
    {
        float currentMouseXPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Debug.Log("MouseInput, x: " + currentMouseXPosition);

        OnNewInputValue?.Invoke(currentMouseXPosition);
    }
}
