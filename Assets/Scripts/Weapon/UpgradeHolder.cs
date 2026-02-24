using System;
using UnityEngine;

public class UpgradeHolder : MonoBehaviour, IUpgradeHolder
{
    private float _firerate = 0;
    private float _damage = 0;

    public event Action Upgraded;

    public void AddFirerate(float value)
    {
        _firerate += value;

        Upgraded.Invoke();
    }

    public void AddDamage(float value)
    {
        _damage += value;

        Upgraded.Invoke();
    }

    public float GetBonusFirerate()
    {
        return _firerate;
    }

    public float GetBonusDamage()
    {
        return _damage;
    }
}

public interface IUpgradeHolder
{
    public float GetBonusFirerate();

    public float GetBonusDamage();
}
