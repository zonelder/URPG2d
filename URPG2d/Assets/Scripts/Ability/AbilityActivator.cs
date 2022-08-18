using System.Collections;
using UnityEngine;
using System;

public enum UnitState
{
    WAITING,
    USE_ABILITY,
}
public class AbilityActivator : MonoBehaviour
{
    private UnitState state = UnitState.WAITING;
    private Coroutine _curAbility;
    private Movement _unitMove;
    private UnitEntity _unit;

    public void TryActivateAbility(ActiveAbility curAbility)
    {
        if (!curAbility.Cooldown.IsReady)
            Debug.Log("cooldown");
        Debug.Log("try activate");
        if (state == UnitState.WAITING && curAbility.Cooldown.IsReady)
        {
            if (_curAbility == null)
                _curAbility = StartCoroutine(AbilityByTime(curAbility));
        }
    }
    private IEnumerator AbilityByTime(ActiveAbility curAbility)
    {
        state = UnitState.USE_ABILITY;
        //_unitMove.MoveStrategy.IsMoveable = false;
        yield return StartCoroutine(curAbility.AbilityByTime(_unit));
        state = UnitState.WAITING;
        //_unitMove.MoveStrategy.IsMoveable = true;
        _curAbility = null;
    }

    private void OnEnable()
    {
        _unitMove = GetComponent<Movement>();
        _unit = GetComponent<UnitEntity>();
        if (_unit == null)
            Debug.Log("null");
    }
}
