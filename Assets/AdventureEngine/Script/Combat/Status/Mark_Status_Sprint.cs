using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Sprint : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "SprintActive" && GetKey("Active") != Value)
            {
                if (Value == 1)
                    SetKey("Active", 1);
                else if (Value == 0)
                    SetKey("Active", 0);
            }
            else if (Key == "Sprint")
            {
                if (SprintActive())
                    return 1;
                else
                    return 0;
            }
            else if (Key == "Speed" && SprintActive())
                return Value * GetKey("SprintSpeed");
            else if (Key == "SprintEnergyCost")
                return Value * GetKey("SprintEnergyCost");
            return base.PassValue(Key, Value);
        }

        public bool SprintActive()
        {
            return GetKey("Active") == 1 && Source.PassValue("Energy") < Source.PassValue("MaxEnergy");
        }

        public override void CommonKeys()
        {
            // "Active": Whether source is currently in sprint
            // "SprintSpeed": Movement speed mod while in sprint
            // "SprintEnergyCost": Energy drain while in sprint
            base.CommonKeys();
        }
    }
}