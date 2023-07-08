using UnityEngine;

public class FocusArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Поймали рембо!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Потеряли рембо!");
    }
}
