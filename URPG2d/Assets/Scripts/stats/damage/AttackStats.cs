[System.Serializable]
public class AttackStats
{
    public Multiplier DamageAmp;
    public float  Duration;

    public AttackStats(float dAmp = 1, float duration = 1)
    {
        DamageAmp = new Multiplier(dAmp);
        Duration = duration;
    }
    public void SetAll(float dAmp, float time)
    {
        DamageAmp.SetValue(dAmp);
        //Duration.SetCooldown(time);
        Duration = time;
    }
}