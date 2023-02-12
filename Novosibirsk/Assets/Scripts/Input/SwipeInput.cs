using System;
using UnityEngine;

[Serializable]
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
