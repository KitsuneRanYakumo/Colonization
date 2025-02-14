using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _minDistanceToPoint = 0.1f;

    private Coroutine _coroutine;

    public event Action PositionReached;

    public void MoveToPosition(Vector3 position)
    {
        StopMoving();
        _coroutine = StartCoroutine(MoveForward(position));
    }

    public void StopMoving()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator MoveForward(Vector3 position)
    {
        Vector2 positionResouceOnPlaneXZ = new Vector2(position.x, position.z);
        Vector2 positionCollectorOnPlaneXZ = new Vector2(transform.position.x, transform.position.z);

        while ((positionResouceOnPlaneXZ - positionCollectorOnPlaneXZ).sqrMagnitude > _minDistanceToPoint * _minDistanceToPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     new Vector3(position.x, transform.position.y, position.z),
                                                     _speed * Time.deltaTime);
            positionCollectorOnPlaneXZ = new Vector2(transform.position.x, transform.position.z);

            yield return null;
        }

        PositionReached?.Invoke();
    }
}