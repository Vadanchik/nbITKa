using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputServcice))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHandler))]
public class Player : MonoBehaviour
{
    private InputServcice _input;
    private Movement _movement;
    private WeaponHandler _shooter;

    public void Initialize(ISpawner<Bullet> bulletSpawner, WeaponData starterWeapon)
    {
        _input = GetComponent<InputServcice>();
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<WeaponHandler>();
        _movement.Initialize(_input);

        _shooter.SetWeapon(starterWeapon);
        _shooter.Initialize(bulletSpawner);
    }
}
