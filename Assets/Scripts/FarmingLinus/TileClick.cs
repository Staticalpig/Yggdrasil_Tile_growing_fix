using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClick : MonoBehaviour
{
    public TileBase newTile; // Tile to change to
    public TileBase specificTile; // Tile that must be present to allow clicking
    private Tilemap tilemap;

    private void Start()
    {
        // Find the Tilemap component
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Update()
    {
        // Check for mouse click within the boundaries of the Tilemap
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<Tilemap>() == tilemap)
            {
                // Check if the clicked tile is the specific tile
                Vector3Int cellPos = tilemap.WorldToCell(hit.point);
                TileBase originalTile = tilemap.GetTile(cellPos);
                if (originalTile == specificTile)
                {
                    ChangeTile(hit.point);
                }
            }
        }
    }

    private void ChangeTile(Vector3 worldPos)
    {
        // Convert the mouse position to the cell position on the Tilemap
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        // Change the tile at the clicked position
        tilemap.SetTile(cellPos, newTile);
    }
}
