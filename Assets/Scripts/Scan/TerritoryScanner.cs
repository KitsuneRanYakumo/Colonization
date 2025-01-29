using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TerritoryScanner : MonoBehaviour
{
    [SerializeField] private ResourceDetector _resourceDetector;
    [SerializeField, Min(1)] private float _scanningRange = 50;
    [SerializeField] private float _scanningSpeed = 20f;

    private SphereCollider _sphereCollider;
    private Vector3 _startSize;

    public event Action ScanCompleted;
    public event Action<Resource> ResourceFound;
    public event Action<Vector3> ScaleChanged;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        _resourceDetector.ResourceDetected += OnResourceFound;
    }

    private void Start()
    {
        _sphereCollider.isTrigger = true;
        _startSize = transform.localScale;
    }

    private void OnDisable()
    {
        _resourceDetector.ResourceDetected -= OnResourceFound;
    }

    public void ScanTerritory()
    {
        StartCoroutine(IncreaseSizeZone());
    }

    private IEnumerator IncreaseSizeZone()
    {
        while (transform.localScale.x < _scanningRange)
        {
            transform.localScale += _scanningSpeed * Time.deltaTime * Vector3.one;
            ScaleChanged?.Invoke(transform.localScale);
            yield return null;
        }

        transform.localScale = _startSize;
        ScaleChanged?.Invoke(transform.localScale);
        ScanCompleted?.Invoke();
    }

    private void OnResourceFound(Resource resource)
    {
        ResourceFound?.Invoke(resource);
    }
}