using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DrawLine : MonoBehaviour {
        public GameObject PointI;
        public GameObject PointII;
        [Space]
        public float Width = 1;
        public Color Color = Color.white;
        [Space]
        public GameObject Top;
        public GameObject Middle;
        public GameObject Down;
        public List<SpriteRenderer> SRs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Draw()
        {
            if (!DrawControl.GetMain().Lines.Contains(this))
                DrawControl.GetMain().Lines.Add(this);
            Vector2 Ori = PointI.transform.position;
            Vector2 Target = PointII.transform.position;
            if (Target.x == Ori.x)
                Target.x -= 0.001f;
            if (Target.y == Ori.y)
                Target.y -= 0.001f;
            transform.position = Ori + (Target - Ori) * 0.5f;
            transform.up = Target - Ori;
            Down.transform.position = Ori;
            Top.transform.position = Target;
            Top.transform.localScale = new Vector3(Width * 5f, Width * 5f, 1);
            Down.transform.localScale = new Vector3(Width * 5f, Width * 5f, 1);
            Middle.transform.localScale = new Vector3(Width, (Target - Ori).magnitude, 1);
            foreach (SpriteRenderer SR in SRs)
                SR.color = Color;
        }
    }
}