using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Aim : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AimActive" && GetKey("Active") != Value)
            {
                if (Value == 1)
                    AimActive();
                else if (Value == 0)
                    AimDisable();
            }
            else if (Key == "Aim")
                return GetKey("Active");
            else if (Key == "Speed" && GetKey("Active") > 0)
                return Value * GetKey("AMSpeed");
            return base.PassValue(Key, Value);
        }

        public void AimActive()
        {
            Source.SetGCD(GetKey("GCD"));
            SetKey("Active", 1);
        }

        public void AimDisable()
        {
            SetKey("Active", 0);
        }

        public override void CommonKeys()
        {
            // "Active": Whether source is currently aiming
            // "GCD": Cool down after aiming
            // "AMSpeed": Aim movement speed
            base.CommonKeys();
        }
    }
}