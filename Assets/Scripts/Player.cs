using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private VariableJoystick _joystick;
    
    private void Update()
    {
        if( _joystick.Vertical != 0 && _joystick.Horizontal != 0)
        {
            _mover.Move(new Vector2(_joystick.Horizontal, _joystick.Vertical));
        }
    }
}
