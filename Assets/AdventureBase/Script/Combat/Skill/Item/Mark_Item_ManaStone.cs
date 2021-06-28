using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Item_ManaStone : Mark_Skill {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "ManaRecovery")
                return Value * (1 + GetKey("BaseRecoveryMod") + GetKey("RecoveryMod") * GetKey("Count"));
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "BaseRecoveryMod": Base mana recovery mod
            // "RecoveryMod": Mana recovery mod per stack
            base.CommonKeys();
        }
    }
}