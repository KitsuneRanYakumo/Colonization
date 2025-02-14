using System;
using UnityEngine;

public class FlagInstaller : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;

    public event Action<Vector3> DirectionChanged;
    public event Action DirectionInstalled;

    private void OnEnable()
    {
        _userInput.SettingButtonPressed += DetermineStartPosition;
        _userInput.SettingButtonHeldDown += ChangeDirection;
        _userInput.SettingButtonReleased += DetermineFinalPosition;
    }

    private void OnDisable()
    {
        _userInput.SettingButtonPressed -= DetermineStartPosition;
        _userInput.SettingButtonHeldDown -= ChangeDirection;
        _userInput.SettingButtonReleased -= DetermineFinalPosition;
    }

    private void DetermineStartPosition()
    {
        TryGetPosition(out _startPosition);
    }

    private void ChangeDirection()
    {
        if (TryGetPosition(out _endPosition))
        {
            _direction = _endPosition - _startPosition;
            
            if (_direction != Vector3.zero)
            {
                DirectionChanged?.Invoke(_direction);
            }
        }
    }

    private void DetermineFinalPosition()
    {
        ChangeDirection();
        DirectionInstalled?.Invoke();
    }

    private bool TryGetPosition(out Vector3 clickPoint)
    {
        clickPoint = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            clickPoint = hit.point;

            return true;
        }

        return false;
    }
}