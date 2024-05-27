
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerFamingTileMapReadController : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private List<TileData> tileData;

    Dictionary<TileBase, TileData> tileDataDictionary = new Dictionary<TileBase, TileData>();

    private void Start()
    {
        // Populate the dictionary with the tile data
        foreach (TileData data in tileData)
        {   
            // For each tile in the tileData list, add the tile and the data to the dictionary
            foreach (TileBase tile in data.tiles) 
            {
                tileDataDictionary.Add(tile, data);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetTileBase(Input.mousePosition, true);
        
        }
    }

    private Vector3 getPosition(Vector2 pos, bool isMouse)
    {   
        Vector3 worldPos;
        if (isMouse)
        {
            worldPos = Camera.main.ScreenToWorldPoint(pos);
        }
        else
        {
            worldPos = pos;
        }
        return worldPos;
    }

    public TileBase GetTileBase(Vector2 pos, bool isMouse)
    {
        Vector3 worldPos = getPosition(pos, isMouse);
        Vector3Int cellPos = tileMap.WorldToCell(worldPos);
    
    
        TileBase tile = tileMap.GetTile(cellPos);

        Debug.Log("The tile is: " + tile.name + " at position: " + cellPos);
        return tile;
       
    }

}
