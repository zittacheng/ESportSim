using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_SelectingPosition : Targeting {

        public override Vector2 FindPosition(Card Source)
        {
            return CombatControl.Main.SelectingPosition;
        }
    }
}