using System;
using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private AIMovementController _aIController; //TODO перенести потом в Bootstrap

    public event EventHandler FilmingHasBegun;

    private void Awake()
    {
        if (_aIController != null)
        {
            FilmingHasBegun += _aIController.Run;
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
}
