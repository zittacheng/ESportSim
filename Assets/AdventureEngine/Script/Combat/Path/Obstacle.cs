using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Obstacle : MonoBehaviour {
        public List<GameObject> PointObjects;
        public List<Vector2> Points;
        public List<Line> Lines;
        public List<float> Distances;

        public void Awake()
        {
            IniPoints();
            IniLines();
            IniDistances();
            PathControl.Main.AddObstacle(this);
        }

        public void IniPoints()
        {
            Points = new List<Vector2>();
            foreach (GameObject G in PointObjects)
                Points.Add(G.transform.position);
        }

        public void IniLines()
        {
            Lines = new List<Line>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i != Points.Count - 1)
                    Lines.Add(new Line(new Vector2(Points[i].x, Points[i].y), new Vector2(Points[i + 1].x, Points[i + 1].y)));
                else
                    Lines.Add(new Line(new Vector2(Points[i].x, Points[i].y), new Vector2(Points[0].x, Points[0].y)));
            }
        }

        public void IniDistances()
        {
            Distances = new List<float>();
            foreach (Line L in Lines)
                Distances.Add(L.GetDistance());
        }

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public bool Colliding(Vector2 StartPoint, Vector2 EndPoint, out Vector2 Contact)
        {
            if (StartPoint.x == EndPoint.x)
                StartPoint.x += 0.0001f;
            if (StartPoint.y == EndPoint.y)
                StartPoint.y += 0.0001f;
            Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
            List<Line> ContactLines = new List<Line>();
            List<Vector2> ContactPoints = new List<Vector2>();
            foreach (Line L in Lines)
            {
                Vector2 PointI = L.PointI;
                Vector2 PointII = L.PointII;
                PointI += (PointI - PointII).normalized * 0.1f;
                PointII += (PointII - PointI).normalized * 0.1f;
                if (PathControl.Colliding(StartPoint, EndPoint, PointI, PointII, out Contact))
                {
                    ContactLines.Add(L);
                    ContactPoints.Add(Contact);
                }
            }
            if (ContactLines.Count <= 0)
            {
                Contact = new Vector2(Mathf.Infinity, Mathf.Infinity);
                return false;
            }
            if (ContactLines.Count > 1)
            {
                float D = Mathf.Infinity;
                int I = 0;
                for (int i = 0; i < ContactLines.Count; i++)
                {
                    if ((ContactPoints[i] - StartPoint).magnitude < D)
                    {
                        I = i;
                        D = (ContactPoints[i] - StartPoint).magnitude;
                    }
                }
                Contact = ContactPoints[I];
            }
            else
                Contact = ContactPoints[0];
            Contact += (StartPoint - Contact).normalized * 0.1f;
            return true;
        }
    }

    public class Line {
        public Vector2 PointI;
        public Vector2 PointII;

        public Line(Vector2 P1, Vector2 P2)
        {
            PointI = P1;
            PointII = P2;
        }

        public float GetDistance()
        {
            return (PointII - PointI).magnitude;
        }
    }
}