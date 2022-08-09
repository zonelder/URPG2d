using UnityEngine;

namespace stats
{
    [System.Serializable]
    public class Stats//хранение и расчеты для юнитов
    {
        public Strip MP;
        public Strip HP;
        public Strip SP;

        public Damage Damage;
        public SecondaryStats Amplifiers;

        public Stats(BaseStats baseStats)
        {
            HP = new Strip(baseStats.HP);
            MP = new Strip(baseStats.MP);
            SP = new Strip(baseStats.SP);
            Damage = new Damage();
            Amplifiers = new SecondaryStats();
        }


        public Stats()
        {
            Damage = new Damage();
            Amplifiers = new SecondaryStats();
        }

        public GeneratedDamage CalculateDamage()
        {
            GeneratedDamage damage = Damage.calculate();
            damage.damage *= Amplifiers.DamageAmp;
            return damage;
        }
        public void AddAttackEffects(AttackStats attackStats)
        {
            Amplifiers.AttackSpeedAmp += attackStats.DamageAmp;
        }
        public void DistractAttackEffects(AttackStats attackStats)
        {
            Amplifiers.AttackSpeedAmp -= attackStats.DamageAmp;
        }

    }
}