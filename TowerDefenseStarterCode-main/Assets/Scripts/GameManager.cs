using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TowerMenuPrefab; // Reference to the TowerMenu prefab
    public List<GameObject> Archers; // List of Archer tower prefabs
    public List<GameObject> Swords; // List of Sword tower prefabs
    public List<GameObject> Wizards; // List of Wizard tower prefabs
    private ConstructionSite selectedSite; // Remember the selected site

    // Define WaveInfo class within GameManager
    public class WaveInfo
    {
        public int enemyCount;
        // Add any other properties needed for a wave
    }

    private TowerMenu towerMenu; // Reference to the TowerMenu instance

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

    // Updated SelectSite method to work with ConstructionSite objects
    // Updated SelectSite method to work with ConstructionSite objects
    public void SelectSite(ConstructionSite site)
    {
        // Remember the selected site
        selectedSite = site;

        // Get the TowerMenu component
        towerMenu = TowerMenuPrefab.GetComponent<TowerMenu>();

        // Pass the selected site to the TowerMenu
        towerMenu.SetSite(site);
    }



    // Updated SetSelectedSite method to work with ConstructionSite objects
    public void SetSelectedSite(ConstructionSite site)
    {
        selectedSite = site;
    }

    public void Build(TowerType type, SiteLevel level)
    {
        // Je kunt niet bouwen als er geen site is geselecteerd
        if (selectedSite == null)
        {
            return;
        }

        // Selecteer de juiste lijst op basis van het torentype
        List<GameObject> towerList = null;
        switch (type)
        {
            case TowerType.Archer:
                towerList = Archers;
                break;
            case TowerType.Sword:
                towerList = Swords;
                break;
            case TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        // Gebruik een switch met het niveau om een GameObject-toren te maken
        GameObject towerPrefab = towerList[(int)level];

        // Haal de positie van de ConstructionSite op
        Vector3 buildPosition = selectedSite.GetBuildPosition();

        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);

        // Configureer de geselecteerde site om de toren in te stellen
        selectedSite.SetTower(towerInstance, level, type); // Voeg level en type toe als
        towerMenu.SetSite(null);
    }
    void Start()
    {
        // No need to assign towerMenu anymore
        towerMenu = TowerMenuPrefab.GetComponent<TowerMenu>();
    }
}
