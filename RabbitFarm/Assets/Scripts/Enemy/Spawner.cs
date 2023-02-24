using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameManager gameManager;

    public Enemy enemy1;
    public Enemy enemy2;
    public Enemy enemy3;

    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    public int enemyCount;
    public float timeBetweenSpawns = 1.5f;

    int a;
    int b;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        NextWave();
    }

    private void Update()
    { 
        if(gameManager.state != EState.GAMEOVER)
        { 
        if(enemiesRemainingToSpawn > 0 && Time.time> nextSpawnTime)
            {
                Enemy spawnedEnemy;
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + timeBetweenSpawns;
                switch (a)
                {
                    default:
                        break;
                    case 0:
                        spawnedEnemy = Instantiate(enemy1, transform.position, Quaternion.identity) as Enemy;
                        spawnedEnemy.OnDeath += OnEnemyDeath;
                        break;
                    case 1:
                        spawnedEnemy = Instantiate(enemy2, transform.position, Quaternion.identity) as Enemy;
                        spawnedEnemy.OnDeath += OnEnemyDeath;
                        break;
                    case 2:
                        spawnedEnemy = Instantiate(enemy3, transform.position, Quaternion.identity) as Enemy;
                        spawnedEnemy.OnDeath += OnEnemyDeath;
                        break;
                }
            }
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if(enemiesRemainingAlive ==0)
        {
            NextWave();
        }
    }
    
    void NextWave()
    {
        ActiveSpawner();
        if (currentWaveNumber < Mathf.Infinity)
        {
            if (currentWaveNumber >= 3)
            {
                a = Random.Range(0, 3);
            }
            else if (currentWaveNumber >= 2)
            {
                a = Random.Range(0, 2);
            }
            else
            {
                a = Random.Range(0, 1);
            }
            currentWaveNumber++;
            Debug.Log("Wave: " + currentWaveNumber);

            enemiesRemainingToSpawn = (currentWaveNumber + 3) * 2;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            Debug.Log(enemiesRemainingToSpawn);
        }
    }
    void ActiveSpawner()
    {
        b = Random.Range(0, 3);

        if (b == 2)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Invoke("ActiveSpawner", 10.0f);
        }
    }
}
