using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _spead;

    public void Move(Vector2 direction)
    {
        Vector2 distance = direction * _spead * Time.deltaTime;
        Vector3 distance3 = new Vector3(distance.x, 0, distance.y);
        transform.position += distance3;
        transform.LookAt(transform.position + distance3);
    }
}
