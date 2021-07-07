using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DialogueAdvanceArrow : MonoBehaviour {
        public GameObject AnimBase;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            AnimBase.SetActive(DialogueControl.Main.GetCurrentDialogue() && DialogueControl.Main.GetCurrentDialogue().GetDefaultChoice() && !DialogueControl.Main.InProcess);
        }
    }
}