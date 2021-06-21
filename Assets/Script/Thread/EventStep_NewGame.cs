using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_NewGame : EventStep {
        public float StartDelay;
        public float EndDelay;
        public float CurrentDelay;
        public int CurrentState = -1;
        public int Result;
        public bool Inied = false;
        [Space]
        public GameObject VictoryStep;
        public GameObject LoseStep;

        public override void OnEffect()
        {
            Inied = false;
            CurrentState = -1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
            Result = -1;
        }

        public override void EffectUpdate(float Value)
        {
            if (!CombatControl.Main)
                return;
            if (!Inied)
            {
                CombatIni();
                return;
            }

            CurrentDelay += Time.deltaTime;
            
            if (CurrentState == 0 && CurrentDelay >= StartDelay)
            {
                CurrentState = 1;
                CurrentDelay = 0;
                CombatControl.Main.TimeScale = 1;
            }

            if (CurrentState == 1)
            {
                CombatCheck(out bool Victory, out bool Lose);
                if (Victory && !Lose)
                {
                    CurrentState = 2;
                    CurrentDelay = 0;
                    Result = 1;
                }
                else if (Lose)
                {
                    CurrentState = 2;
                    CurrentDelay = 0;
                    Result = 0;
                }
            }

            if (CurrentState == 2 && CurrentDelay >= EndDelay)
            {
                CurrentState = -1;
                CurrentDelay = 0;

                //UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CombatScene);

                if (Result == 0)
                    ThreadControl.Main.GetCurrentEvent().ActiveStep(LoseStep.GetComponent<EventStep>());
                if (Result == 1)
                    ThreadControl.Main.GetCurrentEvent().ActiveStep(VictoryStep.GetComponent<EventStep>());
            }
        }

        public void CombatIni()
        {
            Inied = true;
            CurrentState = 0;
        }

        public void CombatCheck(out bool Victory, out bool Lose)
        {
            Lose = true;
            Victory = true;
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cards[i];
                if (C.GetSide() == 0 && C.CombatActive())
                    Lose = false;
                if (C.GetSide() == 1 && C.CombatActive())
                    Victory = false;
            }
        }
    }
}