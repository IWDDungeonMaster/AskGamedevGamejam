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
    [SerializeField] private CountdownUI _countdownUI;

    [SerializeField] private int _salaryPeriod;
    [SerializeField] private int _finePeriod;

    private Coroutine _paySalaryCoroutine;
    private Coroutine _imposeFineCoroutine;

    public static bool IsFinished = false;

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

        if (_cameraman == null)
        {
            Debug.LogError($"{typeof(Cameraman)} не присоединён к {gameObject.name}");
        }

        if (_focusArea == null)
        {
            Debug.LogError($"{typeof(FocusArea)} не присоединён к {gameObject.name}");
        }

        if (_countdownUI == null)
        {
            Debug.LogError($"{typeof(CountdownUI)} не присоединён к {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        if (_aIController != null)
        {
            FilmingHasBegun += _aIController.Run;
        }

        if (_focusArea != null)
        {
            _focusArea.ActorInFocus += PaySalary;
            _focusArea.ActorInOutOfFocus += ImposeFine;
        }

        StartCoroutine(StartCountDown());
    }

    private void OnDisable()
    {
        if (_aIController != null)
        {
            FilmingHasBegun -= _aIController.Run;
        }

        if (_focusArea != null)
        {
            _focusArea.ActorInFocus -= PaySalary;
            _focusArea.ActorInOutOfFocus -= ImposeFine;
        }
    }

    private IEnumerator StartCountDown()
    {
        _countdownUI.ChangeText("Ready!");
        yield return new WaitForSeconds(1);
        _countdownUI.ChangeText("Camera!");
        yield return new WaitForSeconds(1);
        _countdownUI.ChangeText("Action!");
        yield return new WaitForSeconds(1);
        _countdownUI.ChangeText("");
        FilmingHasBegun?.Invoke(this, new EventArgs());
    }

    private void PaySalary(object sender, EventArgs e)
    {
        if (_imposeFineCoroutine != null && !IsFinished)
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
        if (_paySalaryCoroutine != null && !IsFinished)
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
