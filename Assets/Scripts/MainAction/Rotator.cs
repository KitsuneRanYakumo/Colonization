using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Coroutine _coroutine;

    public void StartRotate(Vector3 target)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RotateToTarget(target));
    }

    private IEnumerator RotateToTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        float anlgeBetweenForwardToTarget = Vector3.Angle(transform.forward, directionToTarget);
        Vector3 direction = Vector3.Cross(transform.forward, directionToTarget);
        int sign = 1;

        if (direction.y < 0)
            sign = -1;

        while (anlgeBetweenForwardToTarget > 0)
        {
            float rotationSpeedByDeltaTime = _rotationSpeed * Time.deltaTime;
            transform.Rotate(sign * rotationSpeedByDeltaTime * Vector3.up);
            anlgeBetweenForwardToTarget -= rotationSpeedByDeltaTime;
            yield return null;
        }
    }
}