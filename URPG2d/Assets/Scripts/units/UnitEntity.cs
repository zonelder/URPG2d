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

    public Stats Improved;

    private Coroutine _MPAtrophy;
    private Coroutine _HPAtrophy;

    private void Start()
    {
        state = LifeStates.STABLE;
        StartCoroutine(Improved.HP.RegenerateByTime());
        StartCoroutine(Improved.MP.RegenerateByTime());

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
            _MPAtrophy = StartCoroutine(Improved.MP.StartAtrophy());
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
            _HPAtrophy = StartCoroutine(Improved.HP.StartAtrophy());
            state = LifeStates.MIND_ON_THE_EDGE;

        }
    }
    public void GoBodiless()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    public void GoBodily()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
    public void Revive()
    {
        if (IsAlive)
            throw new System.InvalidOperationException("unit is already alive");
        OnRevive?.Invoke();
        state = LifeStates.STABLE;
        Improved.HP.Refresh();

        Improved.MP.Refresh();
    }
    public void Kill()
    {
        state = LifeStates.DEAD;
        Improved.HP.SetEmpty();
        Improved.MP.SetEmpty();
    }
    public bool IsAlive => state != LifeStates.DEAD;

    public void GetDamage(GeneratedDamage damage)
    {
        InfluenceTakenDamage?.Invoke(ref damage);
        Improved.HP.DistractFromCurrent(damage);
        OnGetDamage?.Invoke(this, damage);

    }
    public void DoneDamage(UnitEntity enemy)
    {
        if (enemy != this)
        {
            GeneratedDamage calculatedDamage = Improved.CalculateDamage();
            enemy.GetDamage(calculatedDamage);
            OnDoneDamage?.Invoke(enemy, calculatedDamage);

            if (enemy.UnitDead())
            {
                Exp.CatchExpirience(Exp.DieExpirience());//если после нанесени€ урона хп мало то выдаем опыт убийце
                OnDead?.Invoke();
            }
        }
    }
    public bool UnitDead() => Improved.HP.Current() <= 0;

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
        Improved.HP.OnStripOver += OnHPOver;
        Improved.MP.OnStripOver += OnMPOver;
    }
    private void OnDisable()
    {
        Improved.HP.OnStripOver -= OnHPOver;
        Improved.MP.OnStripOver -= OnMPOver;
    }
}
