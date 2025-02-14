using System;
using UnityEngine;

public class TerritoryScanner : MonoBehaviour
{
    [SerializeField] private ResourceDetector _resourceDetector;
    [SerializeField] private ScaleIncreaser _scaleIncreser;
    [SerializeField] private Timer _timer;

    public event Action<Resource> ResourceFound;
    public event Action CooldownEnded;

    public Timer Timer => _timer;

    public void Initialize()
    {
        _scaleIncreser.Initialize(_resourceDetector.transform);
        _timer.Initialize();
    }

    private void OnEnable()
    {
        _resourceDetector.ResourceDetected += OnResourceFound;
        _scaleIncreser.IncreaseZoneEnded += _timer.StartCountdown;
        _timer.CooldownEnded += OnCooldownEnded;
    }

    private void OnDisable()
    {
        _resourceDetector.ResourceDetected -= OnResourceFound;
        _scaleIncreser.IncreaseZoneEnded -= _timer.StartCountdown;
        _timer.CooldownEnded -= OnCooldownEnded;
    }

    public void Scan()
    {
        _scaleIncreser.StartIncreaseSizeZone();
    }

    private void OnResourceFound(Resource resource)
    {
        ResourceFound?.Invoke(resource);
    }

    private void OnCooldownEnded()
    {
        CooldownEnded?.Invoke();
    }
}