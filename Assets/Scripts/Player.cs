using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private TrackingTree _trackingTree;
    [SerializeField] private PlayerAttack _attack;

    private void OnEnable()
    {
        _trackingTree.Founded += ChopTree;
        _attack.Ended += CheckTree;
    }

    private void OnDisable()
    {        
        _trackingTree.Founded -= ChopTree;
        _attack.Ended -= CheckTree;
    }

    private void Update()
    {
        if( _joystick.Vertical != 0 && _joystick.Horizontal != 0)
        {
            _mover.Move(new Vector2(_joystick.Horizontal, _joystick.Vertical));
        }
    }

    private void ChopTree()
    {
        if (_attack.IsPlay == false)
        {
            _attack.StartAnimation();
        }
    }

    private void CheckTree()
    {
        if (_trackingTree.IsFound)
        {
            _attack.StartAnimation();
        }
    }
}
