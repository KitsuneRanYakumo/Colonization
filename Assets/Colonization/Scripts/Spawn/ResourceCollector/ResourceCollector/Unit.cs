using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator))]
public class Unit : Spawnable<Unit>
{
    [SerializeField] private int _costCreation = 3;
    [SerializeField] private Collector _collector;
    [SerializeField] private Builder _builder;
    [SerializeField] private TargetWaitingPlaceDetector _targetWaitingPlaceDetector;

    private Mover _mover;
    private Rotator _rotator;
    private WaitingPlace _waitingPlace;
    private bool _isBusy;

    public event Action<Unit> Free;
    public event Action Busy;

    public int CostCreation => _costCreation;

    public void Initialize(WaitingPlace waitingPlace, CollectionPlace collectionPlace)
    {
        _collector.Initialize(collectionPlace);
        _waitingPlace = waitingPlace;
        _targetWaitingPlaceDetector.SetWaitingPlace(_waitingPlace);
        _isBusy = false;
        ComeBackToWaitingPlace();
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        _collector.ResourcePickedUp += GoToCollectionPlace;
        _collector.ResourceGiven += Rest;
        _builder.BuildCompleted += InitializeBase;
        _targetWaitingPlaceDetector.TargetWaitingPlaceEscaped += ComeBackToWaitingPlace;
    }

    private void OnDisable()
    {
        _collector.ResourcePickedUp -= GoToCollectionPlace;
        _collector.ResourceGiven -= Rest;
        _builder.BuildCompleted -= InitializeBase;
        _targetWaitingPlaceDetector.TargetWaitingPlaceEscaped -= ComeBackToWaitingPlace;
    }

    public void BringResource(Resource resource)
    {
        BecomeBusy();
        MoveToPosition(resource.transform.position);
        _collector.SetTargetResource(resource);
    }

    public void BuildConstruction(Flag placeConstruction)
    {
        _builder.SetConstruction(placeConstruction);
        _mover.PositionReached += _builder.BuildConstruction;
        MoveToPosition(placeConstruction.transform.position);
    }

    public WaitingPlace GiveWaitingtPlace()
    {
        WaitingPlace place = _waitingPlace;
        _waitingPlace = null;
        _targetWaitingPlaceDetector.SetWaitingPlace(null);
        return place;
    }

    private void BecomeFree()
    {
        _isBusy = false;
        Free?.Invoke(this);
    }

    private void BecomeBusy()
    {
        _isBusy = true;
        Busy?.Invoke();
    }

    private void GoToCollectionPlace()
    {
        MoveToPosition(_collector.ÑollectionPlace.transform.position);
    }

    private void Rest()
    {
        BecomeFree();
        MoveToPosition(_waitingPlace.transform.position);
    }

    private void ComeBackToWaitingPlace()
    {
        if (_isBusy == false)
        {
            MoveToPosition(_waitingPlace.transform.position);
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        _mover.MoveToPosition(position);
        _rotator.StartRotate(position);
    }

    private void InitializeBase(CollectorBase collectorBase)
    {
        collectorBase.Initialize();
        collectorBase.AttachUnit(this);
        collectorBase.StartWork();
        _mover.PositionReached -= _builder.BuildConstruction;
    }
}