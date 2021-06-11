using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class MapControl : MonoBehaviour {
        public static MapControl Main;
        public List<Obstacle> Obstacles;
        [Space]
        public List<Vector2> Tiles;
        public int[,] Grid;
        public float TileSize;
        public Vector2 WorldStartPoint;
        public Vector2 WorldEndPoint;
        public GameObject PathIndicator;
        [HideInInspector] public bool PathFindLock;

        public void AddObstacle(Obstacle O)
        {
            if (!Obstacles.Contains(O))
                Obstacles.Add(O);
        }

        public void Awake()
        {
            Grid = new int[50, 50];
            Tiles = GenerateTiles();
            foreach (Vector2 Tile in Tiles)
            {
                Vector2 g = TileToGrid(Tile);
                Grid[(int)g.x, (int)g.y] = 1;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void RenderTileMap()
        {
            foreach (Vector2 Tile in Tiles)
            {
                GameObject G = Instantiate(PathIndicator, transform);
                G.transform.position = TileToPosition(Tile);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LateUpdate()
        {
            PathFindLock = false;
        }

        public bool CanSee(Vector2 Ori, Vector2 Target)
        {
            foreach (Obstacle O in Obstacles)
            {
                if (O.Colliding(Ori, Target, out _))
                    return false;
            }
            return true;
        }

        public bool CanMove(Vector2 Ori, Vector2 Target, out Vector2 Contact)
        {
            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            foreach (Obstacle O in Obstacles)
            {
                if (O.Colliding(Ori, Target, out Contact))
                    return false;
            }
            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            return true;
        }

        public bool CanHit(Vector2 Ori, Vector2 Target, out Vector2 Contact)
        {
            List<Vector2> C = new List<Vector2>();
            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            foreach (Obstacle O in Obstacles)
            {
                if (O.Colliding(Ori, Target, out Contact))
                    C.Add(Contact);
            }
            if (C.Count <= 0)
            {
                Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
                return true;
            }
            else if (C.Count == 1)
            {
                Contact = C[0];
                return false;
            }
            else
            {
                float D = Mathf.Infinity;
                Vector2 F = C[0];
                for (int i = 0; i < C.Count; i++)
                {
                    if ((C[i] - Ori).magnitude <= D)
                    {
                        F = C[i];
                        D = (C[i] - Ori).magnitude;
                    }
                }
                Contact = F;
                return false;
            }
        }

        public static bool Colliding(Vector2 StartPointI, Vector2 EndPointI, Vector2 StartPointII, Vector2 EndPointII, out Vector2 Contact)
        {
            return Colliding(StartPointI, EndPointI, StartPointII, EndPointII, out Contact, out _);
        }

        public static bool Colliding(Vector2 StartPointI, Vector2 EndPointI, Vector2 StartPointII, Vector2 EndPointII, out Vector2 Contact, out int Step)
        {
            float OriX = StartPointI.x;
            float OriY = StartPointI.y;
            float TargetX = EndPointI.x;
            float TargetY = EndPointI.y;
            float StartX = StartPointII.x;
            float StartY = StartPointII.y;
            float EndX = EndPointII.x;
            float EndY = EndPointII.y;

            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            Step = 0;
            if ((OriX < StartX && TargetX < StartX && StartX < EndX) || (OriX < EndX && TargetX < EndX && EndX < StartX)
                || (OriX > StartX && TargetX > StartX && StartX > EndX) || (OriX > EndX && TargetX > EndX && EndX > StartX)
                || (OriY < StartY && TargetY < StartY && StartY < EndY) || (OriY < EndY && TargetY < EndY && EndY < StartY)
                || (OriY > StartY && TargetY > StartY && StartY > EndY) || (OriY > EndY && TargetY > EndY && EndY > StartY))
                return false;

            float ScaleI = (EndY - StartY) / (EndX - StartX);
            float OffsetI = EndY - (ScaleI * EndX);
            // Y = X * Scale + Offset
            // X = (Y - Offset) / Scale

            float X;
            float Y;

            if (OriX == TargetX)
            {
                Y = ScaleI * OriX + OffsetI;
                Contact = new Vector2(OriX, Y);
                Step = 1;
                //return (Y >= StartY && Y <= EndY) || (Y >= EndY && Y <= StartY);
                return ((Y >= StartY && StartY < EndY) || (Y >= EndY && EndY < StartY)) && ((Y <= StartY && StartY > EndY) || (Y <= EndY && EndY > StartY));
            }
            if (OriY == TargetY)
            {
                X = (OriY - OffsetI) / ScaleI;
                Contact = new Vector2(X, OriY);
                Step = 2;
                return ((X >= StartX && StartX < EndX) || (X >= EndX && EndX < StartX)) && ((X <= StartX && StartX > EndX) || (X <= EndX && EndX > StartX));
            }

            float ScaleII = (TargetY - OriY) / (TargetX - OriX);
            float OffsetII = TargetY - (ScaleII * TargetX);

            Y = ((OffsetII * ScaleI / ScaleII) - OffsetI) / ((ScaleI / ScaleII) - 1);
            X = (OffsetI - Y) / (-ScaleI);
            Contact = new Vector2(X, Y);
            Step = 3;
            return ((X >= StartX && StartX < EndX) || (X >= EndX && EndX < StartX)) && ((X <= StartX && StartX > EndX) || (X <= EndX && EndX > StartX))
                && ((Y >= StartY && StartY < EndY) || (Y >= EndY && EndY < StartY)) && ((Y <= StartY && StartY > EndY) || (Y <= EndY && EndY > StartY))
                && ((X >= OriX && OriX < TargetX) || (X >= TargetX && TargetX < OriX)) && ((X <= OriX && OriX > TargetX) || (X <= TargetX && TargetX > OriX))
                && ((Y >= OriY && OriY < TargetY) || (Y >= TargetY && TargetY < OriY)) && ((Y <= OriY && OriY > TargetY) || (Y <= TargetY && TargetY > OriY));
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public List<Vector2> GenerateTiles()
        {
            List<Vector2> Temp = new List<Vector2>();
            for (float x = WorldStartPoint.x; x <= WorldEndPoint.x; x += TileSize)
            {
                for (float y = WorldStartPoint.y; y <= WorldEndPoint.y; y += TileSize)
                {
                    Vector2 Tile = PositionToTile(new Vector2(x, y));
                    if (CheckTile(Tile))
                        Temp.Add(Tile);
                }
            }
            return Temp;
        }

        public bool CheckTile(Vector2 Tile)
        {
            Vector2 P = TileToPosition(Tile);
            if (!CanSee(P + new Vector2(-TileSize * 0.5f, -TileSize * 0.5f), P + new Vector2(-TileSize * 0.5f, TileSize * 0.5f)))
                return false;
            if (!CanSee(P + new Vector2(-TileSize * 0.5f, TileSize * 0.5f), P + new Vector2(TileSize * 0.5f, TileSize * 0.5f)))
                return false;
            if (!CanSee(P + new Vector2(TileSize * 0.5f, TileSize * 0.5f), P + new Vector2(TileSize * 0.5f, -TileSize * 0.5f)))
                return false;
            if (!CanSee(P + new Vector2(TileSize * 0.5f, -TileSize * 0.5f), P + new Vector2(-TileSize * 0.5f, -TileSize * 0.5f)))
                return false;
            return true;
        }

        public List<Vector2> GeneratePath(Vector2 StartTile, Vector2 EndTile)
        {
            List<Vector2> Frontier = new List<Vector2>();
            List<Vector2> FrontierSource = new List<Vector2>();
            List<Vector2> Processed = new List<Vector2>();
            List<Vector2> ProcessedSource = new List<Vector2>();
            /*if (!GetEmptyTile(StartTile) || !GetEmptyTile(EndTile))
            {
                print("PathNotFound: 131");
                return null;
            }*/

            Frontier.Add(EndTile);
            FrontierSource.Add(EndTile);
            Vector2 SecondTile = new Vector2();
            Vector2 ThirdTile = new Vector2();
            bool Found = false;
            while (Frontier.Count > 0)
            {
                Vector2 Tile = Frontier[0];
                Vector2 TileSource = FrontierSource[0];

                if ((GetEmptyTile(Tile + new Vector2(1, 0)) || Tile + new Vector2(1, 0) == StartTile || Tile + new Vector2(1, 0) == EndTile)
                    && !Processed.Contains(Tile + new Vector2(1, 0)) && !Frontier.Contains(Tile + new Vector2(1, 0)))
                {
                    Vector2 a = Tile + new Vector2(1, 0);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(0, -1)) || Tile + new Vector2(0, -1) == StartTile || Tile + new Vector2(0, -1) == EndTile)
                    && !Processed.Contains(Tile + new Vector2(0, -1)) && !Frontier.Contains(Tile + new Vector2(0, -1)))
                {
                    Vector2 a = Tile + new Vector2(0, -1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(-1, 0)) || Tile + new Vector2(-1, 0) == StartTile || Tile + new Vector2(-1, 0) == EndTile)
                    && !Processed.Contains(Tile + new Vector2(-1, 0)) && !Frontier.Contains(Tile + new Vector2(-1, 0)))
                {
                    Vector2 a = Tile + new Vector2(-1, 0);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(0, 1)) || Tile + new Vector2(0, 1) == StartTile || Tile + new Vector2(0, 1) == EndTile)
                    && !Processed.Contains(Tile + new Vector2(0, 1)) && !Frontier.Contains(Tile + new Vector2(0, 1)))
                {
                    Vector2 a = Tile + new Vector2(0, 1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(1, 1)) || Tile + new Vector2(1, 1) == StartTile || Tile + new Vector2(1, 1) == EndTile) 
                    && !Processed.Contains(Tile + new Vector2(1, 1)) && !Frontier.Contains(Tile + new Vector2(1, 1)))
                {
                    Vector2 a = Tile + new Vector2(1, 1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(1, -1)) || Tile + new Vector2(1, -1) == StartTile || Tile + new Vector2(1, -1) == EndTile) 
                    && !Processed.Contains(Tile + new Vector2(1, -1)) && !Frontier.Contains(Tile + new Vector2(1, -1)))
                {
                    Vector2 a = Tile + new Vector2(1, -1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(-1, -1)) || Tile + new Vector2(-1, -1) == StartTile || Tile + new Vector2(-1, -1) == EndTile) 
                    && !Processed.Contains(Tile + new Vector2(-1, -1)) && !Frontier.Contains(Tile + new Vector2(-1, -1)))
                {
                    Vector2 a = Tile + new Vector2(-1, -1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }
                if ((GetEmptyTile(Tile + new Vector2(-1, 1)) || Tile + new Vector2(-1, 1) == StartTile || Tile + new Vector2(-1, 1) == EndTile) 
                    && !Processed.Contains(Tile + new Vector2(-1, 1)) && !Frontier.Contains(Tile + new Vector2(-1, 1)))
                {
                    Vector2 a = Tile + new Vector2(-1, 1);
                    if (a.x == StartTile.x && a.y == StartTile.y)
                    {
                        Found = true;
                        SecondTile = Tile;
                        ThirdTile = TileSource;
                        break;
                    }
                    else
                    {
                        Frontier.Add(a);
                        FrontierSource.Add(Tile);
                    }
                }

                Processed.Add(Tile);
                ProcessedSource.Add(TileSource);
                Frontier.Remove(Tile);
                FrontierSource.Remove(TileSource);
            }
            if (!Found)
            {
                print("PathNotFound: 218");
                return null;
            }

            List<Vector2> PathTiles = new List<Vector2>();
            PathTiles.Add(StartTile);
            PathTiles.Add(SecondTile);
            if (ThirdTile.x == SecondTile.x && ThirdTile.y == SecondTile.y)
                return FinalizePath(PathTiles);
            else
            {
                while (ThirdTile.x != SecondTile.x || ThirdTile.y != SecondTile.y)
                {
                    PathTiles.Add(ThirdTile);
                    SecondTile = ThirdTile;
                    if (Frontier.IndexOf(SecondTile) != -1)
                        ThirdTile = FrontierSource[Frontier.IndexOf(SecondTile)];
                    else if (Processed.IndexOf(SecondTile) != -1)
                        ThirdTile = ProcessedSource[Processed.IndexOf(SecondTile)];
                    else
                    {
                        print("PathNotFound: 239");
                        return null;
                    }
                }
                return FinalizePath(PathTiles);
            }
        }

        public List<Vector2> FinalizePath(List<Vector2> Tiles)
        {
            List<Vector2> a = new List<Vector2>();
            for (int i = 0; i < Tiles.Count; i++)
            {
                a.Add(TileToPosition(Tiles[i]));
            }
            return a;
        }

        public bool GetEmptyTile(Vector2 Tile)
        {
            /*for (int i = Tiles.Count - 1; i >= 0; i--)
            {
                if (Mathf.Abs(Tiles[i].x - Tile.x) <= 0.1f && Mathf.Abs(Tiles[i].y - Tile.y) <= 0.1f)
                    return true;
            }*/
            Vector2 g = TileToGrid(Tile);
            if (g.x < 0 || g.x > 50 || g.y < 0 || g.y > 50)
                return false;
            return Grid[(int)g.x, (int)g.y] == 1;
            //return false;
        }

        public Vector2 PositionToTile(Vector2 Position)
        {
            return new Vector2(Position.x / TileSize, Position.y / TileSize);
        }

        public Vector2 TileToPosition(Vector2 Tile)
        {
            return new Vector2(Tile.x * TileSize, Tile.y * TileSize);
        }

        public Vector2 TileToGrid(Vector2 Tile)
        {
            return Tile - PositionToTile(WorldStartPoint);
        }

        public Vector2 GridToTile(Vector2 Grid)
        {
            return Grid + PositionToTile(WorldStartPoint);
        }

        public Vector2 ApproxTile(Vector2 Position)
        {
            return new Vector2(Mathf.RoundToInt(Position.x / TileSize), Mathf.RoundToInt(Position.y / TileSize));
        }
    }
}