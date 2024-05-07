using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableTile : MonoBehaviour
{
    // This event will be triggered when the player interacts with the tile
    public delegate void OnTileInteract(Vector3Int tilePosition);
    public event OnTileInteract TileInteracted;

    private void OnMouseDown()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the tilemap from the clicked position
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = GetComponent<Grid>().WorldToCell(clickPosition);
            Tilemap tilemap = GetComponent<Tilemap>();

            // Check if the clicked position is within the tilemap bounds
            if (tilemap.HasTile(gridPosition))
            {
                // Trigger the interaction event with the position of the clicked tile
                if (TileInteracted != null)
                {
                    TileInteracted(gridPosition);
                }
            }
        }
    }
}