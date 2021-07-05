using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationChoice : MonoBehaviour {
        [TextArea] public string Content;
        public List<ConversationCondition> Conditions;
        public List<Sentence> NewSentences;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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

        public virtual void TimePassed()
        {

        }

        public virtual void Effect(Conversation CV)
        {
            for (int i = 0; i < NewSentences.Count; i++)
                CV.AddSentence(NewSentences[i]);
            CV.UpdateLastContent();
        }

        public string GetContent()
        {
            return Content;
        }
    }
}