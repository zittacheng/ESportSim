using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ThreadControl : MonoBehaviour {
        public static ThreadControl Main;
        public List<Event> Thread;
        public Event CurrentEvent;
        [HideInInspector] public bool Active;
        [Space]
        public int TimeIndex = 1;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            EventUpdate();
        }

        public void ProcessAdvance()
        {
            Thread.Clear();
            if (TimeIndex == -1)
            {
                TimeIndex = 0;
                SubUIControl.Main.ActiveWindow("Day");
            }
            else if (TimeIndex == 1)
            {
                TimeIndex = 0;
                SubUIControl.Main.ActiveWindow("Day");
                // Temp
                KeyBase.Main.ChangeKey("Energy", KeyBase.Main.GetKey("EnergyRecovery"));
            }
            else if (TimeIndex == 0)
            {
                TimeIndex = 1;
                SubUIControl.Main.ActiveWindow("Night");
            }
        }

        public void GetCost(out float TimeCost, out float EnergyCost, out float CoinCost)
        {
            TimeCost = 0;
            EnergyCost = 0;
            CoinCost = 0;
            for (int i = Thread.Count - 1; i >= 0; i--)
            {
                TimeCost += Thread[i].GetKey("TimeCost");
                EnergyCost += Thread[i].GetKey("EnergyCost");
                CoinCost += Thread[i].GetKey("CoinCost");
            }
        }

        public void AddEvent(Event E)
        {
            Thread.Add(E);
        }

        public void RemoveEvent(int Index)
        {
            if (Index >= 0 && Index < Thread.Count)
                Thread.RemoveAt(Index);
        }

        public bool CanAddEvent(Event E)
        {
            GetCost(out float TC, out float EC, out float CC);
            if (E.HasKey("TimeCost") && TC + E.GetKey("TimeCost") > KeyBase.Main.GetKey("Time"))
                return false;
            if (E.HasKey("EnergyCost") && EC + E.GetKey("EnergyCost") > KeyBase.Main.GetKey("Energy"))
                return false;
            if (E.HasKey("CoinCost") && CC + E.GetKey("CoinCost") > KeyBase.Main.GetKey("Coin"))
                return false;
            return true;
        }

        public float GetTimeCost(int EndIndex)
        {
            float a = 0;
            for (int i = 0; i < Thread.Count && i < EndIndex; i++)
            {
                if (Thread[i])
                    a += Thread[i].GetKey("TimeCost");
            }
            return a;
        }

        public void StartProcess()
        {
            if (!CanStartProcess())
                return;
            Active = true;
            NextEventProcess();
        }

        public bool CanStartProcess()
        {
            if (Thread.Count <= 0)
                return false;
            GetCost(out float TC, out float EC, out float CC);
            return TC <= KeyBase.Main.GetKey("Time") && EC <= KeyBase.Main.GetKey("Energy") && CC <= KeyBase.Main.GetKey("Coin");
        }

        public void NextEventProcess()
        {
            if (Thread.Count > 0)
            {
                Event E = Thread[0];
                Thread.RemoveAt(0);
                StartEvent(E);
            }
            else
                EndProcess();
        }

        public void EndProcess()
        {
            Active = false;
            ProcessAdvance();
        }

        public void StartEvent(Event E)
        {
            if (!E)
                return;
            CurrentEvent = E;
            E.Effect();
        }

        public void EventUpdate()
        {
            if (GetCurrentEvent())
                GetCurrentEvent().EffectUpdate(Time.deltaTime);
        }

        public void NextStep()
        {
            if (GetCurrentEvent())
                GetCurrentEvent().NextStep();
        }

        public void EndEvent()
        {
            if (!GetCurrentEvent())
                return;
            GetCurrentEvent().OnEnd();
            CurrentEvent = null;
            NextEventProcess();
        }

        public Event GetCurrentEvent()
        {
            return CurrentEvent;
        }

        public Event GetEvent(int Index)
        {
            if (Index < 0 || Index >= Thread.Count)
                return null;
            return Thread[Index];
        }
    }
}