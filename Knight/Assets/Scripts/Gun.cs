using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Bullet bulletPrefab;
    //private float timeShot = 0.5f;

    //public Quaternion gunQuaternion;
    //public Quaternion atkPointQuaternion;

    //private void Start()
    //{
    //    gunQuaternion = transform.rotation;
    //    atkPointQuaternion = attackPoint.rotation;
    //}

    public void Shoot(Transform target = null)
    {
        //rotate gun to enemy        
        if (target != null)
        {
            transform.LookAt(target);           
            transform.Rotate(new Vector3(0, 1, 0), -90, Space.Self);

            attackPoint.LookAt(target);

            //StopAllCoroutines();
            //StartCoroutine(ResetRotation());
        }


        // start shooting
        bulletPrefab.Spawn(attackPoint.position, attackPoint.rotation, attackPoint.forward);

    }

    //IEnumerator ResetRotation()
    //{
    //    yield return new WaitForSeconds(timeShot);
    //    transform.rotation = gunQuaternion;
    //    attackPoint.rotation = atkPointQuaternion;
    //}

}
