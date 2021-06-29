using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class AIControl_CoinBased : AIControl {
        public float Coin = 10;
        public int SwitchCount = 2;
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
            CoinGain(CurrentTurn, Victory);
            ExecuteII(CurrentTurn, Victory);
        }

        public virtual void CoinGain(int CurrentTurn, bool Victory)
        {
            if (CurrentTurn == 0)
                return;
            Coin += ValueBase.GetCoinGain(ValueBase.GetEnemyCoinLevel(KeyBase.Main.GetKey("EnemyLevel")), CurrentTurn);
        }

        public virtual void ExecuteII(int CurrentTurn, bool Victory)
        {
            AIControlUnit U;
            if (CurrentTurn >= Units.Count)
                U = Units[Units.Count - 1];
            else
                U = Units[CurrentTurn];
            if (!U)
                return;
            U.Execute(Source, Victory);
        }

        public virtual void Buy(GameObject Target)
        {
            if (!Target || Coin < Target.GetComponent<Mark>().GetKey("Cost"))
                return;
            Coin -= Target.GetComponent<Mark>().GetKey("Cost");
            CombatControl.Main.AddItem(Target, Source);
        }

        public virtual void Switch(string Key)
        {
            if ((Source.GetCurrentCard() && Source.GetCurrentCard().GetInfo().GetID() == Key) || SwitchCount <= 0)
                return;
            SwitchCount--;
            Source.SwitchCard(Key);
        }
    }
}