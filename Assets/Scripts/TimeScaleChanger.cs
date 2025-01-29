using UnityEngine;

public class TimeScaleChanger : MonoBehaviour
{
    [SerializeField, Min(0)] private float _timeSpeed;

    private void OnValidate()
    {
        Time.timeScale = _timeSpeed;
    }
}