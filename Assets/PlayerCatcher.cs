using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used by the enemies to handle catching the player.
 */
[RequireComponent(typeof(Chaser))]
public class PlayerCatcher : MonoBehaviour
{
    [SerializeField] TilemapCaveGenerator tilemap;
    void Update()
    {
        // Get the target position.
        Vector3 targetPosition = GetComponent<Chaser>().TargetObjectPosition();

        // If caught target - build the map from the start.
        if (transform.position == targetPosition)
        {
            tilemap.buildMap();
        }
    }
}
