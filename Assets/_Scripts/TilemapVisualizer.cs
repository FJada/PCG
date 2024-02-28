using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Cinemachine;


public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSiderLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public GameObject enemy;
    public Transform player;
    public GameObject goal;
    public GameObject enemySpawner;
    public Vector3 playerPosition;
    private float offsetDistance = 0.5f;
    public CinemachineVirtualCamera virtualCamera;

    private int tileCounter = 0;

    private bool instantiatePlayer = true;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Destroy(GameObject.FindGameObjectWithTag("Finish"));
        Destroy(GameObject.FindGameObjectWithTag("EnemySpawner"));

        // Loop through each enemy and destroy them
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        int length = positions.ToList().Count;
        foreach (var position in positions)
        {
            if (instantiatePlayer)
            {
                Instantiate(player, new Vector3(position.x, position.y, 0f), Quaternion.identity);
                playerPosition = new Vector3( position.x, position.y, 0f);
                virtualCamera.Follow = GameObject.FindWithTag("Player").transform;
            }
            instantiatePlayer = false;

            length--;
            Debug.Log(length);
            tileCounter++;
            if(length % 20 == 0 && (length!=2 || length!=1))
            {
                if(tileCounter > 30)
                {
                    instantiateEnemy(new Vector3(position.x, position.y, 0f));
                }
            }
            if (length == 2)
            {
                Vector3 enemySpawnerPos = new Vector3(position.x, position.y, 0f);
                Vector3 directionToPlayer = (playerPosition - enemySpawnerPos).normalized;

                // Offset the random position towards the player by the specified distance
                Vector3 finalPosition = enemySpawnerPos + directionToPlayer * offsetDistance;

                Instantiate(enemySpawner, finalPosition, Quaternion.identity);
                //if (position.x>0)
                //{
                //    if(position.y>0)
                //    {
                //        Instantiate(enemySpawner, new Vector3(position.x-0.5f, position.y-0.5f, 0f), Quaternion.identity);
                //    } else if(position.y < 0)
                //    {
                //        Instantiate(enemySpawner, new Vector3(position.x-0.5f, position.y+0.5f, 0f), Quaternion.identity);
                //    }
                //} else if(position.x < 0)
                //{
                //    if (position.y > 0)
                //    {
                //        Instantiate(enemySpawner, new Vector3(position.x+0.5f, position.y-0.5f, 0f), Quaternion.identity);
                //    }
                //    else if(position.y < 0)
                //    {
                //        Instantiate(enemySpawner, new Vector3(position.x+0.5f, position.y+0.5f, 0f), Quaternion.identity);
                //    }
                //}
            }
            if (length == 1)
            {

                Vector3 goalPos = new Vector3(position.x, position.y, 0f);
                Vector3 directionToPlayer = (playerPosition - goalPos).normalized;

                // Offset the random position towards the player by the specified distance
                Vector3 finalPosition = goalPos + directionToPlayer * offsetDistance;

                Instantiate(goal, finalPosition, Quaternion.identity);
                //if (position.x > 0)
                //{
                //    if (position.y > 0)
                //    {
                //        Instantiate(goal, new Vector3(position.x - 0.5f, position.y - 0.5f, 0f), Quaternion.identity);
                //    }
                //    else if (position.y < 0)
                //    {
                //        Instantiate(goal, new Vector3(position.x - 0.5f, position.y + 0.5f, 0f), Quaternion.identity);
                //    }
                //}
                //else if (position.x < 0)
                //{
                //    if (position.y > 0)
                //    {
                //        Instantiate(goal, new Vector3(position.x + 0.5f, position.y - 0.5f, 0f), Quaternion.identity);
                //    }
                //    else if (position.y < 0)
                //    {
                //        Instantiate(goal, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity);
                //    }
                //}
            }
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSiderLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile!=null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeASInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeASInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeASInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }


    public void instantiateEnemy(Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
    }

    Vector2 AdjustPosition(Vector2 position)
    {
        // Cast rays in all four directions
        RaycastHit2D hitUp = Physics2D.Raycast(position, Vector2.up, 0.5f);
        RaycastHit2D hitDown = Physics2D.Raycast(position, Vector2.down, 0.5f);
        RaycastHit2D hitLeft = Physics2D.Raycast(position, Vector2.left, 0.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(position, Vector2.right, 0.5f);

        // Adjust the position if any ray hits a collider
        if (hitUp.collider != null)
            position.y = hitUp.point.y - 0.5f;

        if (hitDown.collider != null)
            position.y = hitDown.point.y + 0.5f;

        if (hitLeft.collider != null)
            position.x = hitLeft.point.x + 0.5f;

        if (hitRight.collider != null)
            position.x = hitRight.point.x - 0.5f;

        return position;
    }
}
