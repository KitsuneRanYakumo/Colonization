using UnityEngine;

public class ScannedZone : MonoBehaviour
{
    [SerializeField] private TerritoryScanner _scanner;

    private void OnEnable()
    {
        _scanner.ScaleChanged += ViewScale;
    }

    private void OnDisable()
    {
        _scanner.ScaleChanged -= ViewScale;
    }

    private void ViewScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}