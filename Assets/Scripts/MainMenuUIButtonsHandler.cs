using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIButtonsHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _aboutPanel;
    [SerializeField] private CanvasGroup _mainMenuButtons;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void NewGameButtonClick()
    {
        SceneManager.LoadScene("TestPrototype");
    }

    public void AboutButtonClick()
    {
        SwitchUIElementVisibility(_aboutPanel);
        SwitchUIElementVisibility(_mainMenuButtons);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void CloseButtonClick()
    {
        SwitchUIElementVisibility(_mainMenuButtons);
        SwitchUIElementVisibility(_aboutPanel);
    }

    private void SwitchUIElementVisibility(CanvasGroup UIElement)
    {
        float alpha = UIElement.alpha > 0 ? 0 : 1;
        UIElement.alpha = alpha;
        UIElement.blocksRaycasts = UIElement.alpha >= 0;
        UIElement.interactable = UIElement.alpha >= 0;
    }
}
