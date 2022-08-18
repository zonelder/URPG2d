using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class ActiveAbility : AbstractAbility
{
    public Cooldown Cooldown = new Cooldown(1);

    public event Action OnAbilityEnd;
    public event Action OnAbilityStart;

    [SerializeReference] private List<AbstractAttack> _attacks = new List<AbstractAttack>();
    public ActiveAbility(float cooldown) => Cooldown = new Cooldown(cooldown);
    public int StrokesCount => _attacks.Count;
    public void AddAttack(AbstractAttack NewAttack)
    {
        _attacks.Add(NewAttack);
    }
    public AbstractAttack GetAttack(int index) => _attacks[index];

    public IEnumerator AbilityByTime(UnitEntity unit)
    {
        unit.StartCoroutine(Cooldown.Start());
        OnAbilityStart?.Invoke();

        foreach(var attack in _attacks)
        {
            yield return unit.StartCoroutine(attack.AttackByTime(unit));
        }

        OnAbilityEnd?.Invoke();
    }
}
