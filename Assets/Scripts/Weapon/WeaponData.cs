using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create new Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private WeaponBase weapon;
    [SerializeField] private string _name;
    [SerializeField] private WeaponMesh _mesh;
    [SerializeField] private float _firerate;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;

    public string Name => _name;
    public WeaponMesh Mesh => _mesh;
    public float Firerate => _firerate;
    public Bullet Bullet => _bullet;
    public float Damage => _damage;
    public float Lifetime => _lifetime;
    public float Speed => _speed;

    public void Shoot(ISpawner<Bullet> spawner, Vector3 direction)
    {
        spawner.Spawn(_bullet, direction, _mesh.ShootPoint);
    }
}
