using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl_List : AIControl {
        public List<AIControlUnit> Units;

        // Start is called before the first frame update
        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {

        }

        public override void Execute(int CurrentTurn, bool Victory)
        {
            if (CurrentTurn >= Units.Count)
                return;
            if (!Units[CurrentTurn])
                return;
            Units[CurrentTurn].Execute(Source, Victory);
        }
    }
}