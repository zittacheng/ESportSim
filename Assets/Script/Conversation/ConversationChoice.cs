using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNF
{
    public class ConversationChoice : MonoBehaviour {
        public Conversation CV;
        public SentenceGroup NextGroup;
        public string Content;
        public bool OneTime = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTrigger()
        {
            if (OneTime)
                CV.Choices.Remove(this);
        }
    }
}