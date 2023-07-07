using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PersonageJumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _jumpForce;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
