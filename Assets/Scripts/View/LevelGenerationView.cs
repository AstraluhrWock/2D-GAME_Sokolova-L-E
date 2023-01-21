using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class LevelGenerationView : MonoBehaviour
    {
        public Tilemap tilemap;
        public Tile tile;

        public int mapHeight;
        public int mapWidth;

       [Range(0, 100)] public int fillPercent;
       [Range(0, 100)] public int smoothPercent;

        public bool isBounded;
        public int[,] map;
    }
}
