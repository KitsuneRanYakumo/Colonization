using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircularSpawnGrid : MonoBehaviour
{
    [SerializeField] private float _intervalBetweenPointsInCircle = 1;
    [SerializeField] private float _intervalCircles = 1;
    [SerializeField] private float _radiusFirstCircle = 1;
    [SerializeField] private int _partCircleDegrees = 360;
    [SerializeField] private int _stockAmount = 10;

    private List<Vector3> _positions;
    private int _amountCircles;
    private float _radiusCurrentCircle;
    private int _currentPosition;

    public event Action<float> DiameterChanged;

    public void Initialize(int amountCircles = 1)
    {
        if (amountCircles <= 0)
            amountCircles = 1;

        _amountCircles = amountCircles;
        _positions = new List<Vector3>(GetAmountPoints(_radiusFirstCircle));
        _currentPosition = 0;
        _radiusCurrentCircle = _radiusFirstCircle;
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
        int amountPoints = GetAmountPoints(_radiusCurrentCircle);
        float angleStep = _partCircleDegrees / amountPoints * Mathf.Deg2Rad;

        for (int i = 0; i < amountPoints; i++)
        {
            float angle = angleStep * i;
            Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radiusCurrentCircle;
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