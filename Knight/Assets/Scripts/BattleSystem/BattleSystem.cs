using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattleObserve {
    
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Enemy[] walkableEnemies;
    [SerializeField] private Enemy[] flyableEnemies;
    [SerializeField] private Transform[] walkablePosition;
    [SerializeField] private Transform[] flyablePosition;
    private Transform playerPosition;
    public List<Enemy> currentEnemies;
    [SerializeField] private int waveLevel;

    private void Start() {
        playerPosition = GameObject.FindWithTag("Player").transform;
        currentEnemies ??= new List<Enemy>();
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e) {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void StartBattle() {
        print("StartBattle");
        SpawnNewEnemyWave();
    }


    private void SpawnNewEnemyWave()
    {
        //Enemy w = Instantiate(flyableEnemies[1], flyablePosition[2], true);
        //w.Attach(this);
        //currentEnemies.Add(w);
        //return;

        int flyableEnemyNumber = flyableEnemies.Length;
        int walkableEnemyNumber = walkableEnemies.Length;

        while (currentEnemies.Count < 10)
        {
            bool ok = false;

            if(flyableEnemyNumber != 0) 
            for (int i = 0; i < flyablePosition.Length; i++)
            {
                if (Vector3.Distance(flyablePosition[i].position, playerPosition.position) < 16)
                {                   
                    int flyableIndex = UnityEngine.Random.Range(0, flyableEnemyNumber);
                    Enemy enemy = Instantiate(flyableEnemies[flyableIndex], flyablePosition[i]);

                    enemy.Attach(this);
                    currentEnemies.Add(enemy);
                    ok = true;
                }
            }

            if(walkableEnemyNumber != 0) 
            for (int i = 0; i < walkablePosition.Length; i++)
            {
                if (Vector3.Distance(walkablePosition[i].position, playerPosition.position) < 10)
                {
                    int walkableIndex = UnityEngine.Random.Range(0, walkableEnemyNumber);
                    Enemy enemy = Instantiate(walkableEnemies[walkableIndex], walkablePosition[i]);

                    enemy.Attach(this);
                    currentEnemies.Add(enemy);
                    ok = true;
                }
            }

            if (!ok)
            {
                Enemy enemy = Instantiate(flyableEnemies[1], flyablePosition[2]);
                enemy.Attach(this);
                currentEnemies.Add(enemy);
            } 
        }     
    }

    void IBattleObserve.Update(Enemy enemy)
    {
        print("update called");
        currentEnemies.Remove(enemy);

        if (currentEnemies.Count == 0) SpawnNewEnemyWave();
    }
}
