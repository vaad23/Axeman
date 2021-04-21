using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _ofset;

    private void LateUpdate()
    {
        _camera.position = _player.position + _ofset;
    }
}
