using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class JoyStickMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _MoveJStk;
    [SerializeField] private FixedJoystick _ViewJStk;
    [SerializeField] private Animator _animator;
    public bool isMoving = true;
    [SerializeField] private float _moveSpeed;
    private CharacterController _CharacterController;

    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
    }
    public void ChangeSpeed(float moveSpeed)
    {
        //Debug.Log("!");
        //Debug.Log("moveSpeed " + moveSpeed);
        //Debug.Log("_moveSpeed " + _moveSpeed);
        if (moveSpeed != 0)
            _moveSpeed = moveSpeed;

    }
    private void FixedUpdate()
    {
        if(!isMoving) {
            return;
        }

        _rigidbody.velocity = new Vector3(_MoveJStk.Horizontal * _moveSpeed, _rigidbody.velocity.y, _MoveJStk.Vertical * _moveSpeed);
        //_CharacterController.Move(new Vector3(_MoveJStk.Horizontal * _moveSpeed /10, _rigidbody.velocity.y, _MoveJStk.Vertical * _moveSpeed / 10));


        if ((_ViewJStk.Horizontal != 0 || _ViewJStk.Vertical != 0) && _moveSpeed == 0)
        {
            //Debug.Log("!!!!!!!!!!!!!!!!!! "+_rigidbody.velocity);
            _rigidbody.velocity = new Vector3(_MoveJStk.Horizontal, _rigidbody.velocity.y, _MoveJStk.Vertical);
            transform.rotation = Quaternion.LookRotation(new Vector3(_ViewJStk.Horizontal, _rigidbody.velocity.y, _ViewJStk.Vertical));
            return;
        }
        if (_MoveJStk.Horizontal != 0 || _MoveJStk.Vertical != 0)
        {
            //Debug.Log(_rigidbody.velocity);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("Walk", true);
        }
        else
            _animator.SetBool("Walk", false);
        if (_ViewJStk.Horizontal != 0 || _ViewJStk.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(_ViewJStk.Horizontal, _rigidbody.velocity.y, _ViewJStk.Vertical));

    }
}