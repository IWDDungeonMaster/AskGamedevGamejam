#pragma warning disable IDE0044
#pragma warning disable IDE0051
using System;
using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private int _salary;
    [SerializeField] private int _fine;

    [SerializeField] private AIMovementController _aIController; //TODO перенести потом в Bootstrap
    [SerializeField] private Cameraman _cameraman;
    [SerializeField] private FocusArea _focusArea;

    [SerializeField] private int _salaryPeriod;
    [SerializeField] private int _finePeriod;

    private Coroutine _paySalaryCoroutine;
    private Coroutine _imposeFineCoroutine;

    public event EventHandler FilmingHasBegun;

    private void Awake()
    {
        if (_salary == 0)
        {
            _salary = 1;
            Debug.LogWarning($"ГД не назначил зарплату, значение по умолчанию {_salary}");
        }

        if (_fine == 0)
        {
            _fine = -30;
            Debug.LogWarning($"ГД не назначил штраф, значение по умолчанию {_fine}");
        }

        if (_salaryPeriod == 0)
        {
            _salaryPeriod = 1;
            Debug.LogWarning($"ГД не назначил зарплату, значение по умолчанию {_salaryPeriod}");
        }

        if (_finePeriod == 0)
        {
            _finePeriod = 5;
            Debug.LogWarning($"ГД не назначил штраф, значение по умолчанию {_finePeriod}");
        }

        if (_aIController != null)
        {
            FilmingHasBegun += _aIController.Run;
        }

        if (_focusArea != null)
        {
            _focusArea.ActorInFocus += PaySalary;
            _focusArea.ActorInOutOfFocus += ImposeFine;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        Debug.Log("Ready!");
        yield return new WaitForSeconds(1);
        Debug.Log("Camera!");
        yield return new WaitForSeconds(1);
        Debug.Log("Action!");
        yield return new WaitForSeconds(1);
        FilmingHasBegun?.Invoke(this, new EventArgs());
    }

    private void PaySalary(object sender, EventArgs e)
    {
        if (_imposeFineCoroutine != null)
        {
            StopCoroutine(_imposeFineCoroutine);
        }

        _paySalaryCoroutine = StartCoroutine(PaySalaryRoutine());
    }

    private IEnumerator PaySalaryRoutine()
    {
        while (true)
        {
            _cameraman.ChangeMoney(_salary);
            yield return new WaitForSeconds(_salaryPeriod);
        }
    }

    private void ImposeFine(object sender, EventArgs e)
    {
        if (_paySalaryCoroutine != null)
        {
            StopCoroutine(_paySalaryCoroutine);
        }

        _imposeFineCoroutine = StartCoroutine(ImposeFineRoutine());
    }

    private IEnumerator ImposeFineRoutine()
    {
        while (true)
        {
            _cameraman.ChangeMoney(_fine);
            yield return new WaitForSeconds(_finePeriod);
        }
    }
}
