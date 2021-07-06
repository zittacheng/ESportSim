using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class DialogueRenderer : MonoBehaviour {
        public TextMeshPro MainText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MainText.text = DialogueControl.Main.MainText;
        }
    }
}