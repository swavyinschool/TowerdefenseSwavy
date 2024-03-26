using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TowerMenuPrefab;
    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;
    private ConstructionSite selectedSite;
    private TowerMenu towerMenu;

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

    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
        towerMenu = TowerMenuPrefab.GetComponent<TowerMenu>();
        towerMenu.SetSite(site);
    }

    public void SetSelectedSite(ConstructionSite site)
    {
        selectedSite = site;
    }

    public void StartEnemyWave(int enemyType, Enums.Path path)
    {
        EnemySpawner.Instance.StartSpawn(enemyType, path);
    }

    public void Build(Enums.TowerType type, Enums.SiteLevel level)
    {
        if (selectedSite == null)
        {
            return;
        }

        List<GameObject> towerList = null;
        switch (type)
        {
            case Enums.TowerType.Archer:
                towerList = Archers;
                break;
            case Enums.TowerType.Sword:
                towerList = Swords;
                break;
            case Enums.TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        if (towerList == null || (int)level >= towerList.Count)
        {
            return;
        }

        GameObject towerPrefab = towerList[(int)level];
        Vector3 buildPosition = selectedSite.GetBuildPosition();
        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
        selectedSite.SetTower(towerInstance, level, type);
        towerMenu.SetSite(null);
    }

    void Start()
    {
        towerMenu = TowerMenuPrefab.GetComponent<TowerMenu>();
    }
}
