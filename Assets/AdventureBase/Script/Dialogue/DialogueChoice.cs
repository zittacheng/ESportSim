using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class DialogueChoice : MonoBehaviour {
        public Dialogue NextGroup;
        public string Content;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Effect()
        {
            DialogueControl.Main.StartDialogue(NextGroup);
        }

        public virtual void OnTrigger()
        {

        }

        public string GetContent()
        {
            return Content;
        }
    }
}