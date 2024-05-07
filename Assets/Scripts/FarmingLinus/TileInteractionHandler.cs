using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteractionHandler : MonoBehaviour
{
    private void Start()
    {
        // Find the InteractableTile component in children of this GameObject
        InteractableTile[] interactableTiles = GetComponentsInChildren<InteractableTile>();

        // Subscribe to the TileInteracted event for each InteractableTile component found
        foreach (InteractableTile interactableTile in interactableTiles)
        {
            interactableTile.TileInteracted += OnTileInteracted;
        }
    }

    // This method will be called when a tile is interacted with
    private void OnTileInteracted(Vector3Int tilePosition)
    {
        // Perform your desired action here, using the tile position if needed
        Debug.Log("Tile interacted at position: " + tilePosition);
    }
}

