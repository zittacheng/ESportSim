using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stance_Assault : Mark_Status_Stance_Assault_Delay {

        public void EndOfTurn()
        {
            Source.RemoveStatus(this);
            //base.EndOfTurn();
        }
    }
}