using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class MarchingSquaresController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private SquareGrid _squareGrid;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _squareGrid = new SquareGrid(map, squareSize);

        }

        public void DrawOneTile(bool active, Vector3 pos)
        {
            if (active)
            {
                Vector3Int tilePos = new Vector3Int((int)pos.x, (int)pos.y, 0);
                _tilemap.SetTile(tilePos, _tile);
            }
        }


        public void DrawTiles(Tilemap tilemap, Tile ground)
        {
            if (_squareGrid == null) return;

            _tile = ground;
            _tilemap = tilemap;

            for (int x = 0; x < _squareGrid.squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squareGrid.squares.GetLength(1); y++)
                {
                    DrawOneTile(_squareGrid.squares[x, y].topLeft.isActive, _squareGrid.squares[x, y].topLeft.position);
                    DrawOneTile(_squareGrid.squares[x, y].topRight.isActive, _squareGrid.squares[x, y].topRight.position);
                    DrawOneTile(_squareGrid.squares[x, y].bottonLeft.isActive, _squareGrid.squares[x, y].bottonLeft.position);
                    DrawOneTile(_squareGrid.squares[x, y].bottonRight.isActive, _squareGrid.squares[x, y].bottonRight.position);

                }
            }
        }


        public class Node
        {
            public Vector3 position;

            public Node(Vector3 position)
            {
                this.position = position;
            }
        }

        public class NodeController : Node
        {
            public bool isActive;

            public NodeController(Vector3 position, bool isActive) : base(position)
            {
                this.isActive = isActive;
            }
        }

        public class Square
        {
            public NodeController topLeft, topRight, bottonLeft, bottonRight;

            public Square(NodeController tL, NodeController tR, NodeController bL, NodeController bR)
            {
                topLeft = tL;
                topRight = tR;
                bottonLeft = bL;
                bottonRight = bR;
            }
        }

        public class SquareGrid
        {
            public Square[,] squares;

            public SquareGrid(int[,] map, float squareSize)
            {
                int nodeCountX = map.GetLength(0);
                int nodeCountY = map.GetLength(1);

                float mapWidth = nodeCountX * squareSize;
                float mapHeight = nodeCountY * squareSize;

                float size = squareSize / 2;

                float width = mapWidth / 2;
                float height = -mapHeight / 2;

                NodeController[,] controllers = new NodeController[nodeCountX, nodeCountY];

                for (int x = 0; x < nodeCountX; x++)
                {
                    for (int y = 0; y < nodeCountY; y++)
                    {
                        Vector3 position = new Vector3(width + (x * squareSize) + size, height + (y * squareSize) + size, 0);
                        controllers[x, y] = new NodeController(position, map[x, y] == 1);
                    }
                }

                squares = new Square[nodeCountX-1, nodeCountY-1];

                for (int x = 0; x < nodeCountX - 1; x++)
                {
                    for (int y = 0; y < nodeCountY - 1; y++)
                    {
                        squares[x, y] = new Square(controllers[x, y + 1], controllers[x + 1, y], controllers[x + 1, y + 1], controllers[x, y]);
                    }
                }
            }
        }
    }
}
