using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Data")]
public class TileData : ScriptableObject
{
    [field: SerializeField] public TileBase[] tiles { get; set; }
    [field: SerializeField] public AudioClip[] audioClip { get; set; }
    [field: SerializeField] public TileType[] tileType { get; set; }
}
