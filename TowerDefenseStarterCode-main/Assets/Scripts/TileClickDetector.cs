using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileClickDetector : MonoBehaviour
{
    public Camera cam; // Assign your main camera here through the Inspector 
    public Tilemap tilemap; // Assign your tilemap here through the Inspector 

    public ConstructionSite SelectedSite { get; private set; }
    public Vector3 SelectedPosition { get; private set; }
    public TileBase SelectedTile { get; private set; }

    private Dictionary<Vector3Int, ConstructionSite> siteMap = new Dictionary<Vector3Int, ConstructionSite>();

    private void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null && tile.name == "buildingPlaceGrass")
            {
                ConstructionSite site = new ConstructionSite(cellPosition, tilemap.CellToWorld(cellPosition));
                siteMap[cellPosition] = site;
            }
        }
    }

    // Update is called once per frame 
    void Update()
    {
        // Check for a left mouse button click 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            DetectTileClicked();
        }
    }

    void DetectTileClicked()
    {
        // Convert mouse click position to world space 
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Ensure the z-position is set correctly for 2D 

        // Convert world position to tilemap cell position 
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        // Check if the clicked cell contains a tile 
        TileBase clickedTile = tilemap.GetTile(cellPosition);

        if (clickedTile != null)
        {
            SelectedTile = clickedTile;
            SelectedPosition = tilemap.GetCellCenterWorld(cellPosition);
            if (clickedTile.name == "buildingPlaceGrass")
            {
                if (siteMap.TryGetValue(cellPosition, out ConstructionSite site))
                {
                    SelectedSite = site;
                }
                else
                {
                    SelectedSite = null;
                }
            }
            else
            {
                SelectedSite = null;
            }
        }
        else
        {
            SelectedTile = null;
            SelectedPosition = Vector3.zero;
            SelectedSite = null;
        }
    }
}
