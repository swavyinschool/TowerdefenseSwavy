using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies 

    public float attackRate = 1f; // How often the tower attacks (attacks per second) 

    public int attackDamage = 1; // How much damage each attack does 

    public float attackSize = 1f; // How big the bullet looks 



    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 

    public TowerType type; // the type of this tower 



    // Draw the attack range in the editor for easier debugging 

    void OnDrawGizmosSelected()

    {

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
