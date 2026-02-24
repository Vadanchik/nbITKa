using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolMaxSize = 5;
    [SerializeField] private int _poolCapacity = 5;

    protected ObjectPool<T> _pool;

    public void Initialize()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (obj) => DoOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    protected abstract void PoolRelease(T obj);

    protected abstract T Create();

    protected abstract void DoOnGet(T obj);
}

public interface ISpawner<T> where T : MonoBehaviour
{
    void Spawn(T obj, Vector3 direction, Vector3 position);
}