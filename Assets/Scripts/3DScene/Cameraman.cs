using System;
using UnityEngine;

public class Cameraman : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private int _batteryCharge;

    public event EventHandler<BatteryChangedEventArgs> BatteryChanged;
    public event EventHandler<MoneyChangedEventArgs> MoneyChanged;
    public event Action BatteryDied;
    public event Action LooseAllMoney;

    private void Awake()
    {
        if (_money == 0)
        {
            _money = 100;
            Debug.LogWarning(
                $"{gameObject.name}: ГД не назначил начальное количество денег, " +
                $"значение по умолчанию {_money}"
            );
        }

        if (_batteryCharge == 0)
        {
            _batteryCharge = 100;
            Debug.LogWarning(
                $"{gameObject.name}: ГД не назначил начальное количество батареи, " +
                $"значение по умолчанию {_batteryCharge}"
            );
        }
    }

    public void ChangeBattery(int value)
    {
        int newValue = _batteryCharge + value;

        if (newValue <= 0)
        {
            _batteryCharge = 0;
            BatteryChanged?.Invoke(this, new BatteryChangedEventArgs(_batteryCharge));
            BatteryDied?.Invoke();
            return;
        }

        _batteryCharge = newValue;
        BatteryChanged?.Invoke(this, new BatteryChangedEventArgs(_batteryCharge));
    }

    public void ChangeMoney(int value)
    {
        int newValue = _money + value;

        if (newValue <= 0)
        {
            _money = 0;
            MoneyChanged?.Invoke(this, new MoneyChangedEventArgs(_money, ChangePattern.Decrease));
            LooseAllMoney?.Invoke();
            return;
        }

        ChangePattern changePattern;

        if (newValue < _money)
        {
            changePattern = ChangePattern.Decrease;
        }
        else
        {
            changePattern = ChangePattern.Increase;
        }

        _money = newValue;
        MoneyChanged?.Invoke(this, new MoneyChangedEventArgs(_money, changePattern));
    }
}

public enum ChangePattern
{
    Increase,
    Decrease,
}

public class MoneyChangedEventArgs : EventArgs
{
    public MoneyChangedEventArgs(int value, ChangePattern pattern)
    {
        CurrentValue = value;
        ChangePattern = pattern;
    }

    public int CurrentValue { get; private set; }

    public ChangePattern ChangePattern { get; private set; }
}

public class BatteryChangedEventArgs : EventArgs
{
    public BatteryChangedEventArgs(int value)
    {
        CurrentValue = value;
    }

    public int CurrentValue { get; private set; }
}
