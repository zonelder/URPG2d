using UnityEngine;

[System.Serializable]
public abstract class AbstractAbility
{
    [SerializeField]private string _name;
    protected AbstractAbility(string name)
    {
        _name = name;
    }
    protected AbstractAbility()
    {
        _name = "new ability";
    }
}
