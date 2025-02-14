using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitOwner : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private PlaceOwner _placeOwner;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Unit> _startUnits;

    private List<Unit> _totalUnits;
    private Queue<Unit> _freeUnits;
    private CollectionPlace _collectionPlace;
    private int _busyAmount;

    public event Action StatsChanged;
    public event Action FreeUnitAppeared;

    public int TotalAmountUnits => _totalUnits.Count;

    public int FreeAmountUnits => _freeUnits.Count;

    public int BusyAmountUnits => _busyAmount;

    public int CostUnit => _unitSpawner.SpawnableUnit.CostCreation;

    public void Initialize(CollectionPlace collectionPlace)
    {
        _unitSpawner.Initialize();
        _placeOwner.Initialize();
        _collectionPlace = collectionPlace;

        _totalUnits = new List<Unit>();
        _freeUnits = new Queue<Unit>();

        if (_startUnits.Count > 0)
        {
            InitializeStartUnits();
        }

        _busyAmount = 0;
    }

    public void SpawnUnit(int cost)
    {
        if (cost == _unitSpawner.SpawnableUnit.CostCreation)
        {
            Unit unit = _unitSpawner.GetSpawnable();
            AttachUnit(unit);
            unit.transform.position = _spawnPoint.position;
        }
    }

    public bool TryGetFreeUnit(out Unit unit)
    {
        if (_freeUnits.TryDequeue(out unit))
        {
            StatsChanged?.Invoke();

            return true;
        }

        return false;
    }

    public void AttachUnit(Unit unit)
    {
        unit.Initialize(_placeOwner.GetFreePlace(), _collectionPlace);
        _totalUnits.Add(unit);
        _freeUnits.Enqueue(unit);
        SubscribeToUnitEvents(unit);
        StatsChanged?.Invoke();
    }

    public void RemoveUnit(Unit unit)
    {
        _totalUnits.Remove(unit);
        _placeOwner.TakeFreePlace(unit.GiveWaitingtPlace());
        UnsubscribeToUnitEvents(unit);
    }

    private void AddFreeUnit(Unit unit)
    {
        _freeUnits.Enqueue(unit);
        _busyAmount--;
        FreeUnitAppeared?.Invoke();

        StatsChanged?.Invoke();
    }

    private void InitializeStartUnits()
    {
        for (int i = 0; i < _startUnits.Count; i++)
        {
            if (_startUnits[i].gameObject.activeSelf)
            {
                AttachUnit(_startUnits[i]);
            }
        }
    }

    private void IncreaseBusyUnit()
    {
        _busyAmount++;
        StatsChanged?.Invoke();
    }

    private void SubscribeToUnitEvents(Unit unit)
    {
        unit.Free += AddFreeUnit;
        unit.Busy += IncreaseBusyUnit;
        unit.LifeTimeFinished += UnsubscribeToUnitEvents;
    }

    private void UnsubscribeToUnitEvents(Unit unit)
    {
        unit.Free -= AddFreeUnit;
        unit.Busy -= IncreaseBusyUnit;
        unit.LifeTimeFinished -= UnsubscribeToUnitEvents;
    }
}