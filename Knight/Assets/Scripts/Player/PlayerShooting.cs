using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Animator playerAni;
    [SerializeField] private Gun Gun;
    [SerializeField] bool isHolding = false;
    [SerializeField] public bool alreadyAttacked = false;
    [SerializeField] bool isShotting1 = false;
    [SerializeField] bool isShotting2 = false;
    [SerializeField] float timeAtk = 0.5f;
    List<Enemy> enemies;

    const string shotting1 = "Firing Rifle Idle";
    const string shotting2 = "Firing Rifle Idle 0";

    public void HoldingBtnAtk(bool isHolding)
    {
        this.isHolding = isHolding;

        if (isHolding) StartCoroutine(HoldingBtnAttack());
    }

    public void BtnAtk()
    {
        if (alreadyAttacked) return;
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeAtk - 0.2f);


        if (isShotting1)
        {
            isShotting1 = false;
            isShotting2 = true;
            playerAni.Play(shotting2);
        }
        else if (isShotting2)
        {
            isShotting2 = false;
            isShotting1 = true;
            playerAni.Play(shotting1);
        }
        else
        {
            isShotting2 = false;
            isShotting1 = true;
            playerAni.Play(shotting1);
        }

        // rotate gun and player to enemy
        Enemy enemy = NearEnemy();
        if(enemy != null)
        {
            Vector3 direct = enemy.transform.position;
            direct.y = transform.position.y;

            transform.LookAt(direct);
        }

        // start shooting
        AdjustPosition(enemy);
        Gun.Shoot(enemy != null ? enemy.transform : null);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    Enemy NearEnemy()
    {
        enemies = FindObjectOfType<BattleSystem>().currentEnemies;
        Enemy nearEnemy = null;

        if (enemies.Count > 1)
        {
            nearEnemy = enemies[0];
            for (int i = 1; i < enemies.Count; i++)
            {
                if (Vector3.Distance(nearEnemy.transform.position, transform.position)
                    > Vector3.Distance(enemies[i].transform.position, transform.position))
                {
                    nearEnemy = enemies[i];
                }
            }
        }
        else if (enemies.Count == 1) return enemies[0];

        return nearEnemy;
    }
 
    void AdjustPosition(Enemy enemy)
    {
        if (enemy == null) return;
        Vector3 vector = enemy.transform.position;
        vector.y += enemy.realY();
        
        enemy.transform.position = vector;
    }

    IEnumerator HoldingBtnAttack()
    {
        while (isHolding)
        {
            BtnAtk();
            yield return new WaitForSeconds(timeAtk);
        }
    }

    public void SetGun(Gun gun)
    {
        Gun = gun;
    }
}
