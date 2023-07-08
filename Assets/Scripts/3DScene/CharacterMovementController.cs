#pragma warning disable IDE0044
#pragma warning disable IDE0051

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Cameraman))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private int _forwardSpeed;
    [SerializeField] private int _lateralSpeed;
    [SerializeField] private int _jumpForce;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Cameraman _cameraman;
    [SerializeField] private Animator _animator;

    public int ForwardSpeed => _forwardSpeed;

    public int LateralSpeed => _lateralSpeed;

    public int JumpForce => _jumpForce;

    public bool IsJumping { get; private set; }

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        if (_forwardSpeed == 0)
        {
            _forwardSpeed = 5;
        }

        if (_lateralSpeed == 0)
        {
            _lateralSpeed = 5;
        }

        if (_jumpForce == 0)
        {
            _jumpForce = 5;
        }

        IsJumping = false;
        IsMoving = false;

        _rb ??= GetComponent<Rigidbody>();
        _cameraman ??= GetComponent<Cameraman>();
    }

    private void Update()
    {
        TryToMove();
        TryToJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO добавить определение с чем именно столкнулись.
        IsJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var battery = other.GetComponent<Battery>();

        if (battery is not null)
        {
            _cameraman.ChangeBattery(battery.Value);
        }
    }

    private void TryToMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _forwardSpeed);
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _forwardSpeed);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _lateralSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _lateralSpeed);
        }
        Debug.Log(IsMoving);
        _animator.SetBool("IsRunning", IsMoving);
    }

    private void TryToJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (IsJumping)
            {
                return;
            }

            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            IsJumping = true;
        }
    }
}
