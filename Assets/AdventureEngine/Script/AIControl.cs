using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl : MonoBehaviour {
        public CardGroup Source;
        public List<AIControlUnit> Units;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Execute(int CurrentTurn, bool Victory)
        {
            if (CurrentTurn >= Units.Count)
                return;
            if (!Units[CurrentTurn])
                return;
            Units[CurrentTurn].Execute(Source, Victory);
        }
    }
}