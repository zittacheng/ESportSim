using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class EventStep_ResourceChange : EventStep {

        public override void OnEffect()
        {
            if (HasKey("EnergyChange"))
                GlobalControl.Main.ChangeEnergy(GetKey("EnergyChange"));
            if (HasKey("CoinChange"))
                GlobalControl.Main.ChangeCoin(GetKey("CoinChange"));
            if (HasKey("StreamPointChange"))
                GlobalControl.Main.ChangeStreamPoint(GetKey("StreamPointChange"));
            if (HasKey("GamePointChange"))
                GlobalControl.Main.ChangeGamePoint(GetKey("GamePointChange"));
            if (HasKey("ExpChange"))
                GlobalControl.Main.ChangeExp(GetKey("ExpChange"));

            ThreadControl.Main.NextStep();
        }
    }
}