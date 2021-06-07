using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class EventStep_ResourceChange : EventStep {

        public override void OnEffect()
        {
            if (HasKey("EnergyChange"))
                KeyBase.Main.ChangeKey("Energy", GetKey("EnergyChange"));
            if (HasKey("CoinChange"))
                KeyBase.Main.ChangeKey("Coin", GetKey("CoinChange"));
            if (HasKey("StreamPointChange"))
                KeyBase.Main.ChangeKey("StreamPoint", GetKey("StreamPointChange"));
            if (HasKey("GamePointChange"))
                KeyBase.Main.ChangeKey("GamePoint", GetKey("GamePointChange"));
            if (HasKey("GeneralPointChange"))
                KeyBase.Main.ChangeKey("GeneralPoint", GetKey("GeneralPointChange"));
            ThreadControl.Main.NextStep();
        }
    }
}