using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    private CaveGenerator caveGenerator;

    [Tooltip("The growing percentage of the map.")]
    [SerializeField] private float growPrecentage = 5f;

    [Tooltip("The tile that the player needs to reach for the win.")]
    [SerializeField] private TileBase winTile = null;

    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        
        // CHANGE: Created a buildMap function to build the map:
        buildMap();
    }

/*
 * buildMap was created to randomize a new map, build it on the screen,
 * and then place the enemies on the new tilemap.
 */
    public void buildMap()
    {
        // Randomize a new map, place the tiles and simulate the cave pattern.
        caveGenerator.RandomizeMap();

        GenerateAndDisplayTexture(caveGenerator.GetMap());

        StartCoroutine(SimulateCavePattern());

        // Built for 2 enemies.
        // Find the 2 enemy objects and place them on the map in different edges of the map.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null && enemies.Length == 2)
        {
            enemies[0].transform.position = new Vector3(gridSize - 1, 0, 0);
            enemies[1].transform.position = new Vector3(0, gridSize - 1, 0);
        }

        // Place the player on the bottom left edge of the tilemap.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3Int startPosition = Vector3Int.zero;

        // Convert the zero vector from tilemap cell coordinates to world cell coordinates.
        player.transform.position = tilemap.GetCellCenterWorld(startPosition);
    }

    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern()  {
        for (int i = 0; i < simulationSteps; i++)   {
            yield return new WaitForSeconds(pauseTime);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }
        
        Debug.Log("Simulation completed!");
    }

    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile: floorTile;
                tilemap.SetTile(position, tile);
            }
        }

        // CHANGE: place the winning tile on the top right edge of the tilemap.
        Vector3Int winTilePosition = new Vector3Int(gridSize - 1, gridSize - 1, 0);
        tilemap.SetTile(winTilePosition, winTile);
    }

    public TileBase getWinTile()
    {
        return winTile;
    }

    // If the player reached the WinTile - he should call this function.
    public void reachedWinTile()
    {
        // Grow the tilemap by growPrecentage% and build the new map on the screen.
        gridSize += (int)(gridSize * (growPrecentage / 100));
        caveGenerator.setGridSize(gridSize);
        buildMap();
    }

}
