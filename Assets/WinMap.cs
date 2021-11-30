using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/*
 * This script is used by the player to handle what happens when he reaches the win tile.
 */
public class WinMap : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;

    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    void Update()
    {
        // Get the current tile of the player.
        TileBase tileOnPosition = TileOnPosition(transform.position);
        TilemapCaveGenerator tileGenerator = tilemap.GetComponent<TilemapCaveGenerator>();

        // If the current tile is the WinTile (the target tile needed for the win):
        if (tileOnPosition == tileGenerator.getWinTile())
        {
            // Tell the generator we won the game so it would build a new map.
            tileGenerator.reachedWinTile();
        }
    }
}
