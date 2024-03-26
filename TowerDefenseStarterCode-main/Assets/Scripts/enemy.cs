using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f; // Gezondheid van de vijand
    public int points = 1;
    public Enums.Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
                pathIndex++;

                if (target == null)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Functie om de vijand te beschadigen
    public void Damage(int damage)
    {
        // Verlaag de gezondheidswaarde met het schadebedrag
        health -= damage;

        // Controleer of de gezondheid kleiner of gelijk is aan nul
        if (health <= 0)
        {
            // Vernietig het spelobject van de vijand
            Destroy(gameObject);
        }
    }
}
