using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseButton = KeyCode.BackQuote;
    [SerializeField] private KeyCode _alternativePauseButton = KeyCode.Space;
    [SerializeField] private KeyCode _slowMotionButton = KeyCode.Alpha1;
    [SerializeField] private KeyCode _normalSpeedButton = KeyCode.Alpha2;
    [SerializeField] private KeyCode _fastSpeedButton = KeyCode.Alpha3;
    [SerializeField] private KeyCode _fasterSpeedButton = KeyCode.Alpha4;

    [SerializeField] private KeyCode _selectionButton = KeyCode.Mouse0;
    [SerializeField] private KeyCode _settingButton = KeyCode.Mouse1;

    private Vector2 _mousePosition;

    public event Action PauseButtonPressed;
    public event Action SlowMotionButtonPressed;
    public event Action NormalSpeedButtonPressed;
    public event Action FastSpeedButtonPressed;
    public event Action FasterSpeedButtonPressed;

    public event Action SelectionButtonPressed;

    public event Action SettingButtonPressed;
    public event Action SettingButtonHeldDown;
    public event Action SettingButtonReleased;

    public event Action<Vector2> MousePositionChanged;

    private void Start()
    {
        SetMousePosition(Input.mousePosition);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        HandleMouseMovement();
        HandleTimeControlInput();
        HandleSelectionInput();
        HandleSettingsInput();
    }

    private void HandleMouseMovement()
    {
        if (_mousePosition != (Vector2)Input.mousePosition)
        {
            SetMousePosition(Input.mousePosition);
        }
    }

    private void SetMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
        MousePositionChanged?.Invoke(_mousePosition);
    }

    private void HandleTimeControlInput()
    {
        if (Input.GetKeyDown(_pauseButton) || Input.GetKeyDown(_alternativePauseButton))
        {
            PauseButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(_slowMotionButton))
        {
            SlowMotionButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(_normalSpeedButton))
        {
            NormalSpeedButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(_fastSpeedButton))
        {
            FastSpeedButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(_fasterSpeedButton))
        {
            FasterSpeedButtonPressed?.Invoke();
        }
    }

    private void HandleSelectionInput()
    {
        if (Input.GetKeyDown(_selectionButton))
        {
            SelectionButtonPressed?.Invoke();
        }
    }

    private void HandleSettingsInput()
    {
        if (Input.GetKeyDown(_settingButton))
        {
            SettingButtonPressed?.Invoke();
        }
        else if (Input.GetKey(_settingButton))
        {
            SettingButtonHeldDown?.Invoke();
        }
        else if (Input.GetKeyUp(_settingButton))
        {
            SettingButtonReleased?.Invoke();
        }
    }
}