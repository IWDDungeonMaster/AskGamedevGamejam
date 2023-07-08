using UnityEngine;

/// <summary>
/// Класс представляющий обёртку над стандартным юнити аниматором.
/// </summary>
[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void MoveForward()
    {
        _animator.SetInteger("RunningDirection", 1);
    }
    public void MoveBackward()
    {
        _animator.SetInteger("RunningDirection", -1);
    }

    public void StopMoving()
    {
        _animator.SetInteger("RunningDirection", 0);
    }

    public void JumpUp()
    {
        _animator.SetBool("IsJumping", true);
    }

    public void JumpLeft()
    {
        _animator.SetInteger("JumpDirection", -1);
    }

    public void JumpRight()
    {
        _animator.SetInteger("JumpDirection", 1);
    }
}