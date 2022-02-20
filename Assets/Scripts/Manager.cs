using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager>
{
   
    public GameObject spawnPoint;
    public GameObject[] enemys;
    public int maxEnemyOnScreen;
    public int totalEnemys;
    public int enemysPerSpawn;

    public List<Enemy> EnemyList = new List<Enemy>();
    [SerializeField] private float spawnDelay=0.5f;
   
    void Start()
    {
        StartCoroutine(Spawn());
    }

   
   
    IEnumerator Spawn()
    {
        if (enemysPerSpawn > 0 && EnemyList.Count < totalEnemys)
        {
            for (int i=0; i < enemysPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemyOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemys[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }
    public void RegisterEnemy(Enemy enemy) 
    {
        EnemyList.Add(enemy);
    }
    public void UnRegisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    public void DestroyEnemys() 
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
}
