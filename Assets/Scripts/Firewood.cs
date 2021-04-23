using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Firewood : MonoBehaviour
{
    [SerializeField] private int _mass;
    [SerializeField] private Rigidbody _rigidbody;

    private Coroutine _coroutine;

    public int Mass => _mass;

    public event UnityAction<Firewood> EndedMove;

    public void Init(int mass)
    {
        _mass = mass;
        _coroutine = StartCoroutine(EnableKinamatic(5f));
    }

    public IEnumerator Move(Transform target)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _rigidbody.isKinematic = true;
        }

        float totalMovementTime = 3f; 
        float currentMovementTime = 0f;

        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            currentMovementTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, currentMovementTime / totalMovementTime);

            yield return null;
        }

        EndedMove?.Invoke(this);
    }

    private IEnumerator EnableKinamatic(float time)
    {
        yield return new WaitForSeconds(time);

        if (_rigidbody.isKinematic == false)
        {
            _rigidbody.isKinematic = true;
        }
        _coroutine = null;
    } 
}
