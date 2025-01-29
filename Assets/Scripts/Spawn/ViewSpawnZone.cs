using System.Collections;
using UnityEngine;

public class ViewSpawnZone : MonoBehaviour
{
    [SerializeField] private CircularSpawnGrid _circularSpawnGrid;
    [SerializeField] private float _increaseSpeed = 1;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _circularSpawnGrid.DiameterChanged += StartIncrease;
    }

    private void OnDisable()
    {
        _circularSpawnGrid.DiameterChanged -= StartIncrease;
    }

    private void StartIncrease(float targetSize)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(IncreaseZone(targetSize));
    }

    private IEnumerator IncreaseZone(float targetSize)
    {
        while (transform.localScale.x < targetSize)
        {
            float increaseSpeedDeltaTime = _increaseSpeed * Time.deltaTime;
            transform.localScale += new Vector3(increaseSpeedDeltaTime, 0, increaseSpeedDeltaTime);
            yield return null;
        }
    }
}