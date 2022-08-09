using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private UnitEntity _unit;
    [SerializeField] private Collider _hitBox;
    [SerializeField] private MeshRenderer _mesh;

    public UnitEntity Carrier => _unit;
    public Collider GetHitBox() => _hitBox;
    public void SetHitBox(Collider NewHitBox) => _hitBox = NewHitBox;
    public void ActivateHitBox()
    {
        _hitBox.enabled = true;
        _mesh.enabled = true;
    }
    public void DeactivateHitBox()
    {
        _hitBox.enabled = false;
        _mesh.enabled = false;
    }

    public Transform Transform
    {
        get;
    }
}
