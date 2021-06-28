using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_KeyBase : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (HasKey(Key))
                return GetKey(Key);
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "CCT": Current cast time
            // "MCT": Max cast time
            // "CMSpeed": Speed mod when casting
            base.CommonKeys();
        }
    }
}