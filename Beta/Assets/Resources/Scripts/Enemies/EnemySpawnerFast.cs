﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveFast
{
    public string name;
    public Transform enemy;
    public int count;
    public float rate;
}

public class EnemySpawnerFast : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public PlayerManager playerManager;
    public WaveManager waveManager;

    public WaveFast[] wave;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5.0f;
    public float waveCountdown;
    public int actualWave = 1;

    private float searchCountdown;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }

        actualWave = 1;

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (actualWave == 1) waveManager.WaveOne();
        else if (actualWave == 2) waveManager.WaveTwo();
        else if (actualWave == 3) waveManager.WaveThree();

        if (state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                actualWave += 1;
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(wave[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > wave.Length - 1)
        {
            //nextWave = 0;
            Debug.Log("ALL WAVES COMPLETED");
            playerManager.setVictory();
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0.0f)
        {
            searchCountdown = 1.0f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave (WaveFast _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            if(i == 1)
            {
                
                //waveManager.WaveOne();
            }
            yield return new WaitForSeconds(1.0f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        // Spawn enemy

        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

}
