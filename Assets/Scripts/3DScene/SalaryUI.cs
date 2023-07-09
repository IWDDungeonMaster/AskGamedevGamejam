using TMPro;
using UnityEngine;

public class SalaryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _salaryAmount;

    private void Awake()
    {
        _salaryAmount ??= GetComponent<TextMeshProUGUI>();
    }

    public void ChangeSalary(object sender, MoneyChangedEventArgs e)
    {
        if (_salaryAmount == null)
        {
            return;
        }

        _salaryAmount.text = $"{e.CurrentValue}$";
    }
}
