using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GameCursor : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private RectTransform _position;
    [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Confined;

    private void OnEnable()
    {
        _userInput.MousePositionChanged += SetCursorPosition;
        _userInput.SelectionButtonPressed += HideSystemCursor;
    }

    private void Start()
    {
        HideSystemCursor();
        Cursor.lockState = _lockMode;
    }

    private void OnDisable()
    {
        _userInput.MousePositionChanged -= SetCursorPosition;
        _userInput.SelectionButtonPressed -= HideSystemCursor;
    }

    private void SetCursorPosition(Vector2 position)
    {
        _position.anchoredPosition = position;
    }

    private void HideSystemCursor()
    {
        Cursor.visible = false;
    }
}