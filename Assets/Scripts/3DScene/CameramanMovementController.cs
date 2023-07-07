#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

public class CameramanMovementController : MonoBehaviour
{
    [SerializeField] int _speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }
    }
}
