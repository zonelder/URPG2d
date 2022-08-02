using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public bool Use;//если персонаж взаимодействует с чем-то
    [SerializeField] private Movement _movement;

    private void FixedUpdate()
    {
        float right = Input.GetAxis("Horizontal");
        float up = Input.GetAxis("Jump");
        float InputAmp = 1 + Input.GetAxis("Sprint");
        //_movement.Move(new Vector2(right*InputAmp, up),true);//заглушка пока что
    }
    private void Update()
    {
        Use = false;
        if (Input.GetKey(KeyCode.R))
        {
            Use = true;
        }
    }
}
