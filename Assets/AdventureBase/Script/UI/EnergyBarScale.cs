using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class EnergyBarScale : MonoBehaviour {
        public GameObject ScalePrefab;
        public Vector2 PositionRange;
        public List<GameObject> Scales;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(int Count)
        {
            for (int i = 0; i < Count && i < Scales.Count; i++)
            {
                float D = (PositionRange.y - PositionRange.x) / (Count + 1);
                float x = PositionRange.x + (i + 1) * D;
                if (Scales.Count >= i)
                    Scales.Add(Instantiate(ScalePrefab));
                Scales[i].transform.localPosition = new Vector3(x, 0, 0);
                if (i >= Count && Scales[i])
                    Destroy(Scales[i]);
            }

            for (int i = Scales.Count - 1; i >= 0; i--)
            {
                if (!Scales[i])
                    Scales.RemoveAt(i);
            }
        }
    }
}