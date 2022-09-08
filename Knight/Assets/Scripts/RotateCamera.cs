using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float speed = 5;
    //[SerializeField] private Vector3 startPoint;
    //[SerializeField] private Vector3 endPoint;
    //[SerializeField] private float dragDiistance;

    void Start()
    {
        //dragDiistance = Screen.height * 0.1f;
    }

    void Update()
    {
        float horizontal = joystick.Horizontal * speed;
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

        //if(Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if(touch.phase == TouchPhase.Began)
        //    {
        //        startPoint = touch.position;
        //        endPoint = touch.position;
        //    }
        //    else if(touch.phase == TouchPhase.Moved)
        //    {
        //        endPoint = touch.position;
        //    }
        //    else if(touch.phase == TouchPhase.Ended)
        //    {
        //        endPoint = touch.position;
        //        if(Mathf.Abs(endPoint.x - startPoint.x) > dragDiistance
        //            || Mathf.Abs(endPoint.y - startPoint.y) > dragDiistance)
        //        {
        //            if(Mathf.Abs(endPoint.x - startPoint.x)
        //                > Mathf.Abs(endPoint.y - startPoint.y))
        //            {
        //                if(endPoint.x > startPoint.x)
        //                {
        //                    // right sweep
        //                    print("right sweep");
        //                }
        //                else
        //                {
        //                    // left sweep
        //                    print("left sweep");
        //                }
        //            }
        //            else
        //            {
        //                if (endPoint.y > startPoint.y)
        //                {
        //                    // up sweep
        //                    print("up sweep");
        //                }
        //                else
        //                {
        //                    // down sweep
        //                    print("down sweep");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // it's a tap
        //            print("just a tap");
        //        }
        //    }
        //}
    }
}
