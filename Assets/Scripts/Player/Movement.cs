using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private InputServcice _input;

    public void Initialize(InputServcice input)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = input;
        _input.Moving += Move;
        _input.MoveEnded += Stop;
    }

    private void OnEnable()
    {
        if (_input != null)
        {
            _input.Moving += Move;
            _input.MoveEnded += Stop;
        }
    }

    private void OnDisable()
    {
        if (_input != null)
        {
            _input.Moving -= Move;
            _input.MoveEnded -= Stop;
        }
    }

    public void Move(Vector3 position)
    {
        Debug.Log("Move");
        Vector3 direction = position - transform.position;
        float distance = direction.magnitude;
        direction = direction.normalized;

        if (distance > 0.1f)
        {
            _rigidbody.velocity = direction * _speed;
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
