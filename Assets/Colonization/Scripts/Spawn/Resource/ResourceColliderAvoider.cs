using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceColliderAvoider : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Resource>())
        {
            Physics.IgnoreCollision(_collider, collision.collider);
        }
    }
}