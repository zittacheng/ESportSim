using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardCollider : MonoBehaviour {
        public float ColliderRange;
        public float SelectionRange;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public float GetColliderRange()
        {
            return ColliderRange;
        }

        public float GetSelectionRange()
        {
            return SelectionRange;
        }
    }
}