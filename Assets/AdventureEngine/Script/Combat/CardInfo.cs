using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardInfo : TextInfo {
        [HideInInspector] public Sprite sprite;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Sprite GetSprite()
        {
            return sprite;
        }
    }
}