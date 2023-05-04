using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Stats Stats { get; set; }
    [field: SerializeField] public Background Background { get; set; }

    private void Start()
    {
        Stats = new Stats();
        Background = ScriptableObject.CreateInstance<Background>();
        Spawn();
    }

    private void Update()
    {
        Movement();
    }

    private void Spawn()
    {
        Vector2Int randomPosition;
        do
        {
            //pick a random Vector2Int from Map.Bounds rect
            randomPosition = new Vector2Int(Random.Range((int)Map.Bounds.xMin, (int)Map.Bounds.xMax), Random.Range((int)Map.Bounds.yMin, (int)Map.Bounds.yMax));
        } while (Map.WallTilemap.GetTile(new Vector3Int(randomPosition.x, randomPosition.y, 0)) != null);

        //set the player's position to randomPosition
        transform.position = new Vector3(randomPosition.x, randomPosition.y, 0);
        //set the player's spawnpoint to randomPosition
        Map.SpawnPoint = randomPosition;
    }

    private void Movement()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * Time.deltaTime * Stats.Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * Time.deltaTime * Stats.Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * Stats.Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * Stats.Speed;
        }
    }
}