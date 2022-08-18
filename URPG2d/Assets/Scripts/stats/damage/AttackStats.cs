[System.Serializable]
public class AttackStats
{
    public Multiplier DamageAmp;
    public Cooldown  Duration= new Cooldown(1);

    public AttackStats(float dAmp = 1, float duration = 1)
    {
        DamageAmp = new Multiplier(dAmp);
        Duration.Timer = duration;
    }
    public void SetAll(float dAmp, float time)
    {
        DamageAmp.SetValue(dAmp);
        Duration.Timer = time;
    }
}