using System;
using System.Collections;
using UnityEngine;

public class ScaleIncreaser : MonoBehaviour
{
    [SerializeField] private float _speedIncrease;
    [SerializeField] private float _maxSize;

    private Transform _transform;
    private Vector3 _startSize;

    public event Action IncreaseZoneEnded;

    public void Initialize(Transform transform)
    {
        _transform = transform;
        _startSize = _transform.localScale;
    }

    public void StartIncreaseSizeZone()
    {
        StartCoroutine(IncreaseSizeZone());
    }

    private IEnumerator IncreaseSizeZone()
    {
        while (_transform.localScale.x < _maxSize)
        {
            _transform.localScale += _speedIncrease * Time.deltaTime * Vector3.one;

            yield return null;
        }

        _transform.localScale = _startSize;
        IncreaseZoneEnded?.Invoke();
    }
}