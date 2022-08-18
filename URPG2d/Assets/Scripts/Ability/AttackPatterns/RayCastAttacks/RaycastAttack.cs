using UnityEngine;
using RS;
namespace RS
{
    public enum RaycastState
    {
        BEFORE_AIMING,
        AIMING,
        SHOOT,
        AFTER_SHOOT
    }
}



[System.Serializable]
public class RaycastAttack : AbstractAttack
{
    public override  bool IsDone => rState == RaycastState.AFTER_SHOOT;
    [SerializeField] private float weaponRange = 3;

    private RaycastState rState;
    private UnitEntity _user;
    private Camera _camera;
    private Transform _weaponTransform;
    [SerializeField] private float timeToAim = 0.0f;
    [SerializeField] private float timeToShoot = 0.4f;

    public RaycastAttack(GameObject user)
    {
        _user = user.GetComponent<UnitEntity>();
        _weaponTransform = user.transform.Find("Weapon").transform;
        _camera = user.transform.Find("PlayerCamera").gameObject.GetComponent<Camera>();
    }
    protected sealed override void StartAttack()
    {
        rState = RaycastState.BEFORE_AIMING;
    }
    protected sealed override void TickTime(float delta)
    {
        if (CurrentDuration > timeToAim && rState == RaycastState.BEFORE_AIMING)
        {
            rState = RaycastState.AIMING;
        }
        if (rState == RaycastState.AIMING && !(CurrentDuration > timeToShoot))
        {
            Aim();
        }
        if (rState == RaycastState.AIMING && CurrentDuration > timeToShoot)
        {
            rState = RaycastState.SHOOT;
            Shoot();
        }
    }
    private void Aim()
    {
    }
    private void Shoot()
    {
        Vector2 startPoint = new Vector2(_weaponTransform.position.x, _weaponTransform.position.y);
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - startPoint).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, weaponRange);
        if (hit)
        {
            _user.DoneDamage(hit.collider.GetComponent<UnitEntity>());
        }

        rState = RaycastState.AFTER_SHOOT;
    }
    protected sealed override void EndAttack()
    {
    }

}
