using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_FrostBite : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            StackDuration(M);
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AttackSpeed")
                return Value * GetSpeedMod();
            if (Key == "ManaRecovery")
                return Value * GetManaMod();
            if (Key == "Speed")
                return Value * GetMovementMod();
            return base.PassValue(Key, Value);
        }

        public float GetSpeedMod()
        {
            float a = GetKey("SpeedMod");
            return a;
        }

        public float GetManaMod()
        {
            float a = GetKey("RecoveryMod");
            return a;
        }

        public float GetMovementMod()
        {
            float a = GetKey("MovementSpeedMod");
            return a;
        }

        public override void CommonKeys()
        {
            // "SpeedMod": Attack speed multiply rate
            // "RecoveryMod": Mana recovery change
            // "MovementSpeedMod": Movement speed change
            base.CommonKeys();
        }
    }
}