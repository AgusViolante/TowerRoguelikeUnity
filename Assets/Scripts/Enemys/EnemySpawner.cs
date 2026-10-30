using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] EnemyPath path;
    [SerializeField] int enemyCount = 10;
    [SerializeField] float interval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.init(path);
            yield return new WaitForSeconds(interval);
        }
    }
}
