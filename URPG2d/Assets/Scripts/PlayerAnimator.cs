using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _unitMove;

    private void Start()
    {
        _unitMove.OnJump +=AE_OnJump;
    }
    private void OnDisable()
    {
        _unitMove.OnJump -=AE_OnJump;
    }

    private void Update()
    {
        //Check if character just landed on the ground
        if (_unitMove.IsGrounded)
        {
            _animator.SetBool("Grounded", true);
        }
        else
        {
            _animator.SetBool("Grounded", false);
        }


        // Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", _unitMove.Velocity.y);

        // Set Animation layer for hiding sword
        int boolInt =_unitMove.IsHideSword ? 1 : 0;
        _animator.SetLayerWeight(1, boolInt);

                //Run
         if (_unitMove.IsMoving)
            _animator.SetInteger("AnimState", 1);
        //Idle
        else
            _animator.SetInteger("AnimState", 0);

    }

    private void AE_OnJump()
    {
      
        _animator.SetTrigger("Jump");
        //_animator.SetBool("Grounded", false);

    }
    private void AE_OnJumpToFall()
    {
        Debug.Log("in JUm Tofall");
        _animator.ResetTrigger("Jump");

    }
    void AE_Landing()
    {
        _animator.ResetTrigger("Jump");
    }

}
