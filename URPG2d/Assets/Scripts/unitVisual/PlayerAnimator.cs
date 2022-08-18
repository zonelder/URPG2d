using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _unitMove;
    [SerializeField] private SpriteRenderer _sprite;

    public int Facing => (_sprite.flipX)?(-1):(1);

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

        if (_unitMove.Velocity.x > 0)
        {
           _sprite.flipX = false;
        }

        else if (_unitMove.Velocity.x < 0)
        {
            _sprite.flipX = true;
        }

        // Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", _unitMove.Velocity.y);

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

    }
    private void AE_OnJumpToFall()
    {
        _animator.ResetTrigger("Jump");

    }
    void AE_Landing()
    {
        _animator.ResetTrigger("Jump");
    }

}
