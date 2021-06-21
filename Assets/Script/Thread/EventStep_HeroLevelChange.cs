using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_HeroLevelChange : EventStep {
        [TextArea] public string Description;
        [TextArea] public string Result;
        public string HeroKey;

        public override void OnEffect()
        {
            string rt = Result;
            float OriLevel = KeyBase.Main.GetKey(HeroKey + "Level");
            float lev = ValueBase.GetHeroLevelGain(KeyBase.Main.GetKey("Level"));
            KeyBase.Main.ChangeKey(HeroKey + "Level", lev);
            float CurrentLevel = KeyBase.Main.GetKey(HeroKey + "Level");
            rt = ProcessText(rt, OriLevel, CurrentLevel);
            UIWindow W = SubUIControl.Main.ActiveWindow("HeroResult");
            UIWindow_Result R = (UIWindow_Result)W;
            R.DescriptionText.text = Description;
            R.ResultText.text = rt;
        }

        public string ProcessText(string Ori, float OriLevel, float CurrentLevel)
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
                else if (Key == "OriLevel")
                    C += OriLevel;
                else if (Key == "CurrentLevel")
                    C += CurrentLevel;
                else
                    C += Key;
            }
            C += S;
            return C;
        }
    }
}