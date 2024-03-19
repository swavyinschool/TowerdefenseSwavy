using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    private Coroutine spawnCoroutine; // Store reference to the coroutine

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

   

   

    private void SpawnEnemy(int type, Path path)
    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;
        if (selectedPath.Count > 0)
        {
            var spawnPosition = selectedPath[0].transform.position;
            var newEnemy = Instantiate(Enemies[type], spawnPosition, Quaternion.identity);
            var script = newEnemy.GetComponent<enemy>();
            script.path = path;
            script.target = selectedPath[1];
        }
        else
        {
            Debug.LogWarning("No path available for enemy spawn!");
        }
    }

    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }

    private void OnDestroy()
    {
        // Ensure that the coroutine is stopped when the EnemySpawner is destroyed
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
}