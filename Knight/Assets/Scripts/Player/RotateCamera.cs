using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float horizontalSpeed = 5;
    [SerializeField] private float verticalSpeed = 3;
    //[SerializeField] private Vector3 startPoint;
    //[SerializeField] private Vector3 endPoint;
    //[SerializeField] private float dragDiistance;

    void Start()
    {
        //dragDiistance = Screen.height * 0.1f;
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if (Mathf.Abs(vertical) > 0.85)
        {
            transform.Rotate(new Vector3(1, 0, 0), 
                -vertical * verticalSpeed, Space.Self);
        }
        else if (Mathf.Abs(horizontal) > 0.5)
        {
            transform.Rotate(new Vector3(0, 1, 0),
                horizontal * horizontalSpeed, Space.World);
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
