using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SecondaryStats
{

    public Multiplier SpeedAmp;
    public Multiplier AttackSpeedAmp;
    public Multiplier DamageAmp;
    public Multiplier ExpScoreAmp;

    public void OnMove(ref Vector3 movement)
    {
        movement *= SpeedAmp;
    }
    public void OnAttackTick(ref float delta)
    {
        delta *= AttackSpeedAmp;
    }

    public void OnExpScore(ref float exp)
    {
        exp *= ExpScoreAmp;
    }

    public SecondaryStats()
    {
        SpeedAmp = new Multiplier(1);
        AttackSpeedAmp = new Multiplier(1);
        DamageAmp = new Multiplier(1);
        ExpScoreAmp = new Multiplier(1);
    }
}
