using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

class Blaster : Gun
{
    [SerializeField] private Bullet bulletPrefab;
    //[SerializeField] private float x;
    //[SerializeField] private float y;
    //[SerializeField] private float z;
    //private float timeShot = 0.5f;

    //public Quaternion gunQuaternion;
    //public Quaternion atkPointQuaternion;

    //private void Start()
    //{
    //    gunQuaternion = transform.rotation;
    //    atkPointQuaternion = attackPoint.rotation;
    //}

    public override void Shoot(Transform target = null, float adjustHeight = 0)
    {
        FindObjectOfType<AudioManager>().Play("BlasterSound", transform.position);
        //rotate gun to enemy        
        if (target != null)
        {
            transform.LookAt(target);
            //transform.Rotate(new Vector3(1, 0, 0), x, Space.Self);
            transform.Rotate(new Vector3(0, 1, 0), 90, Space.Self);
            //transform.Rotate(new Vector3(0, 0, 1), z, Space.Self);

            attackPoint.LookAt(target);

            //Adjust enemy position to the original
            if (adjustHeight != 0)
            {
                Vector3 vector = target.position;
                vector.y -= adjustHeight;
                target.position = vector;
            }
            
            //StopAllCoroutines();
            //StartCoroutine(ResetRotation());
        }


        // start shooting
        bulletPrefab.Spawn(attackPoint.position, attackPoint.rotation, attackPoint.forward);

        //Vector3 adjustPosition = new Vector3(attackPoint.position.x,
        //    attackPoint.position.y + adjustHeight, attackPoint.position.z);

        //Vector3 vec1 = attackPoint.position - target.position;
        //Vector3 vec2 = attackPoint.position - adjustPosition;
        //float degree = math.abs(Vector3.Angle(vec2, vec1));

        //bulletPrefab.Spawn(attackPoint.position, 
        //    Quaternion.AngleAxis(degree, Vector3.up) * attackPoint.rotation,
        //    attackPoint.forward);
    }

    public override int GetWeapon()
    {
        return (int)MyGun.Blaster;
    }

    public override bool IsMeleeWeapon()
    {
        return false;
    }

    //IEnumerator ResetRotation()
    //{
    //    yield return new WaitForSeconds(timeShot);
    //    transform.rotation = gunQuaternion;
    //    attackPoint.rotation = atkPointQuaternion;
    //}

    public override float TimeAttack()
    {
        return 0.5f;
    }
}
