using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Level : Mark_Status {
        public string LevelKey;

        public override float PassValue(string Key, float Value)
        {
            if (Key == LevelKey)
                return GetKey("Level");
            return base.PassValue(Key, Value);
        }

        public override void Stack(Mark_Status M)
        {
            if (!M.GetComponent<Mark_Status>())
                return;
            SetKey("Level", M.GetKey("Level"));
        }

        public override void CommonKeys()
        {
            // "Level": Actual level value
            base.CommonKeys();
        }
    }
}