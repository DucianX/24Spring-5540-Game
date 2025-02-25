using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2, 3);
        Debug.Log("coroutine started " + Time.time); 
        StartCoroutine(SpawnEnemies(3));
    }

   

    void SpawnEnemy() {
        var positionOffset = Random.insideUnitSphere * 5;
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    IEnumerator SpawnEnemies(float spawnInterval) {
        Debug.Log("before yield " + Time.time);
        var enemyCount = GameObject.FindGameObjectsWithTag("Dementor").Length;
        while(true) {
            
            if (enemyCount < maxEnemyCount) {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
                Debug.Log("after yield " + Time.time);
            }
        }
    }
}
