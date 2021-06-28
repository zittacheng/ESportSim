using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DrawControl : MonoBehaviour {
        public List<DrawLine> Lines;
        public GameObject LinePrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateLines()
        {
            for (int i = Lines.Count - 1; i >= 0; i--)
            {
                if (!Lines[i])
                    Lines.RemoveAt(i);
            }
        }

        public static DrawControl GetMain()
        {
            return GameObject.FindGameObjectWithTag("DrawControl").GetComponent<DrawControl>();
        }
    }
}