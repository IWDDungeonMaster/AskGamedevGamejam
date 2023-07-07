#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PersonageMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] int _speed;
    [SerializeField] private int _jumpForce;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
