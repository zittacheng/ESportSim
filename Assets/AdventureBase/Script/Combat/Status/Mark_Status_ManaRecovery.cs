using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ManaRecovery : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            StackCount(M);
            StackDuration(M);
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "ManaRecovery")
                return Value * GetFinalMod();
            return base.PassValue(Key, Value);
        }

        public float GetFinalMod()
        {
            float a = GetKey("RecoveryMod");
            if (HasKey("Stack") && HasKey("StackChange"))
                a += GetKey("Stack") * GetKey("StackChange");
            if (GetKey("ItemCountScaling") != 0 && HasKey("ItemCount") && HasKey("StackChange"))
                a += GetKey("ItemCount") * GetKey("StackChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "RecoveryMod": Mana recovery change
            // "StackChange": Mana recovery multiplier change per stack
            // "ItemCountScaling": Whether the mod should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}