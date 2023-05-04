using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static MapGenerator;

public class Dungeon
{
    private List<Cell> _cells;
    private List<Corridor> _corridors;
    private int _iterations = 200;

    public Dungeon()
    {
        GenerateCells();
        ConnectCells();
    }

    private void GenerateCells()
    {
        _cells = new();
        var roomSize = new Vector2Int(8, 16);
        for (int i = 0; i < _iterations; i++)
        {
            var position = new Vector2Int(Random.Range(0, Map.Size.x), Random.Range(0, Map.Size.y));
            var size = new Vector2Int(Random.Range(roomSize.x, roomSize.y), Random.Range(roomSize.x, roomSize.y));
            var newCell = new Cell(new Rect(position, size));
            if (newCell.Overlaps(_cells))
                continue;

            _cells.Add(newCell);

            newCell.Draw();
        }
    }

    private void ConnectCells()
    {
        _corridors = new();

        //starting from the first cell at the bottom left, add it to a connected list
        var firstCell = _cells.OrderBy(cell => cell.Bounds.yMin).First();
        var connectedCells = new List<Cell> { firstCell };
        var unconnectedCells = _cells.Where(cell => !connectedCells.Contains(cell)).ToList();
        var nearestCell = connectedCells.Last().GetNearestCell(unconnectedCells);

        firstCell.DrawTiles(Color.green);
        nearestCell.DrawTiles(Color.blue);

        var walls = new List<Vector2Int>();
        walls.AddRange(firstCell.Walls);
        walls.AddRange(nearestCell.Walls);
        var start = firstCell.GetEntrance(nearestCell);
        var finish = nearestCell.GetEntrance(firstCell);

        Map.WallTilemap.SetTile(new Vector3Int(start.x, start.y, 0), Map.DebugTile);
        Map.WallTilemap.SetTile(new Vector3Int(finish.x, finish.y, 0), Map.DebugTile);

        var corridor = new Corridor(start, finish, walls);
        corridor.Draw();
    }
}