using UnityEngine;

public class BulletSpawner : Spawner<Bullet>, ISpawner<Bullet>
{
    private Vector3 _direction;
    private Vector3 _position;

    public void Spawn(Bullet bullet, Vector3 direction, Vector3 position)
    {
        _prefab = bullet;
        _direction = direction;
        _position = position;
        _pool.Get();
    }

    protected override Bullet Create()
    {
        Bullet bullet = Instantiate(_prefab);

        return bullet;
    }

    protected override void DoOnGet(Bullet bullet)
    {
        bullet.SetStartPosition(_direction, _position);
        bullet.FlyEnded += PoolRelease;
    }

    protected override void PoolRelease(Bullet bullet)
    {
        bullet.FlyEnded -= PoolRelease;
        _pool.Release(bullet);
    }
}
