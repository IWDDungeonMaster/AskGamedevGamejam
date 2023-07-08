#pragma warning disable IDE0044
#pragma warning disable IDE0051

using UnityEngine;

public class Camera2DMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMovementController _cameraman;
    [SerializeField] private int _verticalSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * _verticalSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * _verticalSpeed);
        }

        // ��� ���������� ����� ����� �������������� ��������� � ����������� ��������� � 2D �����.
        // ���� ����� ���������� �� 2D �����, �� 2D ������ ������������ � ��� � ��������.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _cameraman.LateralSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _cameraman.LateralSpeed);
        }
    }
}
