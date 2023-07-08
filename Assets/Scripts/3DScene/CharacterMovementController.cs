#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private int _forwardSpeed;
    [SerializeField] private int _lateralSpeed;
    [SerializeField] private int _jumpForce;

    [SerializeField] private Rigidbody _rb;

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
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _lateralSpeed);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
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
