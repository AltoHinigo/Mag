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

    [SerializeField] private float _moveSpeed;

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_MoveJStk.Horizontal * _moveSpeed, _rigidbody.velocity.y, _MoveJStk.Vertical * _moveSpeed);

        if (_MoveJStk.Horizontal != 0 || _MoveJStk.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("Walk", true);
        }
        else
            _animator.SetBool("Walk", false);
        if (_ViewJStk.Horizontal != 0 || _ViewJStk.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(_ViewJStk.Horizontal, _rigidbody.velocity.y, _ViewJStk.Vertical));
    }
}