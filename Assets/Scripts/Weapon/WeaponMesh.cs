using UnityEngine;

public class WeaponMesh : MonoBehaviour
{
    [SerializeField] private Transform _shootTransform;

    public Vector3 ShootPoint => _shootTransform.position;
    public Vector3 ShootDirection => _shootTransform.forward;
}
