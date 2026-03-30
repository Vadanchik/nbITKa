using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _spawnCartridge;
    [SerializeField, Range(0, 100)] private float _speed;

    private Transform _thisTransform;

    private void Start()
    {
        _thisTransform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        CalculatorDirection calculatorDirection = new CalculatorDirection(_thisTransform, _spawnCartridge);
        Vector3 direction = calculatorDirection.GetDirection();

        IMovement movement = new CalculatorMovementByDirection(direction, _speed);
        ICreatorCartridge creator = new CreatorDefaultCartridge(_prefab, _parent);

        Shooting shooting = new Shooting(movement, creator);
        shooting.Shoot(_spawnCartridge.position);
    }
}
