using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SiteLevel
{
    Onbebouwd,
    Level1,
    Level2,
    Level3
}
public class ConstructionSite
{
    public Vector3Int TilePosition { get; set; }
    public Vector3 WorldPosition { get; set; }
    public SiteLevel Level { get; set; }
    public TowerType TowerType { get; set; }
    private GameObject tower;
    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        // Wijs de tilePosition en worldPosition toe.
        this.TilePosition = tilePosition;
        this.WorldPosition = worldPosition;

        // Pas de Y-waarde van worldPosition aan (met 0.5)
        this.WorldPosition += new Vector3(0, 0.5f, 0);

        // Stel tower gelijk aan null
        this.tower = null;
    }
    public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
    {
        // Controleer eerst of er al een bestaande toren is
        if (tower != null)
        {
            // Als er een bestaande toren is, verwijder deze dan eerst
            GameObject.Destroy(tower);
        }

        // Wijs vervolgens de nieuwe toren toe
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
}
