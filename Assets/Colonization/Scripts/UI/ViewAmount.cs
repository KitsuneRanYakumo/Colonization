using TMPro;
using UnityEngine;

public class ViewAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void DisplayAmount(int amount)
    {
        _text.text = amount.ToString();
    }
}