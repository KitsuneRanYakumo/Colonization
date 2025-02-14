using System;
using UnityEngine;

public class TargetWaitingPlaceDetector : MonoBehaviour
{
    private WaitingPlace _target;

    public event Action TargetWaitingPlaceEscaped;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WaitingPlace waitingPlace))
        {
            if (waitingPlace == _target)
            {
                TargetWaitingPlaceEscaped?.Invoke();
            }
        }
    }

    public void SetWaitingPlace(WaitingPlace target)
    {
        _target = target;
    }
}