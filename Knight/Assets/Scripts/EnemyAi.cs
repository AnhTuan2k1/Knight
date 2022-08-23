
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{
    Transform player;
    Transform enemyTransform;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Enemy enemy;

    Vector3 originPosition;
    //Patroling
    Vector3 patrolingPoint;
    bool patrolingPointSet = false;
    float patrolingPointRange = 10;

    //Attacking
    bool alreadyAttacked = false;
    bool isAttacking = false;
    float timeBetweenAttacks;

    //States
    public bool playerInAttackRange;
    public bool playerInSightRange;
    public float sightRange, attackRange;

    private void Start()
    {
        enemyTransform = GetComponentInParent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
        timeBetweenAttacks = enemy.TimeBetweenAttacks();
        sightRange = enemy.SightRange();
        attackRange = enemy.AttackRange();
        originPosition = enemyTransform.position;

        StartCoroutine(InactiveAfter(3));
    }

    void Update()
    {        
        if (isAttacking)
        {
            if (!alreadyAttacked)
            {
                enemyTransform.LookAt(player);
            }
        }
    }

    IEnumerator InactiveAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CheckPlayerDistance();

        while (true)
        {           
            while (playerInAttackRange)
            {
                //print("player In Attack Range");

                StartCoroutine(AttackPlayer());
                yield return new WaitForSeconds(timeBetweenAttacks);
                CheckPlayerDistance();
            }

            while (playerInSightRange && !playerInAttackRange)
            {
                //print("player In Sight Range");

                ChasePlayer();
                yield return new WaitForSeconds(1);
                CheckPlayerDistance();
            }

            while (!playerInSightRange && !playerInAttackRange)
            {
                //print("player So far away");

                Patroling();
                yield return new WaitForSeconds(1);
                CheckPlayerDistance();
            }
        }      
    }

    private void Patroling()
    {
        if(!patrolingPointSet) SearchWalkPoint();

        enemy.MoveAni();
        if (patrolingPointSet)
            navMeshAgent.SetDestination(patrolingPoint);

        Vector3 distanceToWalkPoint = enemyTransform.position - patrolingPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            patrolingPointSet = false;
    }


    private void ChasePlayer()
    {
        enemy.MoveAni();
        navMeshAgent.SetDestination(player.position);
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

        navMeshAgent.SetDestination(enemyTransform.position); // stop moving

        enemy.VisibleAlert();                                // show alert for attaking    
        yield return new WaitForSeconds(1);
        enemy.InvisibleAlert();

        enemy.AttackAni();                                    // attack
        alreadyAttacked = true;
        yield return new WaitForSeconds(1);

        // delay a while for already Attacking
        while (alreadyAttacked)
        {
            MoveAround();
            yield return new WaitForSeconds(0.5f);
        }

        isAttacking = false;
    }

    IEnumerator Attack()
    {
        enemy.VisibleAlert();       
        yield return new WaitForSeconds(1);
        enemy.AttackAni();

      
        isAttacking = false;
        alreadyAttacked = true;
        enemy.InvisibleAlert();
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        ///End of attack code

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-patrolingPointRange, patrolingPointRange);
        float randomX = UnityEngine.Random.Range(-patrolingPointRange, patrolingPointRange);

        patrolingPoint = new Vector3(originPosition.x + randomX, originPosition.y, originPosition.z + randomZ);

        patrolingPointSet = true;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(enemyTransform.position, player.position);
        if (distance < attackRange)
            playerInAttackRange = true;
        else if(distance < sightRange)
        {
            playerInAttackRange = false;
            playerInSightRange = true;
        }
        else
        {
            playerInAttackRange = false;
            playerInSightRange = false;
        }
    }

    void MoveAround()
    {
        float randomZ = UnityEngine.Random.Range(-2, 2);
        float randomX = UnityEngine.Random.Range(-2, 2);
        Vector3 point = new Vector3(enemyTransform.position.x + randomX,
            enemyTransform.position.y, enemyTransform.position.z + randomZ);

        navMeshAgent.SetDestination(point);
    }
}
