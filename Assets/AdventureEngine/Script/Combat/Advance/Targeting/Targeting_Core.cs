using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Core : Targeting {

        public override Card FindTarget(Card Source)
        {
            for (int i = CombatControl.Main.Cores.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cores[i];
                if (C.GetSide() != Source.GetSide())
                    return C;
            }
            return base.FindTarget(Source);
        }
    }
}