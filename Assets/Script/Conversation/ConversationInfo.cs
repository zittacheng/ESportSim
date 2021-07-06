using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationInfo : MonoBehaviour {
        public string Name;
        public string Key;
        public Sprite Icon;
        public Sprite Image;
        [TextArea] public string Info;

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

        public string GetKey()
        {
            return Key;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }

        public Sprite GetImage()
        {
            return Image;
        }

        public string GetInfo()
        {
            return Info;
        }
    }
}