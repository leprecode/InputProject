using System;
using UnityEngine;

[Serializable]
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
