using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MapGenerator;

public class Corridor
{
    public List<Vector2Int> Tiles { get; set; }
    public List<Vector2Int> Walls { get; set; }

    public Corridor(Vector2Int start, Vector2Int finish, List<Vector2Int> walls)
    {
        Tiles = new List<Vector2Int>();
        Walls = new List<Vector2Int>();
        //if x is greater than y, add one tile to the x axis else add one tile to the y axis until the start and finish are the same
        //if the new tile would overlap with walls change the direction
        //while (start != finish)
        //{
        //    if (Mathf.Abs(start.x - finish.x) > Mathf.Abs(start.y - finish.y))
        //    {
        //        if (start.x < finish.x)
        //        {
        //            start += Vector2Int.right;
        //        }
        //        else
        //        {
        //            start += Vector2Int.left;
        //        }
        //    }
        //    else
        //    {
        //        if (start.y < finish.y)
        //        {
        //            start += Vector2Int.up;
        //        }
        //        else
        //        {
        //            start += Vector2Int.down;
        //        }
        //    }
        //    if (walls.Contains(start))
        //    {
        //        start += GetRandomDirection();
        //    }
        //    Tiles.Add(start);
        //}
    }

    public Vector2Int GetRandomDirection()
    {
        //get a random direction from up, right, down and left
        var directions = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };
        return directions[Random.Range(0, directions.Count)];
    }

    public void Draw()
    {
        DrawTiles();
    }

    public void DrawTiles()
    {
        foreach (var tile in Tiles)
        {
            Map.WallTilemap.SetTile(new Vector3Int(tile.x, tile.y, 0), null);
        }
    }
}