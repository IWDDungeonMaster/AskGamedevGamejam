using UnityEngine;

public class AIJumpController : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _jumpPower;
    [SerializeField][Range(1, 10)] private float _blocksDetectionDistance;
    [SerializeField] private LayerMask _jumpingLayerMask, _groundLayerMask;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position, _collider.bounds.size, 0f, -transform.right, _blocksDetectionDistance, _jumpingLayerMask);
            if (raycastHit2D.collider != null && _jumpingLayerMask == (_jumpingLayerMask | (1 << raycastHit2D.collider.gameObject.layer)))
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(transform.position, _collider.bounds.size, 0f, -transform.up, 1.5f, _groundLayerMask);
        return raycastHit2D.collider != null;
    }
}
