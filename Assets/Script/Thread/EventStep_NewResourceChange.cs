using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_NewResourceChange : EventStep {
        [TextArea] public string Description;
        [TextArea] public string Result;

        public override void OnEffect()
        {
            string rt = Result;

            float ec = 0;
            if (HasKey("EnergyChange") || HasKey("MaxEnergyChange") || HasKey("MinEnergyChange"))
            {
                ec = GetKey("EnergyChange");
                ec += (int)Random.Range(GetKey("MinEnergyChange"), GetKey("MaxEnergyChange"));
                GlobalControl.Main.ChangeEnergy(ec);
            }

            float expc = 0;
            if (HasKey("ExpChange") || HasKey("MaxExpChange") || HasKey("MinExpChange"))
            {
                expc = GetKey("ExpChange");
                expc += (int)Random.Range(GetKey("MinExpChange"), GetKey("MaxExpChange"));
                GlobalControl.Main.ChangeExp(expc);
            }

            float cc = 0;
            if (HasKey("CoinChange") || HasKey("MaxCoinChange") || HasKey("MinCoinChange"))
            {
                cc = GetKey("CoinChange");
                cc += (int)Random.Range(GetKey("MinCoinChange"), GetKey("MaxCoinChange"));
                GlobalControl.Main.ChangeCoin(cc);
            }

            float pc = 0;
            if (HasKey("PopulationChange") || HasKey("MaxPopulationChange") || HasKey("MinPopulationChange"))
            {
                pc = GetKey("PopulationChange");
                pc += (int)Random.Range(GetKey("MinPopulationChange"), GetKey("MaxPopulationChange"));
                GlobalControl.Main.ChangePopulation(pc);
            }
            
            if (HasKey("StreamPointChange"))
                GlobalControl.Main.ChangeStreamPoint(GetKey("StreamPointChange"));
            if (HasKey("GamePointChange"))
                GlobalControl.Main.ChangeGamePoint(GetKey("GamePointChange"));

            rt = ProcessText(rt, ec, expc, cc, pc);

            UIWindow W = SubUIControl.Main.ActiveWindow("Result");
            UIWindow_Result R = (UIWindow_Result)W;
            R.DescriptionText.text = Description;
            R.ResultText.text = rt;
        }

        public string ProcessText(string Ori, float EC, float EXPC, float CC, float PC)
        {
            string C = "";
            string S = Ori;
            while (S.IndexOf("*") != -1)
            {
                C += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "The Command You Want To Add")
                    C += "The Displayed Text";
                else if (Key == "EnergyChange")
                    C += EC;
                else if (Key == "ExpChange")
                    C += EXPC;
                else if (Key == "CoinChange")
                    C += CC;
                else if (Key == "PopulationChange")
                    C += PC;
                else
                    C += Key;
            }
            C += S;
            return C;
        }
    }
}