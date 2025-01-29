using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _minDistanceToPoint = 0.1f;

    private Coroutine _coroutine;

    public void MoveToTarget(Transform target)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveForward(target));
    }

    public void StopMoving()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator MoveForward(Transform target)
    {
        Vector2 positionResouceOnPlaneXZ = new Vector2(target.position.x, target.position.z);
        Vector2 positionCollectorOnPlaneXZ = new Vector2(transform.position.x, transform.position.z);

        while ((positionResouceOnPlaneXZ - positionCollectorOnPlaneXZ).sqrMagnitude > _minDistanceToPoint * _minDistanceToPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     new Vector3(target.position.x, transform.position.y, target.position.z),
                                                     _speed * Time.deltaTime);
            positionCollectorOnPlaneXZ = new Vector2(transform.position.x, transform.position.z);
            yield return null;
        }
    }
}