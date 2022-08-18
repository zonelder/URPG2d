using UnityEngine;
using stats;
using System;

public enum LifeStates
{
    STABLE,
    BODY_ON_THE_EDGE,
    MIND_ON_THE_EDGE,
    DEAD,
    UNDEFINE,
}
public delegate void ChangeDamageMethod(ref GeneratedDamage damage);
public class UnitEntity : MonoBehaviour
{
    public event Action OnRevive;
    // damage packet
    public event ChangeDamageMethod InfluenceTakenDamage;
    public DamageEvent OnGetDamage = new DamageEvent();
    public DamageEvent OnDoneDamage = new DamageEvent();
    //
    public event Action OnDead;

    [SerializeField] private LifeStates state;
    public Level Exp;

    public Stats Stats;

    private Coroutine _MPAtrophy;
    private Coroutine _HPAtrophy;

    private void Start()
    {
        state = LifeStates.STABLE;
        StartCoroutine(Stats.HP.RegenerateByTime());
        StartCoroutine(Stats.MP.RegenerateByTime());

    }
    private void OnHPOver()
    {
        if (state == LifeStates.MIND_ON_THE_EDGE)
        {
            StopCoroutine(_HPAtrophy);
            state = LifeStates.DEAD;
            Kill();
        }
        else
        {
            _MPAtrophy = StartCoroutine(Stats.MP.StartAtrophy());
            state = LifeStates.BODY_ON_THE_EDGE;
        }
    }
    private void OnMPOver()
    {
        if (state == LifeStates.BODY_ON_THE_EDGE)
        {
            StopCoroutine(_MPAtrophy);
            state = LifeStates.DEAD;
            Kill();
        }
        else
        {
            _HPAtrophy = StartCoroutine(Stats.HP.StartAtrophy());
            state = LifeStates.MIND_ON_THE_EDGE;

        }
    }

    public void Revive()
    {
        if (IsAlive)
            throw new System.InvalidOperationException("unit is already alive");
        OnRevive?.Invoke();
        state = LifeStates.STABLE;
        Stats.HP.Refresh();

        Stats.MP.Refresh();
    }
    public void Kill()
    {
        state = LifeStates.DEAD;
        Stats.HP.SetEmpty();
        Stats.MP.SetEmpty();
    }
    public bool IsAlive => state != LifeStates.DEAD;

    private void GetDamage(GeneratedDamage damage)
    {
        InfluenceTakenDamage?.Invoke(ref damage);
        Stats.HP.DistractFromCurrent(damage);
        OnGetDamage?.Invoke(this, damage);

    }
    public void DoneDamage(UnitEntity enemy)
    {
        if (enemy != this)
        {
            GeneratedDamage calculatedDamage = Stats.CalculateDamage();
            enemy.GetDamage(calculatedDamage);
            OnDoneDamage?.Invoke(enemy, calculatedDamage);

            if (enemy.UnitDead())
            {
                Exp.CatchExpirience(Exp.DieExpirience());//если после нанесени€ урона хп мало то выдаем опыт убийце
                OnDead?.Invoke();
            }
        }
    }
    public bool UnitDead() => Stats.HP.Current() <= 0;

    //имитаци€ физики(убрать/помен€ь)
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Weapon>() != null)
        {
            UnitEntity attaker = collision.gameObject.GetComponent<Weapon>().Carrier;//узнаем самого атакующего по его оружию
            attaker.DoneDamage(this);
        }
    }
    private void OnEnable()
    {
        Stats.HP.OnStripOver += OnHPOver;
        Stats.MP.OnStripOver += OnMPOver;
    }
    private void OnDisable()
    {
        Stats.HP.OnStripOver -= OnHPOver;
        Stats.MP.OnStripOver -= OnMPOver;
    }
}
