using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    
    public static List<GameObject> enemyTrack = new List<GameObject>();

    int currentWave = 0;
    int weight = 5;

    void Start ()
    {
        InvokeRepeating("Spawn", 1, 1);
    }


    void Spawn () {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        Debug.Log(enemyTrack.Count);

        if (currentWave == 16)
            playerHealth.currentHealth = 0;

        if (enemyTrack.Count == 0) {
            weight += 3;
            if (currentWave % 3 == 0) {
                for (int i = 0; i < weight; i++) {
                    int spawnPointIndex = Random.Range (0, spawnPoints.Length);
                    enemyTrack.Add(Instantiate(enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation));
                }
            }
            else if (currentWave % 3 == 1) {
                int used = 0;

                while (used < weight) {
                    int enemytype = Random.Range(0, enemies.Length - 1);
                    int sidx = Random.Range(0, spawnPoints.Length);
                    used += enemytype + 1;
                    enemyTrack.Add(Instantiate(enemies[enemytype], spawnPoints[sidx].position, spawnPoints[sidx].rotation));
                }
            }
            else if (currentWave % 3 == 2) {
                int used = 0;

                // Boss
                while (used < weight) {
                    int enemytype = 3;
                    int sidx = Random.Range(0, spawnPoints.Length);
                    used += enemytype + 1;
                    enemyTrack.Add(Instantiate(enemies[enemytype], spawnPoints[sidx].position, spawnPoints[sidx].rotation));
                }

                while (used < weight) {
                    int enemytype = Random.Range(0, enemies.Length - 1);
                    int sidx = Random.Range(0, spawnPoints.Length);
                    used += enemytype + 1;
                    enemyTrack.Add(Instantiate(enemies[enemytype], spawnPoints[sidx].position, spawnPoints[sidx].rotation));
                }
            }

            currentWave++;
            if (currentWave % 3 == 0)
                PlayerHealth.upgradeAvail++;
        }
        ScoreManager.wave = currentWave;
    }
    
}
