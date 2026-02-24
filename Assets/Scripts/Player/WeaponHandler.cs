using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponBase _weapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private UpgradeHolder _upgradeHolder;

    private ISpawner<Bullet> _spawner;
    private bool _isShooting = true;
    private WeaponMesh _currentMesh;

    private void OnEnable()
    {
        _upgradeHolder.Upgraded += UpgradeWeapon;
    }
    
    public void Initialize(ISpawner<Bullet> spawner)
    {
        _spawner = spawner;
        UpgradeWeapon();
    }

    public void SetWeapon(WeaponData weaponData)
    {
        if (_currentMesh != null)
        {
            Destroy(_currentMesh);
        }

        _weapon = new WeaponBase();
        _weapon.Initialize(weaponData);
        _currentMesh = Instantiate(_weapon.Mesh, _weaponPoint);
    }

    public void UpgradeWeapon()
    {
        _weapon.Upgrade(_upgradeHolder, _spawner);
    }
}
