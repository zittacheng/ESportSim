using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Orb : Mark_Status {

        public override void StartOfCombat()
        {
            SetKey("Stack", GetKey("ResetStack"));
            base.StartOfCombat();
        }

        public override void EndOfCombat()
        {
            SetKey("Stack", GetKey("ResetStack"));
            base.EndOfCombat();
        }

        public override void CommonKeys()
        {
            // "ResetStack": Reset to max after combat
            base.CommonKeys();
        }
    }
}