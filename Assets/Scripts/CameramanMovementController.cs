using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameramanMovementController : MonoBehaviour
{
    [SerializeField] int _speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }
    }
}
