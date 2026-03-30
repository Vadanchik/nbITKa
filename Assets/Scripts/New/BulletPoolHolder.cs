using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolHolder
{
    private Dictionary<Type, ObjectPool<Bullet>> _pools = new Dictionary<Type, ObjectPool<Bullet>>();

    private int _poolMaxSize = 5;
    private int _poolCapacity = 5;

    private Vector3 _direction;
    private Vector3 _position;

    public BulletPoolHolder()
    {

    }

    public T Get<T>(Bullet prefab, Vector3 direction, Vector3 position) where T : Bullet
    {
        _direction = direction;
        _position = position;

        if (_pools.TryGetValue(typeof(T), out ObjectPool<Bullet> pool))
        {
            return (T)pool.Get();
        }

        _pools.Add(typeof(T), new ObjectPool<Bullet>(
            createFunc: () => Create<Bullet>(prefab),
            actionOnGet: (obj) => DoOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => UnityEngine.Object.Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            ));

        return (T)pool.Get();
    }

    private T Create<T>(Bullet bullet) where T : Bullet
    {
        return (T)UnityEngine.Object.Instantiate(bullet);
    }

    private void DoOnGet(Bullet bullet)
    {
        bullet.SetStartPosition(_direction, _position);
        bullet.FlyEnded += PoolRelease;
    }

    protected void PoolRelease(Bullet bullet)
    {
        bullet.FlyEnded -= PoolRelease;
        _pools.TryGetValue(bullet.GetType(), out ObjectPool<Bullet> pool);
        pool.Release(bullet);
    }
}
