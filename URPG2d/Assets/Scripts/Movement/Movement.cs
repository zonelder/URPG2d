using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement:MonoBehaviour
{
    public event Action OnJump;
    private bool _isMove = false;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GroundCheck _groundSensor;
    private float _maxSlopeAngle=60.0f;

    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidbody2d;
    private ContactFilter2D _contactFilter;
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(0);

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    public Vector2 GravityVector => Physics2D.gravity;
    public bool IsMoving => _isMove;
    public bool IsGrounded => _groundSensor.State();

    public Vector2 Velocity => _velocity;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(4*Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) &&_groundSensor.State())
        {
            OnJump?.Invoke();
            _groundSensor.Disable(0.2f);
            _velocity.y = 7;
        }

    }

    private void FixedUpdate()
    {
        _velocity += GravityVector * Time.deltaTime;
        _velocity.x = _targetVelocity.x;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

        Vector2 move = deltaPosition;
        Move(move);

    }

    public  void Move(Vector2 move)
    {
        float distance = move.magnitude;
        _isMove = false;
        if (distance > _minMoveDistance)
        {
            _hitBufferList.Clear();
            int count = _rigidbody2d.Cast(move, _contactFilter, _hitBufferList, distance + _shellRadius);
            foreach(var hit in _hitBufferList)
            {
                Vector2 currentNormal = hit.normal;
                Vector2 unnecessaryMove = move.normalized * (move.magnitude-hit.distance);

                float velocityProjection = Vector2.Dot(_velocity, currentNormal);

                if(velocityProjection<0)
                {

                    _velocity = _velocity - currentNormal * velocityProjection;
                }
                float projection = Vector2.Dot(unnecessaryMove, currentNormal);
                if (projection < 0)
                {
                    move += (-projection) * currentNormal;

                    if (Vector2.Angle(currentNormal, GravityVector) > 180 - _maxSlopeAngle)
                    {

                        _groundNormal = currentNormal;
                        // we can walk in a platforms
                        //we also have to ignore gravitation while we stay on a sloping platform, cause unit start falling a but downward;


                    }
                    else
                    {
                        Debug.Log("unit should slide but this feature is not implemented");
                    }

                }

            }
            if (move.magnitude > _minMoveDistance)
            {
                _isMove = true;

                _rigidbody2d.position +=move;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_rigidbody2d != null)
        {
            Gizmos.DrawLine(_rigidbody2d.position, _rigidbody2d.position + _groundNormal);
        }
    }
}