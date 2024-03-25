 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        if (target != null)
        {
            // Draai het projectiel onmiddellijk naar het doelwit bij het starten
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogWarning("Geen doelwit ingesteld voor het projectiel.");
        }
    }

    void Update()
    {
        if (target == null)
        {
            // Als het doelwit niet meer bestaat, verwijder dit object
            Destroy(gameObject);
            return;
        }

        // Beweeg het projectiel naar het doelwit
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Controleer of de afstand tussen dit object en het doelwit kleiner is dan 0.2
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            
           

            // Vernietig dit object nadat het het doelwit heeft geraakt
            Destroy(gameObject);
        }
    }

  
}