using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectorBase : MonoBehaviour
{
    [SerializeField] private int _constructionCost = 5;
    [SerializeField] private Flag _constructionFlag;
    [SerializeField] private UnitOwner _unitOwner;
    [SerializeField] private ResourceOwner _resourceOwner;
    [SerializeField] private KeeperDiscoveredResources _keeperDiscoveredResources;
    [SerializeField] private int _minAmountUnits = 1;
    [SerializeField] private Renderer _renderer;

    public UnitOwner UnitOwner => _unitOwner;

    public ResourceOwner ResourceOwner => _resourceOwner;

    public KeeperDiscoveredResources KeeperDiscoveredResources => _keeperDiscoveredResources;

    public Timer Timer => _keeperDiscoveredResources.Timer;

    public bool IsBuilding { get; private set; } = false;

    public StatsCollectorBase Stats { get; private set; }

    public void Initialize()
    {
        Stats = new StatsCollectorBase(this);
        GenerateColor();
        _keeperDiscoveredResources.Initialize();
        _unitOwner.Initialize(_resourceOwner.CollectionPlace);
        _resourceOwner.Initialize();
        Stats.Update();
    }

    private void OnEnable()
    {
        _unitOwner.StatsChanged += UpdateStats;
        _resourceOwner.StatsChanged += UpdateStats;
        _keeperDiscoveredResources.StatsChanged += UpdateStats;
    }

    private void OnDisable()
    {
        _unitOwner.StatsChanged -= UpdateStats;
        _resourceOwner.StatsChanged -= UpdateStats;
        _keeperDiscoveredResources.StatsChanged -= UpdateStats;
    }

    public void StartWork()
    {
        StartCoroutine(CollectResources());
        StartSpawnUnits();
    }

    public void PrepareConstruction()
    {
        IsBuilding = true;
        
        if (_unitOwner.TotalAmountUnits > _minAmountUnits)
        {
            _resourceOwner.RequiredAmountResourcesCollected -= SpawnUnit;
            _resourceOwner.SetRequiredAmountResources(_constructionCost);
            _resourceOwner.RequiredAmountResourcesCollected += BuildConstruction;
        }
    }

    public void StartSpawnUnits()
    {
        _resourceOwner.RequiredAmountResourcesCollected -= BuildConstruction;
        _resourceOwner.SetRequiredAmountResources(_unitOwner.CostUnit);
        _resourceOwner.RequiredAmountResourcesCollected += SpawnUnit;
    }

    public void AttachUnit(Unit unit)
    {
        _unitOwner.AttachUnit(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _unitOwner.RemoveUnit(unit);
    }

    public void SetPositionFlag(Vector3 position)
    {
        _constructionFlag.SetPosition(position);
    }

    public void SetDirectionFlag(Vector3 direction)
    {
        _constructionFlag.SetDirection(direction);
    }

    private void SpawnUnit()
    {
        int requiredAmountResources = _resourceOwner.GetAmountResources(_unitOwner.CostUnit);
        _unitOwner.SpawnUnit(requiredAmountResources);
    }

    private void BuildConstruction()
    {
        if (_unitOwner.TryGetFreeUnit(out Unit unit))
        {
            _resourceOwner.GetAmountResources(_constructionCost);
            unit.BuildConstruction(_constructionFlag);
            RemoveUnit(unit);
            IsBuilding = false;
            StartSpawnUnits();
        }
        else
        {
            _unitOwner.FreeUnitAppeared += WaitFreeUnit;
        }
    }

    private void WaitFreeUnit()
    {
        BuildConstruction();
        _unitOwner.FreeUnitAppeared -= WaitFreeUnit;
    }

    private IEnumerator CollectResources()
    {
        WaitUntil wait = new WaitUntil(() => CanCollectResources());

        while (enabled)
        {
            yield return wait;

            Resource resource = _keeperDiscoveredResources.GetDiscoveredResource();

            if (_unitOwner.TryGetFreeUnit(out Unit unit))
            {
                unit.BringResource(resource);
            }
        }
    }

    private bool CanCollectResources()
    {
        return _keeperDiscoveredResources.AmountDiscoveredResources > 0 && _unitOwner.FreeAmountUnits > 0;
    }

    private void GenerateColor()
    {
        Color color = Random.ColorHSV();
        _renderer.material.color = color;
        _constructionFlag.SetColor(color);
    }

    private void UpdateStats()
    {
        Stats.Update();
    }
}