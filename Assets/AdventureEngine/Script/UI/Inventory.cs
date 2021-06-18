using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Inventory : MonoBehaviour {
        public List<GameObject> Items;
        public List<Vector2> ItemPositions;
        public Vector2Int IndexRange;
        public int StartIndex;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (StartIndex > IndexRange.y)
                StartIndex = IndexRange.y;
            if (StartIndex < IndexRange.x)
                StartIndex = IndexRange.x;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i < StartIndex)
                    Items[i].transform.localPosition = new Vector3(-100, -100, Items[i].transform.localPosition.z);
                else if (i - StartIndex < ItemPositions.Count)
                    Items[i].transform.localPosition = new Vector3(ItemPositions[i - StartIndex].x, ItemPositions[i - StartIndex].y, Items[i].transform.localPosition.z);
                else
                    Items[i].transform.localPosition = new Vector3(-100, -100, Items[i].transform.localPosition.z);
            }
        }
    }
}