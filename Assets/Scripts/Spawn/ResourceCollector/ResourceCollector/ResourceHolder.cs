using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private ResourceDetector _resourceDetector;
    [SerializeField] private float _liftingSpeed = 1;

    private Resource _targetResource;

    public event Action ResourcePickedUp;
    public event Action ResourceGiven;

    private void OnEnable()
    {
        _resourceDetector.ResourceDetected += TakeTargetResource;
    }

    private void OnDisable()
    {
        _resourceDetector.ResourceDetected -= TakeTargetResource;
    }

    public void SetTargetResource(Resource resource)
    {
        _targetResource = resource;
        _targetResource.LifeTimeFinished += OnResourceGiven;
    }

    public void TakeTargetResource(Resource resource)
    {
        if (_targetResource == resource)
        {
            resource.transform.SetParent(transform);
            resource.BecomeTaken();
            StartCoroutine(PickUpResource());
        }
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

    private void OnResourceGiven(Resource resource)
    {
        transform.DetachChildren();
        _targetResource.LifeTimeFinished -= OnResourceGiven;
        ResourceGiven?.Invoke();
    }
}