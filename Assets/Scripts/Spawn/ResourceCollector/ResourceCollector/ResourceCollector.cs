using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator))]
public class ResourceCollector : Spawnable<ResourceCollector>
{
    [SerializeField] private ResourceHolder _resourceHolder;

    private Mover _mover;
    private Rotator _rotator;
    private Place _waitingPlace;
    private CollectionPlace _collectionPlace;
    private bool _isBusy;

    public event Action<ResourceCollector> BecomeFree;
    public event Action<ResourceCollector> BecomeBusy;

    public void Initialize(Place waitingPlace, CollectionPlace collectionPlace)
    {
        _isBusy = false;
        BecomeFree?.Invoke(this);
        _waitingPlace = waitingPlace;
        _collectionPlace = collectionPlace;
        ComeBackToWaitingPlace();
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        _resourceHolder.ResourcePickedUp += BringResource;
        _resourceHolder.ResourceGiven += ComeBackToWaitingPlace;
    }

    private void OnDisable()
    {
        _resourceHolder.ResourcePickedUp -= BringResource;
        _resourceHolder.ResourceGiven -= ComeBackToWaitingPlace;
    }

    public void GoToResource(Resource resource)
    {
        _resourceHolder.SetTargetResource(resource);
        _isBusy = true;
        BecomeBusy?.Invoke(this);
        MoveToTarget(resource.transform);
    }

    private void BringResource()
    {
        MoveToTarget(_collectionPlace.transform);
    }

    private void MoveToTarget(Transform target)
    {
        _mover.MoveToTarget(target);
        _rotator.StartRotate(target.position);
    }

    private void ComeBackToWaitingPlace()
    {
        _isBusy = false;
        BecomeFree?.Invoke(this);
        MoveToTarget(_waitingPlace.transform);
    }
}