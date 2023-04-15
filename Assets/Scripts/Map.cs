using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Rect Bounds;
    public Tilemap WallTilemap;
    public Tilemap FloorTilemap;
    public RuleTile WallTile;
    public RuleTile FloorTile;
    public RuleTile DebugTile;

    public static Map Instance { get; private set; }
    public static Vector2Int Size { get; private set; }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Size = new Vector2Int((int)Instance.Bounds.width, (int)Instance.Bounds.height);
        GenerateBaseMap();
        new Cave();
    }

    private void GenerateBaseMap()
    {
        Instance.WallTilemap.ClearAllTiles();
        Instance.FloorTilemap.ClearAllTiles();
        for (int y = 0; y < Size.y; y++)
        {
            for (int x = 0; x < Size.x; x++)
            {
                Instance.WallTilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
                Instance.FloorTilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);
            }
        }
    }
}