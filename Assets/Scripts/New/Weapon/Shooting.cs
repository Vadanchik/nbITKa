using UnityEngine;

public class Shooting
{
    private readonly IMovement _movement;
    private readonly ICreatorCartridge _creator;

    public Shooting(IMovement movement, ICreatorCartridge creator)
    {
        _movement = movement;
        _creator = creator;
    }

    public void Shoot(Vector3 position)
    {
        ICartridge cartridge = _creator.Create(position);
        cartridge.ToRun(_movement);
    }
}
