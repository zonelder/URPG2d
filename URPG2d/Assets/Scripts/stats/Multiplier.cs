using UnityEngine;

[System.Serializable]
public class Multiplier
{
    [SerializeField]
    [Range(0, 3)]
    private float _value;

    public Multiplier(float value = 0)
    {
        _value = value;
    }

    public void SetValue(float value) => _value = value;
    public static Multiplier operator +(Multiplier a, Multiplier b) => new Multiplier(a._value + b._value);

    public static Multiplier operator +(Multiplier a, float b) => new Multiplier(a._value + b);
    public static Multiplier operator +(float a, Multiplier b) => new Multiplier(a + b._value);

    public static Multiplier operator -(Multiplier a, Multiplier b) => new Multiplier(a._value - b._value);
    public static Multiplier operator -(Multiplier a, float b) => new Multiplier(a._value - b);
    public static Multiplier operator -(float a, Multiplier b) => new Multiplier(b._value - a);

    public static implicit operator float(Multiplier multiplier) => multiplier._value;

}
