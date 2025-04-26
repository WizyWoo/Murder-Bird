using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuMovement : MonoBehaviour
{

    public CharacterController Controller;
    [Header("Movement Settings")]
    public float Speed;
    public float SprintMult, WalkMult, StepHeight, JumpHeight, Gravity, AirControl, AirDrag, GroundCheckRadShrink;
    private float _effectiveSpeed, _yVelocity, _jumpCheckDelay;
    private Vector3 _velocity;
    public Vector3 Velocity => _velocity;
    [SerializeField]
    private bool _grounded;
    private LayerMask _playerMask;

    private void Start()
    {

        Controller = GetComponent<CharacterController>();
        Controller.stepOffset = StepHeight;

        _playerMask = ~(1 << LayerMask.NameToLayer("Player"));

    }

    private void Update()
    {

        if(transform.position.y < -10 || Input.GetKey(KeyCode.H))
        {
            
            transform.position = new Vector3(0, 10, 0);
            return;

        }

        if(Physics.CheckSphere(transform.position - (Vector3.up * ((Controller.height / 2) + Controller.skinWidth - Controller.radius)), Controller.radius - GroundCheckRadShrink, _playerMask, QueryTriggerInteraction.Ignore))
        {

            _grounded = true;
            Controller.stepOffset = StepHeight;

            _yVelocity = -2;

        }
        else
        {

            _grounded = false;
            _yVelocity -= Gravity * Time.deltaTime;

        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();

        if(Input.GetKey(KeyCode.LeftShift))
            _effectiveSpeed = Speed * SprintMult;
        else if(Input.GetKey(KeyCode.LeftControl))
            _effectiveSpeed = Speed * WalkMult;
        else
            _effectiveSpeed = Speed;

        _effectiveSpeed = _grounded ? _effectiveSpeed : _effectiveSpeed * AirControl;

        if(Input.GetKeyDown(KeyCode.Space))
            _jumpCheckDelay = 0.1f;
        else if(_jumpCheckDelay > 0)
            _jumpCheckDelay -= Time.deltaTime;

        if(_jumpCheckDelay > 0 && _grounded)
        {

            Controller.stepOffset = 0.1f;

            _yVelocity = JumpHeight;

        }

        if(move != Vector3.zero)
            move *= _effectiveSpeed;
        
        if(_grounded)
            _velocity = move + (Vector3.up * _yVelocity);
        else
        {

            _velocity += move;
            _velocity.y = _yVelocity;

        }
            
        Controller.Move(_velocity * Time.deltaTime);

        if(!_grounded)
            _velocity -= move;

    }

}
