using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Axe _axe;
    [SerializeField] private float _damage;

    public bool IsPlay { get; private set; } = false;

    public event UnityAction Ended;

    public void StartAnimation()
    {
        _axe.SetDamage(_damage);
        _animator.SetTrigger("Start");

        IsPlay = true;
    }

    public void End()
    {
        IsPlay = false;
        Ended?.Invoke();
    }
}
