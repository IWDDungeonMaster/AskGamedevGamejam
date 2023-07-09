using UnityEngine;

public class FilmArea : MonoBehaviour
{
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private Transform _arrow;
    [SerializeField] private Transform _target;

    private void Update()
    {
        if (_arrow.gameObject.activeSelf)
        {
            Vector3 dir = _target.position - _centerPoint.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _centerPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _arrow.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _arrow.gameObject.SetActive(true);
    }
}
