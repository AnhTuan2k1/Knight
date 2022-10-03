using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

class Rifle : Gun
{
    [SerializeField] protected Bullet bulletPrefab;

    public override int GetWeapon()
    {
        return (int)MyGun.Rifle;
    }

    public override bool IsMeleeWeapon()
    {
        return false;
    }

    public override void Shoot(Transform target = null, float adjustHeight = 0)
    {
        FindObjectOfType<AudioManager>().Play("RifleSound", transform.position);

        //rotate gun to enemy        
        if (target != null)
        {
            transform.LookAt(target);
            transform.Rotate(new Vector3(1, 0, 0), -85, Space.Self);

            attackPoint.LookAt(target);

            //Adjust enemy position to the original
            if (adjustHeight != 0)
            {
                Vector3 vector = target.position;
                vector.y -= adjustHeight;
                target.position = vector;
            }
        }


        //start shooting
        bulletPrefab.Spawn(attackPoint.position, attackPoint.rotation, attackPoint.forward);
    }

    public override float TimeAttack()
    {
        return 0.3f;
    }
}

