using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class InventoryGroup : MonoBehaviour {
        public Vector2 MainPosition;
        public Vector2 WaitingPosition;
        public List<GameObject> Objects;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActivateObject(GameObject Target)
        {
            Target.transform.position = new Vector3(MainPosition.x, MainPosition.y, Target.transform.position.z);
            if (Target.GetComponent<Inventory>())
                Target.GetComponent<Inventory>().StartIndex = 0;
            foreach (GameObject G in Objects)
            {
                if (G != Target)
                    G.transform.position = new Vector3(WaitingPosition.x, WaitingPosition.y, G.transform.position.z);
            }
        }
    }
}