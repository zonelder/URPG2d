public enum DamageType
{
    common,
    crit
}
public class GeneratedDamage
{
    public float damage;
    public DamageType type;

    public GeneratedDamage(float d, DamageType _type)
    {
        damage = d;
        type = _type;
    }


    public static implicit operator float(GeneratedDamage cur) => cur.damage;
}
