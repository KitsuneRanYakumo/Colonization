using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircularGrid : MonoBehaviour
{
    [SerializeField] private float _intervalBetweenPointsInCircle = 1;
    [SerializeField] private float _intervalCircles = 1;
    [SerializeField] private float _radiusFirstCircle = 1;
    [SerializeField] private int _stockAmount = 10;
    [SerializeField] private List<Vector3> _positions;

    private float _referenceAngle;
    private int _amountCircles;
    private int _currentPosition;
    private float _radiusCurrentCircle;

    public event Action<float> DiameterChanged;

    public void Initialize(float referenceAngle, int amountCircles = 1)
    {
        if (amountCircles <= 0)
            amountCircles = 1;

        _referenceAngle = referenceAngle;
        _amountCircles = amountCircles;
        _currentPosition = 0;
        _radiusCurrentCircle = _radiusFirstCircle;
        _positions = new List<Vector3>(GetAmountPoints(_radiusFirstCircle));
        DiameterChanged?.Invoke((_radiusFirstCircle + _amountCircles * _intervalCircles) * 2);
        CreateGrid();
    }

    public Vector3 GetCurrentPosition()
    {
        if (_positions.Count - _currentPosition < _stockAmount)
            CreatePositionsInCircle();

        return _positions[_currentPosition++];
    }

    public Vector3 GetRandomPosition()
    {
        return _positions[Random.Range(0, _positions.Count)];
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _amountCircles; i++)
            CreatePositionsInCircle();
    }

    private void CreatePositionsInCircle()
    {
        int degreesInCircle = 360;
        int amountPoints = GetAmountPoints(_radiusCurrentCircle);
        float angleStep = degreesInCircle / amountPoints * Mathf.Deg2Rad;
        float referenceAngleToRad = _referenceAngle * Mathf.Deg2Rad;

        for (float i = 0; i < amountPoints; i++)
        {
            float angle = angleStep * i + referenceAngleToRad;
            Vector2 position = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * _radiusCurrentCircle;
            _positions.Add(new Vector3(position.x + transform.position.x,
                                       transform.position.y,
                                       position.y + transform.position.z));
        }

        _radiusCurrentCircle += _intervalCircles;
        DiameterChanged?.Invoke((_radiusFirstCircle + _amountCircles * _intervalCircles) * 2);
    }

    private int GetAmountPoints(float radius)
    {
        float circleLength = 2 * radius * Mathf.PI;

        return Mathf.FloorToInt(circleLength / _intervalBetweenPointsInCircle);
    }
}