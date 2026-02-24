using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponData _weaponData;

    private void Awake()
    {
        _bulletSpawner.Initialize();
        _player.Initialize(_bulletSpawner, _weaponData);
    }
}
