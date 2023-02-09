using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _maxXPosition = 6f;
    [SerializeField] private float _minXPosition = -6f;



    private void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"));
        StartCoroutine(DistanceCovered());
    }

    private void Move(float normilizedXValue)
    {
        if (normilizedXValue == 0)
            return;

        float clampedNewXPosition = Mathf.Clamp(transform.position.x + normilizedXValue, _minXPosition, _maxXPosition);
        Vector3 targetPos = new Vector3(clampedNewXPosition, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _movementSpeed);
    }

    //TODO: Delete this
    private Vector3 initiailPos;
    private bool launched = false;
    private IEnumerator DistanceCovered()
    {
        if (!launched)
        {
            launched = true;
            initiailPos = transform.position;
            yield return new WaitForSeconds(1);
            Debug.Log($"Covered distance in one second: {Mathf.Abs(initiailPos.x - transform.position.x)}");
            launched = false;
        }
    }
}