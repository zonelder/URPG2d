using System;
using UnityEngine;

[System.Serializable]
public class Level
{
    public event Action OnLevelUp;
    private const float nextLvlAmp = 3;
    [SerializeField] private int lvl;
    private float expToUp;
    private float curExp;
    [SerializeField] private float expForDeath;

    public Level()
    {
        lvl = 0;
        expToUp = 1;
        curExp = 0;
        expForDeath = 1;
    }
    public Level(int _lvl)
    {
        lvl = _lvl;
        expToUp = Mathf.Pow(nextLvlAmp, lvl);
        curExp = 0;
        expForDeath = lvl * nextLvlAmp * 10;
    }
    public float DonePersent() => curExp / expToUp;
    public void lvlUp()
    {
        ++lvl;
        curExp = curExp - expToUp;
        expToUp *= nextLvlAmp;
        expForDeath += nextLvlAmp * 10;
        OnLevelUp?.Invoke();
        if (isEnouth)
            lvlUp();
    }
    public int level() => lvl;
    public float RequiredExp() => expToUp;
    public float DieExpirience() => expForDeath;
    public void SetDieExpirience(float exp) => expForDeath = exp;
    public void CatchExpirience(float exp)
    {
        curExp += exp;
        if (isEnouth)
            lvlUp();
    }
    public bool isEnouth => curExp >= expToUp;
}
