using UnityEngine;

public class CalculatorMovementByDirection : IMovement
{
    private readonly Vector3 _direction;
    private readonly float _speed;

    public CalculatorMovementByDirection(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    public Vector3 GetNextPoint()
    {
        return _direction * Time.deltaTime * _speed;
    }
}
