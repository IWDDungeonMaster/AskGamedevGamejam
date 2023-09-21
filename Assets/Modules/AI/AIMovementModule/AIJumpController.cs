using System.Collections;

using SDRGames.Cameraman.AI.Movement;

using UnityEngine;

public class AIJumpController : MonoBehaviour
{
    private const float JUMP_CHECK_PERIOD = 0.01f;

    [SerializeField][Range(1, 10)] private float _jumpPower;
    [SerializeField][Range(1, 10)] private float _blocksDetectionDistance;
    [SerializeField] private LayerMask _jumpingLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _platformLayerMask;
    private AIMovementController _aIMovementController;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _aIMovementController = GetComponent<AIMovementController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        StartCoroutine(JumpCheck());
        StartCoroutine(Unstack());
    }

    // Update is called once per frame
    private IEnumerator JumpCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(JUMP_CHECK_PERIOD);
            if (IsGrounded())
            {
                float chanceToJump = 100;
                RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, -transform.right, _blocksDetectionDistance, _jumpingLayerMask);
                Collider2D raycastCollider = raycastHit2D.collider;
                if (raycastCollider != null && _jumpingLayerMask == (_jumpingLayerMask | (1 << raycastCollider.gameObject.layer)))
                {
                    if (_platformLayerMask == (_platformLayerMask | (1 << (raycastCollider.gameObject.layer))))
                    {
                        chanceToJump = Random.Range(0, 100);
                    }

                    if (chanceToJump > 50)
                    {
                        Jump();
                    }
                }
            }
        }
    }

    private IEnumerator Unstack()
    {
        Vector3 currentPosition;
        while (true)
        {
            yield return null;
            currentPosition = transform.position;
            yield return new WaitForSeconds(0.15f);
            if (currentPosition == transform.position)
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, -transform.up, 0.05f, _groundLayerMask);
        return raycastHit2D.collider != null;
    }
}
