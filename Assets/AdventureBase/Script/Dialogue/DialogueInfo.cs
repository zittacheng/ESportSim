using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DialogueInfo : MonoBehaviour {
        public string Name;
        public DialogueRenderDirection Direction;

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
            return Name;
        }

        public DialogueRenderDirection GetDirection()
        {
            return Direction;
        }
    }

    public enum DialogueRenderDirection
    {
        Left,
        Right
    }
}