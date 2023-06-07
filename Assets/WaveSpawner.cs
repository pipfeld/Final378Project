using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;

}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
    }

    void SpawnWave()
    {
        if(canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.noOfEnemies == 0){
                canSpawn = false;
            }
        }
    }
}
