using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class Sentence : MonoBehaviour {
        public SentenceRenderType RenderType;
        [TextArea] public string Content;
        public List<ConversationCondition> Conditions;
        public List<ConversationChoice> Choices;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetContent()
        {
            return Content;
        }

        public void TimePassed()
        {
            for (int i = 0; i < Conditions.Count; i++)
                Conditions[i].TimePassed();
            for (int i = 0; i < Choices.Count; i++)
                Choices[i].TimePassed();
        }

        public bool Active(Conversation CV)
        {
            bool Temp = true;
            foreach (ConversationCondition CC in Conditions)
            {
                if (!CC.Pass(CV))
                    Temp = false;
            }
            return Temp;
        }
    }

    public enum SentenceRenderType
    {
        Left,
        Right,
    }
}