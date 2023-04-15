using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cave
{
    public Cave()
    {
        GenerateRandomCells();
        SmoothenWalls();
    }

    private void GenerateRandomCells()
    {
        int _threshold = 55;
        for (int y = 0; y < Map.Size.y; y++)
        {
            for (int x = 0; x < Map.Size.x; x++)
            {
                if (x == 0 || x == Map.Size.x - 1 || y == 0 || y == Map.Size.y - 1)
                    continue;
                if (Random.Range(0, 100) < _threshold)
                {
                    Map.Instance.WallTilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    private void SmoothenWalls()
    {
        int _iterations = 5;
        for (int i = 0; i < _iterations; i++)
        {
            for (int y = 0; y < Map.Size.y; y++)
            {
                for (int x = 0; x < Map.Size.x; x++)
                {
                    int neighborWallTiles = GetSurroundingWallCount(x, y);
                    if (neighborWallTiles > 4)
                    {
                        Map.Instance.WallTilemap.SetTile(new Vector3Int(x, y, 0), Map.Instance.WallTile);
                    }
                    else if (neighborWallTiles < 4)
                    {
                        Map.Instance.WallTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < Map.Size.x && neighborY >= 0 && neighborY < Map.Size.y)
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        if (Map.Instance.WallTilemap.GetTile(new Vector3Int(neighborX, neighborY, 0)) != null)
                        {
                            wallCount++;
                        }
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
}