using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSkills : MonoBehaviour
{
    [SerializeField] private SkillHandler _skillBook;

    public void Start()
    {
        ActiveAbility NewAbility2 = new ActiveAbility(1);
        AbstractAttack firstAttack2 = new RaycastAttack(gameObject);
        // Что тестируем то и раскоменчиваем
        //((RaycastAttack)firstAttack2).ShootBehaviour = new TeleportationHit(gameObject);
        //((RaycastAttack)firstAttack2).AimBehaviour = new LinearAim(GetComponent<Movement>().LayerMask);
        NewAbility2.AddAttack(firstAttack2);
        _skillBook.AddAbility(NewAbility2);

    }
}
