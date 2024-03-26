using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> enemies;

    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Verwijder de SpawnTester-methode en de Start-methode, omdat we deze niet nodig hebben

    public void StartSpawn(int enemyType, Enums.Path path) // Hernoem de methode naar StartSpawn
    {
        // Begin met het spawnen van vijanden wanneer de "Start Wave" knop wordt ingedrukt
        spawnCoroutine = StartCoroutine(SpawnEnemies(enemyType, path));
    }

    public void StopSpawn()
    {
        // Stop met het spawnen van vijanden wanneer nodig
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }

    private IEnumerator SpawnEnemies(int enemyType, Enums.Path path)
    {
        while (true)
        {
            // Blijf vijanden spawnen totdat de loop wordt gestopt
            SpawnEnemy(enemyType, path);
            yield return new WaitForSeconds(1f); // Wacht 1 seconde voordat je de volgende vijand spawnt
        }
    }

    public void SpawnEnemy(int type, Enums.Path path)
    {
        var newEnemy = Instantiate(enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponent<enemy>();

        script.path = path;
        script.target = RequestTarget(path, 1);
    }

    public GameObject RequestTarget(Enums.Path path, int index)
    {
        List<GameObject> selectedPath = path == Enums.Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }
}
