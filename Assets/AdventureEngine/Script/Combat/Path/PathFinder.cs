using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class PathFinder : MonoBehaviour {
        public List<Vector2> Path = new List<Vector2>(2);
        public float Delay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Delay -= Time.deltaTime;
        }

        public void PathUpdate(Vector2 Position, Vector2 Target)
        {
            if (Path.Count > 0 && (Position - Path[0]).magnitude <= 0.1f)
                Path.RemoveAt(0);
            if (Delay > 0 || MapControl.Main.PathFindLock)
                return;
            Delay = Random.Range(0.5f, 1.5f);
            MapControl.Main.PathFindLock = true;
            Path.Clear();
            if (MapControl.Main.CanSee(Position, Target))
            {
                //RenderPath(Position, Target);
                return;
            }
            List<Vector2> P = MapControl.Main.GeneratePath(MapControl.Main.ApproxTile(Position), MapControl.Main.ApproxTile(Target));
            if (P == null)
                return;
            P.Add(Target);
            for (int i = 0; i + 1 < P.Count; i++)
            {
                if (MapControl.Main.CanSee(Position, P[i]) && MapControl.Main.CanSee(Position, P[i + 1]))
                {
                    P.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < P.Count; i++)
            {
                for (int j = i + 1; j + 1 < P.Count; j++)
                {
                    if (MapControl.Main.CanSee(P[i], P[j]) && MapControl.Main.CanSee(P[i], P[j + 1]))
                    {
                        P.RemoveAt(j);
                        j--;
                    }
                }
            }
            P.RemoveAt(P.Count - 1);
            Path = P;
            //RenderPath(Position, Target);
        }

        public void RenderPath(Vector2 Position, Vector2 Target)
        {
            if (Path.Count > 0)
            {
                EffectLine.NewLine(Position, Path[0], Color.white, 1, 1, 0);
                EffectLine.NewLine(Path[Path.Count - 1], Target, Color.white, 1, 1, 0);
            }
            else
                EffectLine.NewLine(Position, Target, Color.white, 1, 1, 0);
            for (int i = 0; i + 1 < Path.Count; i++)
                EffectLine.NewLine(Path[i], Path[i + 1], Color.white, 1, 1, 0);
        }

        public Vector2 GetNextPoint()
        {
            if (Path.Count <= 0)
                return new Vector2(Mathf.Infinity, Mathf.Infinity);
            return Path[Path.Count - 1];
        }
    }
}