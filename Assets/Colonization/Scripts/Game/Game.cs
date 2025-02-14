using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private CollectorBase _selectedCollectorBase;
    [SerializeField] private List<ResourceSpawner> _resourceSpawners;
    [SerializeField] private FlagInstaller _flagInstaller;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private CollectorBaseDetector _collectorBaseDetector;

    public event Action<CollectorBase> AnotherConstructionSelected;

    private void OnEnable()
    {
        _userInput.SelectionButtonPressed += SelectCollectorBase;
        _userInput.SettingButtonPressed += PlaceFlag;
        _flagInstaller.DirectionChanged += SetDirectionFlag;
        _flagInstaller.DirectionInstalled += BuildConstruction;
    }

    private void Start()
    {
        _selectedCollectorBase.Initialize();
        AnotherConstructionSelected?.Invoke(_selectedCollectorBase);

        foreach (ResourceSpawner resourceSpawner in _resourceSpawners)
        {
            if (resourceSpawner.gameObject.activeSelf)
            {
                resourceSpawner.Initialize();
                resourceSpawner.StartSpawnResource();
            }
        }

        _selectedCollectorBase.StartWork();
    }

    private void OnDisable()
    {
        _userInput.SelectionButtonPressed -= SelectCollectorBase;
        _userInput.SettingButtonPressed -= PlaceFlag;
        _flagInstaller.DirectionChanged -= SetDirectionFlag;
        _flagInstaller.DirectionInstalled -= BuildConstruction;
    }

    private void BuildConstruction()
    {
        if (_selectedCollectorBase.IsBuilding == false)
        {
            _selectedCollectorBase.PrepareConstruction();
        }
    }

    private void SelectCollectorBase()
    {
        if (_collectorBaseDetector.TryDetectCollectorBase(out CollectorBase collectorBase))
        {
            if (_selectedCollectorBase != collectorBase)
            {
                _selectedCollectorBase = collectorBase;
                AnotherConstructionSelected?.Invoke(_selectedCollectorBase);
            }
        }
    }

    private void PlaceFlag()
    {
        if (_selectedCollectorBase != null)
        {
            if (_groundDetector.TryDetectGround(out Vector3 touchPoint))
            {
                Vector3 positionFlag = touchPoint;
                _selectedCollectorBase.SetPositionFlag(positionFlag);
            }
        }
    }

    private void SetDirectionFlag(Vector3 direction)
    {
        if (_selectedCollectorBase != null)
        {
            _selectedCollectorBase.SetDirectionFlag(direction);
        }
    }
}