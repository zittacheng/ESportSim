using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl_CoinBased : AIControl {
        public float Coin;

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
            CoinGain(CurrentTurn, Victory);
            ExecuteII(CurrentTurn, Victory);
        }

        public virtual void CoinGain(int CurrentTurn, bool Victory)
        {

        }

        public virtual void ExecuteII(int CurrentTurn, bool Victory)
        {

        }
    }
}