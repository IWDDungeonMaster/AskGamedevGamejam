#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

public class Camera2DMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMovementController _cameraman;
    [SerializeField] private int _verticalSpeed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Это управление нужно чтобы компенсировать отдаление и приближение персонажа к 2D сцене.
        // Если игрок отдаляется от 2D сцены, то 2D камера приближается к ней и наоборот.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _cameraman.LateralSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _cameraman.LateralSpeed);
        }
    }
}
