using System;
using UnityEngine;

public class FocusArea : MonoBehaviour
{
    public event EventHandler ActorInFocus;
    public event EventHandler ActorInOutOfFocus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActorInFocus?.Invoke(this, new EventArgs());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActorInOutOfFocus?.Invoke(this, new EventArgs());
    }
}
