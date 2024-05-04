using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Enemypool : MonoBehaviour
{
       [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> enemyList = new List<GameObject>();

    private float poolSize = 15;
   private void Start()
    {
        AddEnemyToPool();
    }

    private void AddEnemyToPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);

            enemyList.Add(enemy);
        }
    }
        public GameObject RequestEnemy()
    {
        foreach (GameObject enemy in enemyList)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);

                return enemy;
            }
        }
        return null;
    }
}
