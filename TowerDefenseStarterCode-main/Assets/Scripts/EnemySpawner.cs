using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> enemies; // Changed "Enemies" to "enemies" to follow naming conventions

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
        var newEnemy = Instantiate(enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponent<enemy>(); // Changed "GetComponentInParent" to "GetComponent"

        // Set path and target for the enemy
        script.path = path;
        script.target = RequestTarget(path, 1); // Start with the second waypoint (index 1)
    }

    private void SpawnTester()
    {
        SpawnEnemy(0, Path.Path1);
    }

    private void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }
}
