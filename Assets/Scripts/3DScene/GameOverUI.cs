using System;
using System.Collections;

using SDRGames.Cameraman.AI.Movement;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panel;
    private CanvasGroup _overlay;
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private AIMovementController _aIController;
    [SerializeField] private Cameraman _cameraman;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _aIController.Finished += Show;
        _cameraman.LooseAllMoney += Show;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        _overlay = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        _aIController.Finished -= Show;
        _cameraman.LooseAllMoney -= Show;
    }

    public void Show(object sender, GameOverEventArgs e)
    {
        _panel.alpha = 1;
        _panel.interactable = true;
        _panel.blocksRaycasts = true;

        _overlay.alpha = 1;
        _overlay.blocksRaycasts = true;
        _overlay.interactable = true;

        if (e.finishReached)
        {
            _header.text = "Well done!";
            _score.text = "The film was shot";
        }
        else
        {
            _header.text = "You are fired!";
            _score.text = "";
        }

        _audioSource.Play();
        _cameraman.source.Stop();
        StartCoroutine(WaitForRestart());
    }

    public void Hide()
    {
        _panel.alpha = 0;
        _panel.interactable = false;
        _panel.blocksRaycasts = false;

        _overlay.alpha = 0;
        _overlay.blocksRaycasts = false;
        _overlay.interactable = false;
    }

    private IEnumerator WaitForRestart()
    {
        yield return null;
        while(!Input.anyKey)
        {
            yield return null;
        }
        StopAllCoroutines();
        SceneManager.LoadScene("MainMenu");
    }
}
