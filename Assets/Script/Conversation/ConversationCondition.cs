using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ConversationCondition : MonoBehaviour {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual bool Pass(Conversation CV)
        {
            return true;
        }

        public virtual void TimePassed()
        {

        }
    }
}