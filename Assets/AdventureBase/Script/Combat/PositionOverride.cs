using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class PositionOverride : MonoBehaviour {
        public Card Source;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual int GetPosition()
        {
            return -999;
        }
    }
}