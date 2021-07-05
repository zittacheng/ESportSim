using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class CVC_Delay : ConversationCondition {
        public int Delay;

        public override bool Pass(Conversation CV)
        {
            return Delay <= 0;
        }

        public override void TimePassed()
        {
            Delay--;
            base.TimePassed();
        }
    }
}