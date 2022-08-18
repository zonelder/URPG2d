using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHit : IHit
{
    private Movement _movement;
    public TeleportationHit(GameObject unit) 
    {
        _movement= unit.GetComponent<Movement>();
    }
    public void Hit(Vector2 hitPoint, UnitEntity user)
    {
        user.StartCoroutine(TeleportationByTime(hitPoint, user));
    }

    private IEnumerator TeleportationByTime(Vector2 TeleportPoint, UnitEntity unit)
    {
        float speed = 5;
        Rigidbody2D unitRigid = unit.GetComponent<Rigidbody2D>();
        Vector2 direction = (TeleportPoint-unitRigid.position).normalized;
        while (unitRigid.position != TeleportPoint)
        {

           //_movement.Move(direction*speed*Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}