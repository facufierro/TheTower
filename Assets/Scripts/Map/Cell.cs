using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MapGenerator;

public class Cell
{
    public Rect Bounds { get; set; }
    public List<Vector2Int> Walls { get; set; }
    public List<Vector2Int> Tiles { get; set; }

    public Cell(Rect bounds)
    {
        Bounds = bounds;
        Walls = SetWalls();
        Tiles = SetTiles();
    }

    public bool Overlaps(List<Cell> cells)
    {
        bool isOutsideOfMap = Bounds.xMin <= 0 || Bounds.xMax >= Map.Size.x || Bounds.yMin <= 0 || Bounds.yMax >= Map.Size.y;

        if (isOutsideOfMap)
            return true;
        foreach (var otherCell in cells)
        {
            bool overlapsWithOtherCell = otherCell.Bounds.Overlaps(Bounds);
            bool overlapsWithOtherCellWalls = Walls.Exists(wall => otherCell.Walls.Contains(wall));

            if (overlapsWithOtherCell || overlapsWithOtherCellWalls)
                return true;
        }

        return false;
    }

    public Cell GetNearestCell(List<Cell> cells)
    {
        //return the nearest cell to this cell
        return cells.OrderBy(cell => Vector2.Distance(cell.Bounds.center, Bounds.center)).First();
    }

    public Vector2Int GetEntrance(Cell nearestCell)
    {
        //return a random tile from the entrance wall
        var entranceWall = GetEntranceWall(nearestCell);
        return entranceWall[Random.Range(0, entranceWall.Count)];
    }

    public List<Vector2Int> GetEntranceWall(Cell nearestCell)
    {
        //return the wall thats closest to the nearest cell
        var entranceWall = new List<Vector2Int>();
        //check if distance y is greater than distance x or vice versa
        var direction = nearestCell.Bounds.center - Bounds.center;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //check if the nearest cell is to the left or right
            if (direction.x > 0)
            {
                //nearest cell is to the right
                entranceWall = Walls.Where(wall => wall.x == Bounds.xMax && wall.y > Bounds.yMin && wall.y < Bounds.yMax - 1).ToList();
            }
            else
            {
                //nearest cell is to the left
                entranceWall = Walls.Where(wall => wall.x == Bounds.xMin - 1 && wall.y > Bounds.yMin && wall.y < Bounds.yMax - 1).ToList();
            }
        }
        else
        {
            //check if the nearest cell is above or below
            if (direction.y > 0)
            {
                //nearest cell is above
                entranceWall = Walls.Where(wall => wall.y == Bounds.yMax && wall.x > Bounds.xMin && wall.x < Bounds.xMax - 1).ToList();
            }
            else
            {
                //nearest cell is below
                entranceWall = Walls.Where(wall => wall.y == Bounds.yMin - 1 && wall.x > Bounds.xMin && wall.x < Bounds.xMax - 1).ToList();
            }
        }

        return entranceWall;
    }

    private List<Vector2Int> SetTiles()
    {
        var tiles = new List<Vector2Int>();
        //add every tile inside the cell to the tiles list
        for (int y = (int)Bounds.yMin; y < Bounds.yMax; y++)
        {
            for (int x = (int)Bounds.xMin; x < Bounds.xMax; x++)
            {
                tiles.Add(new Vector2Int(x, y));
            }
        }
        return tiles;
    }

    private List<Vector2Int> SetWalls()
    {
        var walls = new List<Vector2Int>();
        //add every tile surrounding the cell to the walls list
        for (int y = (int)Bounds.yMin - 1; y < Bounds.yMax + 1; y++)
        {
            for (int x = (int)Bounds.xMin - 1; x < Bounds.xMax + 1; x++)
            {
                if (x == Bounds.xMin - 1 || x == Bounds.xMax || y == Bounds.yMin - 1 || y == Bounds.yMax)
                {
                    walls.Add(new Vector2Int(x, y));
                }
            }
        }

        return walls;
    }

    public void Draw()
    {
        DrawTiles(Color.white);
        DrawWalls();
    }

    public void DrawWalls()
    {
        foreach (var wall in Walls)
        {
            //Map.WallTilemap.SetTile(new Vector3Int(wall.x, wall.y, 0), Map.DebugTile);
            //Map.WallTilemap.SetColor(new Vector3Int(wall.x, wall.y, 0), Color.red);
        }
    }

    public void DrawTiles(Color color)
    {
        for (int y = (int)Bounds.yMin; y < Bounds.yMax; y++)
        {
            for (int x = (int)Bounds.xMin; x < Bounds.xMax; x++)
            {
                Map.WallTilemap.SetTile(new Vector3Int(x, y, 0), null);
                Map.FloorTilemap.SetColor(new Vector3Int(x, y, 0), color);
            }
        }
    }
}