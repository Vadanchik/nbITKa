using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class WeaponBase : MonoBehaviour
{
    [SerializeField]  private string _name;
    [SerializeField] private WeaponMesh _mesh;
    private float _firerate;
    private Bullet _bullet;
    private float _damage;
    private float _lifetime;
    private float _speed;

    private Coroutine _currentShootingCoroutine;

    public string Name { get => _name; set => _name = value; }
    public WeaponMesh Mesh { get => _mesh; set => _mesh = value; }
    public float Firerate { get => _firerate; set => _firerate = value; }
    public Bullet Bullet { get => _bullet; set => _bullet = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Lifetime { get => _lifetime; set => _lifetime = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public void Initialize(WeaponData data)
    {
        _name = data.Name;
        _mesh = data.Mesh;
        _bullet = data.Bullet;
        _firerate = data.Firerate;
        _damage = data.Damage;
        _lifetime = data.Lifetime;
        _speed = data.Speed;

    }

    /*
    public WeaponBase(string name, WeaponMesh mesh, float shootDelay, Bullet bullet, float damage, float lifeTime, float speed)
    {
        _name = name;
        _mesh = mesh;
        _firerate = shootDelay;
        _bullet = bullet;
        _damage = damage;
        _lifetime = lifeTime;
        _speed = speed;
    }
    
    public void StartShoot()
    {
        _currentShootingCoroutine = StartCoroutine(Shooting());
    }

    public void StopShoot()
    {
        if (_currentShootingCoroutine != null)
        {
            StopCoroutine(_currentShootingCoroutine);
        }
    }
    */

    public void Upgrade(IUpgradeHolder holder, ISpawner<Bullet> spawner)
    {
        if (_currentShootingCoroutine != null)
        {
            StopCoroutine(_currentShootingCoroutine);
        }

        float damage = CalculateValue(_damage, holder.GetBonusDamage());

        _bullet.Initialize(damage, _lifetime, _speed);

        float firerate = CalculateValue(_firerate, holder.GetBonusFirerate());

        _currentShootingCoroutine = StartCoroutine(Shooting(firerate, spawner));
    }

    public IEnumerator Shooting(float firerate, ISpawner<Bullet> spawner)
    {
        while (enabled)
        {
            Debug.Log(spawner != null);
            Shoot(spawner);

            yield return new WaitForSeconds(1 / _firerate);
        }
    }

    public void Shoot(ISpawner<Bullet> spawner)
    {
        spawner.Spawn(_bullet, _mesh.ShootDirection, _mesh.ShootPoint);
    }

    private float CalculateValue(float value, float bonus)
    {
        float calculatedValue = value + value * bonus;

        return calculatedValue;
    }
}
