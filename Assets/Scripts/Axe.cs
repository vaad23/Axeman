using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Tree tree))
            tree.TakeDamage(_damage);
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
