using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeScaleChanger : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField, Min(0)] private float _slowMotionSpeed = 0.5f;
    [SerializeField] private float _normalSpeed = 1f;
    [SerializeField] private float _fastSpeed = 2f;
    [SerializeField] private float _fasterSpeed = 3f;

    private float _pauseTimeSpeed = 0f;
    private float _lastTimeSpeed;

    private bool _isGameOnPause => Time.timeScale == _pauseTimeSpeed;

    private void OnValidate()
    {
        if (_normalSpeed < _slowMotionSpeed)
            _normalSpeed = _slowMotionSpeed;

        if (_fastSpeed < _normalSpeed)
            _fastSpeed = _normalSpeed;

        if (_fasterSpeed < _fastSpeed)
            _fasterSpeed = _fastSpeed;
    }

    private void OnEnable()
    {
        _userInput.PauseButtonPressed += OnPause;
        _userInput.SlowMotionButtonPressed += OnSlowMotion;
        _userInput.NormalSpeedButtonPressed += OnNormalSpeed;
        _userInput.FastSpeedButtonPressed += OnFastSpeed;
        _userInput.FasterSpeedButtonPressed += OnFasterSpeed;
    }

    private void OnDisable()
    {
        _userInput.PauseButtonPressed -= OnPause;
        _userInput.SlowMotionButtonPressed -= OnSlowMotion;
        _userInput.NormalSpeedButtonPressed -= OnNormalSpeed;
        _userInput.FastSpeedButtonPressed -= OnFastSpeed;
        _userInput.FasterSpeedButtonPressed -= OnFasterSpeed;
    }

    private void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    private void OnPause()
    {
        if (_isGameOnPause == false)
        {
            _lastTimeSpeed = Time.timeScale;
            ChangeTimeScale(_pauseTimeSpeed);
        }
        else
        {
            Time.timeScale = _lastTimeSpeed;
        }
    }

    private void OnSlowMotion()
    {
        ChangeTimeScale(_slowMotionSpeed);
    }

    private void OnNormalSpeed()
    {
        ChangeTimeScale(_normalSpeed);
    }

    private void OnFastSpeed()
    {
        ChangeTimeScale(_fastSpeed);
    }

    private void OnFasterSpeed()
    {
        ChangeTimeScale(_fasterSpeed);
    }
}