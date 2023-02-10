using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;
    private float maxSwipeDistance = 6f;

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("@");

        endPos = eventData.position;
        swipeDistance = endPos.x - startPos.x;

        if (swipeDistance > maxSwipeDistance)
        {
            swipeDistance = maxSwipeDistance;
        }
        else if (swipeDistance < -maxSwipeDistance)
        {
            swipeDistance = -maxSwipeDistance;
        }

        transform.position = new Vector3(transform.position.x + swipeDistance, transform.position.y, transform.position.z);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        startPos = Vector3.zero;
        endPos = Vector3.zero;

        Debug.Log("@2");

    }

    private void Start()
    {
        Debug.Log("@12312");

        startPos = Vector3.zero;
        endPos = Vector3.zero;
    }
}
