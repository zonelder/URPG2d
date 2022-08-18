using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillHandler : MonoBehaviour
{
    [SerializeField] private List<ActiveAbility> _ability = new List<ActiveAbility>();

    public ActiveAbility GetAbilityAt(int i) => _ability[i];
    public void AddAbility(ActiveAbility newAbility)
    {
        _ability.Add(newAbility);
    }
    public void RemoveAbility(ActiveAbility newAbility)
    {
        _ability.Remove(newAbility);
    }
    public void CreateVoidAbility()
    {
        _ability.Add(new ActiveAbility(1));
    }

}
