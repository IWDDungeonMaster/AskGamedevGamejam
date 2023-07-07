#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

public class PersonageMovementController : MonoBehaviour
{
    [SerializeField] int _speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
