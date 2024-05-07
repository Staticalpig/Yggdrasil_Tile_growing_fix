using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoTileInteractionHandler : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void Start()
    {
        if (tilemap == null)
        {
            Debug.LogWarning("Tilemap reference is not set. Please assign a Tilemap GameObject.");
            return;
        }

        InteractableTile[] interactableTiles = tilemap.GetComponentsInChildren<InteractableTile>();

        foreach (InteractableTile interactableTile in interactableTiles)
        {
            interactableTile.TileInteracted += OnTileInteracted;
        }
    }

    private void OnTileInteracted(Vector3Int tilePosition)
    {
        // Perform your desired action here, using the tile position if needed
        Debug.Log("Tile interacted at position: " + tilePosition);
    }
}
