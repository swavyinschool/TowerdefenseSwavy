using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TowerMenu;
    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    // Define WaveInfo class within GameManager
    public class WaveInfo
    {
        public int enemyCount;
        // Add any other properties needed for a wave
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // No need to assign towerMenu anymore
        // towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
}
