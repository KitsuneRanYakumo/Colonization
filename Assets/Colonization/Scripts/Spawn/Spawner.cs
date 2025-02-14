using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : Spawnable<T>
{
    [SerializeField] protected T PrefabSpawnable;
    [SerializeField] private int _startAmountSpawnables = 1;
    [SerializeField] private int _amountStock = 10;

    protected List<T> Spawnables;
    protected int AmountActive;
    protected int CurrentSpawnable;

    public virtual void Initialize(List<T> spawnables = null)
    {
        Spawnables = new List<T>();

        if (spawnables != null)
            AddSpawnables(spawnables);

        AmountActive = 0;
        CurrentSpawnable = 0;

        for (int i = 0; i < _startAmountSpawnables + _amountStock; i++)
            CreateSpawnable();
    }

    public virtual T GetSpawnable()
    {
        if (Spawnables.Count - AmountActive < _amountStock)
            StartCoroutine(CreateSpawnables(_amountStock));

        while (Spawnables[CurrentSpawnable].gameObject.activeSelf)
        {
            CurrentSpawnable = (CurrentSpawnable + 1) % Spawnables.Count;
        }

        T spawnable = Spawnables[CurrentSpawnable];
        CurrentSpawnable = ++CurrentSpawnable % Spawnables.Count;
        spawnable.LifeTimeFinished += TakeSpawnable;
        AmountActive++;
        spawnable.On();

        return spawnable;
    }

    protected virtual IEnumerator CreateSpawnables(int amountSpawnables)
    {
        for (int i = 0; i < amountSpawnables; i++)
        {
            CreateSpawnable();

            yield return null;
        }
    }

    protected virtual void CreateSpawnable()
    {
        T spawnable = Instantiate(PrefabSpawnable);
        spawnable.Off();
        Spawnables.Add(spawnable);
    }

    protected void AddSpawnables(List<T> spawnables)
    {
        Spawnables.AddRange(spawnables);
    }

    protected void TakeSpawnable(T spawnable)
    {
        spawnable.LifeTimeFinished -= TakeSpawnable;
        spawnable.Off();
        spawnable.Reset();
        AmountActive--;
    }
}