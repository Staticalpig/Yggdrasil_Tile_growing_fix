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

    private float initialSwitchTimer = 0f;
    private float intermediateSwitchTimer = 0f;
    private TileBase finalTileAtStart;
    public int stage { get; private set; } // Stage property accessible from outside
    private bool timerActive = false;
    private float switchTimer = 0f;

    void Start()
    {
        // Set the final tile at start
        finalTileAtStart = finalTile;

        // Start the initial timer
        StartInitialTimer();
    }

    // Add this boolean variable at the top of the script
private bool finalStageReached = false;

// Modify the Update method as follows
void Update()
{
    // If initial timer is active, countdown
    if (!finalStageReached && initialSwitchTimer > 0f)
    {
        initialSwitchTimer -= Time.deltaTime;

        // If timer runs out, switch tiles and reset timer
        if (initialSwitchTimer <= 0f)
        {
            SwitchTiles(initialTile, intermediateTile);
            Debug.Log("Initial stage completed. Timer started for intermediate stage.");
            StartIntermediateTimer();
        }
    }

    // If intermediate timer is active, countdown
    if (!finalStageReached && intermediateSwitchTimer > 0f)
    {
        intermediateSwitchTimer -= Time.deltaTime;

        // If timer runs out, switch tiles directly to the final stage
        if (intermediateSwitchTimer <= 0f)
        {
            SwitchTiles(intermediateTile, finalTile);
            Debug.Log("Intermediate stage completed. Switched directly to final stage.");
            finalStageReached = true;
        }
    }

    // Check if the Final Tile is different from the tile it was set to during start
    if (!finalStageReached && finalTile != finalTileAtStart)
    {
        Debug.Log("Final tile changed. Restarting initial stage timer.");
        StartInitialTimer();
        // Update the stored Final Tile to the new value
        finalTileAtStart = finalTile;
    }
}

    public void StartInitialTimer()
    {
        initialSwitchTimer = initialDelay;
        Debug.Log("Initial stage timer started.");
    }
    public void RestartInitialTimer()
    {
        // Reset stage to initial
        stage = 0;

        // Restart timer for initial stage
        StartTimer(initialDelay);
    }

    private void StartTimer(float delay)
    {
        // Start the timer
        timerActive = true;
        switchTimer = delay;
        Debug.Log("Timer started for initial stage.");
    }

    void StartIntermediateTimer()
    {
        intermediateSwitchTimer = intermediateDelay;
    }


    public void ResetTimers()
    {
        // Reset switch timers
        initialSwitchTimer = initialDelay;
        intermediateSwitchTimer = 0f;
    }

    void SwitchTiles(TileBase fromTile, TileBase toTile)
    {
        // Switch tiles based on the current stage
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) == fromTile)
            {
                tilemap.SetTile(pos, toTile);
            }
        }
    }
}
