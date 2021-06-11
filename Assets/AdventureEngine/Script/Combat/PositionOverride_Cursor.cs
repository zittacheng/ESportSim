using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class PositionOverride_Cursor : PositionOverride {

        public override int GetPosition()
        {
            if (!CombatControl.Main.SelectingCard)
                return -2;
            //return CombatControl.Main.SelectingCard.GetPosition();
            return 0;
        }
    }
}