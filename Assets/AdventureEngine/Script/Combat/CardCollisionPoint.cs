using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardCollisionPoint : MonoBehaviour {
        [HideInInspector] public bool Active;
        public int Index;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active && GetComponentInParent<Card>())
            {
                GetComponentInParent<Card>().AddCollisionPoint(gameObject, Index);
                Active = true;
            }
        }
    }
}