using System.Collections;
using UnityEngine;

public class DefaultBullet : Bullet
{
    [SerializeField] private Rigidbody _rigidbody;

    public override void Initialize(float damage, float lifeTime, float speed)
    {
        _damage = damage;
        _lifeTime = lifeTime;
        _speed = speed;
    }

    public override void SetStartPosition(Vector3 direction, Vector3 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);

        StartCoroutine(Fly(direction, GetFlyEnded()));
    }

    private IEnumerator Fly(Vector3 direction, System.Action<Bullet> flyEnded)
    {
        WaitForSeconds wait = new WaitForSeconds(_lifeTime);
        _rigidbody.velocity = direction * _speed;

        yield return wait;

        EndFly(this);
    }
}
