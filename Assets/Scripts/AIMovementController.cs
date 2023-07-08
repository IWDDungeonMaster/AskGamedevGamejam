using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _minDirectionChangeDelay = 0.5f;
    [SerializeField] private float _maxDirectionChangeDelay = 1.5f;
    [SerializeField] private LayerMask _tubeLayerMask;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  
        _collider = GetComponent<BoxCollider2D>();

        //  StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector2(-_movementSpeed, _rigidbody.velocity.y);

        if(IsGroundedOnTube())
        {
            Debug.Log("Tube");
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(ChangeDirection());
    }

    private bool IsGroundedOnTube()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position, _collider.bounds.size, 0f, -transform.up, 1.5f, _tubeLayerMask);
        return raycastHit2D.collider != null;
    }

    private IEnumerator ChangeDirection()
    {
        while(true)
        {
            float directionChangeDelay = Random.Range(_minDirectionChangeDelay, _maxDirectionChangeDelay);
            yield return new WaitForSeconds(directionChangeDelay);
            if (Random.Range(0, 100) > 70)
            {
                _movementSpeed = -_movementSpeed;
            }
        }
    }
}
