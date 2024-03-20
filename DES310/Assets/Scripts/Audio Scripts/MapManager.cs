using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<TileData> tileData;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var data in tileData)
        {
            foreach (var tile in data.tiles) 
            { 
                dataFromTiles.Add(tile, data);
            }
        }
    }

    public AudioClip GetCurrentTileClip(Vector2 worldPosition) 
    {
        if (tilemap)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
            TileBase tile = tilemap.GetTile(gridPosition);

            int index = Random.Range(0, dataFromTiles[tile].audioClip.Length);
            AudioClip currentFloorClip = dataFromTiles[tile].audioClip[index];

            return currentFloorClip;
        }
        else
        {
            Debug.Log("No tilemap assigned to Map Manager");
            return null;
        }
    }

    public TileType GetTileType(Vector2 worldPosition) 
    {
        if (tilemap)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
            TileBase tile = tilemap.GetTile(gridPosition);

            return dataFromTiles[tile].tileType[0];
        }
        else
        {
            Debug.Log("No tilemap assigned to Map Manager");
            return TileType.METAL;
        }
    }
}
