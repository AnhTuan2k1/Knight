using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleOver;

    private enum State {
        Idle,
        Active,
        BattleOver,
    }
    
    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Enemy[] walkableEnemies;
    [SerializeField] private Enemy[] flyableEnemies;
    [SerializeField] private Transform[] walkablePosition;
    [SerializeField] private Transform[] flyablePosition;
    private Transform playerPosition;
    [SerializeField] private List<Enemy> currentEnemies;
    [SerializeField] private int waveLevel;

    [SerializeField] private State state;

    private void Awake() {
        state = State.Idle;
        currentEnemies ??= new List<Enemy>();
    }

    private void Start() {
        playerPosition = GameObject.FindWithTag("Player").transform;

        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        Invoke("AreEnemiesCleared", 1);
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e) {
        if (state == State.Idle) {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle() {
        print("StartBattle");
        SpawnEnemyWave();
        state = State.Active;
        OnBattleStarted?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        switch (state) {
        case State.Active:               
            break;
        }
    }

    private bool AreEnemiesCleared()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            if (currentEnemies[i] == null) 
                currentEnemies.RemoveAt(i);
        }

        Invoke(nameof(AreEnemiesCleared), 1);
        if (currentEnemies.Count == 0) return true;
        else return false;        
    }

    private void TestBattleOver() {
        if (state == State.Active) {
            if (IsWaveOver()) {

                state = State.BattleOver;
                Debug.Log("Battle Over!");
                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool IsWaveOver()
    {
        return false;
    }

    private void SpawnEnemyWave()
    {
        int flyableEnemyNumber = flyableEnemies.Length;
        int walkableEnemyNumber = walkableEnemies.Length;

        while (currentEnemies.Count < 10)
        {
            bool ok = false;

            for (int i = 0; i < flyablePosition.Length; i++)
            {
                if (Vector3.Distance(flyablePosition[i].position, playerPosition.position) < 16)
                {
                    int flyableIndex = UnityEngine.Random.Range(0, flyableEnemyNumber);
                    currentEnemies.Add(
                        Instantiate(flyableEnemies[flyableIndex], flyablePosition[i])
                        );
                    ok = true;
                }
            }

            for (int i = 0; i < walkablePosition.Length; i++)
            {
                if (Vector3.Distance(walkablePosition[i].position, playerPosition.position) < 10)
                {
                    int walkableIndex = UnityEngine.Random.Range(0, walkableEnemyNumber);
                    currentEnemies.Add(
                        Instantiate(walkableEnemies[walkableIndex], walkablePosition[i])
                        );
                    ok = true;
                }
            }

            if(!ok) currentEnemies.Add(Instantiate(flyableEnemies[0], flyablePosition[1]));
        }     
    }
}
