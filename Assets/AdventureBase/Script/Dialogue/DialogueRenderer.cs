using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class DialogueRenderer : MonoBehaviour {
        public GameObject AnimBase;
        public TextMeshPro MainText;
        public TextMeshPro NameText;
        public TextMeshPro NameTextII;
        public GameObject LeftBase;
        public GameObject RightBase;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            MainText.text = DialogueControl.Main.MainText;

            if (!DialogueControl.Main.GetCurrentDialogue())
            {
                AnimBase.SetActive(false);
                NameText.text = "";
                NameTextII.text = "";
                return;
            }

            AnimBase.SetActive(true);
            DialogueInfo Info = DialogueControl.Main.GetCurrentDialogue().GetInfo();
            if (!Info)
                return;
            LeftBase.SetActive(Info.GetDirection() == DialogueRenderDirection.Left);
            RightBase.SetActive(Info.GetDirection() == DialogueRenderDirection.Right);
            NameText.text = Info.GetName();
            NameTextII.text = Info.GetName();
        }
    }
}