using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal enum UsurperState
{
    MoveToHut, StayAtHut, MoveToPlayer, StayAtPlayer, MoveToSpace, MovetoHide
}

public class Usurper : MonoBehaviour
{
    [SerializeField] private Vector3 Playertarget = new Vector3(-3.16f, 4.78f, 40.92f);
    [SerializeField] private Vector3 Huttarget = new Vector3(0.328f, 8.041f, 54.229f);
    [SerializeField] private Vector3 initPosition = new Vector3(-18.1f, 14.05f, 97.52f);
    [SerializeField] private Vector3 hidePosition = new Vector3(3.94f, 4.78f, 30.37f);
    [SerializeField] private float speed = 2;
    [SerializeField] private float speedq = 6;
    [SerializeField] private GameObject playerlook;

    [SerializeField] UsurperState usurperState = UsurperState.MoveToHut;
    [SerializeField] Animator usurperAni;
    private static string flyForward = "Fly Forward";
    private static string land = "Land"; 
    private static string takeOff = "Take Off";
    private static string flameAttack = "Flame Attack";
    private static string sleep = "Sleep";

    void Start()
    {
        transform.position = initPosition;
        StartCoroutine(MoveLoop());
    }

    void Update()
    {
        if (usurperState == UsurperState.MoveToHut)
        {
            transform.position = Vector3.MoveTowards(transform.position, Huttarget, Time.deltaTime * speedq);
        }
        else if (usurperState == UsurperState.StayAtHut)
        {
            transform.LookAt(Huttarget);
            transform.position = Vector3.MoveTowards(transform.position, Huttarget, Time.deltaTime * speed);
        }
        else if (usurperState == UsurperState.MoveToPlayer)
        {
            transform.LookAt(Playertarget);
            transform.position = Vector3.MoveTowards(transform.position, Playertarget, Time.deltaTime * 4);
        }
        else if (usurperState == UsurperState.StayAtPlayer)
        {
            transform.LookAt(playerlook.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Playertarget, Time.deltaTime * speed);
        }
        else if (usurperState == UsurperState.MovetoHide)
        {
            transform.LookAt(hidePosition);
            transform.position = Vector3.MoveTowards(transform.position, hidePosition, Time.deltaTime * speedq);
        }
    }

    IEnumerator InactiveAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            while(usurperState == UsurperState.MoveToHut)
            {
                usurperAni.Play(flyForward);
                yield return new WaitForSeconds(0.3f);

                if (Vector3.Distance(transform.position, Huttarget) < 6) 
                    usurperState = UsurperState.StayAtHut;
            }

            if(usurperState == UsurperState.StayAtHut)
            {
                usurperAni.Play(land);
                yield return new WaitForSeconds(6);
                usurperAni.Play(takeOff);
                yield return new WaitForSeconds(4);
                usurperState = UsurperState.MoveToPlayer;
            }

            while (usurperState == UsurperState.MoveToPlayer)
            {
                usurperAni.Play(flyForward);
                yield return new WaitForSeconds(0.3f);

                if (Vector3.Distance(transform.position, Playertarget) < 6)
                    usurperState = UsurperState.StayAtPlayer;
            }

            if (usurperState == UsurperState.StayAtPlayer)
            {
                usurperAni.Play(land);
                yield return new WaitForSeconds(6);
                usurperAni.Play(flameAttack);
                yield return new WaitForSeconds(5);
                usurperAni.Play(sleep);
                yield return new WaitForSeconds(5);
                usurperAni.Play(takeOff);
                yield return new WaitForSeconds(4);
                usurperState = UsurperState.MovetoHide;
            }

            //while (usurperState == UsurperState.MoveToSpace)
            //{
            //    usurperAni.Play(flyForward);
            //    yield return new WaitForSeconds(0.3f);
            //    if (Vector3.Distance(transform.position, initPosition) < 3)
            //        usurperState = UsurperState.MovetoHide;
            //}
            while (usurperState == UsurperState.MovetoHide)
            {
                usurperAni.Play(flyForward);
                yield return new WaitForSeconds(0.3f);
                if (Vector3.Distance(transform.position, hidePosition) < 3)
                {
                    transform.position = initPosition;
                    usurperState = UsurperState.MoveToHut;
                }
                   
            }
        }
    }

    private IEnumerator MoveToward()
    {
        yield return new WaitForSeconds(1);
    }
    private IEnumerator MoveToHut()
    {
        transform.position = Vector3.MoveTowards(transform.position, Huttarget, Time.deltaTime * speed);
        yield return new WaitForSeconds(10);
    }
}

public static class TransformExtentions
{
    public static void SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}