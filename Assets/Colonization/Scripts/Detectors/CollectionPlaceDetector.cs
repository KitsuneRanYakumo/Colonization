using System;
using UnityEngine;

public class CollectionPlaceDetector : MonoBehaviour
{
    public event Action<CollectionPlace> CollectionPlaceDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollectionPlace collectionPlace))
        {
            CollectionPlaceDetected?.Invoke(collectionPlace);
        }
    }
}