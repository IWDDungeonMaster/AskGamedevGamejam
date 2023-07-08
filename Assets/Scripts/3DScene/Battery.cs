using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private int _value;

    public int Value => _value;
}
