using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

//[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public bool GenerateCave = false;
    public bool GenerateDungeon = false;

    public Rect Bounds;
    public Tilemap WallTilemap;
    public Tilemap FloorTilemap;
    public RuleTile WallTile;
    public RuleTile FloorTile;
    public RuleTile DebugTile;
    public Vector2Int Size { get; set; }
    public List<Vector2Int> Border { get; set; }
    public Vector2Int SpawnPoint { get; set; }

    public static MapGenerator Map { get; private set; }

    private void Awake()
    {
        Initialize();
        new Cave();
    }

    private void Initialize()
    {
        if (Map == null)
            Map = this;

        Size = new Vector2Int((int)Map.Bounds.width, (int)Map.Bounds.height);
        Border = SetBorder();
        GenerateBaseMap();
    }

    private void Update()
    {
        if (GenerateCave)
        {
            GenerateBaseMap();
            GenerateDungeon = false;
            GenerateCave = false;
            new Cave();
        }
        if (GenerateDungeon)
        {
            GenerateBaseMap();
            GenerateDungeon = false;
            GenerateCave = false;
            new Dungeon();
        }
    }

    private void GenerateBaseMap()
    {
        Map.WallTilemap.ClearAllTiles();
        Map.FloorTilemap.ClearAllTiles();
        for (int y = 0; y < Size.y; y++)
        {
            for (int x = 0; x < Size.x; x++)
            {
                Map.WallTilemap.SetTile(new Vector3Int(x, y, 0), WallTile);
                Map.FloorTilemap.SetTile(new Vector3Int(x, y, 0), FloorTile);
            }
        }
    }

    private List<Vector2Int> SetBorder()
    {
        //return a listo of all the border tiles of the map
        List<Vector2Int> border = new List<Vector2Int>();
        for (int y = 0; y < Size.y; y++)
        {
            for (int x = 0; x < Size.x; x++)
            {
                if (x == 0 || x == Size.x - 1 || y == 0 || y == Size.y - 1)
                {
                    border.Add(new Vector2Int(x, y));
                }
            }
        }
        return border;
    }
}