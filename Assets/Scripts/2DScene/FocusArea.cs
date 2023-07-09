using System;
using UnityEngine;

public class FocusArea : MonoBehaviour
{
    [SerializeField] private int _speed;

    public event EventHandler ActorInFocus;
    public event EventHandler ActorInOutOfFocus;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translationX = Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
        float translationY = Input.GetAxis("Mouse Y") * _speed * Time.deltaTime;

        if(translationX > Screen.width / 2)
        {
            translationX = Screen.width / 2;
        }

        if(translationY > Screen.height)
        {
            translationY = Screen.height;
        }

        // Move translation along the object's z-axis
        transform.Translate(translationX, translationY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActorInFocus?.Invoke(this, new EventArgs());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActorInOutOfFocus?.Invoke(this, new EventArgs());
    }
}
