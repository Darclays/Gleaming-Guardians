using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnDistance = 10f;
    public float spawnInterval = 3f;

    private float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // Selalu spawn di depan (kanan) MC
        Vector3 spawnPos = player.position + new Vector3(spawnDistance, 0, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);  
    }
}