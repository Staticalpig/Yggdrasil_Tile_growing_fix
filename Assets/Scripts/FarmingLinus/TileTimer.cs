using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTimer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase initialTile;
    public TileBase intermediateTile;
    public TileBase finalTile;
    public float initialDelay = 3f; // Delay before switching initial tiles to intermediate tiles (in seconds)
    public float intermediateDelay = 3f; // Delay before switching intermediate tiles to final tiles (in seconds)

    private TileBase[,] previousTiles;
    private bool timerActive = false;
    private float switchTimer = 0f;
    private int stage = 0; // 0: Initial, 1: Intermediate, 2: Final

    void Start()
{
    // Start the timer
    StartTimer();
}



    void Update()
    {
        // If timer is active, countdown
        if (timerActive)
        {
            switchTimer -= Time.deltaTime;

            // If timer runs out, switch tiles and reset timer
            if (switchTimer <= 0f)
            {
                SwitchTiles();
                Debug.Log(GetStageName(stage) + " stage completed.");

                // Increment stage and set timer for next stage
                stage++;
                if (stage == 1)
                {
                    switchTimer = intermediateDelay;
                    Debug.Log("Timer started for intermediate stage.");
                }
                else if (stage == 2)
                {
                    // No need for a timer for the final stage, so we end the process here
                    StopTimer();
                    Debug.Log("All tiles at final stage. Timer stopped.");
                }
            }
        }
    }

    void InitializePreviousTiles()
{
    // Get the bounds of the tilemap
    BoundsInt bounds = tilemap.cellBounds;

    // Check if the previousTiles array needs to be resized
    if (previousTiles == null || previousTiles.GetLength(0) != bounds.size.x || previousTiles.GetLength(1) != bounds.size.y)
    {
        // Resize the previousTiles array to match the size of the tilemap bounds
        previousTiles = new TileBase[bounds.size.x, bounds.size.y];
    }

    // Loop through all positions within the tilemap bounds
    foreach (Vector3Int pos in bounds.allPositionsWithin)
    {
        // Calculate the array index based on the position relative to the tilemap bounds
        int x = pos.x - bounds.xMin;
        int y = pos.y - bounds.yMin;

        // Check if the position is within the tilemap bounds
        if (x >= 0 && x < bounds.size.x && y >= 0 && y < bounds.size.y)
        {
            // Store the tile at the current position in the previousTiles array
            previousTiles[x, y] = tilemap.GetTile(pos);
        }
    }
}




    void StartTimer()
    {
        // Start the timer and set the initial delay
        timerActive = true;
        switchTimer = initialDelay;
        Debug.Log("Timer started for initial stage.");
    }

    void StopTimer()
    {
        // Stop the timer
        timerActive = false;
    }

    void SwitchTiles()
{
    // Switch tiles based on the current stage
    TileBase currentTile = (stage == 0) ? initialTile : intermediateTile;

    BoundsInt bounds = tilemap.cellBounds;

    foreach (Vector3Int pos in bounds.allPositionsWithin)
    {
        int x = pos.x - bounds.xMin;
        int y = pos.y - bounds.yMin;

        if (x < 0 || x >= previousTiles.GetLength(0) || y < 0 || y >= previousTiles.GetLength(1))
        {
            continue;
        }

        if (previousTiles[x, y] == currentTile)
        {
            tilemap.SetTile(pos, GetNextTile(stage));
        }
    }

    // Update the previousTiles array with the current state of the tilemap
    UpdatePreviousTiles();
}




    void UpdatePreviousTiles()
{
    // Get the bounds of the tilemap
    BoundsInt bounds = tilemap.cellBounds;

    // Loop through all positions within the tilemap bounds
    foreach (Vector3Int pos in bounds.allPositionsWithin)
    {
        int x = pos.x - bounds.xMin;
        int y = pos.y - bounds.yMin;

        if (x < 0 || x >= previousTiles.GetLength(0) || y < 0 || y >= previousTiles.GetLength(1))
        {
            continue;
        }

        // Update the previousTiles array with the current state of the tilemap
        previousTiles[x, y] = tilemap.GetTile(pos);
    }
}



    TileBase GetNextTile(int currentStage)
    {
        // Determine the next tile based on the current stage
        return (currentStage == 0) ? intermediateTile : finalTile;
    }

    string GetStageName(int stage)
    {
        // Return the name of the stage based on its index
        return (stage == 0) ? "Initial" : (stage == 1) ? "Intermediate" : "Final";
    }
}
