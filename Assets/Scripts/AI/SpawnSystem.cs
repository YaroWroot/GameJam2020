using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting};

    [System.Serializable]
    public class Wave
    {
        public Transform _enemy;
        public int _count;
        public float _rate;
    }

    public Wave[] _waves;
    private int _nextWave = 0;

    public Transform[] _spawnPoints;

    public float _waveCooldown = 5f;
    private float _waveCountdown;

    private float _checkCountdown = 1f;

    private SpawnState _state = SpawnState.Counting;

    private void Start()
    {
        _waveCountdown = _waveCooldown;
    }

    private void Update()
    {
        if(_state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveComplete();
            }
            else
            {
                return;
            }
        }

        if(_waveCountdown <= 0)
        {
            if(_state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(_waves[_nextWave]));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveComplete()
    {
        _state = SpawnState.Counting;
        _waveCountdown = _waveCooldown;

        if (_nextWave + 1 > _waves.Length - 1)
        {
            _nextWave = 0;
            //Loop for infinite waves here
        }
        else
        {
            _nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        _checkCountdown -= Time.deltaTime;
        if(_checkCountdown <= 0f)
        {
            _checkCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave._count);
        _state = SpawnState.Spawning;

        for (int i = 0; i < _wave._count; i++)
        {
            SpawnEnemy(_wave._enemy);
            yield return new WaitForSeconds(1f / _wave._rate);
        }

        _state = SpawnState.Waiting;


        yield break;
    }

    private void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Instantiate(_enemy, transform.position, transform.rotation);
    }

}
