using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementController : MonoBehaviour
{
    [SerializeField] private float _maxMovementSpeed = 12f;
    [SerializeField] private float _movementSpeedStep = 2f;
    [SerializeField] private float _movementSpeedIncreasePeriod = 1f;
    [SerializeField] private float _minDirectionChangeDelay = 0.5f;
    [SerializeField] private float _maxDirectionChangeDelay = 1.5f;
    [SerializeField] private LayerMask _finishLayerMask;
    [SerializeField] private LayerMask _tubeLayerMask;
    private float _movementSpeed;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private AIJumpController _jumpController;
    [SerializeField] private bool _isRun = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  
        _collider = GetComponent<BoxCollider2D>();
        _jumpController = GetComponent<AIJumpController>();
        _movementSpeed = _movementSpeedStep;


        StartCoroutine(IncreaseSpeedOverTime());
        //StartCoroutine(ChangeDirectionOverTime());
        StartCoroutine(Unstack());
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while(Math.Abs(_movementSpeed) < _maxMovementSpeed)
        { 
            yield return new WaitForSeconds(_movementSpeedIncreasePeriod);
            if (_movementSpeed >= 0)
            {
                _movementSpeed += _movementSpeedStep;
            }
            else
            {
                _movementSpeed -= _movementSpeedStep;
            }
        }

    }

    void Update()
    {
        if (_isRun)
        {
            _rigidbody.velocity = new Vector2(-_movementSpeed, _rigidbody.velocity.y);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
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
                _jumpController.Jump();
            }
            while (!_jumpController.IsGrounded())
            {
                yield return null;
            }
            if (currentPosition == transform.position)
            {
                ChangeDirection();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsFinished())
        {
            _movementSpeed = -_movementSpeedStep * 2;
            StopAllCoroutines();
        }
    }

    public void Run(object sender, EventArgs e)
    {
        _isRun = true;
        _movementSpeed += _movementSpeedStep;
        StartCoroutine(IncreaseSpeedOverTime());
        StartCoroutine(ChangeDirectionOverTime());
        StartCoroutine(Unstack());
    }

    private void Stop()
    {
        _movementSpeed = 0;
        _rigidbody.AddForce(new Vector2(_movementSpeed, _movementSpeed/10), ForceMode2D.Impulse);
    }

    private IEnumerator ChangeDirectionOverTime()
    {
        while(true)
        {
            float directionChangeDelay = UnityEngine.Random.Range(_minDirectionChangeDelay, _maxDirectionChangeDelay);
            yield return new WaitForSeconds(directionChangeDelay);
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        _movementSpeed = -_movementSpeed;
    }

    private bool IsFinished()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, -transform.right, 0.15f, _finishLayerMask);
        return raycastHit2D.collider != null;
    }
}
