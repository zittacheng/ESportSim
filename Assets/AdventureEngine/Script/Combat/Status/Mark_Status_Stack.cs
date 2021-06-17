using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stack : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            StackCount(M);
            StackDuration(M);
        }
    }
}