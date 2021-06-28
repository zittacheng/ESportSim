using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl_Friendly : AIControl_CoinBased {
        public List<AIControlUnit> Units;
        public bool CanSell = true;
        public bool CanBuy = true;
        public bool CanSwitch = true;

        // Start is called before the first frame update
        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {

        }

        public override void CoinGain(int CurrentTurn, bool Victory)
        {
            base.CoinGain(CurrentTurn, Victory);
        }

        public override void ExecuteII(int CurrentTurn, bool Victory)
        {
            CanSell = true;
            CanBuy = true;
            CanSwitch = true;

            if (CurrentTurn >= Units.Count)
                return;
            if (!Units[CurrentTurn])
                return;
            Units[CurrentTurn].Execute(Source, Victory);
            
            base.ExecuteII(CurrentTurn, Victory);
        }

        public float GetDeniedRate()
        {
            return 0.2f;
        }

        public void TrySwitch(string Key, out bool Denied)
        {
            Denied = false;
            if (Source.GetCurrentCard() && Source.GetCurrentCard().GetInfo().GetID() == Key)
                return;
            if (Random.Range(0.001f, 0.999f) <= GetDeniedRate())
            {
                Denied = true;
                CanSwitch = false;
                return;
            }
            Source.SwitchCard(Key);
        }

        public void TrySell(GameObject Target, out bool Denied)
        {
            Denied = false;
            if (!Target)
                return;
            if (Random.Range(0.001f, 0.999f) <= GetDeniedRate())
            {
                Denied = true;
                CanSell = false;
                return;
            }
            Coin += Target.GetComponent<Mark>().GetKey("Cost");
            CombatControl.Main.RemoveItem(Target, Source);
        }

        public void TryBuy(GameObject Target, out bool Denied)
        {
            Denied = false;
            if (!Target || Coin < Target.GetComponent<Mark>().GetKey("Cost"))
                return;
            if (Random.Range(0.001f, 0.999f) <= GetDeniedRate())
            {
                Denied = true;
                CanBuy = false;
                return;
            }
            Coin -= Target.GetComponent<Mark>().GetKey("Cost");
            CombatControl.Main.AddItem(Target, Source);
        }
    }
}