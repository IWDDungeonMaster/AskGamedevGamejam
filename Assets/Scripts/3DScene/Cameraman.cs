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
        }

        if (_batteryCharge == 0)
        {
            _batteryCharge = 100;
        }
    }

    public void ChangeBattery(int value)
    {
        int newValue = _batteryCharge + value;

        if (newValue <= 0)
        {
            _batteryCharge = 0;
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
            LooseAllMoney?.Invoke();
            return;
        }

        _money = newValue;
        MoneyChanged?.Invoke(this, new MoneyChangedEventArgs(_batteryCharge));
    }
}

public class MoneyChangedEventArgs : EventArgs
{
    public MoneyChangedEventArgs(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }
}

public class BatteryChangedEventArgs : EventArgs
{
    public BatteryChangedEventArgs(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }
}
