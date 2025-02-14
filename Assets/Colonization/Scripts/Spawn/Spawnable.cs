using System;
using UnityEngine;

public abstract class Spawnable<T> : MonoBehaviour where T : Spawnable<T>
{
    public event Action<T> LifeTimeFinished;

    public virtual void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void Reset() { }

    public void On()
    {
        gameObject.SetActive(true);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void OnLifeTimeFinished(T spawnable)
    {
        LifeTimeFinished?.Invoke(spawnable);
    }
}