#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private int _lateralSpeed;
    [SerializeField] private int _forwardSpeed;
    [SerializeField] private int _jumpForce;

    [SerializeField] private Rigidbody _rb;

    public int LateralSpeed => _lateralSpeed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _forwardSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _forwardSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _lateralSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _lateralSpeed);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
