using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Rect Map;
    [SerializeField] private Tilemap Walls;
    [SerializeField] private Tilemap Floor;
    [SerializeField] private RuleTile WallTile;
    [SerializeField] private RuleTile FloorTile;
    [SerializeField] private int RoomCount;
    [SerializeField] private Vector2Int RoomSize;
    private List<Rect> RandomRooms = new();

    private void Start()
    {
        GenerateRandomDungeon();
    }

    private void GenerateRandomDungeon()
    {
        GenerateRandomRooms();
        CheckOverlaps();
        DrawRooms();
        ConnectRandomRooms();
    }

    private void DrawMap()
    {
        for (int y = 0; y < Map.height; y++)
        {
            for (int x = 0; x < Map.width; x++)
            {
                Walls.SetTile(new Vector3Int(x, y, 0), WallTile);
                Floor.SetTile(new Vector3Int(x, y, 0), FloorTile);
            }
        }
    }

    private void GenerateRandomRooms()
    {
        for (int i = 0; i < RoomCount; i++)
        {
            int width = Random.Range(RoomSize.x, RoomSize.y);
            int height = Random.Range(RoomSize.x, RoomSize.y);
            int x = Random.Range(width, (int)Map.width - height);
            int y = Random.Range(width, (int)Map.height - height);

            RandomRooms.Add(new Rect(x, y, width, height));
        }
    }

    private void CheckOverlaps()
    {
        for (int i = 0; i < RandomRooms.Count; i++)
        {
            for (int j = 0; j < RandomRooms.Count; j++)
            {
                if (i != j)
                {
                    if (RandomRooms[i].Overlaps(RandomRooms[j]))
                    {
                        //move the room and check again
                        var newRect = new Rect(RandomRooms[i].x + 1, RandomRooms[i].y, RandomRooms[i].width, RandomRooms[i].height);
                        RandomRooms[i] = newRect;
                        i = 0;
                        j = 0;
                    }
                }
            }
        }
    }

    private void DrawRooms()
    {
        foreach (Rect room in RandomRooms)
        {
            for (int y = 0; y < room.height; y++)
            {
                for (int x = 0; x < room.width; x++)
                {
                    Walls.SetTile(new Vector3Int((int)room.x + x, (int)room.y + y, 0), null);
                }
            }
        }
    }

    private void ConnectRandomRooms()
    {
        //connect the centers of the rooms by removing the walls
        var roomCenters = new List<Vector2>();
        foreach (var room in RandomRooms)
        {
            roomCenters.Add(new Vector2(room.x + room.width / 2, room.y + room.height / 2));
            if (roomCenters.Count > 1)
            {
                var lastCenter = roomCenters[roomCenters.Count - 2];
                var currentCenter = roomCenters[roomCenters.Count - 1];
                var x = Mathf.RoundToInt(lastCenter.x);
                var y = Mathf.RoundToInt(lastCenter.y);
                while (x != Mathf.RoundToInt(currentCenter.x))
                {
                    Walls.SetTile(new Vector3Int(x, y, 0), null);
                    if (x < Mathf.RoundToInt(currentCenter.x))
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }
                }
                while (y != Mathf.RoundToInt(currentCenter.y))
                {
                    Walls.SetTile(new Vector3Int(x, y, 0), null);
                    if (y < Mathf.RoundToInt(currentCenter.y))
                    {
                        y++;
                    }
                    else
                    {
                        y--;
                    }
                }
            }
        }
    }
}