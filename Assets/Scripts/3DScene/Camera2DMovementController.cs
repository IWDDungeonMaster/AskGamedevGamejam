#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

public class Camera2DMovementController : MonoBehaviour
{
    [SerializeField] private int _speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }
    }
}
