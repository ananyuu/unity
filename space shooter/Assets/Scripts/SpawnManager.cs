using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private GameObject[] powerupPrefab;
    private bool stopSpawning = false;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleRoutine());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 posEnemy = new Vector3(Random.Range(-8, 8), 7, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posEnemy ,Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnTripleRoutine()
    {
        while( stopSpawning == false)
        {
            Vector3 posTripleShot = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerupPrefab[randomPowerUp] , posTripleShot, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
