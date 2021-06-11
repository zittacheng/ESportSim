using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_Game : EventStep {

        public override void OnEffect()
        {
            float P = GetPerformanceLevel();
            float W = GetWinRate(P);
            float a = Random.Range(0.01f, 0.99f);
            bool Win;
            if (a <= W)
            {
                // Win
                Win = true;
                KeyBase.Main.SetKey("MatchResult", 1);
            }
            else
            {
                // Lose
                Win = false;
                KeyBase.Main.SetKey("MatchResult", 0);
            }
            GetKDA(Win, out int Kill, out int Death, out int Assist);
            KeyBase.Main.SetKey("KillResult", Kill);
            KeyBase.Main.SetKey("DeathResult", Death);
            KeyBase.Main.SetKey("AssistResult", Assist);

            ThreadControl.Main.NextStep();
        }

        public float GetPerformanceLevel()
        {
            int a = (int)KeyBase.Main.GetKey("RoleIndex");
            float b = 0;
            if (a == 0)
            {
                b += KeyBase.Main.GetKey("Coordination");
                b += KeyBase.Main.GetKey("Sustain");
                b += KeyBase.Main.GetKey("Waveclear");
            }
            else if (a == 1)
            {
                b += KeyBase.Main.GetKey("Sustain");
                b += KeyBase.Main.GetKey("Global");
                b += KeyBase.Main.GetKey("Micro");
            }
            else if (a == 2)
            {
                b += KeyBase.Main.GetKey("Perception");
                b += KeyBase.Main.GetKey("Global");
                b += KeyBase.Main.GetKey("Micro");
            }
            else if (a == 3)
            {
                b += KeyBase.Main.GetKey("Micro");
                b += KeyBase.Main.GetKey("Waveclear");
                b += KeyBase.Main.GetKey("Sustain");
            }
            else if (a == 4)
            {
                b += KeyBase.Main.GetKey("Coordination");
                b += KeyBase.Main.GetKey("Perception");
                b += KeyBase.Main.GetKey("Global");
            }
            // Missing: Stress level caculation
            return b;
        }

        public float GetWinRate(float PL)
        {
            return 0.5f;
        }

        public void GetKDA(bool Win, out int Kill, out int Death, out int Assist)
        {
            Kill = Random.Range(0, 20);
            Death = Random.Range(0, 15);
            Assist = Random.Range(0, 25);
        }
    }
}