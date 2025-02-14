using System;
using System.Collections;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private CollectorBase _collectorBasePrefab;
    [SerializeField] private float _speedConstuction = 1;

    private Flag _constructionPlace;

    public event Action<CollectorBase> BuildCompleted;

    public void SetConstruction(Flag constructionPlace)
    {
        _constructionPlace = constructionPlace;
    }

    public void BuildConstruction()
    {
        CollectorBase collectorBase = Instantiate(_collectorBasePrefab,
                    _constructionPlace.transform.position + Vector3.down,
                    _constructionPlace.transform.rotation);
        StartCoroutine(Build(collectorBase));
    }

    private IEnumerator Build(CollectorBase collectorBase)
    {
        while (collectorBase.transform.position.y < _constructionPlace.transform.position.y)
        {
            collectorBase.transform.position += _speedConstuction * Time.deltaTime * Vector3.up;

            yield return null;
        }

        BuildCompleted?.Invoke(collectorBase);
    }
}