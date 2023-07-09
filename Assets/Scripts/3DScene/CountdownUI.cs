using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdown;

    private void Awake()
    {
        _countdown ??= GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string newText)
    {
        _countdown.text = newText;
    }
}
