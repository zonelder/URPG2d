using System.Collections.Generic;
using System.Collections;
using System;

public class DamageEvent
{
    // In other case i can do List<Delegate> _onDamageDelegates;
    // but then i'll have to check the type of each delegate in order to give'em necessary parametrs;
    private readonly List<Action<UnitEntity, GeneratedDamage>> _fullInfoDelegates = new List<Action<UnitEntity, GeneratedDamage>>();
    private readonly List<Action<UnitEntity>> _unitInfoDelegates = new List<Action<UnitEntity>>();
    private readonly List<Action<GeneratedDamage>> _damageInfoDelegates = new List<Action<GeneratedDamage>>();
    private readonly List<Action> _noneInfoDelegates = new List<Action>();

    public void AddSubscriber(Action<UnitEntity, GeneratedDamage> OnDamageMethod) => _fullInfoDelegates.Add(OnDamageMethod);
    public void RemoveSubscriber(Action<UnitEntity, GeneratedDamage> OnDamageMethod) => _fullInfoDelegates.Remove(OnDamageMethod);


    public void AddSubscriber(Action<UnitEntity> OnDamageMethod) => _unitInfoDelegates.Add(OnDamageMethod);
    public void RemoveSubscriber(Action<UnitEntity> OnDamageMethod) => _unitInfoDelegates.Remove(OnDamageMethod);

    public void AddSubscriber(Action<GeneratedDamage> OnDamageMethod) => _damageInfoDelegates.Add(OnDamageMethod);
    public void RemoveSubscriber(Action<GeneratedDamage> OnDamageMethod) => _damageInfoDelegates.Remove(OnDamageMethod);

    public void AddSubscriber(Action OnDamageMethod) => _noneInfoDelegates.Add(OnDamageMethod);
    public void RemoveSubscriber(Action OnDamageMethod) => _noneInfoDelegates.Remove(OnDamageMethod);
    public void Invoke(UnitEntity target, GeneratedDamage damage)
    {
        foreach (var OnGetDamage in _fullInfoDelegates)
        {
            OnGetDamage(target, damage);
        }
        foreach (var OnGetDamage in _unitInfoDelegates)
        {
            OnGetDamage(target);
        }
        foreach (var OnGetDamage in _damageInfoDelegates)
        {
            OnGetDamage(damage);
        }
        foreach (var OnGetDamage in _noneInfoDelegates)
        {
            OnGetDamage();
        }
    }
}
