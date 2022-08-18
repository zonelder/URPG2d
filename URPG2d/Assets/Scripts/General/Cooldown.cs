using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float _cooldownTime;
    private float _remainingTime;

    public float Timer
    {
        get => _cooldownTime;
        set
        {
            if(!IsReady)
            {
                Debug.Log("try to change TImer in countdown");
                if(_remainingTime>value)
                {
                    _remainingTime = value;
                }
            }
        }

    }

    public event Action OnEndCountDown;
    public event Action OnStartCountDown;


    public Cooldown(float cooldownTime)
    {
        _cooldownTime = cooldownTime;
        _remainingTime = 0;
    }
    public float PassedTime
    {
        get
        {
            if (!IsReady)
                return _cooldownTime - _remainingTime;
            else
                throw new InvalidOperationException("timer hasn't start,but was try to get Passed time");
        }
    }
    public bool IsReady => _remainingTime <= 0;


    public IEnumerator Start()
    {
        if (!IsReady)
        {
            Debug.Log("timer is not ready");
        }
        else
        {
            _remainingTime = _cooldownTime;
            OnStartCountDown?.Invoke();
            while(!IsReady)
            {
                _remainingTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _remainingTime = 0.0f;
            OnEndCountDown?.Invoke();
        }
    }
    public void Restart()
    {
        OnEndCountDown?.Invoke();

        _remainingTime = _cooldownTime;

        OnStartCountDown?.Invoke();

    }

    public void RestartCounter()=>_remainingTime = _cooldownTime;

}

