using UnityEngine;

[System.Serializable]
public class Damage
{
    [SerializeField] private float _min;
    [SerializeField] private float _max;
    [SerializeField] private float _critChance;
    public float Min
    {
        get => _min;
        private set
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("damage cant be negative");
            _min = value;
        }
    }
    public float Max
    {
        get => _max;
        private set
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("damage cant be negative");
            _max = value;
        }
    }
    public float CritChanñe
    {
        get => _critChance;
        private set
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("crit chance  cant be negative");
            _critChance = value;
        }
    }
    public Multiplier CritMultiplier;//çíà÷åíèå áîëüøå îäíîãî (åñëè critDamage=1.5 à íàíåñëè ìû 100 óðîíà òî ïðè êðèòå áóäåò 150)

    public Damage()
    {
        Min = 0;
        Max = 0;
        CritChanñe = 0;
        CritMultiplier = new Multiplier();
    }
    public Damage(float min, float max, float critChance, float critMult)
    {
        Min = min;
        Max = max;
        CritChanñe = critChance;
        CritMultiplier = new Multiplier(critMult);
    }
    public void ChangeDamage(float d_min, float d_max)
    {
        Min += d_min;
        Max += d_max;
    }
    public void ChangeCritChance(float d_chance)
    {
        if (CritChanñe + d_chance < 0)
            CritChanñe = 0;
        else
            CritChanñe += d_chance;
    }
    public GeneratedDamage calculate()
    {
        float curDamage = 0;
        DamageType type = DamageType.common;
        curDamage = Random.Range(Min, Max);
        if (Random.Range(0.0f, 100.0f) <= CritChanñe)
        {
            type = DamageType.crit;
            curDamage *= CritMultiplier;
        }
        return new GeneratedDamage(curDamage, type);
    }

}
