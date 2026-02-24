using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _speed;

    public event Action<Bullet> FlyEnded;

    public abstract void Initialize(float damage, float lifeTime, float speed);

    public abstract void SetStartPosition(Vector3 direction, Vector3 startPosition);

    public Action<Bullet> GetFlyEnded()
    {
        return FlyEnded;
    }

    protected void EndFly(Bullet bullet)
    {
        FlyEnded?.Invoke(bullet);
    }
}
