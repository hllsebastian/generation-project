using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private Enemypool enemyPool;
    [SerializeField] private GameObject Rune;
     private float spawnRangeX = 50.0f, spawnRangeY = 180.0f; // Rango en el eje X donde se generan los enemigos
    private float spawnDelay = 5.0f; // Retraso entre cada generación de enemigos
    private int enemiesDefeated = 0;

private void Start()

    {
        for (int i = 0; i < 15; i++)
        {
            SpawnEnemy();
        }

        // Iniciar la generación continua de enemigos
        InvokeRepeating("SpawnEnemy", 0.5f, spawnDelay);
    }

    // Update is called once per frame
    private void SpawnEnemy()
    {
        // Calcular la posición aleatoria dentro del rango definido
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY), 0f);

        // Instanciar el enemigo en la posición calculada
        GameObject enemy = enemyPool.RequestEnemy();

        if (enemy != null)
        {
            enemy.transform.position = spawnPosition;
        }
        else
        {
            // Debug.Log("No enemy available.");
        }

        // Instanciar el powerup en la misma posición (si lo deseas)
        // Instantiate(prefabPowerup, spawnPosition, Quaternion.identity);
    }
    public void EnemyDefeated()
    {
        enemiesDefeated++;

        if (enemiesDefeated >= 15)
        {
            // Instanciar el objeto especial en una posición determinada
            Rune.SetActive(true);
        }
    }
}
