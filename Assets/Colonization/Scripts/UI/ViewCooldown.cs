using UnityEngine;
using UnityEngine.UI;

public class ViewCooldown : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Text _text;
    [SerializeField] private int _decimalPlaces = 2;

    private Timer _timer;

    private void OnEnable()
    {
        SubscribeToTimer();
    }

    private void OnDisable()
    {
        UnsubscribeToTimer();
    }

    public void SetTimer(Timer timer)
    {
        UnsubscribeToTimer();
        _timer = timer;
        SubscribeToTimer();

        if (_timer.IsActive == false)
        {
            HideCooldown();
        }
        else
        {
            ShowCooldown();
        }
    }

    private void ShowCooldown()
    {
        _canvasGroup.alpha = 1;
    }

    private void DisplayCooldown(float currentValue)
    {
        _text.text = currentValue.ToStringFixed(_decimalPlaces);
    }

    private void HideCooldown()
    {
        _canvasGroup.alpha = 0;
    }

    private void SubscribeToTimer()
    {
        if (_timer != null)
        {
            _timer.CountdownBegined += ShowCooldown;
            _timer.CooldownChanged += DisplayCooldown;
            _timer.CooldownEnded += HideCooldown;
        }
    }

    private void UnsubscribeToTimer()
    {
        if (_timer != null)
        {
            _timer.CountdownBegined -= ShowCooldown;
            _timer.CooldownChanged -= DisplayCooldown;
            _timer.CooldownEnded -= HideCooldown;
        }
    }
}