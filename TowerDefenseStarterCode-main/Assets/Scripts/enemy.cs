using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enums.Path path { get; set; }

    public GameObject target { get; set; }

    private int pathIndex = 1;


    public float speed = 1f;

    public float health = 10f;

    public int points = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
