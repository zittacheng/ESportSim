using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class MarkInfo : TextInfo {
        [TextArea] public string Description;
        public Sprite Icon;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetDescription()
        {
            return Description;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }

        public override string GetID()
        {
            return base.GetID();
        }
    }
}