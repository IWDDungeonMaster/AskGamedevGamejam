using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdown;
    private CanvasGroup _overlay;

    private void Awake()
    {
        _overlay ??= GetComponent<CanvasGroup>();
    }

    public void ChangeText(string newText)
    {
        if(newText != "")
        {
            _overlay.alpha = 1;
            _overlay.blocksRaycasts = true;
            _overlay.interactable = true;
        }
        else
        {
            _overlay.alpha = 0;
            _overlay.blocksRaycasts = false;
            _overlay.interactable = false;
        }

        _countdown.text = newText;
    }
}
