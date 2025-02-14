using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _cooldown;

    private float _currentTime;

    public event Action<float> CooldownChanged;
    public event Action CountdownBegined;
    public event Action CooldownEnded;

    public bool IsActive { get; private set; }  = false;

    public void Initialize()
    {
        _currentTime = _cooldown;
        CooldownChanged?.Invoke(_currentTime);
    }

    public void StartCountdown()
    {
        StartCoroutine(Counting());
    }

    private IEnumerator Counting()
    {
        IsActive = true;
        CountdownBegined?.Invoke();

        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            CooldownChanged?.Invoke(_currentTime);

            yield return null;
        }

        CooldownEnded?.Invoke();
        IsActive = false;
        Initialize();
    }
}