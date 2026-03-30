using System.Collections;
using UnityEngine;

public class DefaultCartridge : MonoBehaviour, ICartridge
{
    private Transform _thisTransform;

    public void ToRun(IMovement movement)
    {
        StartCoroutine(Run(movement));
    }

    public void DestroyYourself()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _thisTransform = transform;
    }

    private IEnumerator Run(IMovement movement)
    {
        while (true)
        {
            Vector3 point = movement.GetNextPoint();
            _thisTransform.Translate(point);
            yield return null;
        }
    }
}
