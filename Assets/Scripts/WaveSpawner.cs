using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Room room;

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;
 
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point");
        }  
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //check enemies is alive
            if(!EnemyIsAlive())
            {
                WaveComplete();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                //start spawning
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveComplete()
    {
        Debug.Log("level complited");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if( nextWave + 1 > waves.Length - 1)
        {
            room.isRoomClear = true;
            room.doorVisible.enabled = false;
            room.door.isTrigger = true;
        }
        else{ nextWave++; }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawn wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        // spawn 
        for(int i =0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            // time between spawn
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        // spawn enemy

        Transform sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }
}
