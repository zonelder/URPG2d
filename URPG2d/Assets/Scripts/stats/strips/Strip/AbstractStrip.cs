using UnityEngine;

[System.Serializable]
public class AbstractStrip
{
    [SerializeField] private float _max;
    // Unit per Second
    [SerializeField] private float _regen;
    private float _atrophy;

    public AbstractStrip(float max)
    {
        _max = max;
        _regen = 0;
        _atrophy = 100;
    }

    public AbstractStrip(float max, float reg, float Atrophy = 100)
    {
        _max = max;
        _atrophy = Atrophy;
        _regen = reg;
    }

    public float Max => _max;

    public float Regen => _regen;

    public float Atrophy => _atrophy;

    public void AddToMax(float additional) => _max += additional;

    public void AddRegen(float additional) => _regen += additional;

    public void AddToAtrophy(float additional) => _atrophy += additional;

}

