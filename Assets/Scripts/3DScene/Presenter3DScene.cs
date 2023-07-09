using UnityEngine;

public class Presenter3DScene : MonoBehaviour
{
    [SerializeField] private Cameraman _cameramanModel;
    [SerializeField] private SalaryUI _salaryUI;

    private void Awake()
    {
        if (_cameramanModel == null || _salaryUI == null)
        {
            Debug.LogError($"{gameObject.name} не в состо€нии работать. Ќужно заполнить пол€.");
            return;
        }

        _cameramanModel.MoneyChanged += _salaryUI.ChangeSalary;
    }
}
