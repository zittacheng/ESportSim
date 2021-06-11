using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ADV;

namespace ESP
{
    public class Temp_ResourceText : MonoBehaviour {
        public string Key;
        public string preText;
        public TextMeshPro Text;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Text.text = preText + " " + KeyBase.Main.GetKey(Key);
        }
    }
}