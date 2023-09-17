using System;
using System.Collections;
using System.Collections.Generic;

using SDRGames.Cameraman.MovementSystem.AI.Model;

using UnityEngine;

namespace SDRGames.Cameraman.MovementSystem.AI.Views {
    public class AIMovementView : MonoBehaviour
    { 
        [SerializeField] private LayerMask _finishLayerMask;
        [SerializeField] private LayerMask _tubeLayerMask;
        private float _movementSpeed;
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;
        private AIJumpController _jumpController;
        private bool _isRunning = false;

        public event EventHandler<GameOverEventArgs> Finished;

        public void SetMovementSpeed(float newMovementSpeed)
        {
            _movementSpeed = newMovementSpeed;
        }
            
        public void Run(AIMovementModel aIMovementModel)
        {
            _isRunning = true;
            SetMovementSpeed(aIMovementModel.MovementSpeedStep);
            StartCoroutine(IncreaseSpeedOverTime(aIMovementModel.MaxMovementSpeed, aIMovementModel.MovementSpeedIncreasePeriod, aIMovementModel.MovementSpeedStep));
            StartCoroutine(ChangeDirectionOverTime(aIMovementModel.MinDirectionChangeDelay, aIMovementModel.MaxDirectionChangeDelay));
            StartCoroutine(Unstack());
        }

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _jumpController = GetComponent<AIJumpController>();
            SetMovementSpeed(0);
        }

        void Update()
        {
            if (_isRunning)
            {
                _rigidbody.velocity = new Vector2(-_movementSpeed, _rigidbody.velocity.y);
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator IncreaseSpeedOverTime(float maxMovementSpeed, float movementSpeedIncreasePeriod, float movementSpeedStep)
        {
            while (Math.Abs(_movementSpeed) < maxMovementSpeed)
            {
                yield return new WaitForSeconds(movementSpeedIncreasePeriod);
                if (_movementSpeed >= 0)
                {
                    _movementSpeed += movementSpeedStep;
                }
                else
                {
                    _movementSpeed -= movementSpeedStep;
                }
            }
        }

        private IEnumerator Unstack()
        {
            Vector3 currentPosition;
            while (!IsFinished())
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
                Finished?.Invoke(this, new GameOverEventArgs(true));
                Director.IsFinished = true;
                StopAllCoroutines();
                StartCoroutine(Unstack());
            }
        }

        private IEnumerator ChangeDirectionOverTime(float minDirectionChangeDelay, float maxDirectionChangeDelay)
        {
            while (!IsFinished())
            {
                float directionChangeDelay = UnityEngine.Random.Range(minDirectionChangeDelay, maxDirectionChangeDelay);
                yield return new WaitForSeconds(directionChangeDelay);
                if (UnityEngine.Random.Range(0, 100) > 60)
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
}