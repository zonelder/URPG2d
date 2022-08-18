using UnityEngine;
public class AbilityInput : MonoBehaviour
{
    [SerializeField] SkillHandler _skillBook;
    [SerializeField] AbilityActivator _activator;

    private void Update()
    {
        InputToActivateAbility();
    }
    private void InputToActivateAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbilityAt(0);
        }
    }
    private void UseAbilityAt(int i)
    {
        ActiveAbility curAbility = _skillBook.GetAbilityAt(i);
        _activator.TryActivateAbility(curAbility);
    }


}