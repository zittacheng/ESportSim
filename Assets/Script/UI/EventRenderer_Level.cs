using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventRenderer_Level : EventRenderer {

        public override Event GetTarget()
        {
            return LevelControl.Main.CurrentLevel;
        }
    }
}