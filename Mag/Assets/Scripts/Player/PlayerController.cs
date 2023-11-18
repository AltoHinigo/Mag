using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables: Movement

    public bool useMovement = true;
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    public Vector3 direction { get { return _direction; } set { _direction = value; } }

    [SerializeField] private float speed;

    #endregion
    #region Variables: Rotation

    public bool useRotation = true;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    #endregion
    #region Variables: Gravity

    public bool useGravity = true;
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    /*private void Update()
    {
        _direction = new Vector3(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ? Input.GetKey(KeyCode.D) ? 1f : -1f : 0f, 0f,
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ? Input.GetKey(KeyCode.W) ? 1f : -1f : 0f);
        //_input = new Vector2(_direction.x, _direction.z);
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }*/
    public void Apply(Vector3 direction)
    {
        _direction = direction;
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        //Debug.Log(_characterController.isGrounded);
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        //if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }



    /*public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }*/
}
