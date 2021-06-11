using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class TextInfo : MonoBehaviour {
        [HideInInspector] public string Key;
        public string Name;
        public string RenderName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetName()
        {
            if (RenderName == "")
                return GetID();
            return RenderName;
        }

        public virtual string GetID()
        {
            return Name;
        }
    }
}