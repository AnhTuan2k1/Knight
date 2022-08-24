using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    void Update()
    {
        float horizontal = joystick.Horizontal;
        //float vertical = joystick.Vertical;

        if (horizontal != 0 /*|| vertical != 0*/)
        {
            //float x = transform.rotation.y + vertical;
            //if (x > 5 || x < 175)
            //{
            //    transform.Rotate(new Vector3(1, 0, 0), x, Space.World);
            //}

            transform.Rotate(new Vector3(0, 1, 0), transform.rotation.x +
                horizontal, Space.World);
        }
    }
}
