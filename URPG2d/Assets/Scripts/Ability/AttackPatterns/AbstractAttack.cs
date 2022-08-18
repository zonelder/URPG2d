using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class AbstractAttack
{
    public AttackStats Property = new AttackStats();
    public float CurrentDuration
    {
        get;
        private set;
    }
    public abstract bool IsDone { get; }
    protected abstract void StartAttack();

    public IEnumerator AttackByTime(UnitEntity unit)
    {
        unit.Stats.AddAttackEffects(Property);
        StartAttack();
        CurrentDuration = 0;
        while (!IsDone)
        {
            // то что не зависит от времени на прямую требует его прямой зависимости
            TickTime(Time.fixedDeltaTime * unit.Stats.Amplifiers.AttackSpeedAmp);
            CurrentDuration += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
        unit.Stats.DistractAttackEffects(Property);
        EndAttack();
    }
    protected abstract void TickTime(float delta);
    protected abstract void EndAttack();
}