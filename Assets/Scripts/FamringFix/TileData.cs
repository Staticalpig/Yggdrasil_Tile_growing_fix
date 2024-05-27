
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Farming/Tile Data")]
public class TileData : ScriptableObject
{

    public List<TileBase> tiles;
    public bool isPlowable;
}
