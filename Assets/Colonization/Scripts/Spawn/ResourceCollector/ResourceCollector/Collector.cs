using System;
using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private ResourceDetector _resourceDetector;
    [SerializeField] private CollectionPlaceDetector _collectionPlaceDetector;
    [SerializeField] private float _liftingSpeed = 1;

    private Resource _targetResource;

    public event Action ResourcePickedUp;
    public event Action ResourceGiven;

    public CollectionPlace ÑollectionPlace { get; private set; }

    public void Initialize(CollectionPlace collectionPlace)
    {
        ÑollectionPlace = collectionPlace;
    }

    private void OnEnable()
    {
        _resourceDetector.ResourceDetected += TakeTargetResource;
        _collectionPlaceDetector.CollectionPlaceDetected += ReleaseResource;
    }

    private void OnDisable()
    {
        _resourceDetector.ResourceDetected -= TakeTargetResource;
        _collectionPlaceDetector.CollectionPlaceDetected -= ReleaseResource;
    }

    public void SetTargetResource(Resource resource)
    {
        _targetResource = resource;
    }

    private void TakeTargetResource(Resource resource)
    {
        if (_targetResource != resource)
            return;

        _targetResource.BecomeTaken(transform);
        StartCoroutine(PickUpResource());
    }

    private IEnumerator PickUpResource()
    {
        while (_targetResource.transform.position.y < transform.position.y)
        {
            _targetResource.transform.position += _liftingSpeed * Time.deltaTime * Vector3.up;

            yield return null;
        }

        ResourcePickedUp?.Invoke();
    }

    private void ReleaseResource(CollectionPlace collectionPlace)
    {
        if (_targetResource == null)
            return;

        if (_targetResource.IsTaken == false)
            return;

        if (collectionPlace != ÑollectionPlace)
            return;

        _targetResource.BecomeReleased();
        _targetResource = null;
        ResourceGiven?.Invoke();
    }
}